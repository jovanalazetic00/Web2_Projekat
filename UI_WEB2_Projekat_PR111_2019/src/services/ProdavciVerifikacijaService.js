import {axioss} from "../configuration/axios";


export const SviProdavci = async() => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    return await axioss.get(`/Korisnik/dobaviProdavce`);
};


export const SviVerifikovaniProdavci = async() => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    
    return await axioss.get(`/Korisnik/dobaviVerifikovaneProdavce`);
};



export const OdobriVerifikacijuProdavca = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    
    return await axioss.post(`/Korisnik/verifikacijaProdavca/${id}`);
};


export const OdbijVerifikacijuProdavca = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    
    return await axioss.post(`/Korisnik/odbijVerifikaciju/${id}`);
};



