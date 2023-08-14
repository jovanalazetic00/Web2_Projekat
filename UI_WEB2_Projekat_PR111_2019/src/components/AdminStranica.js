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

  console.log(uloga);
  
  const handleClickVerifikacija = () => {
      navigate('/dobaviProdavce');
  };


  const handleClickPorudzbine = () => {
    navigate('/porudzbina');
  };

  const handleClickProfil = () => {
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
    {}
   </>
);
};

export default AdminStranica;