export class PorudzbinaModel {
    constructor(adresaIsporuke, komentarPorudzbine, cijenaPorudzbine, vrijemeIsporuke, vrijemePorudzbine)
    {
        rhis.cijenaPorudzbine = cijenaPorudzbine;
        this.komentarPorudzbine = komentarPorudzbine;
        this.adresaIsporuke= adresaIsporuke;
        this.vrijemeIsporuke = vrijemeIsporuke;
        this.vrijemePorudzbine = vrijemePorudzbine;
    }
}

export default PorudzbinaModel;