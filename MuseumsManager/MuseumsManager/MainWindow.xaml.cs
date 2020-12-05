using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace MuseumsManager
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int idMuseoSelezionato = 0;

        /**
        void insertIntoTest()
        {
            string queryString = "INSERT INTO TipoMuseo (Descrizione) VALUES (@Descrizione)";
            string connectionString = "Data Source=ARTOLINK\\SQLEXPRESS;Initial Catalog=MuseumsManagerDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@Descrizione", "ciao");
                connection.Open();

                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0)
                    Console.WriteLine("Error inserting data into Database!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            insertIntoTest();
        }
        */
    }
}
