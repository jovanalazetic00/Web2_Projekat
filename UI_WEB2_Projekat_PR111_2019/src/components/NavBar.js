import React, {useContext} from 'react';
import { useNavigate } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Button from '@mui/material/Button';
import AuthContext from '../configuration/AuthProvider';
import { AuthProvider } from '../configuration/AuthProvider';

export default function NavBar(props) {
  const navigate = useNavigate();
  const authContext = useContext(AuthContext);

  const handleHomeClick = () => {
    navigate('/');
  };

 

  const handleLogoutClick = () => {
        
    localStorage.removeItem('token');
    navigate('/');
  
  };
  return (
    <AppBar position="static" sx={{ backgroundColor: '#d3d3d3' }}>
      <Toolbar sx={{ justifyContent: 'space-between' }}>
        <Button color="inherit" onClick={handleHomeClick}>
          Home
        </Button>
        {props.children}
        <Button color="inherit" onClick={handleLogoutClick}>
          Logout
        </Button>
        
      </Toolbar>
    </AppBar>
  );
}
