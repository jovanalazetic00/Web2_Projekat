using System.Text.Json.Serialization;

namespace Web_Projekat_PR111_2019.Models
{
    public class ArtikalIPorudzbina
    {
        private Porudzbina porudzbina;
        private Artikal artikal;
        public int IDPorudzbineAIP { get; set; }
        public int IDArtiklaAIP { get; set; }
        public int KolicinaArtikla { get; set; }

        [JsonIgnore]
        public Porudzbina Porudzbina { get => porudzbina; set => porudzbina = value; }
        [JsonIgnore]
        public Artikal Artikal { get => artikal; set => artikal = value; }
    }
}
