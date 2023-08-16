import React, {useState} from "react";
import { AzurirajArtikal, NoviArtikal, ObrisiArtikal } from "../services/ArtikalService";
import './Tabele.css';
import { Link } from "react-router-dom";
import validator from "validator";


const DodajArtikal = () => {
    const [naziv, setNaziv] = useState('');
    const [cijena, setCijena] = useState(0);
    const [kolicinaArtikla, setKolicinaArtikla] = useState(0);
    const [opis, setOpis] = useState('');
    const [slika, setSlika] = useState(null);
    const [artikal, setArtikal] = useState([]);
    const[id, setID] = useState('');
    const[message, setMessage] = useState('');


  

    const handleSubmit = async(e) => {
      e.preventDefault();


      if (naziv.trim() === '' ||
      cijena.trim() === '' ||
      kolicinaArtikla.trim() === '' ||
      opis.trim() === '' ||
      slika === null) {
    
      setMessage('Sva polja su obavezna.');
      return;
    }


    if (!validator.isAlpha(naziv)) {
      setMessage('Naziv može sadržavati samo slova.');
      return;
    }

    if(cijena < 0)
    {
      setMessage('Cijena mora biti pozitivan broj.');
    }

    if(kolicinaArtikla < 0)
    {
       setMessage('Kolicina mora biti pozitivan broj.');
    }


      const formData = new FormData();
      formData.append('naziv', naziv);
      formData.append('cijena', cijena);
      formData.append('kolicinaArtikla', kolicinaArtikla);
      formData.append('opis', opis);
      formData.append('slika', slika);
       
      
    try{

      await NoviArtikal(formData);
        setNaziv('');
        setCijena(0);
        setKolicinaArtikla(0);
        setOpis('');
        setSlika(null);
    }
   catch(error)
   {
    console.error(error);
   }
  };


return (
    <div className='form'>
      <h2>Dodavanje artikla </h2>
      <form onSubmit={handleSubmit}>
      
      <label htmlFor="naziv">Naziv:</label>
      <input
        type="text"
        id="naziv"
        name="naziv"
        value={naziv}
        onChange={(e) => setNaziv(e.target.value)}
      />

      <label htmlFor="cijena">Cijena:</label>
      <input
        type="number"
        id="cijena"
        name="Cijena"
        value={cijena}
        onChange={(e) => setCijena(e.target.value)}
      />

     <label htmlFor="kolicinaArtikla">Kolicina:</label>
      <input
        type="number"
        id="kolicinaArtikla"
        name="kolicinaArtikla"
        value={kolicinaArtikla}
        onChange={(e) => setKolicinaArtikla(e.target.value)}
      />

      <label htmlFor="opis">Opis:</label>
      <input
        type="text"
        id="opis"
        name="opis"
        value={opis}
        onChange={(e) => setOpis(e.target.value)}
      />
     
      <div className='form'>
        <label> Odaberite sliku: </label>
        <input
          type="file"
          accept="image/*" 
          onChange={(e) => setSlika(e.target.files[0])}
      />
      </div> 

      <button className="btn" type="submit">Dodaj artikal</button>
    </form> <br/>
    <br/>


    <label className='nazad' htmlFor="/dashBoard"> <Link to="/dashBoard">Dash Board</Link> </label>
     <p>{message && (
        <div>
          <p style={{ color: 'red' }}> Greska: {message}</p>
        </div>
      )}</p>
</div>
  );
};

export default DodajArtikal;

