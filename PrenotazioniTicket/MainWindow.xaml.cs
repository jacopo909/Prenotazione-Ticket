using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrenotazioniTicket
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Cliente> Clienti = new List<Cliente>();
        public List<Prenotazione> Prenotazioni = new List<Prenotazione>();
        private string[] orario = new string[] { "18:30", "19:00", "19:30", "20:00" };
       
        public MainWindow()
        {
            InitializeComponent();
            rdbM.IsChecked = true;
        }
       
        private void btnAggiungiCliente_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Cliente cliente = new Cliente(txtNome.Text, txtCognome.Text);                             
                DateTime date;
                cliente.SetSesso(rdbM.IsChecked == true);
                cliente.SetCellulare(txtCellulare.Text);
                this.Clienti.Add(cliente);
                txtNome.Text = "";
                txtCognome.Text = "";
                txtCellulare.Text = "";
                rdbM.IsChecked = true;
                cmbSeleziona.Items.Add(cliente.Stampa_Cliente());
                cmbSelezionaCliente.Items.Add(cliente.Stampa_Cliente());
                date = dpData.SelectedDate.Value;
                string ora = cmbOrario.Text;
                Prenotazione p = new Prenotazione(date,ora,cliente);
                cliente.Prenotazioni.Add(p);
                Prenotazioni.Add(p);
                lboStampa1.Items.Add(p.Stampa());


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbOrario_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in orario)
            {
                cmbOrario.Items.Add(s);
            }
        }
        private void btnCancella_Click(object sender, RoutedEventArgs e)
        {

            int sel = lboStampa1.SelectedIndex;
            if(sel >= 0)
            {
                string NomeCliente = Prenotazioni[sel].c.ToString();

                for (int i = 0; i < Clienti.Count; i++)
                {
                    if(NomeCliente == Clienti[i].ToString())
                    {
                        Clienti[i].RimuoviPrenotazione(Prenotazioni[sel]);
                    }
                    else
                    {
                        MessageBox.Show("non è stato selezionato nessun elemento", "ATTENZIONE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    lboStampa1.Items.RemoveAt(sel);
                    lboStampa1.Items.Clear();
                    Prenotazioni.RemoveAt(sel);
                }
            }
        }
        private void btnPreCliente_Click(object sender,RoutedEventArgs e)
        {
            if(cmbSelezionaCliente.SelectedIndex != -1)
            {
                lboStampa2.Items.Clear();

                double prezzo = 0;

                int count = Clienti[cmbSelezionaCliente.SelectedIndex].ContaPre();

                lboStampa2.Items.Add($"{cmbSelezionaCliente.SelectedValue}: ");

                lboStampa2.Items.Add($"Prenotazioni: {count}");

                prezzo = Clienti[cmbSelezionaCliente.SelectedIndex].CostoPrenotazioniCli();

                lboStampa2.Items.Add($"Totale: {prezzo}");

                cmbSelezionaCliente.SelectedIndex = -1;

            }
            else
            {
                MessageBox.Show("Devi selzionare un cliente", "ATTENZIONE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void btnPrevento_Click(object sender, RoutedEventArgs e)
        {
            if(cmbSelezionaCliente.SelectedIndex != -1 && cmbSelezionaOrario.SelectedIndex != -1)
            {
                lboStampa2.Items.Clear();
                int Ora = Clienti[cmbSelezionaCliente.SelectedIndex].ConteggioPrenotazioneEvento(cmbSelezionaOrario.SelectedValue.ToString());
                lboStampa2.Items.Add(Ora);
            }
            else
            {
                MessageBox.Show("Devi selzionare un cliente ed orario","ATTENZIONE",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }
        private void btnEsci_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnPulisci_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Clear();
            txtCognome.Clear();
            txtCellulare.Clear();
            rdbM.IsChecked = true;
            cmbSelezionaCliente.SelectedIndex = -1;
            dpData.SelectedDate = null;
            cmbSelezionaOrario.SelectedIndex = -1;
            lboStampa1.Items.Clear();
            cmbSeleziona.SelectedIndex = -1;
            cmbOrario.SelectedIndex = -1;
            lboStampa2.Items.Clear();
        }

        
    }
}
