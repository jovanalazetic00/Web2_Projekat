export class RegistracijaModel  {
    constructor(ime, prezime, korisnickoIme, email, lozinka, potvrdaLozinke, datumRodjenja, adresa, tipKorisnika, slika) {
     this.ime = ime;
     this.prezime = prezime;
     this.korisnickoIme = korisnickoIme;
     this.email = email;
     this.lozinka = lozinka;
     this.potvrdaLozinke = potvrdaLozinke;
     this.datumRodjenja = datumRodjenja;
     this.adresa = adresa;
     this.tipKorisnika = tipKorisnika;
     this.slika = slika;
    }
 }

 export default RegistracijaModel;