﻿namespace Web_Projekat_PR111_2019.Models
{
    public class ArtikalIPorudzbina
    {
        public Porudzbina Porudzbina { get; set; }
        public Artikal Artikal { get; set; }
        public int IDPorudzbine { get; set; }
        public int IDArtikla { get; set; }
        public int KolicinaArtikla { get; set; }
       
    }
}
