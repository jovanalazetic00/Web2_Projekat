import { axioss } from '../configuration/axios';



export const RegistracijaService = async (formData) => {
  const response = await axioss.post(`/Registracija/registracija`, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data;
};