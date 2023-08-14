import React from "react";
import { useNavigate } from "react-router-dom";
import NavBar from "./NavBar";
import { Button } from "@mui/material";
import jwtDecode from "jwt-decode";



const KupacStranica = () => {
  
  const navigate = useNavigate();
  const token = localStorage.getItem('token');

 
    const dekodiranToken = jwtDecode(token);
    const id = dekodiranToken['IdKorisnika'];
    console.log(dekodiranToken);
    console.log(id);
 


    const handleClickProfil = () => {
      navigate(`/profil/${id}`);
    };

    const handleClickNovaPorudzbina = () => {
      navigate(`/dodajPorudzbinu`);
    };

    const handleClickPrethodnaPorudzbina = () => {
      navigate(`/prethodnePorudzbine/${id}`);
    };


return (
  <>
     <NavBar >
      <Button
        color="inherit"
        style = {{backgroundColor: '#d3d3d3'}}
        onClick={handleClickProfil}
      >
        Profil 
      </Button>

      <Button
        color="inherit"
        style = {{backgroundColor: '#d3d3d3'}}
        onClick={handleClickNovaPorudzbina}
      >
          Nova porudzbina
      </Button>

      <Button
        color="inherit"
        style = {{backgroundColor: '#d3d3d3'}}
        onClick={handleClickPrethodnaPorudzbina}
      >
          Prethodne porudzbine
      </Button>
      
      </NavBar>
    {}
  </>
);


};

export default KupacStranica;