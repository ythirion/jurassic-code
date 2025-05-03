import React from 'react';
import { Dinosaur } from '../../services/api';
import { Card, StatusIndicator, DinoTracker, DinoBlip, VisitorCounter, Terminal } from '../styled';

interface ZoneStatus {
  name: string;
  isOpen: boolean;
  dinosaurs: Dinosaur[];
}

interface ParkStatusProps {
  zones: ZoneStatus[];
}

const ParkStatus: React.FC<ParkStatusProps> = ({ zones }) => {
  // Calculate stats
  const totalZones = zones.length;
  const openZones = zones.filter(z => z.isOpen).length;
  const totalDinosaurs = zones.reduce((acc, zone) => acc + zone.dinosaurs.length, 0);
  const carnivores = zones.reduce((acc, zone) => 
    acc + zone.dinosaurs.filter(d => d.isCarnivorous).length, 0);
  const herbivores = totalDinosaurs - carnivores;
  const sickDinosaurs = zones.reduce((acc, zone) => 
    acc + zone.dinosaurs.filter(d => d.isSick).length, 0);
  
  // Calculate park safety status
  const securityStatus = calculateSecurityStatus(zones);

  // Generate random visitor count
  const visitorCount = Math.floor(Math.random() * 5000) + 1000;
  
  return (
    <div>
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(300px, 1fr))', gap: '20px', marginBottom: '30px' }}>
        <Card>
          <h3>Zone Status</h3>
          <div style={{ fontSize: '2.5rem', fontWeight: 'bold', marginBottom: '10px' }}>
            {openZones}/{totalZones}
          </div>
          <div style={{ display: 'flex', alignItems: 'center' }}>
            <StatusIndicator status={openZones > 0 ? 'open' : 'closed'} />
            <span>{openZones > 0 ? `${openZones} zones open` : 'All zones closed'}</span>
          </div>
        </Card>
        
        <Card>
          <h3>Dinosaur Population</h3>
          <div style={{ fontSize: '2.5rem', fontWeight: 'bold', marginBottom: '10px' }}>
            {totalDinosaurs}
          </div>
          <div>
            <div>{carnivores} Carnivores</div>
            <div>{herbivores} Herbivores</div>
            {sickDinosaurs > 0 && (
              <div style={{ color: '#ff9800', marginTop: '8px' }}>
                {sickDinosaurs} Requiring Medical Attention
              </div>
            )}
          </div>
        </Card>
        
        <Card>
          <h3>Security Status</h3>
          <div style={{ 
            fontSize: '2.5rem', 
            fontWeight: 'bold', 
            marginBottom: '10px',
            color: 
              securityStatus === 'Critical' ? '#f44336' : 
              securityStatus === 'Warning' ? '#ff9800' : 
              '#4caf50'
          }}>
            {securityStatus}
          </div>
          <div>
            Park containment systems are {securityStatus === 'Stable' ? 'functioning normally' : 'experiencing issues'}
          </div>
        </Card>
        
        <Card>
          <h3>Park Visitors</h3>
          <VisitorCounter>
            <span>{visitorCount.toLocaleString()}</span>
          </VisitorCounter>
          <div>
            {openZones === 0 
              ? 'Park closed to visitors' 
              : `${visitorCount.toLocaleString()} guests currently in the park`}
          </div>
        </Card>
      </div>
      
      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(450px, 1fr))', gap: '20px' }}>
        <Card>
          <h3>Dinosaur Tracking System</h3>
          <DinoTracker>
            {zones.flatMap(zone => zone.dinosaurs.map(dino => {
              // Generate random position for each dinosaur
              const angle = Math.random() * 2 * Math.PI;
              const distance = Math.random() * 0.4 + 0.05; // between 5% and 45% from center
              const top = 50 + Math.sin(angle) * distance * 100;
              const left = 50 + Math.cos(angle) * distance * 100;
              
              return (
                <DinoBlip 
                  key={dino.name}
                  isCarnivorous={dino.isCarnivorous}
                  style={{
                    top: `${top}%`,
                    left: `${left}%`
                  }}
                  title={`${dino.name} (${dino.species})`}
                />
              );
            }))}
          </DinoTracker>
          <p style={{ textAlign: 'center', marginTop: '10px' }}>
            {totalDinosaurs} dinosaurs currently being tracked
          </p>
        </Card>
        
        <Card>
          <h3>System Logs</h3>
          <Terminal>
            <pre>
{`[${new Date().toLocaleTimeString()}] System booted successfully
[${new Date(Date.now() - 120000).toLocaleTimeString()}] Perimeter fence diagnostic complete
[${new Date(Date.now() - 300000).toLocaleTimeString()}] Feeding schedules updated
[${new Date(Date.now() - 600000).toLocaleTimeString()}] Security cameras calibrated
[${new Date(Date.now() - 900000).toLocaleTimeString()}] ${sickDinosaurs} dinosaurs flagged for medical attention
[${new Date(Date.now() - 1800000).toLocaleTimeString()}] Weather forecast: Clear skies, 85°F
[${new Date(Date.now() - 3600000).toLocaleTimeString()}] Daily backup complete
[${new Date(Date.now() - 7200000).toLocaleTimeString()}] System maintenance scheduled for next week
> _`}
            </pre>
          </Terminal>
        </Card>
      </div>
    </div>
  );
};

// Helper function to calculate security status
function calculateSecurityStatus(zones: ZoneStatus[]): 'Stable' | 'Warning' | 'Critical' {
  // Check for critical issues
  const hasMixedTypes = zones.some(zone => {
    const carnivores = zone.dinosaurs.filter(d => d.isCarnivorous).length;
    const herbivores = zone.dinosaurs.filter(d => !d.isCarnivorous).length;
    return carnivores > 0 && herbivores > 0;
  });
  
  const hasOpenZoneWithMixedTypes = zones.some(zone => {
    if (!zone.isOpen) return false;
    const carnivores = zone.dinosaurs.filter(d => d.isCarnivorous).length;
    const herbivores = zone.dinosaurs.filter(d => !d.isCarnivorous).length;
    return carnivores > 0 && herbivores > 0;
  });
  
  if (hasOpenZoneWithMixedTypes) {
    return 'Critical';
  }
  
  if (hasMixedTypes) {
    return 'Warning';
  }
  
  return 'Stable';
}

export default ParkStatus;