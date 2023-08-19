import React, { useState, useEffect } from "react";
import { AzurirajArtikal, ObrisiArtikal, SviArtikliProdavca } from "../services/ArtikalService";
import jwtDecode from "jwt-decode";
import { Link } from "react-router-dom";
import validator from 'validator';

const AzuriranjeArtikla = () => {

  const [naziv, setNaziv] = useState('');
  const [cijena, setCijena] = useState(0);
  const [kolicinaArtikla, setKolicinaArtikla] = useState(0);
  const [opis, setOpis] = useState('');
  const [slika, setSlika] = useState(null);
  const [message, setMessage] = useState('');

  const [artikal, setArtikal] = useState([]);
  const [artikli, setArtikli] = useState([]);
  const [id, setID] = useState([]);

  const token = localStorage.getItem('token');
  const dekodiranToken = jwtDecode(token);
  const idd = dekodiranToken['IdKorisnika'];

  const azurirani = async () => {
    try {
      const response = await SviArtikliProdavca(idd);
      setArtikli(response.data);
    } catch (error) {
      console.error('Greška prilikom dobavljanja artikala:', error);
    }
  };

  const handleObrisi = async (id) => {
    if (id < 0) {
      setMessage('ID mora biti pozitivan broj.');
    }
    try {
      await ObrisiArtikal(id);
      setID('');
      azurirani();
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    const dobaviArtikle = async () => {
      try {
        const response = await SviArtikliProdavca(idd);
        setArtikli(response.data);
      } catch (error) {
        console.error('Greška prilikom dobavljanja artikala:', error);
      }
    }
    dobaviArtikle();
  }, []);

  const handleAzurirajArtikal = async () => {
    console.log("Pokušaj ažuriranja artikla...");

    const formData = new FormData();
    formData.append('id', id);
    formData.append('naziv', naziv);
    formData.append('cijena', cijena);
    formData.append('kolicinaArtikla', kolicinaArtikla);
    formData.append('opis', opis);
    formData.append('slika', slika);

    if (id.trim() === 0 ||
      naziv.trim() === '' ||
      cijena.trim() === 0 ||
      kolicinaArtikla.trim() === 0 ||
      opis.trim() === '' ||
      slika === null) {
      setMessage('Sva polja su obavezna');
      return;
    }

    if (!validator.isAlpha(naziv)) {
      setMessage('Naziv može sadržavati samo slova.');
      return;
    }

    if (cijena < 0) {
      setMessage('Cijena mora biti pozitivan broj.');
    }

    if (id < 0) {
      setMessage('ID mora biti pozitivan broj.');
    }

    if (kolicinaArtikla < 0) {
      setMessage('Kolicina mora biti pozitivan broj.');
    }

    try {
      await AzurirajArtikal(id, formData);
      console.log("Artikal uspešno ažuriran.");
      setArtikal({
        ...id, naziv, cijena, kolicinaArtikla, opis, slika
      });
      setID('');
      setNaziv('');
      setCijena('');
      setKolicinaArtikla('');
      setOpis('');
      setSlika(null);
      azurirani();
    } catch (err) {
      console.log("Greška pri ažuriranju artikla:", err);
    }
  };

  return (
    <div className="form">
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
          </tr>
        </thead>
        <tbody>
          {artikli.map((art) => (
            <tr key={art.artikalId}>
              <td>{art.artikalId}</td>
              <td>{art.naziv}</td>
              <td>{art.cijena}</td>
              <td>{art.kolicinaArtikla}</td>
              <td>{art.opis}</td>
              <td>
                {art.slika && (
                  <img
                    src={`data:image/png;base64,${art.slika}`}
                    alt="Slika korisnika"
                    width={90}
                    height={90}
                  />
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
     


        <h2>Azuriranje artikla</h2>
     
        <div className='form'>
        <form>
        <div className='form'>
        <label>
            ID:
            <input
            type="number"
            value={id}
            onChange={(e) => setID(e.target.value)}
            />
        </label>

        </div>
        <div className='form'>
        <label>
            Naziv:
            <input
            type="text"
            value={naziv}
            onChange={(e) => setNaziv(e.target.value)}
            />
        </label>

        </div>
        <br />
        <div className='form'>
        <label>
            Cijena:
            <input
            type="text"
            value={cijena}
            onChange={(e) => setCijena(e.target.value)}
            />
        </label>

        </div>
        <br />
        <div className='form'>
        <label>
            Kolicina artikla:
            <input
            type="text"
            value={kolicinaArtikla}
            onChange={(e) => setKolicinaArtikla(e.target.value)}
            />
        </label>
        
        </div>
        <br />
        <div className='form'>
        <label>
            Opis:
            <input
            type="text"
            value={opis}
            onChange={(e) => setOpis(e.target.value)}
            />
        </label>
        </div>

        <br />
        <div className='form'>
        <label>
            Odaberite sliku:
            <input
            type="file"
            accept="image/*"
            onChange={(e) => setSlika(e.target.files[0])}
            />
        </label>
        </div>
        
        <button className='btn' type="button" onClick={handleAzurirajArtikal}>
            Ažuriraj artikal
        </button>
        </form>
        <div>
        <p>  {message && (
        <div>
          <p style={{ color: 'red' }}> Greska: {message}</p>
        </div>
          )}</p>

        </div>


    <h2>Obrisi artikal</h2>
      <input
        type="number"
        name="id"
        value={id}
        onChange={(e) => setID(e.target.value)}
      />
      <button className="btn" type="button" onClick={() => handleObrisi(id)}>
        Obriši
      </button>
      <div>
      <Link to="/dashBoard">Nazad</Link>
      </div>
</div>
</div>
);
};

export default AzuriranjeArtikla;