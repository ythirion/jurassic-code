import React, { useState, useEffect } from 'react';
import { Dinosaur, AddDinosaurRequest, JurassicParkClient } from '../../services/api';
import { FormGroup, Input, Select, Checkbox, Label, Button, AlertBox } from '../styled';

interface AddDinosaurFormProps {
  zones: string[];
  onDinosaurAdded: () => void;
}

const AddDinosaurForm: React.FC<AddDinosaurFormProps> = ({ zones, onDinosaurAdded }) => {
  const [name, setName] = useState('');
  const [species, setSpecies] = useState('');
  const [isCarnivorous, setIsCarnivorous] = useState(false);
  const [isSick, setIsSick] = useState(false);
  const [zone, setZone] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  
  const client = new JurassicParkClient();
  
  // Update zone selection when zones prop changes
  useEffect(() => {
    if (zones.length > 0 && zone === '') {
      setZone(zones[0]);
    }
  }, [zones, zone]);
  
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setLoading(true);
    setError(null);
    setSuccess(null);
    
    try {
      // Check each field individually to provide clearer error messages
      if (!name) {
        throw new Error('Please enter a dinosaur name.');
      }
      
      if (!species) {
        throw new Error('Please enter a species.');
      }
      
      if (!zone) {
        throw new Error('Please select a zone. If no zones are available, create one first.');
      }
      
      const dinosaur: Dinosaur = {
        name,
        species,
        isCarnivorous,
        isSick,
        lastFed: new Date().toISOString()
      };
      
      const request: AddDinosaurRequest = {
        zoneName: zone,
        dinosaur
      };
      
      const result = await client.addDinosaurToZone(request);
      setSuccess(result);
      
      // Reset form
      setName('');
      setSpecies('');
      setIsCarnivorous(false);
      setIsSick(false);
      // Keep the current zone selected if it's still valid
      if (zones.length > 0) {
        setZone(zones[0]);
      }
      
      // Notify parent component
      onDinosaurAdded();
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
        <Label htmlFor="name">Dinosaur Name*</Label>
        <Input
          id="name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Enter a unique identifier"
          required
        />
      </FormGroup>
      
      <FormGroup>
        <Label htmlFor="species">Species*</Label>
        <Input
          id="species"
          value={species}
          onChange={(e) => setSpecies(e.target.value)}
          placeholder="e.g. Tyrannosaurus Rex"
          required
        />
      </FormGroup>
      
      <FormGroup>
        <Label htmlFor="zone">Assign to Zone*</Label>
        <Select
          id="zone"
          value={zone}
          onChange={(e) => setZone(e.target.value)}
          required
        >
          {zones.length > 0 ? (
            zones.map((zoneName) => (
              <option key={zoneName} value={zoneName}>
                {zoneName}
              </option>
            ))
          ) : (
            <option value="" disabled>
              No zones available
            </option>
          )}
        </Select>
      </FormGroup>
      
      <FormGroup>
        <Label style={{ display: 'flex', alignItems: 'center' }}>
          <Checkbox 
            checked={isCarnivorous}
            onChange={(e) => setIsCarnivorous(e.target.checked)}
          />
          Carnivorous
        </Label>
      </FormGroup>
      
      <FormGroup>
        <Label style={{ display: 'flex', alignItems: 'center' }}>
          <Checkbox 
            checked={isSick}
            onChange={(e) => setIsSick(e.target.checked)}
          />
          Medical Condition
        </Label>
      </FormGroup>
      
      <Button type="submit" disabled={loading || zones.length === 0}>
        {loading ? 'Adding...' : 'Add Dinosaur'}
      </Button>
    </form>
  );
};

export default AddDinosaurForm;