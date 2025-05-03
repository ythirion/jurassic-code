import React, { useState, useEffect } from 'react';
import { JurassicParkClient, Dinosaur } from '../services/api';
import { PageHeader, LoadingSpinner, WarningFlash } from '../components/styled';
import ParkStatus from '../components/dashboard/ParkStatus';
import Layout from '../components/Layout';

interface Zone {
  name: string;
  isOpen: boolean;
  dinosaurs: Dinosaur[];
}

const Dashboard: React.FC = () => {
  const [zones, setZones] = useState<Zone[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [securityBreach, setSecurityBreach] = useState(false);
  
  const client = new JurassicParkClient();
  
  // Function to check for security breaches
  const checkSecurityStatus = (parkZones: Zone[]) => {
    const breach = parkZones.some(zone => {
      if (!zone.isOpen) return false;
      
      const carnivores = zone.dinosaurs.filter(d => d.isCarnivorous).length;
      const herbivores = zone.dinosaurs.filter(d => !d.isCarnivorous).length;
      
      return carnivores > 0 && herbivores > 0;
    });
    
    setSecurityBreach(breach);
  };
  
  // Fetch initial data
  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        
        // Fetch zones from the API
        const apiZones = await client.getAllZones();
        
        setZones(apiZones);
        checkSecurityStatus(apiZones);
      } catch (err) {
        setError('Failed to load park data');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };
    
    fetchData();
    
    // Refresh data every 30 seconds
    const interval = setInterval(() => {
      fetchData();
    }, 30000);
    
    return () => clearInterval(interval);
  }, []);
  
  return (
    <Layout>
      <PageHeader>
        <h1>Park Dashboard</h1>
        <p>Real-time monitoring and management of park systems and assets</p>
      </PageHeader>
      
      {loading ? (
        <div style={{ textAlign: 'center', padding: '50px 0' }}>
          <LoadingSpinner />
          <p>Loading park data...</p>
        </div>
      ) : error ? (
        <div style={{ textAlign: 'center', padding: '50px 0', color: '#f44336' }}>
          <p>{error}</p>
          <button 
            onClick={() => window.location.reload()}
            style={{
              padding: '8px 16px',
              backgroundColor: '#1c5e2f',
              color: 'white',
              border: 'none',
              borderRadius: '4px',
              marginTop: '16px',
              cursor: 'pointer'
            }}
          >
            Retry
          </button>
        </div>
      ) : (
        <ParkStatus zones={zones} />
      )}
      
      <WarningFlash active={securityBreach} />
    </Layout>
  );
};

export default Dashboard;