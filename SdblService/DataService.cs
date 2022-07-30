using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace SdblDB
{

    public class DataService
    {
        static bool firstRun = true;
        string conString;
        string descriptor = @"(DESCRIPTION = 
                                (ADDRESS =
                                    (PROTOCOL = TCP)
                                    (HOST = tecri)
                                    (PORT = 1521)
                                )
                                (CONNECT_DATA =
                                    (SERVER = DEDICATED)
                                    (SERVICE_NAME = tecri)
                                )
						      )";

        string queryCountHAN = "SELECT count(*) from HANDELSPRODUKTE_WEB "
                                + "WHERE upper(HANDELSBEZEICHNUNG) = upper(:HANDELSBEZEICHNUNG)";

        public DataService()
        {
            if (firstRun)
            {
                firstRun = false;
                //vermeiden (beide notwendig): 'OracleInternal.NotificationServices.ONSException'
                Oracle.ManagedDataAccess.Client.OracleConfiguration.LoadBalancing = false;
                Oracle.ManagedDataAccess.Client.OracleConfiguration.HAEvents = false;
            }
            conString = $"Data Source={descriptor};Persist Security Info=True;User ID=sdbl;Password=sdbl";
        }

        public string UploadSDB(string Handelsbezeichnung)
        {
            string result = "OK";
            int n = 0;
            try
            {
                System.Diagnostics.Debug.WriteLine("Open Connection");
                using (var con = new OracleConnection(conString))
                {
                    con.Open();

                    //Anzahl bestimmen (0=insert):
                    System.Diagnostics.Debug.WriteLine($"Open ({Handelsbezeichnung}) Count");
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        cmd.BindByName = true;
                        cmd.CommandText = queryCountHAN;
                        cmd.Parameters.Add(":HANDELSBEZEICHNUNG", OracleDbType.Varchar2).Value = Handelsbezeichnung;

                        //cmd.ExecuteNonQuery();
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                n++;
                                System.Diagnostics.Debug.WriteLine($"Count:{n}");
                            }
                        }
                    }
                    if (n == 0)
                    {
                        result = "Fehler bei Count";
                    }
                    else
                    {
                        //Daten insert/update
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            System.Diagnostics.Debug.WriteLine("Connection Closed");
            return result;
        } //UploadSDB
    } //class
}  //namespace
