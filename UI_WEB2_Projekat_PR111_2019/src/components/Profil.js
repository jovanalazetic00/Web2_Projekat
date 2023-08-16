import React, {useEffect, useState} from 'react';
import {AzurirajProfilKorisinika, MojProfil} from '../services/ProfilService';
import { useParams } from 'react-router-dom';
import './Tabele.css';
import { Link } from 'react-router-dom';
import { Button } from '@mui/material';
import jwtDecode from 'jwt-decode';
import validator from 'validator';
import { ProvjeriMail } from '../services/LogovanjeService';

const Profil = () => {
  const{id} = useParams();
  const [korisnik, setKorisnik] = useState([]);

  const [ime, setIme] = useState('');
  const [prezime, setPrezime] = useState('');
  const [korisnickoIme, setKorisnickoIme] = useState('');
  const [email, setEmail] = useState('');
  const [lozinka, setLozinka] = useState('');
  const [potvrdaLozinke, setPotvrdaLozinke] = useState('');
  const [datumRodjenja, setDatumRodjenja] = useState('');
  const [adresa, setAdresa] = useState('');
  const [azuriranje, setAzuriranje] = useState(false);
  const[message, setMessage] = useState('');
  const[slika, setSlika] = useState(null);
  const[messVerifikacija, setMessVerifikacija] = useState('');


  const token = localStorage.getItem('token');
  const dekodiranToken = jwtDecode(token);
  const idd = dekodiranToken['IdKorisnika'];
  const uloga = dekodiranToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  const statusVerifrikacije = dekodiranToken['StatusVerifrikacije'];

  const provjeriEmail = async () => {
    try {
      setMessage('');
      const emailPostoji = await ProvjeriMail(email);
       if (emailPostoji) {
         setMessage('Mail već postoji u bazi');
       }
      
    } catch (error) {
      console.log(error);
      
    }
  };
  

  const handleAzuriraj = async() => {

    if (ime.trim() === '' ||
      prezime.trim() === '' ||
      korisnickoIme.trim() === '' ||
      email.trim() === '' ||
      lozinka.trim() === '' ||
      potvrdaLozinke.trim() === '' ||
      datumRodjenja.trim() === '' ||
      adresa.trim() === '' ||
      slika === null) {
    
    setMessage('Sva polja su obavezna.');
    return;
  }

  if (lozinka !== potvrdaLozinke) {
    setMessage("Lozinka i ponovljena lozinka se ne poklapaju");
   return;
  }
  

  if(lozinka.length < 5)
  {
   setMessage("Lozinka mora imati vise od 5 karaktera");
   return;
  }

  if(potvrdaLozinke.length < 5)
  {
   setMessage("Potvrdjena lozinka mora imati vise od 5 karaktera");
   return;
  }

  if (!validator.isAlpha(ime)) {
   setMessage('Ime može sadržavati samo slova.');
   return;
 }

 
 if (!validator.isAlpha(prezime)) {
   setMessage('Prezime može sadržavati samo slova.');
   return;
 }

 if(!slika)
 {
   setMessage('Odaberite sliku.');
   return;
 }
  
    const formData = new FormData();
     
    formData.append('id', id);
    formData.append('ime', ime);
    formData.append('prezime', prezime);
    formData.append('korisnickoIme', korisnickoIme);
    formData.append('email', email);
    formData.append('lozinka', lozinka);
    formData.append('potvrdaLozinke', potvrdaLozinke);
    formData.append('datumRodjenja', datumRodjenja);
    formData.append('adresa', adresa);
    formData.append('slika', slika);

    try{

        provjeriEmail();
          await AzurirajProfilKorisinika(id, formData);
          setKorisnik({
              ...korisnik, ime, prezime, korisnickoIme, email, datumRodjenja, adresa, slika 
          });
          
          setIme('');
          setPrezime('');
          setKorisnickoIme('');
          setEmail('');
          setLozinka('');
          setPotvrdaLozinke('');
          setDatumRodjenja('');
          setAdresa('');
          setSlika(null);
          azuriraj();
          
      }
      catch(err)
      {
        console.log(err);
      }
    };


    const handleLogoutClick = () => {
        
      localStorage.removeItem('token');
    
    };


 useEffect(() => {
  const get = async() => {
  try{
      const response = await MojProfil(id);
      console.log(response);
      setKorisnik(response.data);
  }
  catch(error)
  {
      console.log(error);
  }
  };
  get();
}, [id]);


const azuriraj = async() => {
  const response = await MojProfil(id);
  console.log(response);
  setKorisnik(response.data);
}


useEffect(() => {
  if (uloga === 'Prodavac') {
    setMessVerifikacija(statusVerifrikacije);
  }
}, [uloga, statusVerifrikacije]);

return (
  <div>
    {messVerifikacija && (
      <div>
        <p style={{ color: 'blue' }}> Status verifikacije: {messVerifikacija}</p>
      </div>
    )}
  <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
    <Button color="inherit" onClick={handleLogoutClick}>
      Logout
    </Button>
  </div>

    <h2>Profil korisnika</h2>
    <table  style={{ whiteSpace: "nowrap" }}  >
      <tbody>
        <tr>
          <th>Korisnik ID</th>
          <td>{korisnik.idKorisnika}</td>
        </tr>
        <tr>
          <th>Ime</th>
          <td>{korisnik.ime}</td>
        </tr>
        <tr>
          <th>Prezime</th>
          <td>{korisnik.prezime}</td>
        </tr>
        <tr>
          <th>Korisničko ime</th>
          <td>{korisnik.korisnickoIme}</td>
        </tr>
        <tr>
          <th>Email</th>
          <td>{korisnik.email}</td>
        </tr>
        <tr>
          <th>Adresa</th>
          <td>{korisnik.adresa}</td>
        </tr>
        <tr>
          <th>Datum rođenja</th>
          <td>{korisnik.datumRodjenja}</td>
        </tr>
        <tr>
          <th>Uloga</th>
          <td>
              {(() => {
                  switch (korisnik.tipKorisnika) {
                  case 0:
                      return "Administrator";
                  case 1:
                      return "Kupac";
                  case 2:
                      return "Prodavac";
                  default:
                      return "";
                  }
              })()}
            </td>
        </tr>
        <tr>
          <th>Slika korisnika</th>
          {korisnik.slika && ( <td>
       <img src={`data:image/png;base64,${korisnik.slika}`} 
       alt="Slika korisnika"
       width={150}
       height={150}
       /></td>)}
        </tr>
      </tbody>
    </table>


  <h2>Azuriranje korisnika</h2>
    <div className='form'>
    <form>
      <div className='form'>
      <label>
        Ime:
        <input
          type="text"
          value={ime}
          onChange={(e) => setIme(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Prezime:
        <input
          type="text"
          value={prezime}
          onChange={(e) => setPrezime(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Korisničko ime:
        <input
          type="text"
          value={korisnickoIme}
          onChange={(e) => setKorisnickoIme(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Email:
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Lozinka:
        <input
          type="password"
          value={lozinka}
          onChange={(e) => setLozinka(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Potvrdi lozinku:
        <input
          type="password"
          value={potvrdaLozinke}
          onChange={(e) => setPotvrdaLozinke(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Datum rođenja:
        <input
          type="date"
          value={datumRodjenja}
          onChange={(e) => setDatumRodjenja(e.target.value)}
        />
      </label>

      </div>
      <br />
      <div className='form'>
      <label>
        Adresa:
        <input
          type="text"
          value={adresa}
          onChange={(e) => setAdresa(e.target.value)}
        />
      </label>

      </div>
      <div className='form'>
        <label>Odaberite sliku:</label>
      <input 
          type="file" 
          accept="image/*"
          onChange={(e) => setSlika(e.target.files[0])}
        />
      </div>
      <br />
      <button className='btn' type="button" onClick={handleAzuriraj}>
        Ažuriraj
      </button>
    </form>

    {azuriranje && (
      <div>
        <h2>Ažurirani podaci:</h2>
        <p>Ime: {ime}</p>
        <p>Prezime: {prezime}</p>
        <p>Korisničko ime: {korisnickoIme}</p>
        <p>Email: {email}</p>
        <p>Datum rođenja: {datumRodjenja}</p>
        <p>Adresa: {adresa}</p>
        <p>Slika: {slika} </p>
      </div>
    )}
  </div>
  <label className='nazad' htmlFor="/dashBoard"> <Link to="/dashBoard">Dash Board</Link> </label>
  <label className='nazad' htmlFor="/pocetna"> <Link to="/">Povratak na pocetnu stranicu</Link> </label>

  <p>{message && (
      <div>
        <p style={{ color: 'red' }}> Greska: {message}</p>
      </div>
    )}</p>
  
  </div>
);
};

export default Profil;
