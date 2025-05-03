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
      const request: ZoneToggleRequest = { zoneName };
      
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
            <Grid>
              {zones.map((zone) => (
                <ZoneCard 
                  key={zone.name}
                  name={zone.name}
                  isOpen={zone.isOpen}
                  dinosaurs={zone.dinosaurs}
                  onToggle={handleToggleZone}
                  onSelectDinosaurToMove={handleSelectDinosaurToMove}
                />
              ))}
            </Grid>
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