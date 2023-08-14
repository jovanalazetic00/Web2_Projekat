import {axioss} from "../configuration/axios";

export const MojProfil = async(id) => {
  const token = localStorage.getItem("token");
  axioss.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  return await axioss.get(`/Korisnik/${id}/profil`);
};


export const AzurirajProfilKorisinika = async(id, formData) => {

  const resp =  await axioss.put(`/Korisnik/${id}`, formData, 
  {headers: 
      {
        "Content-Type":"multipart/form-data",
      },
    }
  );
  return resp.data;
};
