#!/bin/bash
# Jurassic Code Cleanup Script
# This script helps identify and remove dead code, unused files, and dependencies

echo "Starting Jurassic Code cleanup..."

# Create a backup
TIMESTAMP=$(date +"%Y%m%d-%H%M%S")
BACKUP_DIR="backup-$TIMESTAMP"
echo "Creating backup in $BACKUP_DIR..."
mkdir -p $BACKUP_DIR
cp -r * $BACKUP_DIR/

# Step 1: Remove unused .NET files
echo -e "\nRemoving unused backend files..."
rm -f JurassicCode.API/WeatherForecast.cs
rm -f JurassicCode.API/Controllers/WeatherForecastController.cs

# Step 2: Remove unused frontend assets
echo -e "\nRemoving unused frontend assets..."
rm -f jurassic-ui/src/assets/react.svg
rm -f jurassic-ui/public/vite.svg

# Step 3: Rename misleading files
echo -e "\nRenaming misleading files..."
if [ -f JurassicCode/Class1.cs ]; then
  echo "Renaming Class1.cs to ParkService.cs"
  mv JurassicCode/Class1.cs JurassicCode/ParkService.cs
  
  # Update references in .csproj file
  sed -i '' 's/Class1\.cs/ParkService\.cs/g' JurassicCode/JurassicCode.csproj
fi

# Step 4: Modify .csproj to remove unused dependencies
echo -e "\nRemoving unused NuGet dependencies..."
# Create a backup of the csproj file
cp JurassicCode/JurassicCode.csproj JurassicCode/JurassicCode.csproj.bak

# Remove Confluent.Kafka package reference
sed -i '' '/<PackageReference Include="Confluent.Kafka"/,/<\/PackageReference>/d' JurassicCode/JurassicCode.csproj

# Remove Polly package reference
sed -i '' '/<PackageReference Include="Polly"/,/<\/PackageReference>/d' JurassicCode/JurassicCode.csproj

# Step 5: Clean up App.css - replace with minimal version
echo -e "\nCleaning up unused CSS..."
cat > jurassic-ui/src/App.css << EOF
#root {
  width: 100%;
  margin: 0 auto;
  text-align: center;
}

/* Keep only used styles, remove unused ones */
EOF

# Step 6: Clean up styled components - create a backup and remove unused ones
echo -e "\nRemoving unused styled components..."
cp jurassic-ui/src/components/styled/index.ts jurassic-ui/src/components/styled/index.ts.bak

# Show diff of what would be removed
echo -e "\nSummary of changes made:"
echo "1. Removed unused backend files:"
echo "   - JurassicCode.API/WeatherForecast.cs"
echo "   - JurassicCode.API/Controllers/WeatherForecastController.cs"
echo "2. Removed unused frontend assets:"
echo "   - jurassic-ui/src/assets/react.svg"
echo "   - jurassic-ui/public/vite.svg"
echo "3. Renamed Class1.cs to ParkService.cs"
echo "4. Removed unused NuGet packages:"
echo "   - Confluent.Kafka"
echo "   - Polly"
echo "5. Cleaned up App.css to remove unused styles"
echo "6. Created backup of styled components (index.ts.bak)"

echo -e "\nTo apply styled component cleanup, manually edit:"
echo "jurassic-ui/src/components/styled/index.ts"
echo "And remove these unused components:"
echo "- Terminal"
echo "- SecurityCamera"
echo "- DinoTracker"
echo "- DinoBlip"
echo "- VisitorCounter"
echo "- Scanner"
echo "- CloseButton"

echo -e "\nNext steps:"
echo "1. Review the changes by comparing with the backup in $BACKUP_DIR"
echo "2. Run tests to make sure nothing is broken"
echo "3. Review and remove duplicate initialization code between Init.cs and Program.vb"
echo "4. Consider renaming JurrassicCode.Console project to fix typo"
echo "5. Translate non-English comments in ParkService.cs"

echo -e "\nCleanup script completed!"