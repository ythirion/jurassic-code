import React from 'react';
import { Dinosaur } from '../../services/api';
import { DinoCard, Badge, Divider } from '../styled';

interface DinosaurCardProps {
  dinosaur: Dinosaur;
  onMove?: (dinosaurName: string) => void;
}

const DinosaurCard: React.FC<DinosaurCardProps> = ({ dinosaur, onMove }) => {
  const lastFedDate = new Date(dinosaur.lastFed);
  const timeElapsed = Date.now() - lastFedDate.getTime();
  const hoursElapsed = Math.floor(timeElapsed / (1000 * 60 * 60));
  
  // Determine feeding status
  let feedingStatus = 'Well Fed';
  let statusColor = '#4caf50';
  
  if (hoursElapsed > 12) {
    feedingStatus = 'Hungry';
    statusColor = '#ff9800';
  }
  
  if (hoursElapsed > 24) {
    feedingStatus = 'Starving';
    statusColor = '#f44336';
  }
  
  return (
    <DinoCard isCarnivorous={dinosaur.isCarnivorous}>
      <h3>{dinosaur.name}</h3>
      <div style={{ marginBottom: '12px' }}>
        <Badge type={dinosaur.isCarnivorous ? 'carnivore' : 'herbivore'}>
          {dinosaur.isCarnivorous ? 'Carnivore' : 'Herbivore'}
        </Badge>
        {dinosaur.isSick && <Badge type="sick">Sick</Badge>}
      </div>
      
      <p><strong>Species:</strong> {dinosaur.species}</p>
      <p><strong>Last Fed:</strong> {lastFedDate.toLocaleString()}</p>
      <p><strong>Status:</strong> <span style={{ color: statusColor }}>{feedingStatus}</span></p>
      
      {dinosaur.isSick && (
        <p style={{ color: '#ff9800', marginTop: '8px' }}>
          <strong>Medical Attention Required</strong>
        </p>
      )}
      
      {onMove && (
        <>
          <Divider />
          <button 
            onClick={() => onMove(dinosaur.name)}
            style={{
              padding: '8px 16px',
              backgroundColor: '#1c5e2f',
              color: 'white',
              border: 'none',
              borderRadius: '4px',
              cursor: 'pointer',
              width: '100%'
            }}
          >
            Move Dinosaur
          </button>
        </>
      )}
    </DinoCard>
  );
};

export default DinosaurCard;