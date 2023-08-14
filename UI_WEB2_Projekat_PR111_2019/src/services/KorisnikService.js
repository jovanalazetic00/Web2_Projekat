import {axioss} from "../configuration/axios";

export const SviKorisnici = async () => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  
    return await axioss.get(`/Korisnik`);
};


export const ObrisiKorisnika = async(id) => {
    return await axioss.delete(`/Korisnik/${id}`);
};


export const DobaviKorisnikaPoId = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    return await axioss.get(`/Korisnik/${id}`);
};

