import React, { useState } from 'react';
import './Logovanje.css';
import { Link, useNavigate } from 'react-router-dom';
import { LogovanjeService, ProvjeriMail, setHeader } from '../services/LogovanjeService';



  
export const Logovanje = () => {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [lozinka, setLozinka] = useState('');
    const [error, setError] = useState('');
    const [message, setMessage] = useState('');


    const provjeriEmail = async () => {
      try {
    
        const emailPostoji = await ProvjeriMail(email);
    
      } catch (error) {
        console.log(error);
        setMessage('Mail ne postoji u bazi');
      }
  };


    const handleSubmit = async (e) => {
      e.preventDefault();
      try {

        provjeriEmail();

        const token = await LogovanjeService(email, lozinka);

          if(token !== null)
          {
            localStorage.setItem('token', token);
            setHeader(token);
        
            setEmail('');
            setLozinka('');
            navigate('/dashBoard');
          }
        }catch (error)
        {
          setMessage("Greska pri unosu podataka");
        }
    };


 

  return (
    <div className="form">
      <h2>Logovanje</h2>
      <form onSubmit={handleSubmit}>
        <div className="form">
          <label htmlFor="email">E-mail: </label>
          <input type="email" id="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
        </div>
        <div className="form">
          <label htmlFor="lozinka">Lozinka: </label>
          <input
            type="password"
            id="lozinka"
            value={lozinka}
            onChange={(e) => setLozinka(e.target.value)}
            required
          />
        </div>
        <button className="btn">Loguj se</button>
        <br/>
      </form>
      
        {error && <p className="error">{error}</p>}
        <br />
        <label className="nazad" htmlFor="/home">
          <Link to="/">Povratak na početnu stranicu</Link>
        </label>

        <p>  {message && (
        <div>
          <p style={{ color: 'red' }}> Greska: {message}</p>
        </div>
      )}</p>
         <p>  {error && (
        <div>
          <p style={{ color: 'red' }}> Greska: {error}</p>
        </div>
      )}</p>
        
    </div>
  );
};

export default Logovanje;