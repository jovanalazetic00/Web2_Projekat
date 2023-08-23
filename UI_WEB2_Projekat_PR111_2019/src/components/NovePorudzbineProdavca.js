import React from "react";
import {NovePorudzbine} from "../services/PorudzbinaService";
import { useState, useEffect } from "react";
import './Tabele.css'
import jwtDecode from "jwt-decode";
import { useNavigate } from "react-router-dom";
import { Link } from 'react-router-dom';

const NovePorudzbineProdavca = () => {
    const [porudzbine, setPorudzbina] = useState([]);
    const navigate = useNavigate('');
    const token = localStorage.getItem('token');
    const dekodiranToken = jwtDecode(token);
    const id = dekodiranToken['IdKorisnika'];
    
      useEffect(() => {
        const get = async() => 
        {
            const response = await NovePorudzbine(id);
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
    <h2>Nove porudzbine</h2>
  <table>
    <thead>
        <tr>
          <th>ID porudžbine:</th>
          <th>Adresa isporuke:</th>
          <th>Komentar porudžbine:</th>
          <th>Cijena porudžbine:</th>
          <th>Status porudžbine:</th>
          <th>Vrijeme isporuke:</th>
          <th></th>
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
            <td>
              <button className="btn" onClick={() => prikaziSveArtikleProdavac(porudzbina.idPorudzbine)}>
               DETALJNIJE
              </button>
            </td>
        </tr>
      ))}
    </tbody>
  </table>

   <Link to="/dashBoard">Dash Board</Link> 
  </div>
);
};

export default NovePorudzbineProdavca;