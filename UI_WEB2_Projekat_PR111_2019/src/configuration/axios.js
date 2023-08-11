import axios from 'axios';

export  const axioss = axios.create({
    baseURL :  `${process.env.REACT_APP_API_URL}`
});