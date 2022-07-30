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
        const string descriptor = @"(DESCRIPTION = 
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
        string conString = $"Data Source={descriptor};Persist Security Info=True;User ID=sdbl;Password=sdbl";

        string queryCountHAN = "SELECT count(*) from HANDELSPRODUKTE_WEB "
                                + "WHERE upper(HANDELSBEZEICHNUNG) = upper(:HANDELSBEZEICHNUNG)";

        public DataService()
        {
            Oracle.ManagedDataAccess.Client.OracleConfiguration.LoadBalancing = false;
            Oracle.ManagedDataAccess.Client.OracleConfiguration.HAEvents = false;
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
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            n++;
                            System.Diagnostics.Debug.WriteLine($"Count:{n}");
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
            return result;
        } //UploadSDB
    } //class
}  //namespace
