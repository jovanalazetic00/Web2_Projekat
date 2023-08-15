import {axioss} from "../configuration/axios";

export const SvePorudzbine = async() => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    return await axioss.get(`/Porudzbina`);
};


export const NovaPorudzbina = async(reqData) =>{

    const response = await axioss.post(`/Porudzbina/dodajPorudzbinu`, reqData);
    return response.data;
};


export const PrethodnePorudzbineKupca = async(id) => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    return await axioss.get(`/Porudzbina/prethodnePorudzbine/${id}`);
};


export const OtkaziPorudzbinu = async(id) => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    return await axioss.put(`/Porudzbina/otkaziPorudzbinu/${id}`);
};


export const DobaviPorudzbinuPoID = async(id) => {
     const token = localStorage.getItem("token");
     axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    return await axioss.get(`/Porudzbina/${id}`);
};


export const SvePorudzbineKupca = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    
    return await axioss.get(`/Porudzbina/porudzbineKupca/${id}`);
};


export const NovePorudzbine = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    
    return await axioss.get(`/Porudzbina/novePorudzbineProdavca/${id}`);
};



export const MojePorudzbine = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    
    return await axioss.get(`/Porudzbina/mojePorudzbineProdavac/${id}`);
};



export const SviArtikliPorudzbine = async(id) => {
    
    const response =  await axioss.get(`/Porudzbina/dobaviArtiklePorudzbine/${id}`);
    return response;
};




export const SviArtikliPorudzbineProdavac = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    const response =  await axioss.get(`/Porudzbina/dobaviArtiklePorudzbineProdavca/${id}`);
    return response;
};