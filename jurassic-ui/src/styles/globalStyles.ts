import { createGlobalStyle } from 'styled-components';
import { theme } from './theme';

export const GlobalStyles = createGlobalStyle`
  @font-face {
    font-family: 'Jurassic Park';
    src: url('/fonts/jurassic-park.woff2') format('woff2');
    font-weight: normal;
    font-style: normal;
    font-display: swap;
  }
  
  * {
    box-sizing: border-box;
    margin: 0;
    padding: 0;
  }
  
  html, body {
    height: 100%;
    width: 100%;
    overflow-x: hidden;
  }
  
  body {
    background-color: ${theme.colors.background};
    background-image: url('/images/jungle-background.jpg');
    background-size: cover;
    background-attachment: fixed;
    background-position: center;
    color: ${theme.colors.text};
    font-family: ${theme.fonts.main};
    line-height: 1.5;
  }
  
  #root {
    display: flex;
    flex-direction: column;
    min-height: 100vh;
  }
  
  h1, h2, h3, h4, h5, h6 {
    font-family: ${theme.fonts.title};
    color: ${theme.colors.accent};
    margin-bottom: ${theme.spacing.md};
    letter-spacing: 1px;
  }
  
  h1 {
    font-size: 3rem;
    text-shadow: 0 0 10px rgba(235, 190, 23, 0.5);
  }
  
  h2 {
    font-size: 2.5rem;
  }
  
  h3 {
    font-size: 2rem;
  }
  
  h4 {
    font-size: 1.5rem;
  }
  
  button {
    cursor: pointer;
  }
  
  a {
    color: ${theme.colors.accent};
    text-decoration: none;
    transition: color 0.2s ${theme.animations.easeOut};
    
    &:hover {
      color: ${theme.colors.primary};
    }
  }
  
  .container {
    width: 100%;
    max-width: 1200px;
    margin: 0 auto;
    padding: 0 ${theme.spacing.md};
  }
  
  .glass-panel {
    background-color: rgba(0, 0, 0, 0.7);
    backdrop-filter: blur(10px);
    border: 1px solid rgba(255, 255, 255, 0.1);
    border-radius: ${theme.borderRadius.medium};
    padding: ${theme.spacing.lg};
  }
`;

export default GlobalStyles;