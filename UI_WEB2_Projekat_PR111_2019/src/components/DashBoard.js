import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import jwtDecode from "jwt-decode";
import AdminStranica from "./AdminStranica";
import KupacStranica from "./KupacStranica";
import ProdavacStranica from "./ProdavacStranica";
import { DobaviKorisnikaPoId } from "../services/KorisnikService";
import NavBar from "./NavBar";
import { Button } from "@mui/material";
import { Link } from "react-router-dom";

const DashBoard = () => {
  const navigate = useNavigate();
  const [korisnik, setKorisnik] = useState(null);

  const token = localStorage.getItem("token");

  let uloga = "";
  let id = "";

  try {
    const dekodiranToken = jwtDecode(token);
    uloga = dekodiranToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
    id = dekodiranToken["IdKorisnika"];
    console.log("Uloga:", uloga);
    console.log("ID korisnika:", id);
  } catch (error) {
    console.error("Greška prilikom dekodiranja tokena:", error);
  }

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

    if (uloga === "Prodavac" && id) {
      console.log("Pozivanje funkcije za dobavljanje korisnika");
      dobavljenKorisnik();
    }
  }, [id, uloga]);

  console.log("Prikazuje se komponenta Dashboard");
  console.log("Trenutna uloga:", uloga);
  console.log("Korisnik:", korisnik);

  return (
    <div>
      <h1>Dashboard</h1>
      {uloga === "Administrator" && <AdminStranica />}
      {uloga === "Kupac" && <KupacStranica />}
      {uloga === "Prodavac" && korisnik && korisnik.statusVerifrikacije === 1 && korisnik.verifikovan === true && (
        <ProdavacStranica />
      )}
      {uloga === "Prodavac" && korisnik && korisnik.statusVerifrikacije !== 1 && korisnik.verifikovan === false && (
        navigate(`/profil/${id}`)
      )}
    </div>
  );
};

export default DashBoard;
