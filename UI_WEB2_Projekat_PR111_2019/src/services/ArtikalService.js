import {axioss} from "../configuration/axios";

export const NoviArtikal = async(formData) =>{
    
    const response = await axioss.post(`/Artikal/dodajArtikal`, formData, 
    {headers: 
        {
          "Content-Type":"multipart/form-data",
        }
      });
    return response.data;
};


export const AzurirajArtikal = async(id, formData) => {

    return await axioss.put(`/Artikal/${id}`, formData,
    {headers: 
        {
          "Content-Type":"multipart/form-data",
        }
      }
    );
};


export const ObrisiArtikal = async(id) => {

    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    return await axioss.delete(`/Artikal/${id}`);
};


export const SviArtikli = async() => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    
    return await axioss.get(`/Artikal`);
};



export const SviArtikliProdavca = async(id) => {
    const token = localStorage.getItem("token");
    axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;

    
    return await axioss(`/Artikal/dobaviArtikleProdavca/${id}`);
};
