import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import { ThemeProvider } from 'styled-components'
import { theme } from './styles/theme'
import GlobalStyles from './styles/globalStyles'
import Dashboard from './pages/Dashboard'
import Zones from './pages/Zones'
import Dinosaurs from './pages/Dinosaurs'
import Compatibility from './pages/Compatibility'

function App() {
  return (
    <ThemeProvider theme={theme}>
      <GlobalStyles />
      <Router>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/zones" element={<Zones />} />
          <Route path="/dinosaurs" element={<Dinosaurs />} />
          <Route path="/compatibility" element={<Compatibility />} />
        </Routes>
      </Router>
    </ThemeProvider>
  )
}

export default App