import React from 'react';
import { PageHeader, Card } from '../components/styled';
import SpeciesCompatibilityChecker from '../components/compatibility/SpeciesCompatibilityChecker';
import Layout from '../components/Layout';

const Compatibility: React.FC = () => {
  return (
    <Layout>
      <PageHeader>
        <h1>Species Compatibility</h1>
        <p>Check if different dinosaur species can safely coexist in the same zone</p>
      </PageHeader>
      
      <Card>
        <h2>Compatibility Analysis Tool</h2>
        <p style={{ marginBottom: '24px' }}>
          This tool uses advanced behavioral analysis and paleontological data to determine 
          whether two species can safely cohabitate in a shared environment without risk of 
          predation or territorial conflict.
        </p>
        
        <SpeciesCompatibilityChecker />
      </Card>
      
      <div style={{ marginTop: '40px' }}>
        <h2>Common Compatible Combinations</h2>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))', gap: '20px', marginTop: '20px' }}>
          <Card>
            <h3>Herbivore Groupings</h3>
            <ul style={{ marginTop: '12px', paddingLeft: '20px' }}>
              <li>Triceratops + Stegosaurus</li>
              <li>Brachiosaurus + Parasaurolophus</li>
              <li>Ankylosaurus + Gallimimus</li>
              <li>Stegosaurus + Apatosaurus</li>
              <li>Triceratops + Pachycephalosaurus</li>
            </ul>
          </Card>
          
          <Card>
            <h3>Carnivore Groupings</h3>
            <ul style={{ marginTop: '12px', paddingLeft: '20px' }}>
              <li>Velociraptor + Dilophosaurus (similar size)</li>
              <li>Allosaurus + Baryonyx (similar hunting patterns)</li>
              <li>Carnotaurus + Ceratosaurus</li>
              <li>Compsognathus groups (small carnivores)</li>
              <li>Note: T-Rex should always be isolated</li>
            </ul>
          </Card>
          
          <Card>
            <h3>Marine Combinations</h3>
            <ul style={{ marginTop: '12px', paddingLeft: '20px' }}>
              <li>Plesiosaur + Ichthyosaur</li>
              <li>Small marine reptiles together</li>
              <li>Avoid mixing with Mosasaurus</li>
              <li>Keep Kronosaurus isolated</li>
              <li>Elasmosaurus compatible with smaller species</li>
            </ul>
          </Card>
          
          <Card>
            <h3>Avian Combinations</h3>
            <ul style={{ marginTop: '12px', paddingLeft: '20px' }}>
              <li>Pteranodon + Rhamphorhynchus</li>
              <li>Dimorphodon + Archaeopteryx</li>
              <li>Small pterosaurs can group together</li>
              <li>Keep Quetzalcoatlus separate from smaller species</li>
              <li>Pterodactylus can cohabitate with similar sized species</li>
            </ul>
          </Card>
        </div>
      </div>
      
      <div style={{ marginTop: '40px' }}>
        <h2>Safety Guidelines</h2>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))', gap: '20px', marginTop: '20px' }}>
          <Card>
            <h3>Size Differential Rule</h3>
            <p style={{ marginTop: '12px' }}>
              Species with a significant size differential (more than 50% difference in weight or length) 
              should not be placed in the same enclosure, regardless of diet.
            </p>
          </Card>
          
          <Card>
            <h3>Territorial Spacing</h3>
            <p style={{ marginTop: '12px' }}>
              Each specimen requires a minimum territory size based on their natural 
              habits. Overcrowding leads to stress and increased aggression.
            </p>
          </Card>
          
          <Card>
            <h3>Predator-Prey Dynamics</h3>
            <p style={{ marginTop: '12px' }}>
              Never place species that had a historical predator-prey relationship in 
              the same enclosure, even if both are herbivores.
            </p>
          </Card>
          
          <Card>
            <h3>Temporal Separation</h3>
            <p style={{ marginTop: '12px' }}>
              Species that never coexisted in the same time period may have no evolved 
              behaviors for interaction and can be unpredictable when housed together.
            </p>
          </Card>
        </div>
      </div>
    </Layout>
  );
};

export default Compatibility;