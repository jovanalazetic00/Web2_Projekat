using AutoMapper;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Mapper
{
    public class MapDTO:Profile
    {
        public MapDTO()
        {
            CreateMap<Korisnik, DTOKorisnik>().ReverseMap();
        }
    }
}
