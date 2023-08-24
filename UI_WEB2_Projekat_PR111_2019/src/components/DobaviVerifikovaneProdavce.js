import { SviProdavci, SviVerifikovaniProdavci } from "../services/ProdavciVerifikacijaService";
import { useState, useEffect } from "react";
import './Tabele.css';
import { Link } from "react-router-dom";


const DobaviVerifikovaneProdavce = () => {
    const [prodavac, setProdavac] = useState([]);

    useEffect(() => {
      const get = async() => 
      {
        try
        {
          const response = await SviVerifikovaniProdavci();
          console.log(response);
          setProdavac(response.data);
        }
        catch(err)
        {
          console.error(err);
        }
         
      }
      get();
    }, []);


    return (
        <div>
        <h2>Svi verifikovani prodavci</h2>
        <table>
          <thead>
            <tr>
              <th>ID prodavca:</th>
              <th>Ime:</th>
              <th>Prezime:</th>
              <th>Korisnicko ime:</th>
              <th>Email:</th>
              <th>Verifikovan:</th>
              <th>Status verifikacije:</th>
              <th>Datum rodjenja:</th>
              <th>Adresa:</th>
              <th>Slika:</th>
            </tr>
          </thead>
          <tbody>
            {prodavac.map((prod) => (
              <tr key={prod.idKorisnika}>
                <td> {prod.idKorisnika}</td>
                <td>{prod.ime}</td>
                <td>{prod.prezime}</td>
                <td>{prod.korisnickoIme}</td>
                <td>{prod.email}</td>
                <td>{prod.verifikovan ? "True" : "False"}</td>
                <td>
                {(() => {
                  switch (prod.statusVerifrikacije) {
                    case 0:
                      return "UObradi";
                    case 1:
                      return "Verifikovan";
                    case 2:
                      return "Odbijen";
                    default:
                      return "";
                  }
                })()}
              </td>
                <td>{prod.datumRodjenja}</td>
                <td>{prod.adresa}</td>
                <td>
                    {prod.slika && ( 
                      <img src={`data:image/png;base64,${prod.slika}`} 
                        alt="Slika korisnika"
                        width={80}
                        height={80}
                    />)} 
                </td>
              </tr>
            ))}
          </tbody>
        </table>
       <br/><br/>
        
        <label className='nazad' htmlFor="/dobaviProdavce"> <Link to="/dobaviProdavce">Nazad</Link> </label>
    
      </div>
    );
};

export default DobaviVerifikovaneProdavce;