export const theme = {
  colors: {
    primary: '#1c5e2f', // Jurassic Park green
    secondary: '#e02c2c', // Danger red
    accent: '#ebbe17', // Warning yellow
    background: '#0a0a0a', // Dark background
    text: '#ffffff',
    textSecondary: '#cccccc',
    border: '#333333',
    warning: '#ff9800',
    success: '#4caf50',
    danger: '#f44336',
    cardBackground: 'rgba(20, 20, 20, 0.8)',
  },
  fonts: {
    main: "'Roboto', sans-serif",
    title: "'Impact', 'Arial Black', sans-serif", // Using standard fonts that are similar to Jurassic Park style
  },
  breakpoints: {
    mobile: '576px',
    tablet: '768px',
    desktop: '1024px',
    widescreen: '1200px',
  },
  sizes: {
    headerHeight: '80px',
    footerHeight: '60px',
  },
  shadows: {
    small: '0 1px 3px rgba(0, 0, 0, 0.12), 0 1px 2px rgba(0, 0, 0, 0.24)',
    medium: '0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23)',
    large: '0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23)',
    highlight: '0 0 10px #ebbe17, 0 0 20px #ebbe17',
  },
  animations: {
    easeOut: 'cubic-bezier(0.25, 0.46, 0.45, 0.94)',
    bounce: 'cubic-bezier(0.68, -0.55, 0.27, 1.55)',
  },
  spacing: {
    xs: '4px',
    sm: '8px',
    md: '16px',
    lg: '24px',
    xl: '32px',
    xxl: '48px',
  },
  borderRadius: {
    small: '4px',
    medium: '8px',
    large: '16px',
    round: '50%',
  },
};

export type Theme = typeof theme;