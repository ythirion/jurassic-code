pkill -f "dotnet run JurassicCode-Lite.sln --project=JurassicCode.API"
pkill -f "npm install --prefix jurassic-ui"
pkill -f "npm run dev --prefix jurassic-ui"

echo "All specified processes have been stopped."