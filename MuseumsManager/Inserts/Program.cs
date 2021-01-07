using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Inserts
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Museo> lm = DBObject<Museo>.SelectAll();
            List<FamigliaMusei> lfm = DBObject<FamigliaMusei>.SelectAll();
            List<DateTime> lma = new List<DateTime>();

            for (int i = 0; i < 12; i++)
            {
                DateTime tmp = new DateTime(2020, i+1, 1);
                lma.Add(tmp);
            }
            lma.ForEach(dt =>
            {
                lm.ForEach(m => 
                {
                    //Statistiche Museo
                    {
                        Statistiche statistiche = new Statistiche();
                        //Numero di biglietti venduti
                        string numBiglietti = "SELECT COUNT(*) AS numBiglietti " +
                            "FROM Biglietto " +
                            "WHERE idMuseo = " + m.idMuseo + " AND MONTH(DataValidita) = " + dt.Month + " AND YEAR(DataValidita) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numBiglietti, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numBiglietti"]))
                                        statistiche.NumBigliettiVenduti = (int)sqlDataReader["numBiglietti"];
                                }
                            }
                        }

                        //Numero di manutenzioni svolte
                        string numManutenzioni = "SELECT COUNT(*) AS numManutenzioni " +
                            "FROM RegistroManutenzioni " +
                            "WHERE idMuseo = " + m.idMuseo + " AND MONTH(Data) = " + dt.Month + " AND YEAR(Data) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numManutenzioni, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numManutenzioni"]))
                                        statistiche.NumManutenzioni = (int)sqlDataReader["numManutenzioni"];
                                }
                            }
                        }

                        //Numero di nuovi contenuti aggiunti
                        string numNuoviContenuti = "SELECT COUNT(*) AS numNuoviContenuti " +
                            "FROM Contenuto INNER JOIN Sezione ON Contenuto.idSezione = Sezione.idSezione " +
                            "WHERE idMuseo = " + m.idMuseo + " AND MONTH(DataArrivoMuseo) = " + dt.Month + " AND YEAR(DataArrivoMuseo) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numNuoviContenuti, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numNuoviContenuti"]))
                                        statistiche.NumContenutiNuovi = (int)sqlDataReader["numNuoviContenuti"];
                                }
                            }
                        }

                        //Numero di giorni di chiusura totali
                        string numGiorniChiusura = "SELECT COUNT(*) AS numGiorniChiusura " +
                            "FROM CalendarioChiusure " +
                            "WHERE idMuseo = " + m.idMuseo + " AND MONTH(Data) = " + dt.Month + " AND YEAR(Data) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numGiorniChiusura, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numGiorniChiusura"]))
                                        statistiche.NumChiusure = (int)sqlDataReader["numGiorniChiusura"];
                                }
                            }
                        }

                        //Spese totali
                        string speseTotali = "SELECT SUM(Spese) AS speseTotali " +
                            "FROM ((SELECT SUM(Prezzo) AS Spese FROM RegistroManutenzioni WHERE idMuseo = " + m.idMuseo + " AND MONTH(Data) = " + dt.Month + " AND YEAR(Data) = " + dt.Year + ") " +
                            "UNION (" +
                            "SELECT SUM(StipendioOra * DATEDIFF(hour, DataEntrata, DataUscita)) AS Spese FROM Personale INNER JOIN RegistroPresenze ON Personale.idPersonale = RegistroPresenze.idPersonale WHERE idMuseo = " + m.idMuseo + " AND (MONTH(DataEntrata) = " + dt.Month + " AND YEAR(DataEntrata) = " + dt.Year + " OR MONTH(DataUscita) = " + dt.Month + " AND YEAR(DataUscita) = " + dt.Year + " ))) AS Spese";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(speseTotali, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["speseTotali"]))
                                        statistiche.SpeseTotali = (double)sqlDataReader["speseTotali"];
                                }
                            }
                        }

                        //Fatturato
                        string fatturato = "SELECT SUM(PrezzoAcquisto) AS fatturato " +
                            "FROM Biglietto " +
                            "WHERE idMuseo = " + m.idMuseo + " AND MONTH(DataValidita) = " + dt.Month + " AND YEAR(DataValidita) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(fatturato, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["fatturato"]))
                                        statistiche.Fatturato = (double)sqlDataReader["fatturato"];
                                }
                            }
                        }

                        if (DBObject<Statistiche>.CustomSelect(new SqlCommand("SELECT Statistiche.* FROM Statistiche INNER JOIN StatisticheMuseo ON Statistiche.idStatistiche = StatisticheMuseo.idStatistiche WHERE idMuseo = " + m.idMuseo + " AND MeseAnno = '" + dt.Date.ToString("yyyy-MM-dd") + "';")).Count == 0)
                        {
                            int res = DBObject<Statistiche>.Insert("MeseAnno", dt.Date.ToString("yyyy-MM-dd"), "SpeseTotali", statistiche.SpeseTotali, "Fatturato", statistiche.Fatturato, "NumBigliettiVenduti", statistiche.NumBigliettiVenduti, "NumPresenzeTotali", statistiche.NumPresenzeTotali, "NumManutenzioni", statistiche.NumManutenzioni, "NumContenutiNuovi", statistiche.NumContenutiNuovi, "NumChiusure", statistiche.NumChiusure);

                            if (res != 0)
                            {
                                res = DBObject<StatisticheMuseo>.Insert("idStatistiche", res, "idMuseo", m.idMuseo);
                            }
                        }
                    }
                });

                lfm.ForEach(fm =>
                {
                    //Statistiche Famiglia Musei
                    {
                        int idFamiglia = fm.idFamiglia;
                        Statistiche statistiche = new Statistiche();

                        //Numero di biglietti venduti
                        string numBiglietti = "SELECT COUNT(*) AS numBiglietti " +
                            "FROM Biglietto " +
                            "WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND MONTH(DataValidita) = " + dt.Month + " AND YEAR(DataValidita) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numBiglietti, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numBiglietti"]))
                                        statistiche.NumBigliettiVenduti = (int)sqlDataReader["numBiglietti"];
                                }
                            }
                        }

                        //Numero di manutenzioni svolte
                        string numManutenzioni = "SELECT COUNT(*) AS numManutenzioni " +
                            "FROM RegistroManutenzioni " +
                            "WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND MONTH(Data) = " + dt.Month + " AND YEAR(Data) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numManutenzioni, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numManutenzioni"]))
                                        statistiche.NumManutenzioni = (int)sqlDataReader["numManutenzioni"];
                                }
                            }
                        }

                        //Numero di nuovi contenuti aggiunti
                        string numNuoviContenuti = "SELECT COUNT(*) AS numNuoviContenuti " +
                            "FROM Contenuto INNER JOIN Sezione ON Contenuto.idSezione = Sezione.idSezione " +
                            "WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND MONTH(DataArrivoMuseo) = " + dt.Month + " AND YEAR(DataArrivoMuseo) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numNuoviContenuti, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numNuoviContenuti"]))
                                        statistiche.NumContenutiNuovi = (int)sqlDataReader["numNuoviContenuti"];
                                }
                            }
                        }

                        //Numero di giorni di chiusura totali
                        string numGiorniChiusura = "SELECT COUNT(*) AS numGiorniChiusura " +
                            "FROM CalendarioChiusure " +
                            "WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND MONTH(Data) = " + dt.Month + " AND YEAR(Data) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(numGiorniChiusura, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["numGiorniChiusura"]))
                                        statistiche.NumChiusure = (int)sqlDataReader["numGiorniChiusura"];
                                }
                            }
                        }

                        //Spese totali
                        string speseTotali = "SELECT SUM(Spese) AS speseTotali " +
                            "FROM ((SELECT SUM(Prezzo) AS Spese FROM RegistroManutenzioni WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND MONTH(Data) = " + dt.Month + " AND YEAR(Data) = " + dt.Year + ") " +
                            "UNION (" +
                            "SELECT SUM(StipendioOra * DATEDIFF(hour, DataEntrata, DataUscita)) AS Spese FROM Personale INNER JOIN RegistroPresenze ON Personale.idPersonale = RegistroPresenze.idPersonale WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND (MONTH(DataEntrata) = " + dt.Month + " AND YEAR(DataEntrata) = " + dt.Year + " OR MONTH(DataUscita) = " + dt.Month + " AND YEAR(DataUscita) = " + dt.Year + "))) AS Spese";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(speseTotali, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["speseTotali"]))
                                        statistiche.SpeseTotali = (double)sqlDataReader["speseTotali"];
                                }
                            }
                        }

                        //Fatturato
                        string fatturato = "SELECT SUM(PrezzoAcquisto) AS fatturato " +
                            "FROM Biglietto " +
                            "WHERE idMuseo IN (SELECT idMuseo FROM Museo WHERE idFamiglia = " + idFamiglia + " ) AND MONTH(DataValidita) = " + dt.Month + " AND YEAR(DataValidita) = " + dt.Year + ";";

                        using (DBConnection dBConnection = new DBConnection())
                        {
                            SqlCommand sqlCommand = new SqlCommand(fatturato, dBConnection.Connection);
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                if (sqlDataReader.Read())
                                {
                                    if (!DBNull.Value.Equals(sqlDataReader["fatturato"]))
                                        statistiche.Fatturato = (double)sqlDataReader["fatturato"];
                                }
                            }
                        }

                        if (DBObject<Statistiche>.CustomSelect(new SqlCommand("SELECT Statistiche.* FROM Statistiche INNER JOIN StatisticheFamigliaMusei ON Statistiche.idStatistiche = StatisticheFamigliaMusei.idStatistiche WHERE idFamiglia = " + idFamiglia + " AND MeseAnno = '" + dt.Date.ToString("yyyy-MM-dd") + "';")).Count == 0)
                        {
                            int res = DBObject<Statistiche>.Insert("MeseAnno", dt.Date.ToString("yyyy-MM-dd"), "SpeseTotali", statistiche.SpeseTotali, "Fatturato", statistiche.Fatturato, "NumBigliettiVenduti", statistiche.NumBigliettiVenduti, "NumPresenzeTotali", statistiche.NumPresenzeTotali, "NumManutenzioni", statistiche.NumManutenzioni, "NumContenutiNuovi", statistiche.NumContenutiNuovi, "NumChiusure", statistiche.NumChiusure);

                            if (res != 0)
                            {
                                res = DBObject<StatisticheFamigliaMusei>.Insert("idStatistiche", res, "idFamiglia", idFamiglia);
                            }
                        }
                    }
                });
            });

        }
    }
}
