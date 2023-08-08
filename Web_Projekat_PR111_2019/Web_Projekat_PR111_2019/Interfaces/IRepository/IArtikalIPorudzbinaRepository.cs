using System.Net.NetworkInformation;
using Web_Projekat_PR111_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Projekat_PR111_2019.Interfaces.IRepository
{
    public interface IArtikalIPorudzbinaRepository
    {
        Task<List<ArtikalIPorudzbina>> DobaviStavkePorudzbine(int idPorudzbine);
    }
}
