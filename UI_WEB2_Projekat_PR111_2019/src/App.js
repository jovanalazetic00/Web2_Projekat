
import './App.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Pocetna from './components/Pocetna';
import Logovanje from './components/Logovanje';
import Registracija from './components/Registracija';
import DashBoard from './components/DashBoard';


function App() {
  return (
    <Router>
      
        <Routes>
          <Route path="/" element={<Pocetna />} />
          <Route path="/logovanje" element={<Logovanje />} />
          <Route path="/registracija" element={<Registracija />} />
          <Route path="/dashBoard" element={<DashBoard />} />
        </Routes>
      
    </Router>
  );
}

export default App;
