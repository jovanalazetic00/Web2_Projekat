import React from "react";
import {MojePorudzbine} from "../services/PorudzbinaService";
import { useState, useEffect } from "react";
import './Tabele.css';
import jwtDecode from "jwt-decode";
import { useNavigate, Link } from "react-router-dom";


const MojePorudzbineProd = () => {
    const [porudzbine, setPorudzbina] = useState([]);
    const navigate = useNavigate('');
    const token = localStorage.getItem('token');
    const dekodiranToken = jwtDecode(token);
    const id = dekodiranToken['IdKorisnika'];
    
      useEffect(() => {
        const get = async() => 
        {
            const response = await MojePorudzbine(id);
            console.log(response);
            setPorudzbina(response.data);
        }
        get();
      }, []);  

      const prikaziSveArtikleProdavac = (id) => 
      {  
        navigate(`/prikazArtikalaProdavac/${id}`);
      };
    

return (
  <div>
    <h2>Moje porudzbine</h2>
  <table>
    <thead>
      <tr>
          <th>ID Porudžbine:</th>
          <th>Adresa:</th>
          <th>Komentar:</th>
          <th>Cijena:</th>
          <th>Status porudzbine:</th>
          <th>Datum i vrijeme dostave:</th>
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
            {/* <td>
                  {porudzbina.slika && ( 
                      <img src={`data:image/png;base64,${porudzbina.slika}`} 
                       alt="Slika korisnika"
                       width={80}
                      height={80}
                  />)}
             </td>   */}
            <td>
              <button className="btn" onClick={() => prikaziSveArtikleProdavac(porudzbina.idPorudzbine)}>
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

export default MojePorudzbineProd;