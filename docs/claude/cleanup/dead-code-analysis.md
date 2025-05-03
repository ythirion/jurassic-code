# Jurassic Code: Dead Code Analysis

This document identifies useless dependencies, dead code, and unused files in the Jurassic Code project to help clean up the codebase.

## Unused Dependencies

### Backend (.NET)
- **Confluent.Kafka** (version 0.11.1) in JurassicCode.csproj
  - No imports or usage found in any .cs files
  - Can be safely removed from the project file
  
- **Polly** (version 2.2.0) in JurassicCode.csproj
  - No imports or usage found in any .cs files
  - Can be safely removed from the project file

## Dead Code

### Backend
1. **WeatherForecastController.cs**
   - Marked with "TODO should be removed" comment
   - Not used anywhere in the application
   - Sample code left over from project template

2. **WeatherForecast.cs**
   - Only used by the unused WeatherForecastController
   - Can be safely removed

3. **Duplicate initialization code**
   - Both Init.cs and Program.vb contain the same dinosaur initialization logic
   - Should be consolidated to avoid duplication

4. **Class1.cs**
   - Misleading file name that actually contains the core ParkService implementation
   - Should be renamed to ParkService.cs to match its content

5. **Non-English comments**
   - `// Logique complexe et inutile pour déterminer la coexistence` in Class1.cs
   - Should be translated or removed for consistency

### Frontend
1. **Unused styled components** in `/jurassic-ui/src/components/styled/index.ts`:
   - `Terminal` - No references found
   - `SecurityCamera` - No references found
   - `DinoTracker` - No references found
   - `DinoBlip` - No references found
   - `VisitorCounter` - No references found
   - `Scanner` - No references found
   - `CloseButton` - Not used in modal dialogs

2. **Unused CSS in App.css**:
   - `.logo`, `.logo.react` classes
   - `.card` class
   - `.read-the-docs` class
   - `logo-spin` animation
   
3. **Redundant component definitions**:
   - `ZoneCard` defined in both styled/index.ts and zones/ZoneCard.tsx

## Unused Files

### Backend
1. **JurassicCode.API/WeatherForecast.cs**
   - Only used by the unused controller
   - Can be safely removed

2. **JurassicCode.API/Controllers/WeatherForecastController.cs**
   - Marked with TODO for removal
   - Can be safely removed

### Frontend
1. **Frontend Assets**:
   - `/jurassic-ui/src/assets/react.svg` - Not imported anywhere
   - `/jurassic-ui/public/vite.svg` - Default Vite asset, not used in the application

## Project Structure Issues

1. **Naming Issues**:
   - JurrassicCode.Console project has a typo in the name (extra 'r')
   - Class1.cs should be renamed to match its actual purpose (ParkService)
   
2. **Poor variable naming**:
   - Single-letter variables (i, j, etc.) throughout the codebase
   - Makes code harder to understand and maintain

## Recommended Cleanup Actions

### Immediate Removals (Safe to Delete)
- Remove WeatherForecastController.cs and WeatherForecast.cs
- Remove unused npm and NuGet packages
- Remove unused image assets (react.svg, vite.svg)
- Remove unused styled components and CSS classes

### Refactoring Required
- Consolidate duplicate initialization code between Init.cs and Program.vb
- Rename Class1.cs to ParkService.cs
- Review and potentially rename JurrassicCode.Console project to fix typo
- Replace non-English comments with English versions

### For Further Investigation
- Verify if any logging or monitoring uses Confluent.Kafka before removal
- Check if any resilience patterns might need Polly in the future
- Validate if styled components might be used in planned future features