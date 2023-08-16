import React from "react";
import { PrethodnePorudzbineKupca, SvePorudzbine, SvePorudzbineKupca } from "../services/PorudzbinaService";
import { useState, useEffect } from "react";
import './Tabele.css';
import jwtDecode from "jwt-decode";
import { OtkaziPorudzbinu } from "../services/PorudzbinaService";
import { useNavigate, Link } from "react-router-dom";

const PrethodnePorudzbine = () => {
    const [porudzbine, setPorudzbina] = useState([]);
    const[svePorudzbine, setSvePorudzbine] = useState([]);
    const[message, setMessage] = useState('');
     
    const token = localStorage.getItem('token');
    const dekodiranToken = jwtDecode(token);
    const id = dekodiranToken['IdKorisnika'];
    const navigate = useNavigate('');


    useEffect(() => {
      const get = async() => 
      {
        const response = await PrethodnePorudzbineKupca(id);
        console.log(response);
        setPorudzbina(response.data);
      }
      get();
    }, []);


    useEffect(() => {
      const svePorKupca = async() => 
        {
          const response = await SvePorudzbineKupca(id);
          console.log(response);
          setSvePorudzbine(response.data);
        }
        svePorKupca();
    }, []);



    const osvjeziPorudzbine = async() => 
    {
      const response = await SvePorudzbineKupca(id);
      console.log(response);
      setSvePorudzbine(response.data);
    };



      const otkaziPorudzbinu = async (idPorudzbine) => {
        try {
          await OtkaziPorudzbinu(idPorudzbine);
          setMessage("Uspjesno");
          osvjeziPorudzbine();
        } catch (error) {

          console.log("Greška pri otkazivanju porudžbine:", error);
          setMessage(error.response.data);
        }
      };

    const prikaziSveArtikle = (id) => 
    {  
      navigate(`/prikazArtikalaKupac/${id}`);
    };
        

return (
<div>
<h2> Sve porudžbine kupca</h2>
  <table>
    <thead>
      <tr>
        <th>ID Porudžbine:</th>
        <th>Adresa:</th>
        <th>Komentar:</th>
        <th>Cijena:</th>
        <th>Status porudzbine:</th>
        <th>Datum i vrijeme dostave:</th>
        <th> Vrijeme porudzbine</th>
      </tr>
    </thead>
    <tbody>
      {svePorudzbine.map((sve) => (
        <tr key={sve.idPorudzbine}>
          <td>{sve.idPorudzbine}</td>
          <td>{sve.adresaIsporuke}</td>
          <td>{sve.komentarPorudzbine}</td>
          <td>{sve.cijenaPorudzbine}</td>
          <td>
              {(() => {
                  switch (sve.statusPorudzbine) {
                  case 0:
                      return "UObradi";
                  case 1:
                      return "Odbijena,";
                  case 2:
                      return "Prihvacena";
                  case 3:
                    return "Otkazana";
                  default:
                      return "";
                  }
              })()}
            </td>
          
          <td>{sve.vrijemeIsporuke}</td>
          
            <td>{sve.vrijemePorudzbine}</td>
            <td>
            <button className="btn" onClick={() => otkaziPorudzbinu(sve.idPorudzbine)}>Otkazi</button>
            </td>
            <td>
              <button className="btn" onClick={() => prikaziSveArtikle(sve.idPorudzbine)}>
                OPSIRNIJE
              </button>
            </td>
        </tr>
      ))}
    </tbody>
  </table>
  {message && (
    <div>
      <p style={{ color: 'blue' }}>{message}</p>
     </div>
  )}


  <h2> Prethodne porudzbine kupca</h2>
  <table>
    <thead>
      <tr>
        <th>ID Porudžbine:</th>
        <th>Adresa:</th>
        <th>Komentar:</th>
        <th>Cijena:</th>
        <th>Status porudzbine:</th>
        <th>Datum i vrijeme dostave:</th>
        <th>Vrijeme porudzbine</th>
      </tr>
    </thead>
    <tbody>
    {porudzbine.map((porudzbina) => (
        <tr key={porudzbina.idPorudzbine}>
          <td>{porudzbina.idPorudzbine}</td>
          <td>{porudzbina.adresaIsporuke}</td>
          <td>{porudzbina.komentarPorudzbine}</td>
          <td>{porudzbina.cijenaPorudzbine}</td>
          <td>
              {(() => {
                  switch (porudzbina.statusPorudzbine) {
                  case 0:
                      return "UObradi";
                  case 1:
                      return "Odbijena,";
                  case 2:
                      return "Prihvacena";
                  case 3:
                    return "Otkazana";
                  default:
                      return "";
                  }
              })()}
            </td>
          
          <td>{porudzbina.vrijemeIsporuke}</td>
          
            <td>{porudzbina.vrijemePorudzbine}</td>
            <td>
              <button className="btn" onClick={() => prikaziSveArtikle(porudzbina.idPorudzbine)}>
                OPSIRNIJE
              </button>
            </td>
        </tr>
      ))}
    </tbody>
  </table>
 
  <label className='nazad' htmlFor="/dashBoard"> <Link to="/dashBoard">Dash Board</Link> </label>
  </div>
);
};

export default PrethodnePorudzbine;
