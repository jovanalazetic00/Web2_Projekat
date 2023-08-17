import React, { useState} from 'react';
import './Tabele.css'
import { useParams, Link } from 'react-router-dom';
import { SviArtikliPorudzbine } from '../services/PorudzbinaService';
import { useEffect } from 'react';
import { AxiosError } from 'axios';

const ArtikliPorudzbine = () => {
    const[artikli, setArtikli] = useState([]);
    const { id } = useParams();
    
    console.log(id);
    

    useEffect(() => {
        const get = async() => 
        {
          const response = await SviArtikliPorudzbine(id);
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
              width={90}
              height={90}
         />)}
        </td>
          </tr>
        ))}
      </tbody>
    </table>
    <Link to="/porudzbina"> Nazad</Link>
    </div>
  );
};

export default ArtikliPorudzbine;