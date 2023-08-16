import React, { useContext } from "react";
import { useState, useEffect } from "react";
import jwtDecode from "jwt-decode";
import { useNavigate } from "react-router-dom";
import AdminStranica from "./AdminStranica";
import KupacStranica from "./KupacStranica";
import ProdavacStranica from "./ProdavacStranica";
import { DobaviKorisnikaPoId } from "../services/KorisnikService";
import NavBar from "./NavBar";
import { Button } from "@mui/material";
import { Link } from "react-router-dom";

const DashBoard = () => {
  const navigate = useNavigate();
  const [korisnik, setKorisnik] = useState('');

  const token = localStorage.getItem('token');
  console.log('Dekodirani token:', token);

  const dekodiranToken = jwtDecode(token);
  const uloga = dekodiranToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  const id = dekodiranToken['IdKorisnika'];
  console.log('Uloga:', uloga);
  console.log('ID korisnika:', id);

  useEffect(() => {
    const dobavljenKorisnik = async() => {

      try{
        const resp = await DobaviKorisnikaPoId(id);
        console.log('Odgovor od API-ja za dobavljanje korisnika:', resp);

        setKorisnik(resp.data);
      }
      catch(er)
      {
        console.log('Greška prilikom dobavljanja korisnika:', er);
      }
    };

    if(uloga === 'Prodavac')
    {
      console.log('Pozivanje funkcije za dobavljanje korisnika');
      dobavljenKorisnik();
    }

  }, [id, uloga] );

  console.log('Prikazuje se komponenta Dashboard');
  console.log('Trenutna uloga:', uloga);
  console.log('Korisnik:', korisnik);



 return (
      <div>
        {uloga === 'Administrator' && (
            <AdminStranica></AdminStranica>
        )}
       
       {uloga === 'Kupac' && (
             <KupacStranica></KupacStranica>
        )}

      {uloga === 'Prodavac' && korisnik && korisnik.statusVerifrikacije === 1 && korisnik.verifikovan === true &&(
          <ProdavacStranica></ProdavacStranica>
        )}
     

      {uloga === 'Prodavac' && korisnik.statusVerifrikacije !== 1  && korisnik.verifikovan === false &&(
          navigate(`/profil/${id}`)
        )}
   
      </div>
 
  );

};

export default DashBoard;