import React, { useState } from 'react';
import { Link ,  useNavigate} from 'react-router-dom';
import { RegistracijaService } from '../services/RegistracijaService'; // Pobrinite se da ova putanja bude tačna
import './Registracija.css';

const Registracija = () => {
  const navigate = useNavigate();
  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [email, setEmail] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [potvrdaLozinke, setPotvrdaLozinke] = useState('');
  const [datumRodjenja, setDatumRodjenja] = useState('');
  const [adresa, setAdresa] = useState('');
  const [tipKorisnika, setTipKorisnika] = useState('Kupac');
  const [slika, setSlika] = useState(null);
  
  const [message, setMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append('ime', ime);
    formData.append('prezime', prezime);
    formData.append('korisnickoIme', korisnickoIme);
    formData.append('email', email);
    formData.append('lozinka', lozinka);
    formData.append('potvrdaLozinke', potvrdaLozinke);
    formData.append('datumRodjenja', datumRodjenja);
    formData.append('adresa', adresa);
    formData.append('tipKorisnika', tipKorisnika);
    formData.append('slika', slika);


    try {
      await RegistracijaService(formData);

      setIme('');
      setPrezime('');
      setKorisnickoIme('');
      setEmail('');
      setLozinka('');
      setPotvrdaLozinke('');
      setDatumRodjenja('');
      setAdresa('');
      setSlika(null);

      navigate("/logovanje");
    } catch (error) {
      if (error.response && error.response.data && error.response.data.errors) {
        const errors = error.response.data.errors;

        if (Array.isArray(errors)) {
          setMessage(errors.join('\n'));
        } else if (typeof errors === 'string') {
          setMessage(errors);
        } else {
          setMessage('Došlo je do greške.');
        }
      } else {
        console.log(error);
      }
    }
  };

  return (
    <div className='form'>
      <h2>Registracija</h2>
      <form onSubmit={handleSubmit}>
        <div className='form'>
          <label htmlFor="ime">Ime: </label>
          <input
            type="text"
            id="ime"
            value={ime}
            onChange={(e) => setIme(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="prezime">Prezime: </label>
          <input 
            type="text"
            id="prezime"
            value={prezime}
            onChange={(e) => setPrezime(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="korisnickoIme">Korisničko ime: </label>
          <input
            type="text"
            id="korisnickoIme"
            value={korisnickoIme}
            onChange={(e) => setKorisnickoIme(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="email">E-mail: </label>
          <input 
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="lozinka">Lozinka: </label>
          <input
            type="password"
            id="lozinka"
            value={lozinka}
            onChange={(e) => setLozinka(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="potvrdaLozinke">Potvrda lozinke: </label>
          <input 
            type="password"
            id="potvrdaLozinke"
            value={potvrdaLozinke}
            onChange={(e) => setPotvrdaLozinke(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="datumRodjenja">Datum rođenja: </label>
          <input 
            type="date"
            id="datumRodjenja"
            value={datumRodjenja}
            onChange={(e) => setDatumRodjenja(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="adresa">Adresa: </label>
          <input 
            type="text"
            id="adresa"
            value={adresa}
            onChange={(e) => setAdresa(e.target.value)}
            required
          />
        </div>
        <div className='form'>
          <label htmlFor="tipKorisnika">Uloga korisnika: </label>
          <select
            id="tipKorisnika"
            value={tipKorisnika}
            onChange={(e) => setTipKorisnika(e.target.value)}
            required
          >
            
            <option value="Kupac">Kupac</option>
            <option value="Prodavac">Prodavac</option>
          </select>
        </div>
        <div className='form'>
          <label>Odaberite sliku: </label>
          <input
            type="file"
            accept="image/*" 
            onChange={(e) => setSlika(e.target.files[0])}
          />
        </div>
        <div className='form'>
          <button className='btn' type="submit" >Registruj se</button>
          <br/>
        </div>
      </form>
      <label className='nazad' htmlFor="/home">
        <Link to="/">Povratak na početnu stranicu</Link>
      </label>
      {message && (
        <div>
          <p style={{ color: 'red' }}>Greška: {message}</p>
        </div>
      )}
    </div>
  );
};

export default Registracija;
