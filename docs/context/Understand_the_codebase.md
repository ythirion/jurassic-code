
```human
I'm new on this project. Helps me understand it so that I work on it and be efficient faster.
```


# Without knowledge of the files

To help you get up to speed with the project and work efficiently, here’s a structured approach:

1. Understand the Project's Purpose
Ask Questions: What is the project trying to achieve? Is it a web app, API, CLI tool, or something else?
Check Documentation: Look for a README.md, CONTRIBUTING.md, or other documentation files in the project root. These often explain the purpose, setup, and usage of the project.
Explore the Codebase: Look for high-level files like package.json (for Node.js projects), requirements.txt (Python), or pom.xml (Java) to understand dependencies and frameworks.
2. Analyze the Project Structure
Look at the folder and file organization. Common patterns include:
Frontend: src/, components/, views/, styles/
Backend: api/, routes/, controllers/, models/
Configuration: .env, config/, settings/
Tests: tests/, __tests__/, or spec/
Identify the entry point of the application (e.g., index.js, main.py, app.tsx).
3. Set Up the Project
Follow the setup instructions in the documentation (if available). Typical steps include:
Cloning the repository:
Installing dependencies:
Running the project locally:
Running tests:
4. Identify Key Files
Configuration Files: .env, config.js, or similar files for environment variables.
Main Logic: Look for files like app.js, server.js, or index.tsx to understand the core logic.
Dependencies: Check package.json, requirements.txt, or similar files to see what libraries and frameworks are used.
Tests: Look for test files to understand how the code is validated.
5. Understand the Workflow
Version Control: Check the branching strategy (e.g., main, develop, feature branches).
Task Management: Are there issues or tasks in a tool like GitHub Issues, Jira, or Trello?
Code Reviews: Understand the process for submitting and reviewing pull requests.
6. Explore the Code
Start with the entry point of the application and trace how the code flows.
Look for comments and function names to understand what each part does.
Identify dependencies and how they are used.
7. Ask for Help
If the project has a team, ask for:
A walkthrough of the codebase.
Key areas to focus on.
Any known pain points or challenges.
8. Take Notes
Document what you learn as you explore the project. This will help you onboard faster and serve as a reference later.
Let me know if you'd like help analyzing specific files or sections of the project!

# With knowledge of the codebase

To help you get started with the Jurassic Code project and work efficiently, here’s a structured approach:

1. Understand the Project's Purpose
Goal: The project appears to be a system for managing a dinosaur park, including zones, dinosaurs, and real-time monitoring.
Key Features:
Zone management (e.g., adding, toggling, and moving dinosaurs between zones).
Compatibility checks for dinosaur species.
Real-time data ingestion and monitoring.
Automated reporting and alerts.
2. Analyze the Project Structure
The project follows a layered architecture with a mix of backend and frontend components:

Backend
Key Folders:
JurassicCode: Contains core business logic and domain models.
JurassicCode.API: Exposes API endpoints (e.g., ParkController).
JurassicCode.Db2: Handles data persistence.
JurassicCode.Tests: Contains unit tests for backend functionality.
Key Classes:
ParkService: Core service managing zones and dinosaurs.
DataAccessLayer: Static class for database interactions.
Zone and Dinosaur: Represent domain entities.
Frontend
Key Folders:
jurassic-ui: React-based frontend for user interaction.
src/components: Contains styled components and features like SpeciesCompatibilityChecker.
src/styles: Centralized theme and global styles.
Key Files:
main.tsx: Entry point for the React app.
index.css: Global CSS styles.
3. Set Up the Project
Follow these steps to build and run the project:

Backend
Build the solution:
Run tests:
Frontend
Install dependencies:
Build the frontend:
4. Key Areas to Focus On
Backend:
ParkService: Central to the business logic.
DataAccessLayer: Handles database interactions.
Tests: Review and improve test coverage.
Frontend:
SpeciesCompatibilityChecker: Implements compatibility checks.
index.ts: Defines reusable styled components.
5. Known Issues and Improvements
Code Smells:
Static DataAccessLayer with global state.
Lack of dependency injection in some areas.
Poor naming conventions (e.g., Class1, Init).
Testing:
Backend has decent coverage (~81%), but frontend lacks tests (0% coverage).
Dependencies:
Backend is outdated (netcoreapp3.1 is no longer supported).
Frontend dependencies are relatively up-to-date.
6. Next Steps
Refactor:
Follow the Mikado graph in prompt-result.md to refactor ParkController and ParkService.
Add Tests:
Focus on frontend test coverage.
Enhance Architecture:
Improve separation of concerns and adhere to SOLID principles.
Let me know if you'd like help with specific files or tasks!
