import React, { useState } from 'react';
import { MoveDinosaurRequest, JurassicParkClient } from '../../services/api';
import { FormGroup, Select, Label, Button, AlertBox, Modal, ModalContent, CloseButton } from '../styled';

interface MoveDinosaurFormProps {
  zones: string[];
  dinosaurName: string;
  currentZone: string;
  onClose: () => void;
  onMoveSuccess: () => void;
}

const MoveDinosaurForm: React.FC<MoveDinosaurFormProps> = ({ 
  zones, 
  dinosaurName, 
  currentZone, 
  onClose, 
  onMoveSuccess 
}) => {
  const [toZone, setToZone] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  
  const client = new JurassicParkClient();
  
  const otherZones = zones.filter(zone => zone !== currentZone);
  
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);
    
    try {
      if (!toZone) {
        throw new Error('Please select a destination zone.');
      }
      
      const request: MoveDinosaurRequest = {
        fromZoneName: currentZone,
        toZoneName: toZone,
        dinosaurName
      };
      
      const result = await client.moveDinosaur(request);
      setSuccess(result);
      
      // Notify parent component after a short delay
      setTimeout(() => {
        onMoveSuccess();
        onClose();
      }, 1500);
      
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unknown error occurred');
    } finally {
      setLoading(false);
    }
  };
  
  return (
    <Modal onClick={onClose}>
      <ModalContent onClick={(e) => e.stopPropagation()}>
        <h2>Move {dinosaurName}</h2>
        <p>Current location: <strong>{currentZone}</strong></p>
        
        <form onSubmit={handleSubmit}>
          {error && (
            <AlertBox type="error">
              <strong>Error:</strong> {error}
            </AlertBox>
          )}
          
          {success && (
            <AlertBox type="success">
              <strong>Success:</strong> {success}
            </AlertBox>
          )}
          
          <FormGroup>
            <Label htmlFor="toZone">Destination Zone*</Label>
            <Select
              id="toZone"
              value={toZone}
              onChange={(e) => setToZone(e.target.value)}
              required
            >
              <option value="" disabled>Select a zone</option>
              {otherZones.map((zone) => (
                <option key={zone} value={zone}>
                  {zone}
                </option>
              ))}
            </Select>
          </FormGroup>
          
          <div style={{ display: 'flex', gap: '12px', marginTop: '24px' }}>
            <Button type="submit" disabled={loading || otherZones.length === 0}>
              {loading ? 'Moving...' : 'Confirm Transfer'}
            </Button>
            <Button 
              type="button" 
              onClick={onClose}
              color="danger"
            >
              Cancel
            </Button>
          </div>
        </form>
        
        <CloseButton onClick={onClose}>×</CloseButton>
      </ModalContent>
    </Modal>
  );
};

export default MoveDinosaurForm;