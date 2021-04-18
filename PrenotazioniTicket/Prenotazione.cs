using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrenotazioniTicket
{
    public class Prenotazione 
    {
        private const double Prezzo = 20;
        private List<Prenotazione> prenotazioni = new List<Prenotazione>();
        public string Ora { get; set; }

        public DateTime Data { get; set; }
        public Cliente c { get; set; }

        public Prenotazione(DateTime data,string ora,Cliente cliente)
        {
            c = cliente;
            cliente.AddPrenotazione(this);
            this.Ora = ora;
            this.Data = data;

        }
        public void AddPrenotazione(Prenotazione p)
        {
            prenotazioni.Remove(p);
        }

        public string Stampa()
        {
            return $"{c.Stampa_Cliente()} {Data.ToShortDateString()} {Ora} {CostoPrenotazione()}euro";
        }
        public double CostoPrenotazione()
        {
            double prezzo = 0;
            if(c.GetSesso() == "M" && this.Ora == "18:00" || c.GetSesso() == "F")
            {
                prezzo = Prezzo - ((Prezzo * 10) / 100);
            }
            else
            {
                prezzo = Prezzo;
            }
            return prezzo;
        }
    }
}
