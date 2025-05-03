import React, { useState } from 'react';
import { ZoneRequest, JurassicParkClient } from '../../services/api';
import { FormGroup, Input, Checkbox, Label, Button, AlertBox } from '../styled';

interface AddZoneFormProps {
  onZoneAdded: () => void;
}

const AddZoneForm: React.FC<AddZoneFormProps> = ({ onZoneAdded }) => {
  const [name, setName] = useState('');
  const [isOpen, setIsOpen] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  
  const client = new JurassicParkClient();
  
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);
    
    try {
      if (!name) {
        throw new Error('Please enter a zone name.');
      }
      
      const request: ZoneRequest = {
        name,
        isOpen
      };
      
      const result = await client.addZone(request);
      setSuccess(result);
      
      // Reset form
      setName('');
      setIsOpen(false);
      
      // Notify parent component
      onZoneAdded();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unknown error occurred');
    } finally {
      setLoading(false);
    }
  };
  
  return (
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
        <Label htmlFor="name">Zone Name*</Label>
        <Input
          id="name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="e.g. T-Rex Paddock"
          required
        />
      </FormGroup>
      
      <FormGroup>
        <Label style={{ display: 'flex', alignItems: 'center' }}>
          <Checkbox 
            checked={isOpen}
            onChange={(e) => setIsOpen(e.target.checked)}
          />
          Zone is open for visitors
        </Label>
      </FormGroup>
      
      {isOpen && (
        <AlertBox type="warning">
          <strong>Warning:</strong> Opening a zone to visitors requires safety protocols to be in place.
        </AlertBox>
      )}
      
      <Button type="submit" disabled={loading}>
        {loading ? 'Creating...' : 'Create Zone'}
      </Button>
    </form>
  );
};

export default AddZoneForm;