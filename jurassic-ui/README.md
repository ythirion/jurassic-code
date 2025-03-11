# Jurassic Park Control System

A React-based frontend for the JurassicCode API, providing a visual interface to monitor and manage dinosaur assets and park zones in real-time. Inspired by the Jurassic Park movies.

## Features

- **Dashboard:** Real-time monitoring of park systems, security status, and dinosaur tracking
- **Zone Management:** Create, monitor, and toggle zones for visitor access
- **Dinosaur Management:** Add, monitor, and relocate dinosaurs throughout the park
- **Species Compatibility:** Check if different species can safely coexist in the same habitat

## Technologies Used

- React
- TypeScript
- Vite
- React Router for navigation
- Styled Components for theming and styling

## Getting Started

### Prerequisites

- Node.js (v14+)
- npm or yarn

### Installation

1. Clone the repository
2. Install dependencies:
   ```
   npm install
   ```
3. Start the development server:
   ```
   npm run dev
   ```

### Building for Production

```
npm run build
```

## API Integration

This UI connects to the JurassicCode API for all dinosaur and park management functionality. The API client is located in `src/services/api.ts`.

## UI Design

The interface is inspired by control systems seen in the Jurassic Park films, featuring:

- Dark theme with accent colors matching the Jurassic Park branding
- Security-focused UI elements like warning indicators and status monitors
- Interactive dinosaur tracking system
- Terminal-like outputs for technical information

## Future Enhancements

- Real-time notifications for security breaches
- Park visitor tracking and capacity management
- Weather monitoring and impact on dinosaur behavior
- Detailed dinosaur health monitoring and medical management
- Enhanced security camera views of enclosures

## Credits

- UI inspired by the Jurassic Park films by Universal Pictures
- Dinosaur information based on paleontological research
- Developed for use with the JurassicCode API backend
