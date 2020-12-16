using Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Diagnostics;
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
    /// Comunica con view (MainWindow.xaml) e model (Entities).
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Variabili globali
        Museo museoSelezionato;
        string defaultSelectedTextBoxValue;

        /*
        void setPlaceHolder(TextBox textBox, string placeHolder)
        {
            textBox.FontStyle = FontStyles.Italic;
            textBox.Foreground = Brushes.Gray;
            textBox.Text = placeHolder;
        }

        void restoreNormalTextStyle(TextBox textBox)
        {
            textBox.FontStyle = FontStyles.Normal;
            textBox.Foreground = Brushes.Black;
            textBox.Text = "";
        }
        */

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
                MessageBox.Show("Errore, operazione non consentita: E\' possibile che l'operazione sia già stata fatta, oppure il sistema non riesce a connettersi al database", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// //Setta lo stato di apertura del museo. (aperto/chiuso)
        /// </summary>
        void setMuseumStatus()
        {
            String currentDate = DateTime.Today.ToString("yyyy-MM-dd");

            //se (per il museo selezionato): (TUTTI i giorni di chiusura non corrispondono alla data di oggi E, UNO QUALSIASI di quelli di apertura speciale corrisponde alla data di oggi) OPPURE (TUTTI i giorni di chiusura non corrispondono alla data di oggi E, TUTTI quelli di apertura speciale NON CORRISPONDONO alla data di oggi)
            if (DBObject<CalendarioChiusure>.SelectAll().Where(cc => cc.idMuseo == this.museoSelezionato.idMuseo).All(cc => (cc as CalendarioChiusure).Data.ToString("yyyy-MM-dd") != currentDate) && DBObject<CalendarioApertureSpeciali>.SelectAll().Where(cas => cas.idMuseo == this.museoSelezionato.idMuseo).Any(cas => (cas as CalendarioApertureSpeciali).Data.ToString("yyyy-MM-dd") == currentDate) ||
                DBObject<CalendarioChiusure>.SelectAll().Where(cc => cc.idMuseo == this.museoSelezionato.idMuseo).All(cc => (cc as CalendarioChiusure).Data.ToString("yyyy-MM-dd") != currentDate) && DBObject<CalendarioApertureSpeciali>.SelectAll().Where(cas => cas.idMuseo == this.museoSelezionato.idMuseo).All(cas => (cas as CalendarioApertureSpeciali).Data.ToString("yyyy-MM-dd") != currentDate))
            {
                lbl_riepilogo_statoApertura.Foreground = Brushes.Green;
                lbl_riepilogo_statoApertura.Content = "Aperto";
            }
            else
            {
                lbl_riepilogo_statoApertura.Foreground = Brushes.Red;
                lbl_riepilogo_statoApertura.Content = "Chiuso";
            }
        }

        /// <summary>
        /// //Setta gli orari di apertura/chiusura del museo
        /// </summary>
        void setMuseumSchedule()
        {
            List<CalendarioApertureSpeciali> cap = new List<CalendarioApertureSpeciali>(DBObject<CalendarioApertureSpeciali>.SelectAll());
            bool ok = false;
            for (int i = 0; i < cap.Count; i++)
            {
                if (cap[i].Data.ToString("gg/MM/yyyy") == DateTime.Today.ToString("gg/MM/yyyy"))
                {
                    ok = true;
                    lbl_riepilogo_orarioApertura.Content = cap[i].OrarioApertura.ToString();
                    lbl_riepilogo_orarioChiusura.Content = cap[i].OrarioChiusura.ToString();
                }
            }
            if (!ok)
            {
                lbl_riepilogo_orarioApertura.Content = museoSelezionato.OrarioAperturaGenerale;
                lbl_riepilogo_orarioChiusura.Content = museoSelezionato.OrarioChiusuraGenerale;
            }
            lbl_orari_valoreOrarioApertura.Content = museoSelezionato.OrarioAperturaGenerale;
            lbl_orari_valoreOrarioChiusura.Content = museoSelezionato.OrarioChiusuraGenerale;
        }

        void setMuseumFamily()
        {
            List<FamigliaMusei> lfm = new List<FamigliaMusei>(DBObject<FamigliaMusei>.SelectAll());
            bool ok = false;
            for (int i = 0; i < lfm.Count; i++)
            {
                if (museoSelezionato.idFamiglia == lfm[i].idFamiglia)
                {
                    lbl_riepilogo_valoreFamiglia.Content = lfm[i].Nome;
                    ok = true;
                }
            }
            if (!ok)
                lbl_riepilogo_valoreFamiglia.Content = "Nessuna";
        }

        /// <summary>
        /// Setta tutti i tipi associati al museo selezionato.
        /// </summary>
        void setMuseumTypes()
        {
            List<Museo_Tipologia> parzialeMuseoTipologia = new List<Museo_Tipologia>(DBObject<Museo_Tipologia>.SelectAll().Where(mt => mt.idMuseo == museoSelezionato.idMuseo));
            List<TipoMuseo> tabellaTipoMuseo = new List<TipoMuseo>(DBObject<TipoMuseo>.SelectAll());
            List<TipoMuseo> TipiMuseoSelezionato = new List<TipoMuseo>();
            lsv_riepilogo_tipiMuseo.Items.Clear();

            bool ok;
            for (int i = 0; i < tabellaTipoMuseo.Count; i++)
            {
                ok = false;

                for (int j = 0; j < parzialeMuseoTipologia.Count; j++)
                {
                    if (tabellaTipoMuseo[i].idTipoMuseo == parzialeMuseoTipologia[j].idTipoMuseo)
                        ok = true;
                }
                if (ok)
                    TipiMuseoSelezionato.Add(tabellaTipoMuseo[i]);
            }
            for (int i = 0; i < TipiMuseoSelezionato.Count; i++)
            {
                lsv_riepilogo_tipiMuseo.Items.Add(TipiMuseoSelezionato[i].Descrizione);
            }
        }

        void setMuseumAreas()
        {
            List<Sezione> ls = new List<Sezione>(DBObject<Sezione>.SelectAll().Where(s => s.idMuseo == museoSelezionato.idMuseo));
            lsv_riepilogo_sezioni.ItemsSource = ls;
            lsv_riepilogo_sezioni.DisplayMemberPath = "Nome";
        }

        //Eventi Click

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
                int idMuseo_Tipologia = DBObject<Museo_Tipologia>.Insert("idMuseo", m.idMuseo, "idTipoMuseo", ((TipoMuseo)cmb_museoCreazione_tipoMuseo.SelectedItem).idTipoMuseo);

                if (checkQueryResult(m.idMuseo) && checkQueryResult(idMuseo_Tipologia))
                    MessageBox.Show("Museo inserito correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Qualche parametro che si sta cercando di inserire non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Aggiunge il tipo di museo selezionato al museo selezionato.
        /// </summary>
        private void btn_museoCreazione_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_museoCreazione_selezionaMuseo.SelectedItem != null && cmb_museoCreazione_selezionaTipo.SelectedItem != null)
            {
                if (checkQueryResult(DBObject<Museo_Tipologia>.Insert("idMuseo", ((Museo)cmb_museoCreazione_selezionaMuseo.SelectedItem).idMuseo, "idTipoMuseo", ((TipoMuseo)cmb_museoCreazione_selezionaTipo.SelectedItem).idTipoMuseo)))
                {
                    MessageBox.Show("Tipo \"" + ((TipoMuseo)cmb_museoCreazione_selezionaTipo.SelectedItem).Descrizione + "\" assegnato correttamente al museo \"" + ((Museo)cmb_museoCreazione_selezionaMuseo.SelectedItem).Nome + "\"!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                    cmb_museoCreazione_selezionaMuseo.ItemsSource = null;
                    cmb_museoCreazione_selezionaTipo.ItemsSource = null;
                }
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Rimuove il tipo di museo selezionato dal museo selezionato.
        /// </summary>
        private void btn_eliminaTipo_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_eliminaTipo_selezionaTipo.Items.Count == 1)
            {
                MessageBox.Show("Non è possibile eliminare il tipo selezionato: un museo deve possedere almeno un tipo!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                cmb_eliminaTipo_selezionaMuseo.ItemsSource = null;
                cmb_eliminaTipo_selezionaTipo.ItemsSource = null;
                return;
            }
            if (cmb_eliminaTipo_selezionaMuseo.SelectedItem != null && cmb_eliminaTipo_selezionaTipo.SelectedItem != null)
            {
                if (checkQueryResult(DBRelationN2NOnlyIndexes<Museo_Tipologia>.Delete("idMuseo", ((Museo)cmb_eliminaTipo_selezionaMuseo.SelectedItem).idMuseo, "idTipoMuseo", ((TipoMuseo)cmb_eliminaTipo_selezionaTipo.SelectedItem).idTipoMuseo)))
                {
                    MessageBox.Show("Tipo \"" + ((TipoMuseo)cmb_eliminaTipo_selezionaTipo.SelectedItem).Descrizione + "\" rimosso correttamente dal museo \"" + ((Museo)cmb_eliminaTipo_selezionaMuseo.SelectedItem).Nome + "\"!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                    cmb_eliminaTipo_selezionaMuseo.ItemsSource = null;
                    cmb_eliminaTipo_selezionaTipo.ItemsSource = null;
                }
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
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

        /// <summary>
        /// Metodo di rimozione del museo selezionato.
        /// </summary>
        private void btn_museoCreazione_eliminaMuseo_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_museoCreazione_eliminaMuseo.SelectedItem != null)
            {
                Museo museoSelezionato = cmb_museoCreazione_eliminaMuseo.SelectedItem as Museo;
                string nomeMuseo = ((Museo)cmb_museoCreazione_eliminaMuseo.SelectedItem).Nome;

                if (checkQueryResult(DBEntity.Delete<Museo>("idMuseo", museoSelezionato.idMuseo)))
                {
                    MessageBox.Show("Museo \"" + nomeMuseo + "\" rimosso correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                    cmb_museoCreazione_eliminaMuseo.ItemsSource = null;
                }
            }
            else
                MessageBox.Show("Nessun museo selezionato!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Creazione di una nuova famiglia di musei.
        /// </summary>
        private void btn_famigliaMusei_crea_Click(object sender, RoutedEventArgs e)
        {
            if (txt_famigliaMusei_nome.Text != "" && txt_famigliaMusei_nome.Text != "Nome")
            {
                if (checkQueryResult(DBObject<FamigliaMusei>.Insert("Nome", txt_famigliaMusei_nome.Text)))
                    MessageBox.Show("Nuova famiglia di musei inserita correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                txt_famigliaMusei_nome.Clear();
            }
        }

        /// <summary>
        /// Aggiunta di un museo ad una famiglia di musei.
        /// </summary>
        private void btn_famigliaMusei_aggiungiMuseo_Click(object sender, RoutedEventArgs e)
        {
            int res = (cmb_famigliaMusei_selezionaMuseo.SelectedItem as Museo).Update("idMuseo", (cmb_famigliaMusei_selezionaMuseo.SelectedItem as Museo).idMuseo, "idFamiglia", (cmb_famigliaMusei_selezionaFamiglia.SelectedItem as FamigliaMusei).idFamiglia);
            if (checkQueryResult(res))
                MessageBox.Show("Museo aggiunto correttamente alla famiglia di musei selezionata!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            cmb_famigliaMusei_selezionaMuseo.ItemsSource = null;
            cmb_famigliaMusei_selezionaFamiglia.ItemsSource = null;
        }

        /// <summary>
        /// Rimozione di un museo dalla sua famiglia di musei.
        /// </summary>
        private void btn_famigliaMusei_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            FamigliaMusei fm = cmb_famigliaMusei_rimuoviMuseo_famiglia.SelectedItem as FamigliaMusei;
            Museo m = cmb_famigliaMusei_rimuoviMuseo.SelectedItem as Museo;

            if (fm != null && m != null)
            {
                int res = m.Update("idMuseo", m.idMuseo, "idFamiglia", "NULL");
                if (checkQueryResult(res))
                    MessageBox.Show("Museo rimosso correttamente dalla famiglia di musei selezionata!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                cmb_famigliaMusei_rimuoviMuseo_famiglia.ItemsSource = null;
                cmb_famigliaMusei_rimuoviMuseo.ItemsSource = null;
            }
            else
                MessageBox.Show("Non tutti i parametri sono stati compilati!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        /// <summary>
        /// Aggiunta periodo storico
        /// </summary>
        private void btn_periodoStorico_inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (txt_periodoStorico_nome.Text != "" && txt_periodoStorico_nome.Text != "Nome" &&
                txt_periodoStorico_annoInizio.Text != "" && txt_periodoStorico_annoInizio.Text != "Anno di inizio" &&
                txt_periodoStorico_annoFine.Text != "" && txt_periodoStorico_annoFine.Text != "Anno di fine" &&
                txt_periodoStorico_descrizione.Text != "" && txt_periodoStorico_descrizione.Text != "Descrizione")
            {
                int res = DBObject<PeriodoStorico>.Insert("Nome", txt_periodoStorico_nome.Text, "AnnoInizio", txt_periodoStorico_annoInizio.Text, "AnnoFine", txt_periodoStorico_annoFine.Text, "Descrizione", txt_periodoStorico_descrizione.Text);
                if (checkQueryResult(res))
                    MessageBox.Show("Aggiunto periodo storico correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);

                txt_periodoStorico_nome.Clear();
                txt_periodoStorico_annoInizio.Clear();
                txt_periodoStorico_annoFine.Clear();
                txt_periodoStorico_descrizione.Clear();
            }
        }

        /// <summary>
        /// Rimozione periodo storico.
        /// </summary>
        private void btn_periodoStorico_elimina_Click(object sender, RoutedEventArgs e)
        {
            //PANTIE DEVI FARE IL CONTROLLO SE IL PERIODO STORICO E' PRESENTE IN MUSEO_PERIODOSTORICO E IN CONTENUTO NON SI PUO' ELIMINARE!!!
            if (!(cmb_periodoStorico_elimina.SelectedItem is null))
            {
                int res = DBEntity.Delete<PeriodoStorico>("idPeriodoStorico", (cmb_periodoStorico_elimina.SelectedItem as PeriodoStorico).idPeriodoStorico);
                if (checkQueryResult(res))
                    MessageBox.Show("Periodo storico eliminato correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                cmb_periodoStorico_elimina.ItemsSource = null;
            }
        }

        /// <summary>
        /// Aggiunta di un nuovo creatore.
        /// </summary>
        private void btn_creatore_inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_creatore_nome.Text.Equals("Nome") &&
                !txt_creatore_nome.Text.Equals("") &&
                !txt_creatore_cognome.Text.Equals("Cognome") &&
                !txt_creatore_cognome.Text.Equals("") &&
                !txt_creatore_annoNascita.Text.Equals("Anno di nascita") &&
                !txt_creatore_annoNascita.Text.Equals("") &&
                int.TryParse(txt_creatore_annoNascita.Text, out int annoNascita) &&
                !txt_creatore_descrizione.Text.Equals("Descrizione") &&
                !txt_creatore_descrizione.Text.Equals(""))
            {
                if (checkQueryResult(DBObject<Creatore>.Insert("Nome", txt_creatore_nome.Text, "Cognome", txt_creatore_cognome.Text, "AnnoNascita", annoNascita, "Descrizione", txt_creatore_descrizione.Text)))
                    MessageBox.Show("Creatore inserito correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Qualche parametro che si sta cercando di inserire non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Rimozione di un creatore.
        /// </summary>
        private void btn_creatore_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_creatore_rimuovi.SelectedItem != null)
            {
                //if(DBRelationN2NOnlyIndexes<Museo_Creatore>.SelectAll().Any(mc => ((Museo_Creatore)mc).idCreatore == ((Creatore)cmb_creatore_rimuovi.SelectedItem).idCreatore) ||
                //DBRelationN2NOnlyIndexes<Creato>.SelectAll().Any(cc => ((Creato)cc).idCreatore == ((Creato)cmb_creatore_rimuovi.SelectedItem).idCreatore))
                //{
                //MessageBox.Show("Impossibile eliminare il creatore selezionato: è possibile che esso abbia altre dipendenze in altri musei o contenuti!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                //else
                //{
                string nomeCreatore = ((Creatore)cmb_creatore_rimuovi.SelectedItem).ToString();
                if (checkQueryResult(DBEntity.Delete<Creatore>("idCreatore", (cmb_creatore_rimuovi.SelectedItem as Creatore).idCreatore)))
                {
                    MessageBox.Show("Creatore \"" + nomeCreatore + "\" eliminato correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                    cmb_creatore_rimuovi.ItemsSource = null;
                }
                //}
            }
            else
                MessageBox.Show("Nessun creatore selezionato!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Aggiunta data di apertura speciale
        /// </summary>
        private void btn_aperturaSpeciale_inserisci_Click(object sender, RoutedEventArgs e)
        {
            int nBiglMax = 0;
            TimeSpan oa = new TimeSpan(), oc = new TimeSpan();
            if (!(cal_aperturaSpeciale_data.SelectedDate is null) && !(cmb_museo_selezionaMuseo.SelectedItem is null) && int.TryParse(txt_aperturaSpeciale_numBigliettiMax.Text, out nBiglMax) &&
                TimeSpan.TryParse(txt_aperturaSpeciale_nuovoOrarioApertura.Text, out oa) &&
                TimeSpan.TryParse(txt_aperturaSpeciale_nuovoOrarioChiusura.Text, out oc))
            {
                Museo m = (Museo)cmb_museo_selezionaMuseo.SelectedItem;
                DateTime date = (DateTime)cal_aperturaSpeciale_data.SelectedDate;
                string data = date.Date.ToString("yyyy-MM-dd");
                if (DBObject<CalendarioChiusure>.Select("idMuseo", m.idMuseo, "Data", data).Count == 0)
                {
                    int res = DBObject<CalendarioApertureSpeciali>.Insert("Data", data, "idMuseo", m.idMuseo, "OrarioApertura", oa, "OrarioChiusura", oc, "NumBigliettiMax", nBiglMax);
                    if (checkQueryResult(res))
                        MessageBox.Show("Giorno di apertura speciale aggiunto correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Data presente come giorno di chiusura!", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                txt_aperturaSpeciale_numBigliettiMax.Clear();
                txt_aperturaSpeciale_nuovoOrarioApertura.Clear();
                txt_aperturaSpeciale_nuovoOrarioChiusura.Clear();
            }
            List<CalendarioApertureSpeciali> lcas = DBObject<CalendarioApertureSpeciali>.Select("idMuseo", this.museoSelezionato.idMuseo);
            ObservableCollection<CalendarioApertureSpeciali> calendarioApertureSpeciali = new ObservableCollection<CalendarioApertureSpeciali>(lcas);
            calendarioApertureSpeciali.CollectionChanged += (s, eventArgs) =>
            {

                switch (eventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                        {
                            List<CalendarioApertureSpeciali> casRmv = new List<CalendarioApertureSpeciali>(eventArgs.OldItems.Cast<CalendarioApertureSpeciali>());
                            casRmv.ForEach(cas => DBEntity.Delete<CalendarioApertureSpeciali>("idCalendarioApertureSpeciali", cas.idCalendarioApertureSpeciali));
                            break;
                        }
                    case NotifyCollectionChangedAction.Add:
                        {
                            List<CalendarioApertureSpeciali> casAdd = new List<CalendarioApertureSpeciali>(eventArgs.NewItems.Cast<CalendarioApertureSpeciali>());
                            casAdd.ForEach(cas => cas.idMuseo = museoSelezionato.idMuseo);
                            casAdd.ForEach(cas => DBObject<CalendarioApertureSpeciali>.Insert("Data", cas.Data, "OrarioApertura", cas.OrarioApertura, "OrarioChiusura", cas.OrarioChiusura, "NumBigliettiMax", cas.NumBigliettiMax, "idMuseo", cas.idMuseo));
                            break;
                        }
                }
            };
            dtg_giornateAperturaSpeciale.DataContext = calendarioApertureSpeciali;
        }

        /// <summary>
        /// Eliminazione giorno di apertura speciale per museo
        /// </summary>
        private void btn_aperturaSpeciale_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (!(cmb_aperturaSpeciale_elimina.SelectedItem is null))
            {
                CalendarioApertureSpeciali cal = cmb_aperturaSpeciale_elimina.SelectedItem as CalendarioApertureSpeciali;

                int res = DBEntity.Delete<CalendarioApertureSpeciali>("idCalendarioApertureSpeciali", cal.idCalendarioApertureSpeciali);
                if (checkQueryResult(res))
                    MessageBox.Show("Giorno di apertura speciale eliminato correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                cmb_aperturaSpeciale_elimina.ItemsSource = null;
            }
            List<CalendarioApertureSpeciali> lcas = DBObject<CalendarioApertureSpeciali>.Select("idMuseo", this.museoSelezionato.idMuseo);
            ObservableCollection<CalendarioApertureSpeciali> calendarioApertureSpeciali = new ObservableCollection<CalendarioApertureSpeciali>(lcas);
            calendarioApertureSpeciali.CollectionChanged += (s, eventArgs) =>
            {

                switch (eventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                        {
                            List<CalendarioApertureSpeciali> casRmv = new List<CalendarioApertureSpeciali>(eventArgs.OldItems.Cast<CalendarioApertureSpeciali>());
                            casRmv.ForEach(cas => DBEntity.Delete<CalendarioApertureSpeciali>("idCalendarioApertureSpeciali", cas.idCalendarioApertureSpeciali));
                            break;
                        }
                    case NotifyCollectionChangedAction.Add:
                        {
                            List<CalendarioApertureSpeciali> casAdd = new List<CalendarioApertureSpeciali>(eventArgs.NewItems.Cast<CalendarioApertureSpeciali>());
                            casAdd.ForEach(cas => cas.idMuseo = museoSelezionato.idMuseo);
                            casAdd.ForEach(cas => DBObject<CalendarioApertureSpeciali>.Insert("Data", cas.Data, "OrarioApertura", cas.OrarioApertura, "OrarioChiusura", cas.OrarioChiusura, "NumBigliettiMax", cas.NumBigliettiMax, "idMuseo", cas.idMuseo));
                            break;
                        }
                }
            };
            dtg_giornateAperturaSpeciale.DataContext = calendarioApertureSpeciali;
        }

        /// <summary>
        /// Aggiunta giorno di chiusura
        /// </summary>
        private void btn_chiusura_inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (!(cal_chiusura_data.SelectedDate is null) && !(cmb_museo_selezionaMuseo.SelectedItem is null))
            {
                Museo m = (Museo)cmb_museo_selezionaMuseo.SelectedItem;
                DateTime date = (DateTime)cal_chiusura_data.SelectedDate;
                string data = date.Date.ToString("yyyy-MM-dd");
                if (DBObject<CalendarioApertureSpeciali>.Select("idMuseo", m.idMuseo, "Data", data).Count == 0)
                {
                    int res = DBObject<CalendarioChiusure>.Insert("Data", data, "idMuseo", m.idMuseo);
                    if (checkQueryResult(res))
                        MessageBox.Show("Giorno di chiusura aggiunto correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Data presente come giorno di apertura speciale!", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            List<CalendarioChiusure> lcc = DBObject<CalendarioChiusure>.Select("idMuseo", this.museoSelezionato.idMuseo);
            ObservableCollection<CalendarioChiusure> calendarioChiusure = new ObservableCollection<CalendarioChiusure>(lcc);
            calendarioChiusure.CollectionChanged += (s, eventArgs) =>
            {
                switch (eventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                        {
                            List<CalendarioChiusure> ccRmv = new List<CalendarioChiusure>(eventArgs.OldItems.Cast<CalendarioChiusure>());
                            ccRmv.ForEach(cc => DBEntity.Delete<CalendarioChiusure>("idCalendarioChiusure", cc.idCalendarioChiusure));
                            break;
                        }
                    case NotifyCollectionChangedAction.Add:
                        {
                            List<CalendarioChiusure> ccAdd = new List<CalendarioChiusure>(eventArgs.NewItems.Cast<CalendarioChiusure>());
                            ccAdd.ForEach(cc => cc.idMuseo = museoSelezionato.idMuseo);
                            ccAdd.ForEach(cc => DBObject<CalendarioChiusure>.Insert("Data", cc.Data, "idMuseo", cc.idMuseo));
                            break;
                        }
                }
            };
            dtg_giornateChiusura.DataContext = calendarioChiusure;
        }

        /// <summary>
        /// Eliminazione giorno di chiusura per museo
        /// </summary>
        private void btn_chiusura_elimina_Click(object sender, RoutedEventArgs e)
        {
            if (!(cmb_chiusura_elimina.SelectedItem is null))
            {
                CalendarioChiusure cal = cmb_chiusura_elimina.SelectedItem as CalendarioChiusure;

                int res = DBEntity.Delete<CalendarioChiusure>("idCalendarioChiusure", cal.idCalendarioChiusure);
                if (checkQueryResult(res))
                    MessageBox.Show("Giorno di chiusura eliminato correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);

                cmb_chiusura_elimina.ItemsSource = null;
            }

            List<CalendarioChiusure> lcc = DBObject<CalendarioChiusure>.Select("idMuseo", this.museoSelezionato.idMuseo);
            ObservableCollection<CalendarioChiusure> calendarioChiusure = new ObservableCollection<CalendarioChiusure>(lcc);
            calendarioChiusure.CollectionChanged += (s, eventArgs) =>
            {
                switch (eventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Remove:
                        {
                            List<CalendarioChiusure> ccRmv = new List<CalendarioChiusure>(eventArgs.OldItems.Cast<CalendarioChiusure>());
                            ccRmv.ForEach(cc => DBEntity.Delete<CalendarioChiusure>("idCalendarioChiusure", cc.idCalendarioChiusure));
                            break;
                        }
                    case NotifyCollectionChangedAction.Add:
                        {
                            List<CalendarioChiusure> ccAdd = new List<CalendarioChiusure>(eventArgs.NewItems.Cast<CalendarioChiusure>());
                            ccAdd.ForEach(cc => cc.idMuseo = museoSelezionato.idMuseo);
                            ccAdd.ForEach(cc => DBObject<CalendarioChiusure>.Insert("Data", cc.Data, "idMuseo", cc.idMuseo));
                            break;
                        }
                }
            };
            dtg_giornateChiusura.DataContext = calendarioChiusure;
        }

        private void btn_provenienza_inserisci_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_provenienza_nome.Text.Equals("Nome") &&
                !txt_provenienza_nome.Text.Equals("") &&
                !txt_provenienza_descrizione.Text.Equals("Descrizione") &&
                !txt_provenienza_descrizione.Text.Equals(""))
            {
                if (checkQueryResult(DBObject<Provenienza>.Insert("Nome", txt_provenienza_nome.Text, "Descrizione", txt_provenienza_descrizione.Text)))
                    MessageBox.Show("Provenienza inserita correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_provenienza_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_provenienza_rimuovi.SelectedItem != null)
            {
                //if(DBRelationN2NOnlyIndexes<Museo_Provenienza>.SelectAll().Any(mp => ((Museo_Provenienza)mp).idProvenienza == ((Provenienza)cmb_provenienza_rimuovi.SelectedItem).idProvenienza) ||
                //DBObject<Contenuto>.SelectAll().Any(c => ((Contenuto)c).idProvenienza == ((Provenienza)cmb_creatore_rimuovi.SelectedItem).idProvenienza))
                //{
                //MessageBox.Show("Impossibile eliminare la provenienza selezionata: è possibile che essa abbia altre dipendenze in altri musei o contenuti!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
                //else
                //{
                string nomeProvenienza = ((Provenienza)cmb_provenienza_rimuovi.SelectedItem).Nome;
                if (checkQueryResult(DBEntity.Delete<Provenienza>("idProvenienza", (cmb_provenienza_rimuovi.SelectedItem as Provenienza).idProvenienza)))
                {
                    MessageBox.Show("La provenienza \"" + nomeProvenienza + "\" è stata eliminata correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                    cmb_provenienza_rimuovi.ItemsSource = null;
                }
                //}
            }
            else
                MessageBox.Show("Nessuna provenienza selezionata!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Crea una nuova categoria di sezione.
        /// </summary>
        private void btn_categoriaSezione_crea_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_categoriaSezione_descrizione.Text.Equals("Descrizione") &&
                !txt_categoriaSezione_descrizione.Text.Equals(""))
            {
                if (checkQueryResult(DBObject<TipoSezione>.Insert("Descrizione", txt_categoriaSezione_descrizione.Text)))
                    MessageBox.Show("Nuova categoria di sezione inserita correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("La descrizione non è stata compilata correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_orari_modifica_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan orarioApertura, orarioChiusura;
            if (!txt_orari_nuovoOrarioApertura.Text.Equals("") &&
               !txt_orari_nuovoOrarioChiusura.Text.Equals("") &&
               TimeSpan.TryParse(txt_orari_nuovoOrarioApertura.Text, out orarioApertura) &&
               TimeSpan.TryParse(txt_orari_nuovoOrarioChiusura.Text, out orarioChiusura))
            {
                museoSelezionato.Update("idMuseo", museoSelezionato.idMuseo, "OrarioAperturaGenerale", orarioApertura, "OrarioChiusuraGenerale", orarioChiusura);
                museoSelezionato = DBObject<Museo>.Select("idMuseo", museoSelezionato.idMuseo).First();
                MessageBox.Show("Nuovi orari di apertura/chiusura generali impostati correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                setMuseumSchedule();
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_sezioni_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_sezioni_nome.Text.Equals("") &&
               !txt_sezioni_nome.Text.Equals("Nome") &&
               !txt_sezioni_descrizione.Text.Equals("") &&
               !txt_sezioni_descrizione.Text.Equals("Descrizione") &&
               cmb_sezioni_tipoSezione.SelectedIndex != -1 &&
               cmb_sezioni_padre.SelectedIndex != -1)
            {
                int index = DBObject<Sezione>.Insert("Nome", txt_sezioni_nome.Text, "Descrizione", txt_sezioni_descrizione.Text, "idSezionePadre", ((Sezione)cmb_sezioni_padre.SelectedItem).idSezione, "idMuseo", museoSelezionato.idMuseo);
                DBObject<Sezione_Tipologia>.Insert("idSezione", index, "idTipoSezione", ((TipoSezione)cmb_sezioni_tipoSezione.SelectedItem).idTipoSezione);
                MessageBox.Show("Nuova sottosezione aggiunta correttamente al museo!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                setMuseumAreas();
            }
            else if (!txt_sezioni_nome.Text.Equals("") &&
               !txt_sezioni_nome.Text.Equals("Nome") &&
               !txt_sezioni_descrizione.Text.Equals("") &&
               !txt_sezioni_descrizione.Text.Equals("Descrizione") &&
               cmb_sezioni_tipoSezione.SelectedIndex != -1)
            {
                int index = DBObject<Sezione>.Insert("Nome", txt_sezioni_nome.Text, "Descrizione", txt_sezioni_descrizione.Text, "idMuseo", museoSelezionato.idMuseo);
                DBObject<Sezione_Tipologia>.Insert("idSezione", index, "idTipoSezione", ((TipoSezione)cmb_sezioni_tipoSezione.SelectedItem).idTipoSezione);
                MessageBox.Show("Nuova sezione aggiunta correttamente al museo!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                setMuseumAreas();
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary> //MODIFICA DEL PADRE ATTUALMENTE NON FUNZIONANTEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE
        /// Metodo per la modifica di ogni aspetto di una sezione: nome, descrizione, tipo e padre.
        /// </summary>
        private void btn_sezioni_modifica_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_sezioni_selezionaSezione.SelectedIndex != -1)
            {
                /* Metodo per trovare l'idTipoSezione interessato. Anche se la relazione nel DB è N_a_N il programma ne utilizza solo uno, quindi fa .First().
                   Per modificare un tipo di sezione specifico associato a una sezione, occorrebbe aggiungere nell'interfaccia una combobox di selezione dell'interessato da modificare. 
                */
                List<Sezione_Tipologia> tabellaSezioneTipologia = new List<Sezione_Tipologia>(DBObject<Sezione_Tipologia>.SelectAll());
                Sezione sezioneSelezionata = (cmb_sezioni_selezionaSezione.SelectedItem as Sezione);
                List<Sezione_Tipologia> parzialeSezioneTipologia = new List<Sezione_Tipologia>();
                
                for (int i = 0; i < tabellaSezioneTipologia.Count; i++)
                {
                    if (sezioneSelezionata.idSezione == tabellaSezioneTipologia[i].idSezione)
                    {
                        parzialeSezioneTipologia.Add(tabellaSezioneTipologia[i]);
                    }
                }

                //Controlli per scegliere la query corretta.
                if (
                    !txt_sezioni_modificaNome.Text.Equals("") &&
                    !txt_sezioni_modificaDescrizione.Text.Equals("") &&
                    cmb_sezioni_modificaTipo.SelectedIndex != -1 &&
                    cmb_sezioni_modificaPadre.SelectedIndex != -1)
                {
                    DBEntity.Update<Sezione>("idSezione", sezioneSelezionata.idSezione, "Nome", txt_sezioni_modificaNome.Text, "Descrizione", txt_sezioni_modificaDescrizione.Text, "idSezionePadre", (cmb_sezioni_modificaPadre.SelectedItem as Sezione).idSezionePadre);
                    DBRelationN2NOnlyIndexes<Sezione_Tipologia>.Delete("idSezione", sezioneSelezionata.idSezione, "idTipoSezione", parzialeSezioneTipologia.First().idTipoSezione);
                    DBObject<Sezione_Tipologia>.Insert("idSezione", sezioneSelezionata.idSezione, "idTipoSezione", (cmb_sezioni_modificaTipo.SelectedItem as TipoSezione).idTipoSezione);
                }
                else
                if (!txt_sezioni_modificaNome.Text.Equals("") &&
                    !txt_sezioni_modificaDescrizione.Text.Equals("") &&
                    cmb_sezioni_modificaTipo.SelectedIndex != -1)
                {
                    DBEntity.Update<Sezione>("idSezione", sezioneSelezionata.idSezione, "Nome", txt_sezioni_modificaNome.Text, "Descrizione", txt_sezioni_modificaDescrizione.Text);
                    DBRelationN2NOnlyIndexes<Sezione_Tipologia>.Delete("idSezione", sezioneSelezionata.idSezione, "idTipoSezione", parzialeSezioneTipologia.First().idTipoSezione);
                    DBObject<Sezione_Tipologia>.Insert("idSezione", sezioneSelezionata.idSezione, "idTipoSezione", (cmb_sezioni_modificaTipo.SelectedItem as TipoSezione).idTipoSezione);
                }
                else 
                if(!txt_sezioni_modificaNome.Text.Equals("") &&
                    !txt_sezioni_modificaDescrizione.Text.Equals("") &&
                    cmb_sezioni_modificaPadre.SelectedIndex != -1)
                {
                    DBEntity.Update<Sezione>("idSezione", sezioneSelezionata.idSezione, "Nome", txt_sezioni_modificaNome.Text, "Descrizione", txt_sezioni_modificaDescrizione.Text, "idSezionePadre", (cmb_sezioni_modificaPadre.SelectedItem as Sezione).idSezionePadre);
                    
                }
                else if (txt_sezioni_modificaNome.Text == sezioneSelezionata.Nome && txt_sezioni_modificaDescrizione.Text == sezioneSelezionata.Descrizione)
                {
                    MessageBox.Show("Non è stata apportata alcuna modifica!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else 
                if (!txt_sezioni_modificaNome.Text.Equals("") &&
                    !txt_sezioni_modificaDescrizione.Text.Equals(""))
                {
                    DBEntity.Update<Sezione>("idSezione", sezioneSelezionata.idSezione, "Nome", txt_sezioni_modificaNome.Text, "Descrizione", txt_sezioni_modificaDescrizione.Text);
                }  
                else
                {
                    MessageBox.Show("Qualche parametro non è stato compilato!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Sezione modificata correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                lsv_riepilogo_sottosezioni.ItemsSource = null;
                setMuseumAreas();  
            }
            else
                MessageBox.Show("Nessuna sezione selezionata!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_tipoContenuto_crea_Click(object sender, RoutedEventArgs e)
        {
            if (txt_tipoContenuto_descrizione.Text != "" && txt_tipoContenuto_descrizione.Text != "Descrizione")
            {
                int res = DBObject<TipoContenuto>.Insert("Descrizione", txt_tipoContenuto_descrizione.Text);
                if (checkQueryResult(res))
                    MessageBox.Show("Tipo contenuto aggiunto correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                txt_tipoContenuto_descrizione.Clear();
            }
        }

        private void btn_tipoBiglietti_crea_Click(object sender, RoutedEventArgs e)
        {
            if (!txt_tipoBiglietti_nome.Text.Equals("") &&
                !txt_tipoBiglietti_nome.Text.Equals("Nome") &&
                !txt_tipoBiglietti_prezzo.Text.Equals("") &&
                int.TryParse(txt_tipoBiglietti_prezzo.Text, out int prezzo) &&
                !txt_tipoBiglietti_descrizione.Text.Equals("") &&
                !txt_tipoBiglietti_descrizione.Text.Equals("Descrizione"))
            {
                DBObject<TipoBiglietto>.Insert("Nome", txt_tipoBiglietti_nome.Text, "Prezzo", prezzo, "Descrizione", txt_tipoBiglietti_descrizione.Text, "idMuseo", museoSelezionato.idMuseo);
                MessageBox.Show("Tipo di biglietto aggiunto correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                lsv_tipiBiglietti.ItemsSource = DBObject<TipoBiglietto>.SelectAll().Where(tb => tb.idMuseo == museoSelezionato.idMuseo);
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btn_tipoBiglietti_modifica_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_tipoBiglietti_selezionaBiglietto.SelectedIndex != -1 &&
                !txt_tipoBiglietti_nuovoNome.Text.Equals("") &&
                !txt_tipoBiglietti_nuovoNome.Text.Equals("Nome") &&
                !txt_tipoBiglietti_nuovoPrezzo.Text.Equals("") &&
                int.TryParse(txt_tipoBiglietti_nuovoPrezzo.Text, out int prezzo) &&
                !txt_tipoBiglietti_nuovaDescrizione.Text.Equals("") &&
                !txt_tipoBiglietti_nuovaDescrizione.Text.Equals("Descrizione"))
            {
                DBEntity.Update<TipoBiglietto>("idTipoBiglietto", (cmb_tipoBiglietti_selezionaBiglietto.SelectedItem as TipoBiglietto).idTipoBiglietto, "Nome", txt_tipoBiglietti_nuovoNome.Text, "Prezzo", prezzo, "Descrizione", txt_tipoBiglietti_nuovaDescrizione.Text, "idMuseo", museoSelezionato.idMuseo);
                MessageBox.Show("Tipo di biglietto modificato correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                lsv_tipiBiglietti.ItemsSource = DBObject<TipoBiglietto>.SelectAll().Where(tb => tb.idMuseo == museoSelezionato.idMuseo);
            }
            else
                MessageBox.Show("Qualche parametro non è stato compilato correttamente!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        /// <summary>
        /// Aggiunta contenuto
        /// </summary>
        private void btn_contenuti_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            DateTime r = new DateTime(), a = new DateTime();
            if (!(cmb_contenuti_sezione.SelectedItem is null) &&
                !(cmb_contenuti_provenienza.SelectedItem is null) &&
                !(cmb_contenuti_creatore.SelectedItem is null) &&
                !(cmb_contenuti_periodoStorico.SelectedItem is null) &&
                txt_contenuti_nome.Text != "" && txt_contenuti_nome.Text != "Nome" &&
                txt_contenuti_descrizione.Text != "" && txt_contenuti_descrizione.Text != "Descrizione" &&
                DateTime.TryParse(txt_contenuti_dataRitrovamento.Text, out r) &&
                DateTime.TryParse(txt_contenuti_dataArrivo.Text, out a))
            {
                Sezione s = cmb_contenuti_sezione.SelectedItem as Sezione;
                Provenienza p = cmb_contenuti_provenienza.SelectedItem as Provenienza;
                Creatore c = cmb_contenuti_creatore.SelectedItem as Creatore;
                PeriodoStorico ps = cmb_contenuti_periodoStorico.SelectedItem as PeriodoStorico;

                string n = txt_contenuti_nome.Text, d = txt_contenuti_descrizione.Text;
                int res = 0;
                if (!(cmb_contenuti_padre.SelectedItem is null))
                {
                    Contenuto cp = cmb_contenuti_padre.SelectedItem as Contenuto;
                    res = DBObject<Contenuto>.Insert("Nome", n, "Descrizione", d, "DataRitrovamento", r.Date.ToString("yyyy-MM-dd"), "DataArrivoMuseo", a.Date.ToString("yyyy-MM-dd"), "idContenutoPadre", cp.idContenuto, "idProvenienza", p.idProvenienza, "idPeriodoStorico", ps.idPeriodoStorico, "idSezione", s.idSezione);
                    if (checkQueryResult(res))
                        MessageBox.Show("Contenuto inserito correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    res = DBObject<Contenuto>.Insert("Nome", n, "Descrizione", d, "DataRitrovamento", r.Date.ToString("yyyy-MM-dd"), "DataArrivoMuseo", a.Date.ToString("yyyy-MM-dd"), "idProvenienza", p.idProvenienza, "idPeriodoStorico", ps.idPeriodoStorico, "idSezione", s.idSezione);
                    if (checkQueryResult(res))
                        MessageBox.Show("Contenuto inserito correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                DBObject<Museo_Creatore>.Insert("idCreatore", c.idCreatore, "idMuseo", museoSelezionato.idMuseo);
                DBObject<Creato>.Insert("idCreatore", c.idCreatore, "idContenuto", res);
                DBObject<Museo_PeriodoStorico>.Insert("idPeriodoStorico", ps.idPeriodoStorico, "idMuseo", museoSelezionato.idMuseo);
                DBObject<Museo_Provenienza>.Insert("idProvenienza", p.idProvenienza);
            }
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
            cmb_museoCreazione_tipoMuseo.ItemsSource = DBObject<TipoMuseo>.SelectAll();
            cmb_museoCreazione_tipoMuseo.DisplayMemberPath = "Descrizione";
        }

        private void cmb_museo_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_museo_selezionaMuseo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_museo_selezionaMuseo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista dei musei esistenti.
        /// </summary>
        private void cmb_famigliaMusei_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            List<Museo> lm = new List<Museo>(DBObject<Museo>.SelectAll().Where(m => m.idFamiglia == 0));
            cmb_famigliaMusei_selezionaMuseo.ItemsSource = lm;
            cmb_famigliaMusei_selezionaMuseo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista delle famiglie esistenti.
        /// </summary>
        private void cmb_famigliaMusei_selezionaFamiglia_DropDownOpened(object sender, EventArgs e)
        {
            List<FamigliaMusei> lfm = new List<FamigliaMusei>(DBObject<FamigliaMusei>.SelectAll());
            cmb_famigliaMusei_selezionaFamiglia.ItemsSource = lfm;
            cmb_famigliaMusei_selezionaFamiglia.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista delle famiglie esistenti.
        /// </summary>
        private void cmb_famigliaMusei_rimuoviMuseo_famiglia_DropDownOpened(object sender, EventArgs e)
        {
            List<FamigliaMusei> lfm = new List<FamigliaMusei>(DBObject<FamigliaMusei>.SelectAll());
            cmb_famigliaMusei_rimuoviMuseo_famiglia.ItemsSource = lfm;
            cmb_famigliaMusei_rimuoviMuseo_famiglia.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista dei musei esistenti collegati alla famiglia.
        /// </summary>
        private void cmb_famigliaMusei_rimuoviMuseo_DropDownOpened(object sender, EventArgs e)
        {
            FamigliaMusei fm = cmb_famigliaMusei_rimuoviMuseo_famiglia.SelectedItem as FamigliaMusei;
            if (fm != null)
            {
                List<Museo> lm = new List<Museo>(DBObject<Museo>.SelectAll().Where(m => m.idFamiglia == fm.idFamiglia));
                cmb_famigliaMusei_rimuoviMuseo.ItemsSource = lm;
                cmb_famigliaMusei_rimuoviMuseo.DisplayMemberPath = "Nome";
            }
        }

        /// <summary>
        /// Lista dei musei dei quali aggiungere un nuovo tipo.
        /// </summary>
        private void cmb_museoCreazione_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_museoCreazione_selezionaMuseo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_museoCreazione_selezionaMuseo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista dei tipi disponibili da aggiungere ad un museo.
        /// </summary>
        private void cmb_museoCreazione_selezionaTipo_DropDownOpened(object sender, EventArgs e)
        {
            Museo museoSelezionato = cmb_museoCreazione_selezionaMuseo.SelectedItem as Museo;

            if (museoSelezionato == null)
                return;

            List<Museo_Tipologia> tabellaMuseoTipologia = new List<Museo_Tipologia>(DBObject<Museo_Tipologia>.SelectAll());
            List<TipoMuseo> tabellaTipoMuseo = new List<TipoMuseo>(DBObject<TipoMuseo>.SelectAll());
            List<Museo_Tipologia> parzialeMuseoTipologia = new List<Museo_Tipologia>();
            List<TipoMuseo> TipiDisponibili = new List<TipoMuseo>();

            for (int i = 0; i < tabellaMuseoTipologia.Count; i++)
            {
                if (museoSelezionato.idMuseo == tabellaMuseoTipologia[i].idMuseo)
                {
                    parzialeMuseoTipologia.Add(tabellaMuseoTipologia[i]);
                }
            }
            bool ok;

            for (int i = 0; i < tabellaTipoMuseo.Count; i++)
            {
                ok = false;

                for (int j = 0; j < parzialeMuseoTipologia.Count; j++)
                {
                    if (tabellaTipoMuseo[i].idTipoMuseo == parzialeMuseoTipologia[j].idTipoMuseo)
                        ok = true;
                }
                if (!ok)
                    TipiDisponibili.Add(tabellaTipoMuseo[i]);
            }
            cmb_museoCreazione_selezionaTipo.ItemsSource = TipiDisponibili;
            cmb_museoCreazione_selezionaTipo.DisplayMemberPath = "Descrizione";
        }

        /// <summary>
        /// Lista dei periodi storici da eliminare
        /// </summary>
        private void cmb_periodoStorico_elimina_DropDownOpened(object sender, EventArgs e)
        {
            List<PeriodoStorico> lps = DBObject<PeriodoStorico>.SelectAll();
            List<Museo_PeriodoStorico> lmps = DBObject<Museo_PeriodoStorico>.SelectAll();
            cmb_periodoStorico_elimina.ItemsSource = lps.Where(ps => !lmps.Any(mps => mps.idPeriodoStorico == ps.idPeriodoStorico));
            cmb_periodoStorico_elimina.DisplayMemberPath = "Nome";
        }

        private void cmb_eliminaTipo_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_eliminaTipo_selezionaMuseo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_eliminaTipo_selezionaMuseo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Evento per l'eliminazione di un tipo (DI MUSEO) da un museo.
        /// Mostra solamente i tipi che sono stati assegnati al museo interessato e che possono, quindi, essere eliminati.
        /// </summary>
        private void cmb_eliminaTipo_selezionaTipo_DropDownOpened(object sender, EventArgs e)
        {
            Museo museoSelezionato = cmb_eliminaTipo_selezionaMuseo.SelectedItem as Museo;

            if (museoSelezionato == null)
                return;

            List<Museo_Tipologia> tabellaMuseoTipologia = new List<Museo_Tipologia>(DBObject<Museo_Tipologia>.SelectAll());
            List<TipoMuseo> tabellaTipoMuseo = new List<TipoMuseo>(DBObject<TipoMuseo>.SelectAll());
            List<Museo_Tipologia> parzialeMuseoTipologia = new List<Museo_Tipologia>();
            List<TipoMuseo> TipiDisponibili = new List<TipoMuseo>();

            for (int i = 0; i < tabellaMuseoTipologia.Count; i++)
            {
                if (museoSelezionato.idMuseo == tabellaMuseoTipologia[i].idMuseo)
                {
                    parzialeMuseoTipologia.Add(tabellaMuseoTipologia[i]);
                }
            }
            bool ok;

            for (int i = 0; i < tabellaTipoMuseo.Count; i++)
            {
                ok = false;

                for (int j = 0; j < parzialeMuseoTipologia.Count; j++)
                {
                    if (tabellaTipoMuseo[i].idTipoMuseo == parzialeMuseoTipologia[j].idTipoMuseo)
                        ok = true;
                }
                if (ok)
                    TipiDisponibili.Add(tabellaTipoMuseo[i]);
            }
            cmb_eliminaTipo_selezionaTipo.ItemsSource = TipiDisponibili;
            cmb_eliminaTipo_selezionaTipo.DisplayMemberPath = "Descrizione";
        }

        private void cmb_museoCreazione_eliminaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_museoCreazione_eliminaMuseo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_museoCreazione_eliminaMuseo.DisplayMemberPath = "Nome";
        }

        private void cmb_creatore_rimuovi_DropDownOpened(object sender, EventArgs e)
        {
            cmb_creatore_rimuovi.ItemsSource = DBObject<Creatore>.SelectAll();
        }

        /// <summary>
        /// Lista di possibili aperture speciali da eliminare per museo
        /// </summary>
        private void cmb_aperturaSpeciale_elimina_DropDownOpened(object sender, EventArgs e)
        {
            Museo m = cmb_museo_selezionaMuseo.SelectedItem as Museo;
            if (!(m is null))
            {
                cmb_aperturaSpeciale_elimina.ItemsSource = DBObject<CalendarioApertureSpeciali>.Select("idMuseo", m.idMuseo);
                cmb_aperturaSpeciale_elimina.DisplayMemberPath = "Data";
                cmb_aperturaSpeciale_elimina.ItemStringFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Lista di possibili chiusure da eliminare per museo
        /// </summary>
        private void cmb_chiusura_elimina_DropDownOpened(object sender, EventArgs e)
        {
            Museo m = cmb_museo_selezionaMuseo.SelectedItem as Museo;
            if (!(m is null))
            {
                cmb_chiusura_elimina.ItemsSource = DBObject<CalendarioChiusure>.Select("idMuseo", m.idMuseo);
                cmb_chiusura_elimina.DisplayMemberPath = "Data";
                cmb_chiusura_elimina.ItemStringFormat = "yyyy-MM-dd";
            }
        }

        private void cmb_provenienza_rimuovi_DropDownOpened(object sender, EventArgs e)
        {
            cmb_provenienza_rimuovi.ItemsSource = DBObject<Provenienza>.SelectAll();
            cmb_provenienza_rimuovi.DisplayMemberPath = "Nome";
        }

        private void cmb_sezioni_tipoSezione_DropDownOpened(object sender, EventArgs e)
        {
            cmb_sezioni_tipoSezione.ItemsSource = DBObject<TipoSezione>.SelectAll();
            cmb_sezioni_tipoSezione.DisplayMemberPath = "Descrizione";
        }

        private void cmb_sezioni_padre_DropDownOpened(object sender, EventArgs e)
        {
            cmb_sezioni_padre.ItemsSource = DBObject<Sezione>.SelectAll().Where(s => s.idMuseo == this.museoSelezionato.idMuseo);
            cmb_sezioni_padre.DisplayMemberPath = "Nome";
        }

        private void cmb_sezioni_selezionaSezione_DropDownOpened(object sender, EventArgs e)
        {
            cmb_sezioni_selezionaSezione.ItemsSource = DBObject<Sezione>.SelectAll().Where(s => s.idMuseo == this.museoSelezionato.idMuseo);
            cmb_sezioni_selezionaSezione.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Evento per mostrare solamente i tipi non assegnati ad una sezione.
        /// Ricordando che i tipi sono N a N, verranno mostrati tutti tranne quelli non assegnati, indipendentemente, quindi, dal museo.
        /// </summary>
        private void cmb_sezioni_modificaTipo_DropDownOpened(object sender, EventArgs e)
        {
            if (cmb_sezioni_selezionaSezione.SelectedIndex == -1)
                return;

            List<Sezione_Tipologia> tabellaSezioneTipologia = new List<Sezione_Tipologia>(DBObject<Sezione_Tipologia>.SelectAll());
            List<TipoSezione> tabellaTipoSezione = new List<TipoSezione>(DBObject<TipoSezione>.SelectAll());
            List<Sezione_Tipologia> parzialeSezioneTipologia = new List<Sezione_Tipologia>();
            List<TipoSezione> TipiDisponibili = new List<TipoSezione>();

            for (int i = 0; i < tabellaSezioneTipologia.Count; i++)
            {
                if (((Sezione)cmb_sezioni_selezionaSezione.SelectedItem).idSezione == tabellaSezioneTipologia[i].idSezione)
                {
                    parzialeSezioneTipologia.Add(tabellaSezioneTipologia[i]);
                }
            }
            bool ok;

            for (int i = 0; i < tabellaTipoSezione.Count; i++)
            {
                ok = false;

                for (int j = 0; j < parzialeSezioneTipologia.Count; j++)
                {
                    if (tabellaTipoSezione[i].idTipoSezione == parzialeSezioneTipologia[j].idTipoSezione)
                        ok = true;
                }
                if (!ok)
                    TipiDisponibili.Add(tabellaTipoSezione[i]);
            }
            cmb_sezioni_modificaTipo.ItemsSource = TipiDisponibili;
            cmb_sezioni_modificaTipo.DisplayMemberPath = "Descrizione";
        }

        private void cmb_sezioni_modificaPadre_DropDownOpened(object sender, EventArgs e)
        {
            if (cmb_sezioni_selezionaSezione.SelectedIndex != -1)
            {
                cmb_sezioni_modificaPadre.ItemsSource = DBObject<Sezione>.SelectAll().Where(s => s.idMuseo == this.museoSelezionato.idMuseo);
                cmb_sezioni_modificaPadre.DisplayMemberPath = "Nome";
            }
        }


        //Sezione eventDropDown di Contenuti
        private void cmb_contenuti_filtroSezione_DropDownOpened(object sender, EventArgs e)
        {
            cmb_contenuti_filtroSezione.ItemsSource = DBObject<Sezione>.Select("idMuseo", museoSelezionato.idMuseo);
            cmb_contenuti_filtroSezione.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_filtroProvenienza_DropDownOpened(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT Provenienza.* FROM Provenienza INNER JOIN Museo_Provenienza ON Provenienza.idProvenienza = Museo_Provenienza.idProvenienza WHERE Museo_Provenienza.idMuseo = @idMuseo;");
            sqlCommand.Parameters.AddWithValue("@idMuseo", museoSelezionato.idMuseo);
            cmb_contenuti_filtroSezione.ItemsSource = DBObject<Sezione>.CustomSelect(sqlCommand);
            cmb_contenuti_filtroSezione.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_filtroCreatore_DropDownOpened(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT Creatore.* FROM Creatore INNER JOIN Museo_Creatore ON Creatore.idCreatore = Museo_Creatore.idCreatore WHERE Museo_Creatore.idMuseo = @idMuseo;");
            sqlCommand.Parameters.AddWithValue("@idMuseo", museoSelezionato.idMuseo);
            cmb_contenuti_filtroCreatore.ItemsSource = DBObject<Creatore>.CustomSelect(sqlCommand);
            cmb_contenuti_filtroCreatore.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_filtroPeriodoStorico_DropDownOpened(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("SELECT PeriodoStorico.* FROM PeriodoStorico INNER JOIN Museo_PeriodoStorico ON PeriodoStorico.idPeriodoStorico = Museo_PeriodoStorico.idPeriodoStorico WHERE Museo_PeriodoStorico.idMuseo = @idMuseo;");
            sqlCommand.Parameters.AddWithValue("@idMuseo", museoSelezionato.idMuseo);
            cmb_contenuti_filtroPeriodoStorico.ItemsSource = DBObject<PeriodoStorico>.CustomSelect(sqlCommand);
            cmb_contenuti_filtroPeriodoStorico.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_filtroTipoContenuto_DropDownOpened(object sender, EventArgs e)
        {
            cmb_contenuti_filtroTipoContenuto.ItemsSource = DBObject<TipoContenuto>.SelectAll();
            cmb_contenuti_filtroTipoContenuto.DisplayMemberPath = "Descrizione";
        }

        private void cmb_contenuti_sezione_DropDownOpened(object sender, EventArgs e)
        {
            cmb_contenuti_sezione.ItemsSource = DBObject<Sezione>.Select("idMuseo", museoSelezionato.idMuseo);
            cmb_contenuti_sezione.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_provenienza_DropDownOpened(object sender, EventArgs e)
        {
            cmb_contenuti_provenienza.ItemsSource = DBObject<Provenienza>.SelectAll();
            cmb_contenuti_provenienza.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_creatore_DropDownOpened(object sender, EventArgs e)
        {
            cmb_contenuti_creatore.ItemsSource = DBObject<Creatore>.SelectAll();
            cmb_contenuti_creatore.DisplayMemberPath = "{Binding}";
        }

        private void cmb_contenuti_periodoStorico_DropDownOpened(object sender, EventArgs e)
        {
            cmb_contenuti_periodoStorico.ItemsSource = DBObject<PeriodoStorico>.SelectAll();
            cmb_contenuti_periodoStorico.DisplayMemberPath = "Nome";
        }

        private void cmb_contenuti_padre_DropDownOpened(object sender, EventArgs e)
        {
            if (!(cmb_contenuti_sezione.SelectedItem is null))
            {
                cmb_contenuti_padre.ItemsSource = DBObject<Contenuto>.Select("idSezione", (cmb_contenuti_sezione.SelectedItem as Sezione).idSezione);
                cmb_contenuti_padre.DisplayMemberPath = "Nome";
            }

        }

        private void cmb_contenuti_elimina_DropDownOpened(object sender, EventArgs e)
        {
            if (!(cmb_contenuti_sezione.SelectedItem is null))
            {
                cmb_contenuti_elimina.ItemsSource = DBObject<Contenuto>.Select("idSezione", (cmb_contenuti_sezione.SelectedItem as Sezione).idSezione);
                cmb_contenuti_elimina.DisplayMemberPath = "Nome";
            }
        }

        private void cmb_addTipoContenuto_contenuto_DropDownOpened(object sender, EventArgs e)
        {
            if (!(cmb_contenuti_sezione.SelectedItem is null))
            {
                cmb_addTipoContenuto_contenuto.ItemsSource = DBObject<Contenuto>.Select("idSezione", (cmb_contenuti_sezione.SelectedItem as Sezione).idSezione);
                cmb_addTipoContenuto_contenuto.DisplayMemberPath = "Nome";
            }
        }

        private void cmb_delTipoContenuto_contenuto_DropDownOpened(object sender, EventArgs e)
        {
            if (!(cmb_contenuti_sezione.SelectedItem is null))
            {
                cmb_delTipoContenuto_contenuto.ItemsSource = DBObject<Contenuto>.Select("idSezione", (cmb_contenuti_sezione.SelectedItem as Sezione).idSezione);
                cmb_delTipoContenuto_contenuto.DisplayMemberPath = "Nome";
            }
        }

        private void cmb_addTipoContenuto_tipo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_addTipoContenuto_tipo.ItemsSource = DBObject<TipoContenuto>.SelectAll();
            cmb_addTipoContenuto_tipo.DisplayMemberPath = "Descrizione";
        }

        private void cmb_delTipoContenuto_tipo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_delTipoContenuto_tipo.ItemsSource = DBObject<TipoContenuto>.SelectAll();
            cmb_delTipoContenuto_tipo.DisplayMemberPath = "Descrizione";
        }




        private void cmb_sezioni_elimina_DropDownOpened(object sender, EventArgs e)
        {
            //cmb_sezioni_elimina.ItemsSource = DBObject<Sezione>.SelectAll().Where(s => s.idSezionePadre != 0 || s.idSezionePadre != s.idSezionePadre); //METODO SBAGLIATO PERCHE' DEVE PRENDERE SOLAMENTE GLI ULTIMI FIGLI DELL'ALBERO
            cmb_sezioni_elimina.DisplayMemberPath = "Nome";
        }

        private void cmb_tipoBiglietti_selezionaBiglietto_DropDownOpened(object sender, EventArgs e)
        {
            cmb_tipoBiglietti_selezionaBiglietto.ItemsSource = DBObject<TipoBiglietto>.SelectAll().Where(tb => tb.idMuseo == museoSelezionato.idMuseo);
            cmb_tipoBiglietti_selezionaBiglietto.DisplayMemberPath = "Nome";
        }

        //Eventi SelectionChanged

        /// <summary>
        /// Evento per la selezione del museo da visualizzare.
        /// Attiva ogni window relativa al museo.
        /// </summary>
        private void cmb_museo_selezionaMuseo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_museo_selezionaMuseo.SelectedIndex != -1)
            {
                museoSelezionato = (Museo)cmb_museo_selezionaMuseo.SelectedItem;
                lbl_museo_nomeMuseo.Content = museoSelezionato.Nome;
                tbi_calendario.IsEnabled = true;
                tbi_biglietti.IsEnabled = true;
                tbi_contenuti.IsEnabled = true;
                tbi_personale.IsEnabled = true;
                tbi_registri.IsEnabled = true;
                tbi_statistiche.IsEnabled = true;
                gpb_riepilogo.IsEnabled = true;
                gpb_sezioni.IsEnabled = true;
                gpb_categoriaSezione.IsEnabled = true;
                gpb_orari.IsEnabled = true;
                setMuseumStatus();
                setMuseumSchedule();
                setMuseumFamily();
                setMuseumTypes();
                setMuseumAreas();



            }
            else
            {
                museoSelezionato = null;
                tbi_calendario.IsEnabled = false;
                tbi_biglietti.IsEnabled = false;
                tbi_contenuti.IsEnabled = false;
                tbi_personale.IsEnabled = false;
                tbi_registri.IsEnabled = false;
                tbi_statistiche.IsEnabled = false;
                gpb_riepilogo.IsEnabled = true;
                gpb_sezioni.IsEnabled = false;
                gpb_categoriaSezione.IsEnabled = false;
                gpb_orari.IsEnabled = false;
                lsv_riepilogo_sezioni.ItemsSource = null;
                lsv_riepilogo_sottosezioni.ItemsSource = null;
                lbl_museo_nomeMuseo.Content = "";
                lbl_riepilogo_statoApertura.Content = "";
                lbl_riepilogo_orarioApertura.Content = "";
                lbl_riepilogo_orarioChiusura.Content = "";
                lbl_riepilogo_valoreFamiglia.Content = "";
                lsv_riepilogo_tipiMuseo.Items.Clear();

            }
        }

        /// <summary>
        /// Evento per cancellare la lista di tipi disponibili da aggiungere se il museo viene cambiato.
        /// </summary>
        private void cmb_museoCreazione_selezionaMuseo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_museoCreazione_selezionaTipo.ItemsSource = null;
        }

        /// <summary>
        /// Evento per cancellare la lista di tipi disponibili da cancellare se il museo viene cambiato.
        /// </summary>
        private void cmb_eliminaTipo_selezionaMuseo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_eliminaTipo_selezionaTipo.ItemsSource = null;
        }
         
        /// <summary>
        /// Opzioni generali per quando si cambia tab.
        /// </summary>
        private void tab_finestre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(e.Source is null) && e.Source.GetType().Equals(typeof(TabControl)))
            {
                TabControl tabControl = e.Source as TabControl;

                if (!(tabControl.SelectedItem is null))
                {
                    TabItem tabItem = tabControl.SelectedItem as TabItem;
                    if (tabItem.Header.Equals("Calendario") && !(this.museoSelezionato is null))
                    {
                        List<CalendarioApertureSpeciali> lcas = DBObject<CalendarioApertureSpeciali>.Select("idMuseo", this.museoSelezionato.idMuseo);
                        ObservableCollection<CalendarioApertureSpeciali> calendarioApertureSpeciali = new ObservableCollection<CalendarioApertureSpeciali>(lcas);

                        calendarioApertureSpeciali.CollectionChanged += (s, eventArgs) =>
                        {

                            switch (eventArgs.Action)
                            {
                                case NotifyCollectionChangedAction.Remove:
                                    {
                                        List<CalendarioApertureSpeciali> casRmv = new List<CalendarioApertureSpeciali>(eventArgs.OldItems.Cast<CalendarioApertureSpeciali>());
                                        casRmv.ForEach(cas => DBEntity.Delete<CalendarioApertureSpeciali>("idCalendarioApertureSpeciali", cas.idCalendarioApertureSpeciali));
                                        break;
                                    }
                                case NotifyCollectionChangedAction.Add:
                                    {
                                        List<CalendarioApertureSpeciali> casAdd = new List<CalendarioApertureSpeciali>(eventArgs.NewItems.Cast<CalendarioApertureSpeciali>());
                                        casAdd.ForEach(cas => cas.idMuseo = museoSelezionato.idMuseo);
                                        casAdd.ForEach(cas => DBObject<CalendarioApertureSpeciali>.Insert("Data", cas.Data, "OrarioApertura", cas.OrarioApertura, "OrarioChiusura", cas.OrarioChiusura, "NumBigliettiMax", cas.NumBigliettiMax, "idMuseo", cas.idMuseo));
                                        break;
                                    }
                            }
                        };
                        dtg_giornateAperturaSpeciale.DataContext = calendarioApertureSpeciali;
                        dtg_giornateAperturaSpeciale.CellEditEnding += (s, eventArgs) =>
                        {
                            CalendarioApertureSpeciali cas = eventArgs.Row.Item as CalendarioApertureSpeciali;
                            DBEntity.Update<CalendarioApertureSpeciali>("idCalendarioApertureSpeciali", cas.idCalendarioApertureSpeciali, eventArgs.Column.Header.ToString(), (eventArgs.EditingElement as TextBox).Text);
                        };


                        List<CalendarioChiusure> lcc = DBObject<CalendarioChiusure>.Select("idMuseo", this.museoSelezionato.idMuseo);
                        ObservableCollection<CalendarioChiusure> calendarioChiusure = new ObservableCollection<CalendarioChiusure>(lcc);

                        calendarioChiusure.CollectionChanged += (s, eventArgs) =>
                        {
                            switch (eventArgs.Action)
                            {
                                case NotifyCollectionChangedAction.Remove:
                                    {
                                        List<CalendarioChiusure> ccRmv = new List<CalendarioChiusure>(eventArgs.OldItems.Cast<CalendarioChiusure>());
                                        ccRmv.ForEach(cc => DBEntity.Delete<CalendarioChiusure>("idCalendarioChiusure", cc.idCalendarioChiusure));
                                        break;
                                    }
                                case NotifyCollectionChangedAction.Add:
                                    {
                                        List<CalendarioChiusure> ccAdd = new List<CalendarioChiusure>(eventArgs.NewItems.Cast<CalendarioChiusure>());
                                        ccAdd.ForEach(cc => cc.idMuseo = museoSelezionato.idMuseo);
                                        ccAdd.ForEach(cc => DBObject<CalendarioChiusure>.Insert("Data", cc.Data, "idMuseo", cc.idMuseo));
                                        break;
                                    }
                            }
                        };
                        dtg_giornateChiusura.DataContext = calendarioChiusure;
                        dtg_giornateChiusura.CellEditEnding += (s, eventArgs) =>
                        {
                            CalendarioChiusure cc = eventArgs.Row.Item as CalendarioChiusure;
                            DBEntity.Update<CalendarioChiusure>("idCalendarioChiusure", cc.idCalendarioChiusure, eventArgs.Column.Header.ToString(), (eventArgs.EditingElement as TextBox).Text);
                        };
                    }
                    if (tabItem.Header.Equals("Museo") && !(this.museoSelezionato is null))
                    {
                        museoSelezionato = DBObject<Museo>.Select("idMuseo", museoSelezionato.idMuseo).First();
                        setMuseumStatus();
                        setMuseumSchedule();
                        setMuseumFamily();
                        setMuseumTypes();
                        setMuseumAreas();
                    }
                    if (tabItem.Header.Equals("Biglietti"))
                    {
                        lsv_tipiBiglietti.ItemsSource = DBObject<TipoBiglietto>.SelectAll().Where(tb => tb.idMuseo == museoSelezionato.idMuseo);

                        //Query per ottenere tutte le info sui biglietti comprati.
                        //string sqlCommandString = "SELECT DataValidita, PrezzoAcquisto, Prezzo, Nome, Descrizione FROM Biglietto INNER JOIN TipoBiglietto ON Biglietto.idTipoBiglietto = TipoBiglietto.idTipoBiglietto WHERE Biglietto.idMuseo = " + museoSelezionato.idMuseo;
                        //lsv_bigliettiComprati.ItemsSource = DBObject<Biglietto>.CustomSelect(new SqlCommand(sqlCommandString));
                    }
                }
            }
        }

        /// <summary>
        /// Evento per popolare la listview di sottosezioni alla selezione di una sezione.
        /// </summary>
        private void lsv_riepilogo_sezioni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Sezione> sottosezioniDiSezione = new List<Sezione>();

            if (lsv_riepilogo_sezioni.SelectedIndex != -1)
            {
                List<Sezione> sezioni = new List<Sezione>(DBObject<Sezione>.SelectAll());
                Sezione selezionata = (Sezione)lsv_riepilogo_sezioni.SelectedItem;

                for (int i = 0; i < sezioni.Count; i++)
                {
                    if (sezioni[i].idSezionePadre == selezionata.idSezione)
                    {
                        sottosezioniDiSezione.Add(sezioni[i]);
                    }
                }
                lsv_riepilogo_sottosezioni.ItemsSource = sottosezioniDiSezione;
            }
        }

        /// <summary>
        /// Evento per resettare le combobox di padre e tipo sezione alla selezione di una nuova sezione. 
        /// </summary>
        private void cmb_sezioni_selezionaSezione_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb_sezioni_modificaTipo.ItemsSource = null;
            cmb_sezioni_modificaPadre.ItemsSource = null;
            txt_sezioni_modificaNome.FontStyle = FontStyles.Italic;
            txt_sezioni_modificaNome.Foreground = Brushes.Gray;
            txt_sezioni_modificaDescrizione.FontStyle = FontStyles.Italic;
            txt_sezioni_modificaDescrizione.Foreground = Brushes.Gray;

            if (cmb_sezioni_selezionaSezione.SelectedIndex != -1)
            {          
                txt_sezioni_modificaNome.Text = (cmb_sezioni_selezionaSezione.SelectedItem as Sezione).Nome;
                txt_sezioni_modificaDescrizione.Text = (cmb_sezioni_selezionaSezione.SelectedItem as Sezione).Descrizione;
            }     
            else
            {
                txt_sezioni_modificaNome.Text = "Nome";
                txt_sezioni_modificaDescrizione.Text = "Descrizione";
            }
        }

        private void cmb_tipoBiglietti_selezionaBiglietto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_tipoBiglietti_nuovoNome.FontStyle = FontStyles.Italic;
            txt_tipoBiglietti_nuovoNome.Foreground = Brushes.Gray;
            txt_tipoBiglietti_nuovoPrezzo.FontStyle = FontStyles.Italic;
            txt_tipoBiglietti_nuovoPrezzo.Foreground = Brushes.Gray;
            txt_tipoBiglietti_nuovaDescrizione.FontStyle = FontStyles.Italic;
            txt_tipoBiglietti_nuovaDescrizione.Foreground = Brushes.Gray;

            if (cmb_tipoBiglietti_selezionaBiglietto.SelectedIndex != -1)
            {
                txt_tipoBiglietti_nuovoNome.Text = (cmb_tipoBiglietti_selezionaBiglietto.SelectedItem as TipoBiglietto).Nome;
                txt_tipoBiglietti_nuovoPrezzo.Text = (cmb_tipoBiglietti_selezionaBiglietto.SelectedItem as TipoBiglietto).Prezzo.ToString();
                txt_tipoBiglietti_nuovaDescrizione.Text = (cmb_tipoBiglietti_selezionaBiglietto.SelectedItem as TipoBiglietto).Descrizione;
            }
            else
            {
                txt_tipoBiglietti_nuovoNome.Text = "Nuovo nome";
                txt_tipoBiglietti_nuovoPrezzo.Text = "Nuovo prezzo";
                txt_tipoBiglietti_nuovaDescrizione.Text = "Nuova descrizione";
            }
        }





        private void btn_filtraContenuti_Click(object sender, RoutedEventArgs e)
        {
            string sqlCommandString = "SELECT Contenuto.* FROM Contenuto ";
            string whereString = " WHERE ";

            if(!(cmb_contenuti_filtroSezione.SelectedItem is null))
            {
                whereString += "idSezione = " + (cmb_contenuti_filtroSezione.SelectedItem as Sezione).idSezione;
            }

            if (!(cmb_contenuti_filtroProvenienza.SelectedItem is null))
            {
                if (!whereString.Equals(" WHERE "))
                    whereString += "AND ";
                whereString += "idProvenienza = " + (cmb_contenuti_filtroProvenienza.SelectedItem as Sezione).idSezione;
            }
            if (!(cmb_contenuti_filtroCreatore.SelectedItem is null))
            {
                if (!whereString.Equals(" WHERE "))
                    whereString += "AND ";
                sqlCommandString += "INNER JOIN Creato ON Contenuto.idContenuto = Creato.idContenuto ";
                whereString += "idCreatore = " + (cmb_contenuti_filtroCreatore.SelectedItem as Sezione).idSezione;
            }
            if (!(cmb_contenuti_filtroPeriodoStorico.SelectedItem is null))
            {
                if (!whereString.Equals(" WHERE "))
                    whereString += "AND ";
                whereString += "idPeriodoStorico = " + (cmb_contenuti_filtroPeriodoStorico.SelectedItem as Sezione).idSezione;
            }
            if (!(cmb_contenuti_filtroTipoContenuto.SelectedItem is null))
            {
                if (!whereString.Equals(" WHERE "))
                    whereString += "AND ";
                sqlCommandString += "INNER JOIN Contenuto_Tipologia ON Contenuto.idContenuto = Contenuto_Tipologia.idContenuto ";
                whereString += "idTipoContenuto = " + (cmb_contenuti_filtroTipoContenuto.SelectedItem as Sezione).idSezione;
            }

            List<Contenuto> contenuti = DBObject<Contenuto>.CustomSelect(new SqlCommand(sqlCommandString)),
                padri = contenuti.Where(c => c.idContenutoPadre == 0).ToList(),
                allContenuti = DBObject<Contenuto>.SelectAll();

            Dictionary<int, Contenuto> idPairingContenuti = new Dictionary<int, Contenuto>();
            allContenuti.ForEach(c => idPairingContenuti.Add(c.idContenuto, c));

            Dictionary<Contenuto, List<Contenuto>> padreFigli = new Dictionary<Contenuto, List<Contenuto>>();
            contenuti.ForEach(c => padreFigli.Add(c, cercaFigli(c, idPairingContenuti)));

            List<ContenutoForList> contenutoForLists = new List<ContenutoForList>();
            padri.ForEach(c =>
            {
                contenutoForLists.Add(new ContenutoForList() { Contenuto = c, Figli = new List<Contenuto>(padreFigli[c]) });
            });
            lsv_contenuti.ItemsSource = contenutoForLists;


        }

        /// <summary>
        /// Trova i figli di un contenuto
        /// </summary>
        private List<Contenuto> cercaFigli(Contenuto padre, Dictionary<int, Contenuto> idPairingContenuti)
        {
            List<Contenuto> figli = new List<Contenuto>();
            idPairingContenuti.Keys.ToList().ForEach(k =>
            {
                if(isFiglio(k, padre.idContenuto, idPairingContenuti))
                {
                    figli.Add(idPairingContenuti[k]);
                }
            });
            return figli;
        }

        private bool isFiglio(int idFiglio, int idPadre, Dictionary<int, Contenuto> idPairingContenuti)
        {
            if (idPairingContenuti[idFiglio].idContenutoPadre == 0 || idFiglio == idPadre)
                return false;
            bool isFiglio = false;
            int tmpIdPadre, tmpIdFiglio = idFiglio;
            do
            {
                tmpIdPadre = idPairingContenuti[tmpIdFiglio].idContenutoPadre;
                isFiglio = tmpIdPadre == idPadre;
                tmpIdFiglio = tmpIdPadre;
            } while (tmpIdPadre != 0 && tmpIdPadre != idPadre && !isFiglio);
            return isFiglio;
        }





        /// <summary>
        /// Metodo per l'eliminazione di una sezione.
        /// </summary>
        private void btn_sezioni_elimina_Click(object sender, RoutedEventArgs e)
        {

        }

        

        /// <summary>
        /// Rimozione contenuto
        /// </summary>
        private void btn_contenuti_elimina_Click(object sender, RoutedEventArgs e)
        {
            if(!(cmb_contenuti_elimina is null))
            {
                Contenuto contenuto = cmb_contenuti_elimina.SelectedItem as Contenuto;
                res = DBEntity.Delete<Contenuto>("idContenuto", contenuto.idContenuto);
                if (checkQueryResult(res))
                    MessageBox.Show("Contenuto eliminato con tutti i sottocontenuti!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
