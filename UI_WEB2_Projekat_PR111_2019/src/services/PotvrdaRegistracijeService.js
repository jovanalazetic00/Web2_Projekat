import {axioss} from "../configuration/axios"


export const PotvrdaRegistracije = async(id) => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;


    return await axioss.post(`/Korisnik/potvrdaRegistracije/${id}`);
};

export const OdbijRegistraciju = async(id) => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    return await axioss.post(`/Korisnik/odbijRegistraciju/${id}`);
};