# Jurassic Code Backlog

This document contains the product backlog for Jurassic Code, our dinosaur park management system.

## 1. Zone Management

### 1.1 Create Zone
**User Story**: As a park manager, I want to create new zones in the park so that I can organize and segregate different dinosaur species.

**Acceptance Criteria**:
- Zone must have a unique name
- Zone must have an open/closed status
- Cannot create a zone with an existing name
- New zones should appear immediately in the zone list
- System should confirm zone creation

**Examples**:
- Create a zone named "Herbivore Valley" that is open
- Create a zone named "Carnivore Ridge" that is closed
- Attempt to create a zone with an existing name should return an error

### 1.2 Toggle Zone Status
**User Story**: As a park manager, I want to open or close zones so that I can control visitor access and perform maintenance.

**Acceptance Criteria**:
- Can toggle a zone between open and closed status
- Closed zones are visually distinct from open zones
- Cannot move dinosaurs into closed zones
- System should confirm status change

**Examples**:
- Close "Herbivore Valley" for maintenance
- Open "Carnivore Ridge" for visitor access
- Attempt to move a dinosaur to a closed zone should be rejected

### 1.3 View Zones
**User Story**: As a park staff member, I want to view all zones and their details so that I can monitor the park organization.

**Acceptance Criteria**:
- All zones are listed with their names and status
- Each zone shows the count of dinosaurs contained
- Zones indicate security status (Stable, Warning, Critical)
- Zones are sorted by creation date

**Examples**:
- View all zones in dashboard
- Filter to show only open zones
- Sort zones by security status

## 2. Dinosaur Management

### 2.1 Add Dinosaur
**User Story**: As a dinosaur handler, I want to add new dinosaurs to the park so that I can track and manage our dinosaur population.

**Acceptance Criteria**:
- Dinosaur must have a unique name
- Dinosaur must have a species
- Dinosaur must be designated as carnivorous or not
- Dinosaur must be assigned to an existing, open zone
- System should validate species name against known species

**Examples**:
- Add "Rex" as a Tyrannosaurus (carnivorous) to "Carnivore Ridge"
- Add "Sarah" as a Stegosaurus (not carnivorous) to "Herbivore Valley"
- Attempt to add a dinosaur to a closed zone should be rejected

### 2.2 Move Dinosaur Between Zones
**User Story**: As a dinosaur handler, I want to move dinosaurs between zones so that I can manage habitat distribution and maintain safety.

**Acceptance Criteria**:
- Can only move dinosaurs to existing, open zones
- System should check species compatibility before allowing move
- System should update dinosaur location immediately
- Move history should be logged
- Cannot move dinosaurs to zones that would create dangerous combinations

**Examples**:
- Move "Rex" from "Carnivore Ridge" to "Predator Pit"
- Attempt to move "Rex" (carnivore) to "Herbivore Valley" should trigger a warning
- Attempt to move a dinosaur to a non-existent zone should return an error

### 2.3 View Dinosaur Details
**User Story**: As a park staff member, I want to view details of all dinosaurs so that I can monitor health and location.

**Acceptance Criteria**:
- List all dinosaurs with name, species, and current zone
- Show carnivore/herbivore status
- Display health status
- Show feeding status
- Allow filtering by zone, species, or carnivore status

**Examples**:
- View all dinosaurs in "Carnivore Ridge"
- Filter to show only carnivorous dinosaurs
- Sort dinosaurs by health status

## 3. Species Compatibility

### 3.1 Check Species Coexistence
**User Story**: As a safety officer, I want to check if different species can safely coexist so that I can prevent dangerous interactions.

**Acceptance Criteria**:
- System should evaluate compatibility between any two species
- Compatibility check should consider carnivore status
- System should provide clear reasons for incompatibility
- Results should be displayed visually (green/yellow/red)

**Examples**:
- Check if Tyrannosaurus and Stegosaurus can coexist (should be incompatible)
- Check if Stegosaurus and Triceratops can coexist (should be compatible)
- Check if Velociraptor and Tyrannosaurus can coexist (may be compatible with caution)

### 3.2 Zone Security Status
**User Story**: As a park manager, I want to monitor the security status of zones so that I can ensure visitor and dinosaur safety.

**Acceptance Criteria**:
- Each zone has a security status (Stable, Warning, Critical)
- Status is calculated based on dinosaur combinations
- Status updates in real-time when dinosaurs are moved
- Critical status zones are highlighted prominently

**Examples**:
- Zone with only herbivores shows "Stable" status
- Zone with mixed small carnivores and large herbivores shows "Warning" status
- Zone with large carnivores and small herbivores shows "Critical" status

## 4. Park Dashboard

### 4.1 Park Overview
**User Story**: As a park director, I want a dashboard overview of the entire park so that I can monitor operations at a glance.

**Acceptance Criteria**:
- Display total number of zones and their status
- Show dinosaur population statistics
- Highlight zones with critical security status
- Display overall park security status
- Show visitor counter (simulated)

**Examples**:
- Dashboard shows 5 zones (3 open, 2 closed) and 12 dinosaurs
- Security status indicators for all zones
- Alert when any zone reaches critical status

### 4.2 System Logs
**User Story**: As a security officer, I want to view system logs of all park operations so that I can audit actions and investigate incidents.

**Acceptance Criteria**:
- Log all zone creations and status changes
- Log all dinosaur additions and movements
- Include timestamp and user information
- Allow filtering by action type, zone, or dinosaur
- Retain logs for at least 30 days

**Examples**:
- Log entry: "Zone 'Herbivore Valley' created by Admin on 2025-02-24 10:15"
- Log entry: "Dinosaur 'Rex' moved from 'Carnivore Ridge' to 'Predator Pit' by Handler1 on 2025-02-24 11:30"
- Filter logs to show all actions related to "Rex"

## 5. Error Handling and Validation

### 5.1 Input Validation
**User Story**: As a system user, I want clear validation of my inputs so that I can correct errors before submission.

**Acceptance Criteria**:
- Validate all form inputs in real-time
- Provide specific error messages for invalid inputs
- Prevent submission of forms with invalid data
- Highlight fields that need correction

**Examples**:
- Error when attempting to create zone with empty name
- Error when adding dinosaur without selecting species
- Error when attempting to move dinosaur to non-existent zone

### 5.2 Operation Error Handling
**User Story**: As a system user, I want clear error messages when operations fail so that I can understand and resolve issues.

**Acceptance Criteria**:
- Provide specific error messages for failed operations
- Log all errors with context for troubleshooting
- Suggest remediation steps where applicable
- Maintain system stability during error conditions

**Examples**:
- Error message: "Cannot move 'Rex' to 'Herbivore Valley' - Carnivores and herbivores cannot mix"
- Error message: "Cannot create zone - Zone 'Herbivore Valley' already exists"
- System remains operational after encountering errors