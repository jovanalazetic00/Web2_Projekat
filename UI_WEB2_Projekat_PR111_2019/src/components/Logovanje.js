import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Logovanje.css'; 

const Logovanje = () => {
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [email, setEmail] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [greske, setGreske] = useState([]);
  const [uspjesno, setUspjesno] = useState(false);

  const navigate = useNavigate();

  const handleSubmit = async (event) => {
    event.preventDefault();

    if (!korisnickoIme || !email || !lozinka) {
      setGreske(['Unesite sve podatke']);
      setUspjesno(false);
      return;
    }

    if (lozinka.length < 8) {
      setGreske(['Lozinka mora imati barem 8 karaktera']);
      setUspjesno(false);
      return;
    }

    try {
      // Simulacija prijave korisnika
      setUspjesno(true);
      setGreske([]);
      navigate('/dashboard');
    } catch (error) {
      setGreske(['Niste ispravno popunili podatke']);
    }
  };

  return (
    <div className="forma-container">
      <h2>Prijava</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Korisničko ime:</label>
          <input
            type="text"
            value={korisnickoIme}
            onChange={(e) => setKorisnickoIme(e.target.value)}
          />
        </div>
        <div>
          <label>Email:</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <label>Lozinka:</label>
          <input
            type="password"
            value={lozinka}
            onChange={(e) => setLozinka(e.target.value)}
          />
        </div>
        {greske.length > 0 && (
          <div className="error-container">
            <ul>
              {greske.map((greska, index) => (
                <li key={index}>{greska}</li>
              ))}
            </ul>
          </div>
        )}
        {uspjesno && (
          <div className="success-container">
            <p>Prijava uspješna!</p>
          </div>
        )}
        <button type="submit">Prijavi se</button>
      </form>
      <br />
      <Link to="/registracija">Nemate nalog? Registruj se!</Link>
    </div>
  );
};

export default Logovanje;