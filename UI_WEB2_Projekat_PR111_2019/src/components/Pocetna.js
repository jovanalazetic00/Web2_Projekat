import React from 'react';
import { Link } from 'react-router-dom';
import './Pocetna.css';


const Pocetna = () => {
  
  return (
    <nav className="nav-menu">
      <ul>
        <li>
          <Link to="/logovanje">Login</Link>
        </li>
        <li>
          <Link to="/registracija">Registracija</Link>
        </li>
        
      </ul>
    </nav>
  );
}

export default Pocetna;