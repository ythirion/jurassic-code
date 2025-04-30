import React, { useState, useEffect } from 'react';
import { JurassicParkClient, Dinosaur, ZoneToggleRequest } from '../services/api';
import { PageHeader, Grid, Card, Button, TabContainer, TabList, Tab, TabPanel, AlertBox } from '../components/styled';
import ZoneCard from '../components/zones/ZoneCard';
import AddZoneForm from '../components/zones/AddZoneForm';
import MoveDinosaurForm from '../components/dinosaurs/MoveDinosaurForm';
import Layout from '../components/Layout';

interface Zone {
  name: string;
  isOpen: boolean;
  dinosaurs: Dinosaur[];
}

const Zones: React.FC = () => {
  const [zones, setZones] = useState<Zone[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [activeTab, setActiveTab] = useState(0);
  const [selectedDinosaur, setSelectedDinosaur] = useState<{name: string, zone: string} | null>(null);
  const [toggleLoading, setToggleLoading] = useState(false);
  const [selectedZones, setSelectedZones] = useState<string[]>([]);
  
  const client = new JurassicParkClient();
  
  // Fetch initial data
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        
        // Fetch zones from the API
        const apiZones = await client.getAllZones();
        
        setZones(apiZones);
      } catch (err) {
        setError('Failed to load zone data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    
    fetchData();
  }, []);
  
  const handleToggleZone = async (zoneName: string) => {
    setToggleLoading(true);
    
    try {
      // Prepare request
      const request: ZoneToggleRequest = { zoneNames: [zoneName] };
      
      // Call the API
      await client.toggleZone(request);
      
      // Refresh zone data
      const apiZones = await client.getAllZones();
      setZones(apiZones);
    } catch (err) {
      setError(`Failed to toggle zone: ${err instanceof Error ? err.message : 'Unknown error'}`);
      console.error(err);
    } finally {
      setToggleLoading(false);
    }
  };
  
  const handleToggleMultipleZones = async () => {
    if (selectedZones.length === 0) return;
    
    setToggleLoading(true);
    
    try {
      // Prepare request
      const request: ZoneToggleRequest = { zoneNames: selectedZones };
      
      // Call the API
      await client.toggleZone(request);
      
      // Refresh zone data
      const apiZones = await client.getAllZones();
      setZones(apiZones);
      
      // Clear selection
      setSelectedZones([]);
    } catch (err) {
      setError(`Failed to toggle zones: ${err instanceof Error ? err.message : 'Unknown error'}`);
      console.error(err);
    } finally {
      setToggleLoading(false);
    }
  };
  
  const handleZoneSelectionChange = (zoneName: string, isSelected: boolean) => {
    if (isSelected) {
      setSelectedZones(prev => [...prev, zoneName]);
    } else {
      setSelectedZones(prev => prev.filter(z => z !== zoneName));
    }
  };
  
  const handleZoneAdded = async () => {
    try {
      // Refresh zone data from API
      const apiZones = await client.getAllZones();
      setZones(apiZones);
    } catch (err) {
      setError(`Failed to refresh zones: ${err instanceof Error ? err.message : 'Unknown error'}`);
      console.error(err);
    }
  };
  
  const handleSelectDinosaurToMove = (dinosaurName: string, zoneName: string) => {
    setSelectedDinosaur({ name: dinosaurName, zone: zoneName });
  };
  
  const handleMoveDinosaur = async () => {
    // Close the modal
    setSelectedDinosaur(null);
    
    try {
      // Refresh zone data from API
      const apiZones = await client.getAllZones();
      setZones(apiZones);
    } catch (err) {
      setError(`Failed to refresh zones: ${err instanceof Error ? err.message : 'Unknown error'}`);
      console.error(err);
    }
  };
  
  return (
    <Layout>
      <PageHeader>
        <h1>Park Zones</h1>
        <p>Manage containment zones and monitor dinosaur activity</p>
        {toggleLoading && <p><small>(Zone toggle in progress...)</small></p>}
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
            View Zones
          </Tab>
          <Tab 
            active={activeTab === 1} 
            onClick={() => setActiveTab(1)}
          >
            Add New Zone
          </Tab>
        </TabList>
        
        <TabPanel active={activeTab === 0}>
          {loading ? (
            <p>Loading zones...</p>
          ) : zones.length === 0 ? (
            <Card>
              <p style={{ textAlign: 'center', padding: '30px' }}>
                No zones have been created yet. Use the "Add New Zone" tab to create your first zone.
              </p>
            </Card>
          ) : (
            <>
              <div style={{ marginBottom: '20px', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <div>
                  <span style={{ marginRight: '10px' }}>
                    {selectedZones.length > 0 ? `${selectedZones.length} zones selected` : 'Select zones to toggle multiple at once'}
                  </span>
                </div>
                {selectedZones.length > 0 && (
                  <Button
                    onClick={handleToggleMultipleZones}
                    color="primary"
                    disabled={toggleLoading}
                  >
                    Toggle Selected Zones
                  </Button>
                )}
              </div>
              <Grid>
                {zones.map((zone) => (
                  <ZoneCard 
                    key={zone.name}
                    name={zone.name}
                    isOpen={zone.isOpen}
                    dinosaurs={zone.dinosaurs}
                    onToggle={handleToggleZone}
                    onSelectDinosaurToMove={handleSelectDinosaurToMove}
                    onSelect={handleZoneSelectionChange}
                    isSelected={selectedZones.includes(zone.name)}
                  />
                ))}
              </Grid>
            </>
          )}
        </TabPanel>
        
        <TabPanel active={activeTab === 1}>
          <Card>
            <h2>Create New Zone</h2>
            <AddZoneForm onZoneAdded={handleZoneAdded} />
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

export default Zones;