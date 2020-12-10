using Entities;
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
                MessageBox.Show("Errore, operazione non consentita: E\' possibile che l'operazione sia già stata fatta, oppure il sistema non riesce a connettersi al database", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Tipo \""+ ((TipoMuseo)cmb_museoCreazione_selezionaTipo.SelectedItem).Descrizione + "\" assegnato correttamente al museo \"" + ((Museo)cmb_museoCreazione_selezionaMuseo.SelectedItem).Nome + "\"!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
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
            if(cmb_eliminaTipo_selezionaTipo.Items.Count == 1)
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
                //PANTIE DBEntity NON E' GENERICO E STATICO, BISOGNEREBBE CANCELLARLO E METTERE TUTTO IN DBObject SECONDO ME (ma non mi ricordo perchè li avevi divisi sono sincero, appena vedi questo messaggio mandami un'audio). 
                //C'è un pò troppa confusione fra DBEntity, DBObject e DBRelationN2NOnlyIndexes, io lascierei solo questi ultimi due se possibile, ma lascio modificare a te la struttura visto che è tua.
                //BUON LAVORO :) ELIMINA PURE IL COMMENTO UNA VOLTA RISOLTO
                //PS: Quando elimino un museo dovrei andare ad eliminare anche la relazione N a N in Museo_Tipologia? O si elimina da sola teoricamente? AUDIO grazie :) 
                if (checkQueryResult(DBEntity.Delete<Museo>("idMuseo", ((Museo)cmb_museoCreazione_eliminaMuseo.SelectedItem).idMuseo))) 
                {
                    MessageBox.Show("Museo \"" + ((Museo)cmb_eliminaTipo_selezionaMuseo.SelectedItem).Nome + "\" rimosso correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
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
            cmb_famigliaMusei_selezionaMuseo.Items.Clear();
            cmb_famigliaMusei_selezionaFamiglia.Items.Clear();
            int res = (cmb_famigliaMusei_selezionaMuseo.SelectedItem as Museo).Update("idMuseo", (cmb_famigliaMusei_selezionaMuseo.SelectedItem as Museo).idMuseo, "idFamiglia", (cmb_famigliaMusei_selezionaFamiglia.SelectedItem as FamigliaMusei).idFamiglia);
            if (checkQueryResult(res))
                MessageBox.Show("Aggiunto museo alla famiglia musei correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Rimozione di un museo dalla sua famiglia di musei.
        /// </summary>
        private void btn_famigliaMusei_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            cmb_famigliaMusei_rimuoviMuseo_famiglia.Items.Clear();
            cmb_famigliaMusei_rimuoviMuseo.Items.Clear();

            FamigliaMusei fm = cmb_famigliaMusei_rimuoviMuseo_famiglia.SelectedItem as FamigliaMusei;
            Museo m = cmb_famigliaMusei_rimuoviMuseo.SelectedItem as Museo;

            if (fm != null && m != null)
            {
                int res = m.Update("idMuseo", m.idMuseo, "idFamiglia", "NULL");
                if (checkQueryResult(res))
                    MessageBox.Show("Aggiunto museo alla famiglia musei correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Aggiunta periodo storico
        /// </summary>
        private void btn_periodoStorico_inserisci_Click(object sender, RoutedEventArgs e)
        {
            if(txt_periodoStorico_nome.Text != "" && txt_periodoStorico_nome.Text != "Nome" &&
                txt_periodoStorico_annoInizio.Text != "" && txt_periodoStorico_annoInizio.Text != "Anno di inizio" &&
                txt_periodoStorico_annoFine.Text != "" && txt_periodoStorico_annoFine.Text != "Anno di fine" &&
                txt_periodoStorico_descrizione.Text != "" && txt_periodoStorico_descrizione.Text != "Descrizione" &&
                !(cmb_periodoStorico_nelmuseo.SelectedItem is null))
            {
                int res = DBObject<PeriodoStorico>.Insert("Nome", txt_periodoStorico_nome.Text, "AnnoInizio", txt_periodoStorico_annoInizio.Text, "AnnoFine", txt_periodoStorico_annoFine.Text, "Descrizione", txt_periodoStorico_descrizione.Text);
                if (checkQueryResult(res))
                    MessageBox.Show("Aggiunto periodo storico correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                int res2 = DBObject<Museo_PeriodoStorico>.Insert("idMuseo", (cmb_periodoStorico_nelmuseo.SelectedItem as Museo).idMuseo, "idPeriodoStorico", res);
                if (checkQueryResult(res))
                    MessageBox.Show("Aggiunto periodo storico al museo correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);

                txt_periodoStorico_nome.Clear();
                txt_periodoStorico_annoInizio.Clear();
                txt_periodoStorico_annoFine.Clear();
                txt_periodoStorico_descrizione.Clear();
                cmb_periodoStorico_nelmuseo.Items.Clear();
            }
        }

        /// <summary>
        /// Rimozione periodo storico
        /// </summary>
        private void btn_periodoStorico_elimina_Click(object sender, RoutedEventArgs e)
        {
            if(!(cmb_periodoStorico_museo.SelectedItem is null) && !(cmb_periodoStorico_elimina.SelectedItem is null))
            {
                int res = DBRelationN2NOnlyIndexes<Museo_PeriodoStorico>.Delete("idMuseo", (cmb_periodoStorico_museo.SelectedItem as Museo).idMuseo, "idPeriodoStorico", (cmb_periodoStorico_elimina.SelectedItem as PeriodoStorico).idPeriodoStorico);
                if (checkQueryResult(res))
                    MessageBox.Show("Eliminato periodo storico dal museo correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);

                if(!DBObject<Museo_PeriodoStorico>.SelectAll().Any(mps => mps.idPeriodoStorico == (cmb_periodoStorico_elimina.SelectedItem as PeriodoStorico).idPeriodoStorico))
                {
                    res = DBEntity.Delete<PeriodoStorico>("idPeriodoStorico", (cmb_periodoStorico_elimina.SelectedItem as PeriodoStorico).idPeriodoStorico);
                    if (checkQueryResult(res))
                        MessageBox.Show("Eliminato periodo storico totalmente dal db correttamente!", "Operazione eseguita", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                cmb_periodoStorico_museo.Items.Clear();
                cmb_periodoStorico_elimina.Items.Clear();
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

            for(int i = 0; i < tabellaTipoMuseo.Count; i++)
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
        /// Lista musei da selezionare per l'inserimento del periodo storico
        /// </summary>
        private void cmb_periodoStorico_nelmuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_periodoStorico_nelmuseo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_periodoStorico_nelmuseo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista musei da selezionare per l'eliminazione del periodo storico
        /// </summary>
        private void cmb_periodoStorico_museo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_periodoStorico_museo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_periodoStorico_museo.DisplayMemberPath = "Nome";
        }

        /// <summary>
        /// Lista dei periodi storici da eliminare
        /// </summary>
        private void cmb_periodoStorico_elimina_DropDownOpened(object sender, EventArgs e)
        {
            if(!(cmb_periodoStorico_museo.SelectedItem is null))
            {
                int idMuseo = (cmb_periodoStorico_museo.SelectedItem as Museo).idMuseo;
                List<Museo_PeriodoStorico> lmps = DBObject<Museo_PeriodoStorico>.SelectAll().Where(mps => mps.idMuseo == idMuseo).ToList();
                cmb_periodoStorico_elimina.ItemsSource = DBObject<PeriodoStorico>.SelectAll().Where(ps => lmps.Any(mps => mps.idPeriodoStorico ==  ps.idPeriodoStorico));
                cmb_periodoStorico_elimina.DataContext = "Nome";
            }
        }

        private void cmb_eliminaTipo_selezionaMuseo_DropDownOpened(object sender, EventArgs e)
        {
            cmb_eliminaTipo_selezionaMuseo.ItemsSource = DBObject<Museo>.SelectAll();
            cmb_eliminaTipo_selezionaMuseo.DisplayMemberPath = "Nome";
        }

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

        //Eventi SelectionChanged

        /// <summary>
        /// Evento per la selezione del museo da visualizzare.
        /// Attiva ogni window relativa al museo.
        /// </summary>
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
                idMuseoSelezionato = ((Museo)cmb_museo_selezionaMuseo.SelectedItem).idMuseo;
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

        private void btn_periodoStorico_elimina_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
