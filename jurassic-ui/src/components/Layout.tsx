import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import { Footer, Navbar, Logo, NavLinks } from './styled';

interface LayoutProps {
  children: React.ReactNode;
}

const Layout: React.FC<LayoutProps> = ({ children }) => {
  const location = useLocation();
  
  return (
    <>
      <Navbar>
        <Logo>Jurassic Park Control System</Logo>
        <NavLinks>
          <li>
            <Link to="/" className={location.pathname === '/' ? 'active' : ''}>
              Dashboard
            </Link>
          </li>
          <li>
            <Link to="/zones" className={location.pathname.includes('/zones') ? 'active' : ''}>
              Zones
            </Link>
          </li>
          <li>
            <Link to="/dinosaurs" className={location.pathname.includes('/dinosaurs') ? 'active' : ''}>
              Dinosaurs
            </Link>
          </li>
          <li>
            <Link to="/compatibility" className={location.pathname.includes('/compatibility') ? 'active' : ''}>
              Compatibility
            </Link>
          </li>
        </NavLinks>
      </Navbar>
      <main className="container">
        {children}
      </main>
      <Footer>
        <p>© 2025 Jurassic Park™ | InGen Corporation | All Rights Reserved</p>
      </Footer>
    </>
  );
};

export default Layout;