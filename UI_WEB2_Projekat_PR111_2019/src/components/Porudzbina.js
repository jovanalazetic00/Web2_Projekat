import React from "react";
import { SvePorudzbine } from "../services/PorudzbinaService";
import { useState, useEffect } from "react";
import './Tabele.css';
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";


const Porudzbina = () => {
      const [porudzbine, setPorudzbina] = useState([]);
      const navigate = useNavigate('');


      useEffect(() => {
        const get = async() => 
        {
          const response = await SvePorudzbine();
          console.log(response);
          setPorudzbina(response.data);
        }
        get();
      }, []);  


      const prikaziSveArtikle = (id) => 
      {  
        navigate(`/prikazArtikala/${id}`);
      };


  return (
    <div>
    <table>
      <thead>
        <tr>
          <th>ID Porudžbine:</th>
          <th>Adresa isporuke:</th>
          <th>Komentar porudžbine:</th>
          <th>Cijena porudžbine:</th>
          <th>Status porudzbine:</th>
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
                <button className='btn' onClick={() => prikaziSveArtikle(porudzbina.idPorudzbine)}>
                  OPSIRNIJE
                </button>
              </td>
          </tr>
        ))}
      </tbody>
    </table>
    <Link to="/dashBoard"> Dash Board</Link>
    </div>
  );
};

export default Porudzbina;