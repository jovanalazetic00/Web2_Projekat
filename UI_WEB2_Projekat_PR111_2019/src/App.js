import React from 'react';
import { BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import Registracija from './components/Registracija';
import Pocetna from './components/Pocetna';
import Logovanje from './components/Logovanje';

function App() {
  return (
    <Router>
      <Routes>
      <Route path="/" element={<Pocetna />} />
        <Route path="/logovanje" element={<Logovanje />} />
        <Route path="/registracija" element={<Registracija />} />
      </Routes>
      </Router>
  );
}

export default App;