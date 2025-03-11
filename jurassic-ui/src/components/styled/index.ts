import styled from 'styled-components';
import { theme } from '../../styles/theme';

export const Button = styled.button`
  background-color: ${props => props.color === 'danger' 
    ? theme.colors.danger 
    : props.color === 'success' 
      ? theme.colors.success 
      : theme.colors.primary};
  color: ${theme.colors.text};
  border: none;
  border-radius: ${theme.borderRadius.small};
  padding: ${theme.spacing.sm} ${theme.spacing.md};
  font-family: ${theme.fonts.main};
  font-size: 1rem;
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 1px;
  transition: all 0.2s ${theme.animations.easeOut};
  box-shadow: ${theme.shadows.medium};
  
  &:hover {
    transform: translateY(-2px);
    box-shadow: ${theme.shadows.large};
    background-color: ${props => props.color === 'danger' 
      ? '#d32f2f' 
      : props.color === 'success' 
        ? '#388e3c' 
        : '#124020'};
  }
  
  &:active {
    transform: translateY(1px);
    box-shadow: ${theme.shadows.small};
  }
  
  &:disabled {
    background-color: #555;
    cursor: not-allowed;
    transform: none;
    box-shadow: none;
  }
`;

export const Card = styled.div`
  background-color: ${theme.colors.cardBackground};
  border-radius: ${theme.borderRadius.medium};
  padding: ${theme.spacing.lg};
  box-shadow: ${theme.shadows.medium};
  border: 1px solid rgba(255, 255, 255, 0.1);
`;

export const DinoCard = styled(Card)`
  position: relative;
  overflow: hidden;
  transition: all 0.3s ${theme.animations.easeOut};
  
  &:hover {
    transform: translateY(-5px);
    box-shadow: ${theme.shadows.large};
  }
  
  &:before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 5px;
    background: ${props => props.isCarnivorous ? theme.colors.danger : theme.colors.primary};
  }
`;

export const ZoneCard = styled(Card)`
  border: 2px solid ${props => props.isOpen ? theme.colors.success : theme.colors.danger};
  transition: all 0.3s ${theme.animations.easeOut};
  
  &:hover {
    box-shadow: 0 0 15px ${props => props.isOpen ? 'rgba(76, 175, 80, 0.3)' : 'rgba(244, 67, 54, 0.3)'};
  }
`;

export const FormGroup = styled.div`
  margin-bottom: ${theme.spacing.md};
`;

export const Input = styled.input`
  width: 100%;
  padding: ${theme.spacing.sm} ${theme.spacing.md};
  background-color: rgba(255, 255, 255, 0.1);
  border: 1px solid ${theme.colors.border};
  border-radius: ${theme.borderRadius.small};
  color: ${theme.colors.text};
  font-family: ${theme.fonts.main};
  font-size: 1rem;
  
  &:focus {
    outline: none;
    border-color: ${theme.colors.primary};
    box-shadow: 0 0 0 2px rgba(28, 94, 47, 0.3);
  }
  
  &::placeholder {
    color: ${theme.colors.textSecondary};
  }
`;

export const Select = styled.select`
  width: 100%;
  padding: ${theme.spacing.sm} ${theme.spacing.md};
  background-color: rgba(255, 255, 255, 0.1);
  border: 1px solid ${theme.colors.border};
  border-radius: ${theme.borderRadius.small};
  color: ${theme.colors.text};
  font-family: ${theme.fonts.main};
  font-size: 1rem;
  
  &:focus {
    outline: none;
    border-color: ${theme.colors.primary};
    box-shadow: 0 0 0 2px rgba(28, 94, 47, 0.3);
  }
  
  option {
    background-color: ${theme.colors.background};
  }
`;

export const Checkbox = styled.input.attrs({ type: 'checkbox' })`
  margin-right: ${theme.spacing.sm};
  
  &:checked {
    accent-color: ${theme.colors.primary};
  }
`;

export const Label = styled.label`
  display: block;
  margin-bottom: ${theme.spacing.xs};
  color: ${theme.colors.textSecondary};
  font-weight: 500;
`;

export const FlexContainer = styled.div`
  display: flex;
  flex-wrap: wrap;
  gap: ${theme.spacing.md};
`;

export const Grid = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: ${theme.spacing.lg};
`;

export const Badge = styled.span`
  display: inline-block;
  padding: ${theme.spacing.xs} ${theme.spacing.sm};
  background-color: ${props => {
    if (props.type === 'carnivore') return theme.colors.danger;
    if (props.type === 'herbivore') return theme.colors.success;
    if (props.type === 'sick') return theme.colors.warning;
    return theme.colors.primary;
  }};
  border-radius: ${theme.borderRadius.small};
  font-size: 0.8rem;
  font-weight: 500;
  margin-right: ${theme.spacing.xs};
`;

export const PageHeader = styled.header`
  padding: ${theme.spacing.xl} 0;
  margin-bottom: ${theme.spacing.lg};
  text-align: center;
  
  h1 {
    font-size: 3.5rem;
    text-transform: uppercase;
    letter-spacing: 2px;
    margin-bottom: ${theme.spacing.sm};
    text-shadow: 0 0 10px rgba(235, 190, 23, 0.7);
  }
  
  p {
    font-size: 1.2rem;
    max-width: 800px;
    margin: 0 auto;
    color: ${theme.colors.textSecondary};
  }
`;

export const LoadingSpinner = styled.div`
  display: inline-block;
  width: 50px;
  height: 50px;
  border: 5px solid rgba(255, 255, 255, 0.3);
  border-radius: 50%;
  border-top-color: ${theme.colors.primary};
  animation: spin 1s linear infinite;
  margin: ${theme.spacing.md} auto;
  
  @keyframes spin {
    to {
      transform: rotate(360deg);
    }
  }
`;

export const AlertBox = styled.div`
  padding: ${theme.spacing.md};
  background-color: ${props => {
    if (props.type === 'success') return 'rgba(76, 175, 80, 0.2)';
    if (props.type === 'error') return 'rgba(244, 67, 54, 0.2)';
    if (props.type === 'warning') return 'rgba(255, 152, 0, 0.2)';
    return 'rgba(33, 150, 243, 0.2)';
  }};
  border-left: 4px solid ${props => {
    if (props.type === 'success') return theme.colors.success;
    if (props.type === 'error') return theme.colors.danger;
    if (props.type === 'warning') return theme.colors.warning;
    return theme.colors.primary;
  }};
  border-radius: ${theme.borderRadius.small};
  margin-bottom: ${theme.spacing.md};
`;

export const Navbar = styled.nav`
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: rgba(0, 0, 0, 0.8);
  backdrop-filter: blur(10px);
  padding: ${theme.spacing.md} ${theme.spacing.lg};
  box-shadow: ${theme.shadows.medium};
  position: sticky;
  top: 0;
  z-index: 100;
`;

export const Logo = styled.div`
  font-family: ${theme.fonts.title};
  font-size: 1.8rem;
  color: ${theme.colors.accent};
  text-shadow: 0 0 5px rgba(235, 190, 23, 0.5);
  text-transform: uppercase;
  letter-spacing: 1px;
  font-weight: bold;
`;

export const NavLinks = styled.ul`
  display: flex;
  list-style: none;
  
  li {
    margin-left: ${theme.spacing.lg};
    
    a {
      color: ${theme.colors.text};
      text-transform: uppercase;
      font-weight: 500;
      letter-spacing: 1px;
      position: relative;
      
      &:after {
        content: '';
        position: absolute;
        bottom: -5px;
        left: 0;
        width: 0;
        height: 2px;
        background-color: ${theme.colors.accent};
        transition: width 0.3s ${theme.animations.easeOut};
      }
      
      &:hover:after, &.active:after {
        width: 100%;
      }
    }
  }
`;

export const Footer = styled.footer`
  background-color: rgba(0, 0, 0, 0.8);
  text-align: center;
  padding: ${theme.spacing.md};
  margin-top: auto;
  
  p {
    color: ${theme.colors.textSecondary};
    font-size: 0.9rem;
  }
`;

export const Divider = styled.hr`
  border: none;
  border-bottom: 1px solid ${theme.colors.border};
  margin: ${theme.spacing.md} 0;
`;

export const StatusIndicator = styled.div`
  width: 12px;
  height: 12px;
  border-radius: 50%;
  background-color: ${props => props.status === 'open' ? theme.colors.success : theme.colors.danger};
  display: inline-block;
  margin-right: ${theme.spacing.xs};
`;

export const Modal = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
`;

export const ModalContent = styled.div`
  background-color: ${theme.colors.cardBackground};
  border-radius: ${theme.borderRadius.medium};
  padding: ${theme.spacing.xl};
  max-width: 500px;
  width: 100%;
  max-height: 90vh;
  overflow-y: auto;
  box-shadow: ${theme.shadows.large};
  border: 1px solid rgba(255, 255, 255, 0.1);
  
  h2 {
    margin-top: 0;
  }
`;

export const CloseButton = styled.button`
  position: absolute;
  top: 10px;
  right: 10px;
  background: transparent;
  border: none;
  color: ${theme.colors.text};
  font-size: 1.5rem;
  cursor: pointer;
  
  &:hover {
    color: ${theme.colors.accent};
  }
`;

export const TabContainer = styled.div`
  margin-bottom: ${theme.spacing.lg};
`;

export const TabList = styled.div`
  display: flex;
  border-bottom: 1px solid ${theme.colors.border};
  margin-bottom: ${theme.spacing.md};
`;

export const Tab = styled.button`
  padding: ${theme.spacing.sm} ${theme.spacing.md};
  background: transparent;
  border: none;
  color: ${props => props.active ? theme.colors.accent : theme.colors.text};
  font-weight: 500;
  cursor: pointer;
  border-bottom: 2px solid ${props => props.active ? theme.colors.accent : 'transparent'};
  
  &:hover {
    color: ${theme.colors.accent};
  }
`;

export const TabPanel = styled.div`
  display: ${props => props.active ? 'block' : 'none'};
`;

export const Scanner = styled.div`
  position: relative;
  width: 100%;
  height: 5px;
  background-color: rgba(235, 190, 23, 0.3);
  overflow: hidden;
  
  &:before {
    content: '';
    position: absolute;
    top: 0;
    left: -50%;
    width: 50%;
    height: 100%;
    background: linear-gradient(to right, transparent, ${theme.colors.accent}, transparent);
    animation: scan 2s infinite linear;
  }
  
  @keyframes scan {
    0% {
      left: -50%;
    }
    100% {
      left: 100%;
    }
  }
`;

export const Terminal = styled.div`
  background-color: #000;
  color: #00ff00;
  font-family: monospace;
  padding: ${theme.spacing.md};
  border-radius: ${theme.borderRadius.small};
  margin-bottom: ${theme.spacing.md};
  border: 1px solid #00ff00;
  
  pre {
    margin: 0;
    white-space: pre-wrap;
  }
`;

export const SecurityCamera = styled.div`
  position: relative;
  width: 100%;
  aspect-ratio: 16/9;
  background-color: #111;
  border-radius: ${theme.borderRadius.small};
  overflow: hidden;
  margin-bottom: ${theme.spacing.md};
  
  &:before {
    content: 'LIVE';
    position: absolute;
    top: 10px;
    right: 10px;
    background-color: ${theme.colors.danger};
    color: white;
    padding: 2px 6px;
    border-radius: 4px;
    font-size: 0.7rem;
    font-weight: bold;
  }
  
  &:after {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: repeating-linear-gradient(
      0deg,
      rgba(0, 0, 0, 0.1),
      rgba(0, 0, 0, 0.1) 1px,
      transparent 1px,
      transparent 2px
    );
    pointer-events: none;
  }
`;

export const DinoTracker = styled.div`
  position: relative;
  width: 100%;
  aspect-ratio: 1;
  background-color: #143018;
  border-radius: 50%;
  padding: 15px;
  box-shadow: inset 0 0 20px rgba(0, 0, 0, 0.5), 0 0 10px rgba(28, 94, 47, 0.7);
  margin: ${theme.spacing.lg} auto;
  max-width: 300px;
  
  &:before {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    width: 70%;
    height: 70%;
    transform: translate(-50%, -50%);
    border-radius: 50%;
    border: 2px solid rgba(235, 190, 23, 0.7);
  }
  
  &:after {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    width: 40%;
    height: 40%;
    transform: translate(-50%, -50%);
    border-radius: 50%;
    border: 2px solid rgba(235, 190, 23, 0.5);
  }
`;

export const DinoBlip = styled.div`
  position: absolute;
  width: 10px;
  height: 10px;
  background-color: ${props => props.isCarnivorous ? theme.colors.danger : theme.colors.primary};
  border-radius: 50%;
  transform: translate(-50%, -50%);
  box-shadow: 0 0 10px ${props => props.isCarnivorous ? theme.colors.danger : theme.colors.primary};
  
  &:before {
    content: '';
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 20px;
    height: 20px;
    background-color: transparent;
    border-radius: 50%;
    border: 2px solid ${props => props.isCarnivorous ? theme.colors.danger : theme.colors.primary};
    opacity: 0.7;
    animation: ping 1.5s infinite;
  }
  
  @keyframes ping {
    0% {
      opacity: 0.7;
      transform: translate(-50%, -50%) scale(1);
    }
    100% {
      opacity: 0;
      transform: translate(-50%, -50%) scale(2);
    }
  }
`;

export const WarningFlash = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: ${theme.colors.danger};
  opacity: 0;
  pointer-events: none;
  z-index: 9999;
  animation: ${props => props.active ? 'flash 0.5s infinite' : 'none'};
  
  @keyframes flash {
    0%, 100% {
      opacity: 0;
    }
    50% {
      opacity: 0.3;
    }
  }
`;

export const VisitorCounter = styled.div`
  background-color: rgba(0, 0, 0, 0.6);
  padding: ${theme.spacing.sm} ${theme.spacing.md};
  border-radius: ${theme.borderRadius.small};
  display: inline-flex;
  align-items: center;
  margin-bottom: ${theme.spacing.md};
  
  span {
    font-family: 'Digital', monospace;
    font-size: 1.2rem;
    color: ${theme.colors.accent};
    margin-left: ${theme.spacing.sm};
  }
`;