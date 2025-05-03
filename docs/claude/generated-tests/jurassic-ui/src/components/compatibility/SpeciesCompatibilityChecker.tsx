import React, { useState } from 'react';
import { SpeciesCoexistRequest, JurassicParkClient } from '../../services/api';
import { FormGroup, Input, Label, Button, AlertBox, Terminal } from '../styled';

const SpeciesCompatibilityChecker: React.FC = () => {
  const [species1, setSpecies1] = useState('');
  const [species2, setSpecies2] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [result, setResult] = useState<boolean | null>(null);
  const [checkPerformed, setCheckPerformed] = useState(false);
  
  const client = new JurassicParkClient();
  
  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setLoading(true);
    setError(null);
    setResult(null);
    setCheckPerformed(false);
    
    try {
      if (!species1 || !species2) {
        throw new Error('Please enter both species names.');
      }
      
      const request: SpeciesCoexistRequest = {
        species1,
        species2
      };
      
      const compatibilityResult = await client.canSpeciesCoexist(request);
      setResult(compatibilityResult);
      setCheckPerformed(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An unknown error occurred');
    } finally {
      setLoading(false);
    }
  };
  
  return (
    <div>
      <form onSubmit={handleSubmit}>
        {error && (
          <AlertBox type="error">
            <strong>Error:</strong> {error}
          </AlertBox>
        )}
        
        <div style={{ display: 'flex', gap: '16px' }}>
          <FormGroup style={{ flex: 1 }}>
            <Label htmlFor="species1">Species 1*</Label>
            <Input
              id="species1"
              value={species1}
              onChange={(e) => setSpecies1(e.target.value)}
              placeholder="e.g. Tyrannosaurus Rex"
              required
            />
          </FormGroup>
          
          <FormGroup style={{ flex: 1 }}>
            <Label htmlFor="species2">Species 2*</Label>
            <Input
              id="species2"
              value={species2}
              onChange={(e) => setSpecies2(e.target.value)}
              placeholder="e.g. Triceratops"
              required
            />
          </FormGroup>
        </div>
        
        <Button type="submit" disabled={loading}>
          {loading ? 'Analyzing...' : 'Check Compatibility'}
        </Button>
      </form>
      
      {checkPerformed && (
        <div style={{ marginTop: '24px' }}>
          <h3>Compatibility Analysis</h3>
          <Terminal>
            <pre>
{`> ANALYZING SPECIES COMPATIBILITY
> SPECIES 1: ${species1.toUpperCase()}
> SPECIES 2: ${species2.toUpperCase()}
> RUNNING BEHAVIORAL MODELS...
> CHECKING HISTORICAL INTERACTION DATA...
> CONSULTING PALEONTOLOGICAL RECORDS...
> CALCULATING PREDATOR-PREY DYNAMICS...
> ...
> ...
> COMPATIBILITY ASSESSMENT COMPLETE

RESULT: ${result 
  ? 'COMPATIBLE ✓ - These species can safely coexist in the same habitat.' 
  : 'INCOMPATIBLE ✗ - These species should NOT be placed in the same habitat.'}

${!result 
  ? 'WARNING: Placing these species together may result in predation, territorial conflicts, or other dangerous behaviors.'
  : 'NOTE: While compatible, continued monitoring is recommended.'}
`}
            </pre>
          </Terminal>
          
          {!result && (
            <AlertBox type="error" style={{ marginTop: '16px' }}>
              <strong>Safety Alert:</strong> Housing these species together would violate InGen safety protocols.
            </AlertBox>
          )}
          
          {result && (
            <AlertBox type="success" style={{ marginTop: '16px' }}>
              <strong>Good news!</strong> These species can safely share the same zone.
            </AlertBox>
          )}
        </div>
      )}
    </div>
  );
};

export default SpeciesCompatibilityChecker;