import React, { useState} from 'react';
import './Tabele.css'
import { useParams, Link } from 'react-router-dom';
import { SviArtikliPorudzbineProdavac } from '../services/PorudzbinaService';
import { useEffect } from 'react';

const ArtikliPorudzbineProdavac = () => {
    const[artikli, setArtikli] = useState([]);
    const { id } = useParams();
    
    console.log(id);
    

    useEffect(() => {
        const get = async() => 
        {
          const response = await SviArtikliPorudzbineProdavac(id);
          console.log(response.data);
        
          setArtikli(response.data);
          console.log(artikli);
          
        }
        get();
    }, [id]);


return (
    <div>
    <table>
      <thead>
        <tr>
          <th>ID:</th>
          <th>Naziv</th>
          <th>Cijena</th>
          <th>Količina artikla</th>
          <th>Opis</th>
          <th>Slika</th>
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
                 width={80}
                 height={80}
              />)}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
    <div>
    <Link to={`/mojePorudzbine/${id}`} style={{ color: 'white' }}>
      Moje porudžbine
    </Link>
  </div>
  <div>
    <Link to={`/novePorudzbine/${id}`} style={{ color: 'white' }}>
      Nove porudžbine
    </Link>
  </div>
    </div>
  );
};

export default ArtikliPorudzbineProdavac;
