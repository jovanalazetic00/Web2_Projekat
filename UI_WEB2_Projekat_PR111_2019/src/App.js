
import './App.css';
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Pocetna from './components/Pocetna';
import Logovanje from './components/Logovanje';
import Registracija from './components/Registracija';
import DashBoard from './components/DashBoard';
import Korisnik from './components/Korisnik';
import Profil from './components/Profil';
import AdminStranica from './components/AdminStranica';
import KupacStranica from './components/KupacStranica';
import ProdavacStranica from './components/ProdavacStranica';
import PotvrdiRegistraciju from './components/PotvrdaRegistracije';
import ProdavciVerifikacija from './components/ProdavciVerifikacija';
import SviVerifikovaniProdavci  from './components/DobaviVerifikovaneProdavce';
import Porudzbina from './components/Porudzbina';
import PrethodnePorudzbine from './components/PrethodnePorudzbine';

function App() {
  return (
    <Router>
      
        <Routes>
          <Route path="/" element={<Pocetna />} />
          <Route path="/logovanje" element={<Logovanje />} />
          <Route path="/registracija" element={<Registracija />} />
          <Route path="/dashBoard" element={<DashBoard />} />
          <Route path="/korisnik" element={<Korisnik />}/>
          <Route path="/profil/:id" element={<Profil />}/>
          <Route path="/adminStranica" element={<AdminStranica />}/>
          <Route path="/kupacStranica" element={<KupacStranica />}/>
          <Route path="/prodavacStranica" element={<ProdavacStranica />}/>
          <Route path='/potvrdaRegistracije' element={<PotvrdiRegistraciju />}/>
          <Route path='/dobaviProdavce' element={<ProdavciVerifikacija />}/>
          <Route path='/prikaziSveVerifikovane' element={<SviVerifikovaniProdavci />}/>
          <Route path='/porudzbina' element={<Porudzbina/>}/>
          <Route path='/prethodnePorudzbine/:id' element={<PrethodnePorudzbine/>}/>
        </Routes>
      
    </Router>
  );
}

export default App;
