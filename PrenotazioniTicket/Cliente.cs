using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrenotazioniTicket
{
    public class Cliente : MainWindow
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        private string cellulare;
        private string sesso;
        private List<Prenotazione> prenotazioni = new List<Prenotazione>();

        public Cliente(string nome,string cognome)
        {
            Nome = nome;
            Cognome = cognome;
            
        }
        public string GetSesso()
        {
            return sesso;
        }

        public void SetSesso(bool s)
        {
            if(s == true)
            {
                sesso = "M";
            }
            else
            {
                sesso = "F";
            }
        }
        public string GetCellulare()
        {
            return cellulare;
        }
        public void SetCellulare(string provacellulare)
        {
            if(provacellulare.Length < 10 || provacellulare.Length > 10)
            {
                throw new System.Exception("numero non completo");
            }
            else
            {
                try
                {
                    Int64.Parse(provacellulare);
                    cellulare = provacellulare;
                }
                catch (Exception)
                {
                    throw new System.Exception("numero non corretto");
                }
            }
        }

        public List<Prenotazione> GetPrenotaziones()
        {
            return prenotazioni;
        }
        public void AddPrenotazione(Prenotazione p)
        {
            prenotazioni.Add(p);
        }
        
        public void RimuoviPrenotazione(Prenotazione p)
        {
            prenotazioni.Remove(p);
        }
        public int ContaPre()
        {
            int count = 0;
            for (int i = 0; i < prenotazioni.Count; i++)
            {
                count++;
            }
            return count;
        }
        public double CostoPrenotazioniCli()
        {
            double costo = 0;
            for (int i = 0; i < prenotazioni.Count; i++)
            {
                costo = costo + prenotazioni[i].CostoPrenotazione();
            }
            return costo;
        }
        
   
        public string Stampa_Cliente()
        {
            return $"{Nome} {Cognome}";
        }
        public int ConteggioPrenotazioneEvento(string ora)
        {
            int count = 0;
            for (int i = 0; i < prenotazioni.Count; i++)
            {
                if(prenotazioni[i].Ora == ora)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
