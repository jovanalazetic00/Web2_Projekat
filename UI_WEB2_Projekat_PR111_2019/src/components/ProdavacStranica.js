import React from "react";
import { useNavigate } from "react-router-dom";
import NavBar from "./NavBar";
import { Button } from "@mui/material";
import jwtDecode from "jwt-decode";
import AzuriranjeArtikla from "./AzuriranjeArtikla";

const ProdavacStranica = () => {
  const navigate = useNavigate();
  const token = localStorage.getItem('token');
  const [prikaziAzuriranjeArtikla, setPrikaziAzuriranjeArtikla] = React.useState(false);

  const dekodiranToken = jwtDecode(token);
  const id = dekodiranToken['IdKorisnika'];
  const uloga = dekodiranToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  
  const handleClickDodajArtikal = () => {
    navigate('/dodajArtikal');
  };


  const handleClickNovePorudzbine = () => {
    navigate(`/novePorudzbine/${id}`);
  };
  

  const handleClickAzurirajArtikal = () => {
    navigate(`/azuriranjeArtikla/${id}`);
  };


  const handleClickProfil = () => {
    navigate(`/profil/${id}`);
  };

  const handleClickMojePorudzbine = () => {
    navigate(`/mojePorudzbine/${id}`);
  };



return (
  <>
    <NavBar>
      <Button
        color="inherit"
        style={{ backgroundColor: '#d3d3d3' }}
        onClick={handleClickProfil}
      >
        Profil 
      </Button>
      <Button
        color="inherit"
        style={{ backgroundColor: '#d3d3d3' }}
        onClick={handleClickDodajArtikal}
      >
        Dodaj artikal
      </Button>
      
      <Button
        color="inherit"
        style={{ backgroundColor: '#d3d3d3' }}
        onClick={handleClickAzurirajArtikal}
      >
        Azuriraj artikal
      </Button>

      <Button
        color="inherit"
        style={{ backgroundColor: '#d3d3d3' }}
        onClick={handleClickNovePorudzbine}
      >
        Nove porudzbine
      </Button>

      <Button
        color="inherit"
        style={{ backgroundColor: '#d3d3d3' }}
        onClick={handleClickMojePorudzbine}
      >
        Moje porudzbine
      </Button>
    </NavBar>

    
  </>
);
};

export default ProdavacStranica;

