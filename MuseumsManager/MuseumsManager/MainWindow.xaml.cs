﻿using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

namespace MuseumsManager
{
    /// <summary>
    /// Controller di MuseumsManager.
    /// Comunica con view e model.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Variabili globali
        int idMuseoSelezionato = 0;
        string defaultSelectedTextBoxValue;

        //Metodi
        void setTextBoxParameters(RoutedEventArgs e) 
        {
            TextBox t = e.Source as TextBox;

            if (t.FontStyle == FontStyles.Italic)
            {
                defaultSelectedTextBoxValue = t.Text;
                t.FontStyle = FontStyles.Normal;
                t.Foreground = Brushes.Black;
                t.Text = "";
            }   
        }

        void restoreTextBoxParameters(RoutedEventArgs e)
        {
            TextBox t = e.Source as TextBox;

            if (t.Text.Equals(""))
            {
                t.FontStyle = FontStyles.Italic;
                t.Foreground = Brushes.Gray;
                t.Text = defaultSelectedTextBoxValue;
            }
        }

        bool checkQueryResult(int returnedIndex)
        {
            if (returnedIndex < 1)
            {
                MessageBox.Show("Errore, ciò che si vuole creare è già presente, oppure il sistema non riesce a connettersi al database", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }


        //Eventi INSERT INTO

        /// <summary>
        /// Metodo di creazione di un nuovo museo.
        /// </summary>
        private void btn_museoCreazione_crea_Click(object sender, RoutedEventArgs e)
        {
            Museo m = new Museo();

            if (!txt_museoCreazione_nome.Text.Equals("Nome") &&
                !txt_museoCreazione_nome.Text.Equals("") &&
                !txt_museoCreazione_luogo.Text.Equals("Luogo") &&
                !txt_museoCreazione_luogo.Text.Equals("") &&
                !txt_museoCreazione_orarioApertura.Text.Equals("Orario di apertura") &&
                !txt_museoCreazione_orarioApertura.Text.Equals("") &&
                !txt_museoCreazione_orarioChiusura.Text.Equals("Orario di chiusura") &&
                !txt_museoCreazione_orarioChiusura.Text.Equals("") &&
                !txt_museoCreazione_numeroBigliettiMax.Text.Equals("Numero di biglietti max giornalieri") &&
                !txt_museoCreazione_numeroBigliettiMax.Text.Equals("") &&
                cmb_museoCreazione_tipoMuseo.SelectedIndex != -1 &&
                TimeSpan.TryParse(txt_museoCreazione_orarioApertura.Text, out TimeSpan tA) &&
                TimeSpan.TryParse(txt_museoCreazione_orarioChiusura.Text, out TimeSpan tC) &&
                int.TryParse(txt_museoCreazione_numeroBigliettiMax.Text, out int nBMax))
            {
                m.idMuseo = DBObject<Museo>.Insert("Nome", txt_museoCreazione_nome.Text, "Luogo", txt_museoCreazione_luogo.Text, "OrarioAperturaGenerale", tA, "OrarioChiusuraGenerale", tC, "NumBigliettiMaxGenerale", nBMax);
                int idMuseo_Tipologia = DBObject<Museo_Tipologia>.Insert("idMuseo", m.idMuseo, "idTipoMuseo", cmb_museoCreazione_tipoMuseo.SelectedIndex + 1);

                if (checkQueryResult(m.idMuseo) && checkQueryResult(idMuseo_Tipologia))
                    MessageBox.Show("Museo inserito correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Qualche parametro che si sta cercando di inserire non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Metodo di creazione di un nuovo tipo per un museo.
        /// </summary>
        private void btn_categoriaMuseo_crea_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_categoriaMuseo_descrizione.Text.Equals("") && !txt_categoriaMuseo_descrizione.Text.Equals("Descrizione"))
            {
                int index = DBObject<TipoMuseo>.Insert("Descrizione", txt_categoriaMuseo_descrizione.Text);

                if (checkQueryResult(index))
                    MessageBox.Show("Categoria di museo inserita correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Qualche parametro che si sta cercando di inserire non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }




        //Eventi GotFocus
        private void txt_categoriaMuseo_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);     
        }

        private void txt_famigliaMusei_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_museoCreazione_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_museoCreazione_luogo_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }
        private void txt_museoCreazione_orarioApertura_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_museoCreazione_orarioChiusura_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_museoCreazione_numeroBigliettiMax_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_creatore_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_creatore_cognome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_creatore_annoNascita_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_creatore_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_periodoStorico_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_periodoStorico_annoInizio_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_periodoStorico_annoFine_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_periodoStorico_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_provenienza_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_provenienza_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_manutenzioni_prezzo_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_manutenzioni_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_cognome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_cellulare_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_email_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_stipendioOrario_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_nuovoNumero_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_nuovaEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_personale_nuovoStipendio_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_ruolo_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_prezzo_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_nuovoPrezzo_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_nuovaDescrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_contenuti_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_contenuti_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_contenuti_dataRitrovamento_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_contenuti_dataArrivo_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_modificaContenuti_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_modificaContenuti_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_modificaContenuti_dataRitrovamento_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_modificaContenuti_dataArrivo_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_tipoContenuto_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_aperturaSpeciale_nuovoOrarioApertura_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_aperturaSpeciale_nuovoOrarioChiusura_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_aperturaSpeciale_numBigliettiMax_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_sezioni_nome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_sezioni_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_sezioni_modificaNome_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_sezioni_modificaDescrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_categoriaSezione_descrizione_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_orari_nuovoOrarioApertura_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        private void txt_orari_nuovoOrarioChiusura_GotFocus(object sender, RoutedEventArgs e)
        {
            setTextBoxParameters(e);
        }

        //Eventi LostFocus
        private void txt_categoriaMuseo_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_famigliaMusei_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_museoCreazione_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_museoCreazione_luogo_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_museoCreazione_orarioApertura_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_museoCreazione_orarioChiusura_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_museoCreazione_numeroBigliettiMax_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_creatore_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_creatore_cognome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_creatore_annoNascita_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_creatore_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_periodoStorico_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_periodoStorico_annoInizio_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_periodoStorico_annoFine_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_periodoStorico_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_provenienza_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_provenienza_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_manutenzioni_prezzo_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_manutenzioni_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_cognome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_cellulare_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_email_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_stipendioOrario_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_nuovoNumero_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_nuovaEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_personale_nuovoStipendio_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_ruolo_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_prezzo_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_nuovoPrezzo_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_tipoBiglietti_nuovaDescrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_contenuti_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_contenuti_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_contenuti_dataRitrovamento_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_contenuti_dataArrivo_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_modificaContenuti_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_modificaContenuti_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_modificaContenuti_dataRitrovamento_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_modificaContenuti_dataArrivo_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_tipoContenuto_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_aperturaSpeciale_nuovoOrarioApertura_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_aperturaSpeciale_nuovoOrarioChiusura_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_aperturaSpeciale_numBigliettiMax_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_sezioni_nome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_sezioni_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_sezioni_modificaNome_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_sezioni_modificaDescrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_categoriaSezione_descrizione_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_orari_nuovoOrarioApertura_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }

        private void txt_orari_nuovoOrarioChiusura_LostFocus(object sender, RoutedEventArgs e)
        {
            restoreTextBoxParameters(e);
        }


        //Eventi DropDownOpened
        private void cmb_museoCreazione_tipoMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_museoCreazione_tipoMuseo.Items.Clear();
            DBObject<TipoMuseo>.SelectAll().ForEach(x => cmb_museoCreazione_tipoMuseo.Items.Add(((TipoMuseo)x).Descrizione));
            /*List<TipoMuseo> lstmp = tmp.SelectAll().Select(x => (TipoMuseo)x).ToList();
            lstmp.ForEach(x => cmb_museoCreazione_tipoMuseo.Items.Add(x.Descrizione));
            */

            /*using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM TipoMuseo;", dBConnection.Connection);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        cmb_museoCreazione_tipoMuseo.Items.Add(sqlDataReader[1]);
                    }
                }
            }*/
        }

        private void cmb_museo_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_museo_selezionaMuseo.Items.Clear();

            DBObject<Museo>.SelectAll().ForEach(x => cmb_museo_selezionaMuseo.Items.Add(((Museo)x).Nome));
            /*
            using (DBConnection dBConnection = new DBConnection())
            {
                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Museo;", dBConnection.Connection);

                using (SqlDataReader sqlDataReader = dBConnection.SelectQuery(sqlCommand))
                {
                    while (sqlDataReader.Read())
                    {
                        cmb_museo_selezionaMuseo.Items.Add(sqlDataReader[1]);
                    }
                }
            }*/
        }

        //Eventi SelectionChanged
        private void cmb_museo_selezionaMuseo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_museo_selezionaMuseo.SelectedIndex != -1)
            {
                tbi_calendario.IsEnabled = true;
                tbi_biglietti.IsEnabled = true;
                tbi_contenuti.IsEnabled = true;
                tbi_personale.IsEnabled = true;
                tbi_registri.IsEnabled = true;
                tbi_statistiche.IsEnabled = true;
                gpb_sezioni.IsEnabled = true;
                gpb_categoriaSezione.IsEnabled = true;
                gpb_orari.IsEnabled = true;
            }
        }

        /// <summary>
        /// Creazione nuova famiglia da zero
        /// </summary>
        private void btn_famigliaMusei_crea_Click(object sender, RoutedEventArgs e)
        {
            checkQueryResult(DBObject<FamigliaMusei>.Insert("Nome", txt_famigliaMusei_nome.Text));
        }

        /// <summary>
        /// Lista dei musei esistenti
        /// </summary>
        private void cmb_famigliaMusei_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            List<Museo> lm = new List<Museo>(DBObject<Museo>.SelectAll().Where(m => m.idFamiglia == 0));
            cmb_famigliaMusei_selezionaMuseo.ItemsSource = lm;
            cmb_famigliaMusei_selezionaMuseo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista delle famiglie esistenti
        /// </summary>
        private void cmb_famigliaMusei_selezionaFamiglia_DropDownOpened(object sender, EventArgs e)
        {
            List<FamigliaMusei> lfm = new List<FamigliaMusei>(DBObject<FamigliaMusei>.SelectAll());
            cmb_famigliaMusei_selezionaFamiglia.ItemsSource = lfm;
            cmb_famigliaMusei_selezionaFamiglia.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Aggiunta museo a famiglia di musei
        /// </summary>
        private void btn_famigliaMusei_aggiungiMuseo_Click(object sender, RoutedEventArgs e)
        {
            int res = (cmb_famigliaMusei_selezionaMuseo.SelectedItem as Museo).Update("idMuseo", (cmb_famigliaMusei_selezionaMuseo.SelectedItem as Museo).idMuseo, "idFamiglia", (cmb_famigliaMusei_selezionaFamiglia.SelectedItem as FamigliaMusei).idFamiglia);
            checkQueryResult(res);
        }

        /// <summary>
        /// Lista delle famiglie esistenti
        /// </summary>
        private void cmb_famigliaMusei_rimuoviMuseo_famiglia_DropDownOpened(object sender, EventArgs e)
        {
            List<FamigliaMusei> lfm = new List<FamigliaMusei>(DBObject<FamigliaMusei>.SelectAll());
            cmb_famigliaMusei_rimuoviMuseo_famiglia.ItemsSource = lfm;
            cmb_famigliaMusei_rimuoviMuseo_famiglia.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista dei musei esistenti collegati alla famiglia
        /// </summary>
        private void cmb_famigliaMusei_rimuoviMuseo_DropDownOpened(object sender, EventArgs e)
        {
            FamigliaMusei fm = cmb_famigliaMusei_rimuoviMuseo_famiglia.SelectedItem as FamigliaMusei;
            if(fm != null)
            {
                List<Museo> lm = new List<Museo>(DBObject<Museo>.SelectAll().Where(m => m.idFamiglia == fm.idFamiglia));
                cmb_famigliaMusei_rimuoviMuseo.ItemsSource = lm;
                cmb_famigliaMusei_rimuoviMuseo.DisplayMemberPath = "Nome";
            }
        }

        /// <summary>
        /// Rimuovi museo da famiglia
        /// </summary>
        private void btn_famigliaMusei_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            FamigliaMusei fm = cmb_famigliaMusei_rimuoviMuseo_famiglia.SelectedItem as FamigliaMusei;
            Museo m = cmb_famigliaMusei_rimuoviMuseo.SelectedItem as Museo;

            if(fm != null && m != null)
            {
                m.Update("idMuseo", m.idMuseo, "idFamiglia", "NULL");
            }
        }
    }
}
