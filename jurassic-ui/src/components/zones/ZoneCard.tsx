import React from 'react';
import { Dinosaur } from '../../services/api';
import { ZoneCard as StyledZoneCard, Badge, Button, StatusIndicator, FlexContainer, Scanner } from '../styled';
import DinosaurCard from '../dinosaurs/DinosaurCard';

interface ZoneCardProps {
  name: string;
  isOpen: boolean;
  dinosaurs: Dinosaur[];
  onToggle: (zoneName: string) => void;
  onSelectDinosaurToMove: (dinosaurName: string, zoneName: string) => void;
}

const ZoneCard: React.FC<ZoneCardProps> = ({ 
  name, 
  isOpen, 
  dinosaurs, 
  onToggle,
  onSelectDinosaurToMove
}) => {
  const carnivoreCount = dinosaurs.filter(d => d.isCarnivorous).length;
  const herbivoreCount = dinosaurs.filter(d => !d.isCarnivorous).length;
  const sickCount = dinosaurs.filter(d => d.isSick).length;
  
  // Check for dangerous combinations
  const hasMixedTypes = carnivoreCount > 0 && herbivoreCount > 0;
  const isDangerous = !isOpen || hasMixedTypes;
  
  return (
    <StyledZoneCard isOpen={isOpen}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '12px' }}>
        <h3>{name}</h3>
        <div style={{ display: 'flex', alignItems: 'center' }}>
          <StatusIndicator status={isOpen ? 'open' : 'closed'} />
          <span>{isOpen ? 'OPEN' : 'CLOSED'}</span>
        </div>
      </div>
      
      {isDangerous && <Scanner />}
      
      <div style={{ margin: '16px 0' }}>
        <p><strong>Population:</strong> {dinosaurs.length} dinosaurs</p>
        <FlexContainer>
          {carnivoreCount > 0 && (
            <Badge type="carnivore">
              {carnivoreCount} Carnivores
            </Badge>
          )}
          
          {herbivoreCount > 0 && (
            <Badge type="herbivore">
              {herbivoreCount} Herbivores
            </Badge>
          )}
          
          {sickCount > 0 && (
            <Badge type="sick">
              {sickCount} Sick
            </Badge>
          )}
        </FlexContainer>
      </div>
      
      {hasMixedTypes && (
        <div style={{ 
          backgroundColor: 'rgba(244, 67, 54, 0.1)', 
          padding: '8px 12px', 
          borderRadius: '4px',
          borderLeft: '3px solid #f44336',
          marginBottom: '16px'
        }}>
          <p style={{ color: '#f44336', margin: 0 }}>
            <strong>WARNING:</strong> Mixed carnivores and herbivores
          </p>
        </div>
      )}
      
      <div style={{ marginBottom: '16px' }}>
        <Button 
          onClick={() => onToggle(name)}
          color={isOpen ? 'danger' : 'success'}
          style={{ width: '100%' }}
        >
          {isOpen ? 'Close Zone' : 'Open Zone'}
        </Button>
      </div>
      
      {dinosaurs.length > 0 ? (
        <div>
          <h4>Dinosaurs in this zone:</h4>
          {dinosaurs.map(dino => (
            <div key={dino.name} style={{ marginBottom: '12px' }}>
              <DinosaurCard 
                dinosaur={dino} 
                onMove={() => onSelectDinosaurToMove(dino.name, name)}
              />
            </div>
          ))}
        </div>
      ) : (
        <p style={{ textAlign: 'center', color: '#999', fontStyle: 'italic' }}>
          No dinosaurs in this zone
        </p>
      )}
    </StyledZoneCard>
  );
};

export default ZoneCard;