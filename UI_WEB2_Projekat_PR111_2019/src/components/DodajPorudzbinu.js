import React, { useState, useEffect } from "react";
import {
  DobaviPorudzbinuPoID,
  NovaPorudzbina,
  SvePorudzbine,
} from "../services/PorudzbinaService";
import "./Tabele.css";
import { SviArtikli } from "../services/ArtikalService";
import { PrethodnePorudzbineKupca } from "../services/PorudzbinaService";
import { Link } from "react-router-dom";

const DodajPorudzbinu = () => {
  const [adresaIsporuke, setAdresaIsporuke] = useState("");
  const [komentarPorudzbine, setKomentarPorudzbine] = useState("");
  const [artikli, setArtikli] = useState([]);
  const [odabraniArtikal, setOdabraniArtikal] = useState(null);
  const [kolicinaArtikla, setKolicinaArtikla] = useState(0);
  const [artikliIPorudzbine, setArtikliIPorudzbine] = useState([]);
  const [message, setMessage] = useState("");
  const [error, setError] = useState("");

  const osvjeziArtikle = async () => {
    try {
      const response = await SviArtikli();
      setArtikli(response.data);
    } catch (error) {
      console.error("Greška prilikom osvežavanja artikala:", error);
    }
  };

  useEffect(() => {
    const dobaviArtikle = async () => {
      try {
        const response = await SviArtikli();
        setArtikli(response.data);
      } catch (error) {
        console.error("Greška prilikom dobavljanja artikala:", error);
      }
    };
    dobaviArtikle();
  }, []);

  const handleArtikalChange = (artikalId) => {
    setOdabraniArtikal(artikalId);
    setKolicinaArtikla(0);
  };

  const handleKolicinaArtiklaChange = (e) => {
    setKolicinaArtikla(Number(e.target.value));
  };

  const handleDodajArtikalIPorudzbina = () => {
    if (odabraniArtikal && kolicinaArtikla > 0) {
      const artikal = artikli.find(
        (artikal) => artikal.artikalId === odabraniArtikal
      );
      const novaArtikalIPorudzbina = {
        iDArtiklaAIP: artikal.artikalId,
        kolicinaArtikla: kolicinaArtikla, 
      };
      setArtikliIPorudzbine((prevArtikalIPorudzbina) => [...prevArtikalIPorudzbina, novaArtikalIPorudzbina]);
      setOdabraniArtikal(null);
      setKolicinaArtikla(0);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  
    if (
      adresaIsporuke.trim() === '' ||
      kolicinaArtikla === 0 ||
      komentarPorudzbine.trim() === ''
    ) {
      setError("Sva polja su obavezna.");
      return;
    }
  
    try {
      const reqData = {
        adresaIsporuke,
        artikliIPorudzbine,
        komentarPorudzbine,
      };
  
      console.log("Slanje zahteva...", reqData);
  
      const idPor = await NovaPorudzbina(reqData);
      console.log("Id porudžbine:", idPor);
  
      setAdresaIsporuke('');
      setArtikliIPorudzbine([]);
      setKomentarPorudzbine('');
  
      osvjeziArtikle();
      await SvePorudzbine();
      await PrethodnePorudzbineKupca();
  
      const por = await DobaviPorudzbinuPoID(idPor);
      console.log("Vrijeme isporuke:", por.data.vrijemeIsporuke);
      setMessage(por.data.vrijemeIsporuke);
    } catch (error) {
      console.error("Greška prilikom slanja zahteva:", error);
    }
  };
  
  console.log("Render komponente DodajPorudzbinu");

  
    return (
      <div>
        <h2>Dodaj porudžbinu</h2>
        <form onSubmit={handleSubmit}>
          <div className="form">
            <label htmlFor="adresaIsporuke">Adresa isporuke:</label>
            <input
              type="text"
              id="adresaIsporuke"
              name="AdresaIsporuke"
              value={adresaIsporuke}
              onChange={(e) => setAdresaIsporuke(e.target.value)}
            />
          </div>
  
          <div>
            <h2>Dostupni artikli</h2>
            <table>
              <thead>
                <tr>
                  <th>Artikal ID</th>
                  <th>Naziv</th>
                  <th>Cijena</th>
                  <th>Količina artikla</th>
                  <th>Opis</th>
                  <th>Slika</th>
                  <th>Odaberi</th>
                </tr>
              </thead>
              <tbody>
                {artikli.map((artikal) => (
                  <tr key={artikal.artikalId}>
                    <td>{artikal.artikalId}</td>
                    <td>{artikal.naziv}</td>
                    <td>{artikal.cijena}</td>
                    <td>{artikal.kolicinaArtikla}</td>
                    <td>{artikal.opis}</td>
                    
                    <td>
                  {artikal.slika && ( 
                  <img src={`data:image/png;base64,${artikal.slika}`} 
                            alt="Slika korisnika"
                            width={100}
                            height={100}
                    />)}
            
                    </td>
                    <td>
                      <input
                        type="radio"
                        name="odabraniArtikal"
                        value={artikal.artikalId}
                        checked={odabraniArtikal === artikal.artikalId}
                        onChange={() => handleArtikalChange(artikal.artikalId)}
                      />
                    </td>
                  </tr>
                ))}
          
              </tbody>
            </table>
          </div>
  
          <div className="form">
            <label htmlFor="kolicinaArtikla">Količina artikla:</label>
            <input
              type="number"
              id="kolicinaArtikla"
              name="KolicinaArtikla"
              value={kolicinaArtikla}
              min="1"
              onChange={handleKolicinaArtiklaChange}
            />
            <button type="button" onClick={handleDodajArtikalIPorudzbina}>Dodaj</button>
          </div>
  
          <div className="form">
            <label htmlFor="komentarPorudzbine">Komentar porudzbine:</label>
            <textarea
              id="komentarPorudzbine"
              name="KomentarPorudzbine"
              value={komentarPorudzbine}
              onChange={(e) => setKomentarPorudzbine(e.target.value)}
            />
          </div>
  
          <button className="btn" type="submit">Poruči</button>
        </form>
        {message && (
          <div>
            <p style={{ color: 'blue' }}> Vrijeme dostave je:{message}</p>
          </div>
        )}
         {error && (
          <div>
            <p style={{ color: 'blue' }}> Greska:{error}</p>
          </div>
        )}
  <br/><br/>
  <label className='nazad' htmlFor="/dashBoard"> <Link to="/dashBoard">Dash Board</Link> </label>
      </div>
      
    );
  };
  
  export default DodajPorudzbinu;
  