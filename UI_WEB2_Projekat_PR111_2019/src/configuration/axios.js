// axiosInstance.js
import axios from 'axios';

const axioss = axios.create({
  baseURL: process.env.REACT_APP_API_URL, // Promenite ovo prema vašoj putanji
});

export default axioss;
