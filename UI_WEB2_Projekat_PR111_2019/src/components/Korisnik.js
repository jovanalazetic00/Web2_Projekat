import React, { useEffect, useState } from "react";
import { SviKorisnici, ObrisiKorisnika } from "../services/KorisnikService";
import './Tabele.css'

const Korisnik = () => {
  const [korisnici, setKorisnici] = useState([]);
  const [id, setID] = useState('');

  useEffect(() => {
    const get = async () => {
      try {
        const response = await SviKorisnici();
        console.log("Dobavljeni korisnici:", response.data);
        setKorisnici(response.data);
      } catch (error) {
        console.log("Greška pri dobavljanju korisnika:", error);
      }
    }
    get();
  }, []);

  const handleObrisi = async (id) => {
    try {
      console.log("Brisanje korisnika sa ID:", id);
      await ObrisiKorisnika(id);
      const ostaliKorisnici = korisnici.filter((korisnik) => korisnik.idKorisnika !== id);
      setKorisnici(ostaliKorisnici);

      setID('');
    } catch (error) {
      console.log("Greška pri brisanju korisnika:", error);
    }
  };

  return (
    <div className="form">
     
      <h1>Korisnici</h1>
      <table>
        <thead className="form">
          <tr>
            <th>KorisnikID:</th>
            <th>Ime:</th>
            <th>Prezime:</th>
            <th>Korisničko ime:</th>
            <th>Datum rođenja:</th>
            <th>Adresa:</th>
            <th>Tip korisnika:</th>
          </tr>
        </thead>
        <tbody className="form">
          {korisnici.map((korisnik) => (
            <tr key={korisnik.idKorisnika}>
              <td>{korisnik.idKorisnika}</td>
              <td>{korisnik.ime}</td>
              <td>{korisnik.prezime}</td>
              <td>{korisnik.korisnickoIme}</td>
              <td>{korisnik.datumRodjenja}</td>
              <td>{korisnik.adresa}</td>
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
