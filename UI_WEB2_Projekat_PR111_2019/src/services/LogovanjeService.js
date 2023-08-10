import axios from 'axios';
import { axioss } from '../configuration/axios';
const LOGIN_URL = '/Registracija/logovanje';


export const LogovanjeServis = async (email,lozinka) => {

  try {
    const  data  = await axios.post(
      `${process.env.REACT_APP_API_URL}${LOGIN_URL}`,
      JSON.stringify({ email, lozinka }),
      {
        headers: { "Content-Type": "application/json" },
       
      }
   
    );
   console.log(data);
    return data.data;
    
  } catch (err) {
    alert("Greska prilikom logovanja");
    return null;
  }
};


export const setHeader = (token) =>
{

  if(token) 
  {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;


  }  else{
    delete axios.defaults.headers.common['Authorization'];
  }
};





export const ProvjeriMail = async(email) => {
 
   const resp =  await axioss.get(`/Autentifikacija/provjeraMaila/${email}`);
   return resp.data;
};
