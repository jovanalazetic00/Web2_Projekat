import { OdbijVerifikacijuProdavca, OdobriVerifikacijuProdavca, SviProdavci, SviVerifikovaniProdavci } from "../services/ProdavciVerifikacijaService";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";


const ProdavciVerifikacija = () => {
    const [prodavac, setProdavac] = useState([]);
    const [id, setID] = useState([]);
    const [idd, setIDD] = useState([]);

    useEffect(() => {
        const get = async () => {
            try {
                const response = await SviProdavci();
                console.log("Svi prodavci response:", response);
                setProdavac(response.data);
            } catch (error) {
                console.error("Greška prilikom dobavljanja svih prodavaca:", error);
            }
        };
        get();
    }, []);

    const azurirani = async () => {
        try {
            const response = await SviProdavci();
            console.log("Ažurirani prodavci response:", response);
            setProdavac(response.data);
        } catch (error) {
            console.error("Greška prilikom ažuriranja prodavaca:", error);
        }
    };

    const odobriVerifikaciju = async (id) => {
        try {
            await OdobriVerifikacijuProdavca(id);
            console.log("Odobrena verifikacija za prodavca sa ID:", id);
            setID('');
            azurirani();
        } catch (error) {
            console.error("Greška prilikom odobravanja verifikacije:", error);
        }
    };

    const odbijVerifikaciju = async (idd) => {
        try {
            await OdbijVerifikacijuProdavca(idd);
            console.log("Odbačena verifikacija za prodavca sa ID:", idd);
            setIDD('');
            azurirani();
        } catch (error) {
            console.error("Greška prilikom odbijanja verifikacije:", error);
        }
    }; 

    
return (
        <div>
        <h2>Svi prodavci</h2>
        <table>
          <thead>
            <tr>
              <th>ID prodavca:</th>
              <th>Ime:</th>
              <th>Prezime:</th>
              <th>Korisnicko ime:</th>
              <th>Email:</th>
              <th>Verifikovan:</th>
              <th>Status verifikacije:</th>
              <th>Datum rodjenja:</th>
              <th>Adresa:</th>
              <th>Slika:</th>
            </tr>
          </thead>
          <tbody>
            {prodavac.map((prod) => (
              <tr key={prod.idKorisnika}>
                <td>{prod.idKorisnika}</td>
                <td>{prod.ime}</td>
                <td>{prod.prezime}</td>
                <td>{prod.korisnickoIme}</td>
                <td>{prod.email}</td>
                <td>{prod.verifikovan ? "True" : "False"}</td>
                <td>
                {(() => {
                  switch (prod.statusVerifikacije) {
                    case 0:
                      return "Uobradi";
                    case 1:
                      return "Verifikovan";
                    case 2:
                      return "Odbijen";
                    default:
                      return "";
                  }
                })()}
              </td>
                <td>{prod.datumRodjenja}</td>
                <td>{prod.adresa}</td>
                <td>
                  {prod.slika && ( 
                    <img src={`data:image/png;base64,${prod.slika}`} 
                   alt="Slika korisnika"
                       width={80}
                      height={80}
                    />)}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <Link to="/prikaziSveVerifikovane"> Prikazi verifikovane prodavce</Link>


      <h2>Odobri verifikaciju prodavca</h2>
      <input
        type="number"
        name="id"
        value={id}
        onChange={(e) => setID(e.target.value)}
      />
      <button className="btn" type="button" onClick={() => odobriVerifikaciju(id)}>
        Odobri
      </button>

      <h2>Odbij verifikaciju prodavca</h2>
      <input
        type="number"
        name="idd"
        value={idd}
        onChange={(e) => setIDD(e.target.value)}
      />
      <button className="btn" type="button" onClick={() => odbijVerifikaciju(idd)}>
        Odbj 
      </button>
     
      <Link to="/dashBoard"> Dash Board</Link>
        </div>
    );
};

export default ProdavciVerifikacija;