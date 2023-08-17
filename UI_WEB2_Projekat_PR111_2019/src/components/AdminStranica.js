import React from "react"
import { useNavigate } from "react-router-dom";
import NavBar  from "./NavBar";
import {Button} from "@mui/material";
import jwtDecode from "jwt-decode";

const AdminStranica = () => {
  
  const navigate = useNavigate();
  const token = localStorage.getItem('token');

  const dekodiranToken = jwtDecode(token);
  const id = dekodiranToken['IdKorisnika'];
  const uloga = dekodiranToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

  console.log("Uloga:", uloga);
  console.log("ID korisnika:", id);
  
  const handleClickVerifikacija = () => {
    console.log("Kliknuto na Verifikacija");
    navigate('/dobaviProdavce');
  };

  const handleClickPorudzbine = () => {
    console.log("Kliknuto na Sve porudzbine");
    navigate('/porudzbina');
  };

  const handleClickProfil = () => {
    console.log("Kliknuto na Profil");
    navigate(`/profil/${id}`);
  };

  return (
    <>
      <NavBar >
        <Button
          color="inherit"
          style = {{backgroundColor: '#d3d3d3'}}
          onClick={handleClickProfil}>
          Profil 
        </Button>

        <Button
          color="inherit"
          style = {{backgroundColor: '#d3d3d3'}}
          onClick={handleClickVerifikacija}>
          Verifikacija
        </Button>

        <Button
          color="inherit"
          style = {{backgroundColor: '#d3d3d3'}}
          onClick={handleClickPorudzbine}>
          Sve porudzbine 
        </Button>
      </NavBar>
    </>
  );
};

export default AdminStranica;
