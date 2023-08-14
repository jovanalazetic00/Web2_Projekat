import React, {useEffect, useState} from "react";
import { SviKorisnici, ObrisiKorisnika } from "../services/KorisnikService";
import './Tabele.css'


const Korisnik = () => {
  const [korisnici, setKorisnici] = useState([]);
  const [id, setID] = useState('');

  useEffect(() => {
    const get = async() => 
    {
        const response = await SviKorisnici();
        console.log(response);
        setKorisnici(response.data);
    }
    get();
  }, []);

  const handleObrisi = async (id) => {
    try {
      await ObrisiKorisnika(id);
      const ostaliKorisnici = korisnici.filter((korisnik) => korisnik.IdKorisnika !== id);
      setKorisnici(ostaliKorisnici);

      setID('');
    } catch (error) {
      console.log(error);
    }
  };
  

  return (
    <div className="form">
     
      <h1>Korisnici</h1>
      <table>
        <thead className="form">
          <tr>
            <th>KorisnikID:</th>
            <th>Korisničko ime:</th>
            <th>Ime:</th>
            <th>Prezime:</th>
            <th>Adresa:</th>
            <th>Datum rođenja:</th>
            <th>Tip korisnika:</th>
          </tr>
        </thead>
        <tbody className="form">
          {korisnici.map((korisnik) => (
            <tr key={korisnik.idKorisnika}>
              <td>{korisnik.idKorisnika}</td>
              <td>{korisnik.korisnickoIme}</td>
              <td>{korisnik.ime}</td>
              <td>{korisnik.prezime}</td>
              <td>{korisnik.adresa}</td>
              <td>{korisnik.datumRodjenja}</td>
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
          ))}
        </tbody>
      </table>

      <h2>Obrisi korisnika</h2>
      <input
        type="number"
        name="id"
        value={id}
        onChange={(e) => setID(e.target.value)}
      />
      <button className="btn" type="button" onClick={() => handleObrisi(id)}>
        Obriši
      </button>
    </div>
  );
};

export default Korisnik;
