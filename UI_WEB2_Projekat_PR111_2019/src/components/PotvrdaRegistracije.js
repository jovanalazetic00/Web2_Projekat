
import useState from "react";
import {PotvrdaRegistracije, OdbijRegistraciju} from "../services/PotvrdaRegistracijeService";



const PotvrdiRegistraciju = () => {
  //const [korisnik, setKorisnik] = useState([]);
  const [id, setID] = useState([]);
  const [idd, setIDD] = useState([]);
  
  const handlePotvrdiRegistraciju = async (id) => {
    try {
      await PotvrdaRegistracije(id);
      //setKorisnik(prodavac.filter((kor) => kor.korisnikID !== id));
      setID('');
     
    } catch (error) {
      console.log(error);
    }
  };
  

  const handleOdbijRegistraciju = async (idd) => {
  
    try {
      await OdbijRegistraciju(idd);
      setIDD('');
     
    } catch (error) {
      console.log(error);
    }
  };

return (
    <div>
    <h2>Odobri registraciju kroisnika</h2>
    <input
      type="number"
      name="id"
      value={id}
      onChange={(e) => setID(e.target.value)}
    />

    <button className="btn" type="button" onClick={() => handlePotvrdiRegistraciju(id)}>
      Potvrdi registraciju
    </button>


    <h2>Odbij registraciju korisnika</h2>
    <input
      type="number"
      name="idd"
      value={idd}
      onChange={(e) => setIDD(e.target.value)}
    />
    <button className="btn" type="button" onClick={() => handleOdbijRegistraciju(idd)}>
      Odbij registraciju
    </button>
     
      </div>
  );
};

export default PotvrdiRegistraciju;