import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import jwtDecode from "jwt-decode";
import AdminStranica from "./AdminStranica";
import KupacStranica from "./KupacStranica";
import ProdavacStranica from "./ProdavacStranica";
import { DobaviKorisnikaPoId } from "../services/KorisnikService";

const DashBoard = () => {
  const navigate = useNavigate();
  const [korisnik, setKorisnik] = useState('');

  const token = localStorage.getItem('token');

  const dekodiranToken = jwtDecode(token);
  const uloga = dekodiranToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  const id = dekodiranToken['IdKorisnika'];
  console.log("Uloga:", uloga);
  console.log("ID korisnika:", id);

  useEffect(() => {
    const dobavljenKorisnik = async () => {
      try {
        const resp = await DobaviKorisnikaPoId(id);
        console.log("Odgovor od API-ja za dobavljanje korisnika:", resp.data);

        setKorisnik(resp.data);
      } catch (error) {
        console.error("Greška prilikom dobavljanja korisnika:", error);
      }
    };

    if (uloga === "Prodavac") {
      console.log("Pozivanje funkcije za dobavljanje korisnika");
      dobavljenKorisnik();
    }
  }, [id, uloga]);

  return (
    <div>
      {uloga === 'Administrator' && (
        <AdminStranica></AdminStranica>
      )}

      {uloga === 'Kupac' && (
        <KupacStranica></KupacStranica>
      )}

      {uloga === 'Prodavac' && korisnik && korisnik.statusVerifikacije === 1 && korisnik.verifikovan === true && (
        <ProdavacStranica></ProdavacStranica>
      )}

      {uloga === 'Prodavac' && korisnik.statusVerifikacije !== 1 && korisnik.verifikovan === false && (
        navigate(`/profil/${id}`)
      )}
    </div>
  );
};

export default DashBoard;
