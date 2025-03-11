import React, { useState, useEffect } from 'react';
import { JurassicParkClient, Dinosaur } from '../services/api';
import { PageHeader, Grid, Card, Button, TabContainer, TabList, Tab, TabPanel, AlertBox, FormGroup, Select, Label } from '../components/styled';
import DinosaurCard from '../components/dinosaurs/DinosaurCard';
import AddDinosaurForm from '../components/dinosaurs/AddDinosaurForm';
import MoveDinosaurForm from '../components/dinosaurs/MoveDinosaurForm';
import Layout from '../components/Layout';

interface Zone {
  name: string;
  isOpen: boolean;
  dinosaurs: Dinosaur[];
}

const Dinosaurs: React.FC = () => {
  const [zones, setZones] = useState<Zone[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [activeTab, setActiveTab] = useState(0);
  const [selectedDinosaur, setSelectedDinosaur] = useState<{name: string, zone: string} | null>(null);
  const [filter, setFilter] = useState<{
    zone: string;
    dietType: 'all' | 'carnivore' | 'herbivore';
    healthStatus: 'all' | 'healthy' | 'sick';
  }>({
    zone: 'all',
    dietType: 'all',
    healthStatus: 'all'
  });
  
  const client = new JurassicParkClient();
  
  // Fetch initial data
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        
        // For demo purposes, we'll create some mock data
        // In a real app, we would fetch this from the API
        const mockZones: Zone[] = [
          {
            name: 'T-Rex Paddock',
            isOpen: false,
            dinosaurs: [
              {
                name: 'Rexy',
                species: 'Tyrannosaurus Rex',
                isCarnivorous: true,
                isSick: false,
                lastFed: new Date(Date.now() - 3600000 * 2).toISOString() // 2 hours ago
              }
            ]
          },
          {
            name: 'Herbivore Valley',
            isOpen: true,
            dinosaurs: [
              {
                name: 'Tri',
                species: 'Triceratops',
                isCarnivorous: false,
                isSick: false,
                lastFed: new Date(Date.now() - 3600000 * 1).toISOString() // 1 hour ago
              },
              {
                name: 'Spike',
                species: 'Stegosaurus',
                isCarnivorous: false,
                isSick: true,
                lastFed: new Date(Date.now() - 3600000 * 8).toISOString() // 8 hours ago
              }
            ]
          },
          {
            name: 'Raptor Containment',
            isOpen: false,
            dinosaurs: [
              {
                name: 'Blue',
                species: 'Velociraptor',
                isCarnivorous: true,
                isSick: false,
                lastFed: new Date(Date.now() - 3600000 * 5).toISOString() // 5 hours ago
              },
              {
                name: 'Delta',
                species: 'Velociraptor',
                isCarnivorous: true,
                isSick: false,
                lastFed: new Date(Date.now() - 3600000 * 5).toISOString() // 5 hours ago
              }
            ]
          },
          {
            name: 'Aviary',
            isOpen: true,
            dinosaurs: [
              {
                name: 'Flyer',
                species: 'Pteranodon',
                isCarnivorous: true,
                isSick: false,
                lastFed: new Date(Date.now() - 3600000 * 3).toISOString() // 3 hours ago
              }
            ]
          }
        ];
        
        setZones(mockZones);
      } catch (err) {
        setError('Failed to load dinosaur data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    
    fetchData();
  }, []);
  
  const handleDinosaurAdded = () => {
    // In a real app, we would fetch the updated data
    // For this demo, we'll leave it as is, assuming the form would refresh the data
  };
  
  const handleSelectDinosaurToMove = (dinosaurName: string, zoneName: string) => {
    setSelectedDinosaur({ name: dinosaurName, zone: zoneName });
  };
  
  const handleMoveDinosaur = () => {
    // In a real app, we would fetch the updated data
    // For demo, just close the modal
    setSelectedDinosaur(null);
  };
  
  // Apply filters to dinosaurs
  const filteredDinosaurs = zones.flatMap(zone => {
    // Filter by zone if needed
    if (filter.zone !== 'all' && zone.name !== filter.zone) {
      return [];
    }
    
    // Filter the dinosaurs in this zone
    return zone.dinosaurs
      .filter(dino => {
        // Filter by diet type
        if (filter.dietType === 'carnivore' && !dino.isCarnivorous) return false;
        if (filter.dietType === 'herbivore' && dino.isCarnivorous) return false;
        
        // Filter by health status
        if (filter.healthStatus === 'sick' && !dino.isSick) return false;
        if (filter.healthStatus === 'healthy' && dino.isSick) return false;
        
        return true;
      })
      .map(dino => ({
        ...dino,
        zone: zone.name // Add zone information to each dinosaur
      }));
  });
  
  // Get total counts
  const totalDinosaurs = zones.reduce((acc, zone) => acc + zone.dinosaurs.length, 0);
  const carnivores = zones.reduce((acc, zone) => acc + zone.dinosaurs.filter(d => d.isCarnivorous).length, 0);
  const herbivores = zones.reduce((acc, zone) => acc + zone.dinosaurs.filter(d => !d.isCarnivorous).length, 0);
  const sickDinosaurs = zones.reduce((acc, zone) => acc + zone.dinosaurs.filter(d => d.isSick).length, 0);
  
  return (
    <Layout>
      <PageHeader>
        <h1>Dinosaur Management</h1>
        <p>Monitor and manage all dinosaurs in the park</p>
      </PageHeader>
      
      {error && (
        <AlertBox type="error" style={{ marginBottom: '24px' }}>
          <strong>Error:</strong> {error}
          <Button 
            onClick={() => setError(null)}
            style={{ marginLeft: '12px', padding: '4px 8px' }}
            color="danger"
          >
            Dismiss
          </Button>
        </AlertBox>
      )}
      
      <TabContainer>
        <TabList>
          <Tab 
            active={activeTab === 0} 
            onClick={() => setActiveTab(0)}
          >
            View Dinosaurs
          </Tab>
          <Tab 
            active={activeTab === 1} 
            onClick={() => setActiveTab(1)}
          >
            Add New Dinosaur
          </Tab>
        </TabList>
        
        <TabPanel active={activeTab === 0}>
          {loading ? (
            <p>Loading dinosaurs...</p>
          ) : (
            <>
              <Card style={{ marginBottom: '24px' }}>
                <h3>Population Overview</h3>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px', marginTop: '10px' }}>
                  <div>
                    <strong>Total:</strong> {totalDinosaurs} dinosaurs
                  </div>
                  <div>
                    <strong>Carnivores:</strong> {carnivores} ({Math.round(carnivores/totalDinosaurs*100)}%)
                  </div>
                  <div>
                    <strong>Herbivores:</strong> {herbivores} ({Math.round(herbivores/totalDinosaurs*100)}%)
                  </div>
                  <div>
                    <strong>Medical Attention Required:</strong> {sickDinosaurs} ({Math.round(sickDinosaurs/totalDinosaurs*100)}%)
                  </div>
                </div>
              </Card>
              
              <Card style={{ marginBottom: '24px' }}>
                <h3>Filter Dinosaurs</h3>
                <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px', marginTop: '16px' }}>
                  <FormGroup style={{ minWidth: '200px' }}>
                    <Label htmlFor="zoneFilter">Zone</Label>
                    <Select
                      id="zoneFilter"
                      value={filter.zone}
                      onChange={(e) => setFilter({...filter, zone: e.target.value})}
                    >
                      <option value="all">All Zones</option>
                      {zones.map(zone => (
                        <option key={zone.name} value={zone.name}>
                          {zone.name} ({zone.dinosaurs.length})
                        </option>
                      ))}
                    </Select>
                  </FormGroup>
                  
                  <FormGroup style={{ minWidth: '200px' }}>
                    <Label htmlFor="dietFilter">Diet Type</Label>
                    <Select
                      id="dietFilter"
                      value={filter.dietType}
                      onChange={(e) => setFilter({...filter, dietType: e.target.value as any})}
                    >
                      <option value="all">All Types</option>
                      <option value="carnivore">Carnivores Only</option>
                      <option value="herbivore">Herbivores Only</option>
                    </Select>
                  </FormGroup>
                  
                  <FormGroup style={{ minWidth: '200px' }}>
                    <Label htmlFor="healthFilter">Health Status</Label>
                    <Select
                      id="healthFilter"
                      value={filter.healthStatus}
                      onChange={(e) => setFilter({...filter, healthStatus: e.target.value as any})}
                    >
                      <option value="all">All Statuses</option>
                      <option value="healthy">Healthy Only</option>
                      <option value="sick">Medical Attention Required</option>
                    </Select>
                  </FormGroup>
                </div>
              </Card>
              
              {filteredDinosaurs.length === 0 ? (
                <Card>
                  <p style={{ textAlign: 'center', padding: '30px' }}>
                    No dinosaurs match your current filters.
                  </p>
                </Card>
              ) : (
                <Grid>
                  {filteredDinosaurs.map((dinosaur) => (
                    <DinosaurCard 
                      key={dinosaur.name}
                      dinosaur={dinosaur}
                      onMove={(name) => handleSelectDinosaurToMove(name, dinosaur.zone as string)}
                    />
                  ))}
                </Grid>
              )}
            </>
          )}
        </TabPanel>
        
        <TabPanel active={activeTab === 1}>
          <Card>
            <h2>Register New Dinosaur</h2>
            <AddDinosaurForm 
              zones={zones.map(z => z.name)}
              onDinosaurAdded={handleDinosaurAdded} 
            />
          </Card>
        </TabPanel>
      </TabContainer>
      
      {selectedDinosaur && (
        <MoveDinosaurForm 
          zones={zones.map(z => z.name)}
          dinosaurName={selectedDinosaur.name}
          currentZone={selectedDinosaur.zone}
          onClose={() => setSelectedDinosaur(null)}
          onMoveSuccess={handleMoveDinosaur}
        />
      )}
    </Layout>
  );
};

export default Dinosaurs;