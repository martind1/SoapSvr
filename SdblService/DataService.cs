using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using SdblService;

namespace SdblDB
{
    public class SdbData
    {
        public string HANDELSBEZEICHNUNG;
        public string SPRACHE;
        public string DOKU_TYP;
        public string LOESCH_KNZ;
        public string INTERNET_KNZ;
        public string MINERAL;
        public string BESCHICHTUNG;
        public string KOERNUNG;
        public string SDB_BASE64;

        public SdbData( string aHANDELSBEZEICHNUNG,
                        string aSPRACHE,
                        string aDOKU_TYP,
                        string aLOESCH_KNZ,
                        string aINTERNET_KNZ, 
                        string aMINERAL, 
                        string aBESCHICHTUNG, 
                        string aKOERNUNG, 
                        string aSDB_BASE64)
        {
            HANDELSBEZEICHNUNG = aHANDELSBEZEICHNUNG;
            SPRACHE = aSPRACHE;
            DOKU_TYP = aDOKU_TYP;
            LOESCH_KNZ = aLOESCH_KNZ;
            INTERNET_KNZ = aINTERNET_KNZ;
            MINERAL = aMINERAL;
            BESCHICHTUNG = aBESCHICHTUNG;
            KOERNUNG = aKOERNUNG;
            SDB_BASE64 = aSDB_BASE64;
        }
    }

    public class DataService
    {
        private AppService App { get; set; }
        static bool firstRun = true;
        string conString;

        public DataService()
        {
            App = new AppService();
            if (firstRun)
            {
                firstRun = false;
                //OracleInternal.NotificationServices.ONSException vermeiden (beide notwendig): 
                OracleConfiguration.LoadBalancing = false;
                OracleConfiguration.HAEvents = false;
            }
            conString = ConfigurationManager.ConnectionStrings["SdblConnectionString"].ConnectionString;
        }

        // ergibt true wenn Handelsbezeichnung in HANDELSPRODUKTE_WEB gefunden
        private bool HanFound(SdbData data, OracleConnection con, out double fHSPW_ID)
        {
            bool Result = false;
            fHSPW_ID = 0;
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"SELECT HSPW_ID from HANDELSPRODUKTE_WEB 
                                    WHERE upper(HANDELSBEZEICHNUNG) = upper(:HANDELSBEZEICHNUNG)";
                cmd.Parameters.Add(":HANDELSBEZEICHNUNG", OracleDbType.Varchar2).Value = data.HANDELSBEZEICHNUNG;

                //cmd.ExecuteNonQuery();
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Result = true;
                        fHSPW_ID = reader.GetDouble(0);
                        App.Prot0($"HanFound({data.HANDELSBEZEICHNUNG}):{Result} {fHSPW_ID}");
                    }
                    else
                    {
                        Result = false;
                        App.Prot0($"HanFound({data.HANDELSBEZEICHNUNG}):{Result}");
                    }
                }
            }
            return Result;
        } //HanFound

        // aktualisiert Handelsbezeichnung in HANDELSPRODUKTE_WEB 
        private void HanUpdate(SdbData data, OracleConnection con, double fHSPW_ID)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"update HANDELSPRODUKTE_WEB 
                                    set INTERNET_KNZ = :INTERNET_KNZ,
                                        MINERAL = :MINERAL,
                                        KOERNUNG = :KOERNUNG,
                                        BESCHICHTUNG = :BESCHICHTUNG
                                    WHERE HSPW_ID = :HSPW_ID";
                cmd.Parameters.Add(":HSPW_ID", OracleDbType.Double).Value = fHSPW_ID;
                cmd.Parameters.Add(":INTERNET_KNZ", OracleDbType.Char).Value = data.INTERNET_KNZ;
                cmd.Parameters.Add(":MINERAL", OracleDbType.Varchar2).Value = data.MINERAL;
                cmd.Parameters.Add(":KOERNUNG", OracleDbType.Varchar2).Value = data.KOERNUNG;
                cmd.Parameters.Add(":BESCHICHTUNG", OracleDbType.Varchar2).Value = data.BESCHICHTUNG;

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt == 0) 
                {   //update nicht erfolgt
                    App.EError("E10", $"Interner Fehler bei HanUpdate({data.HANDELSBEZEICHNUNG})");
                } else
                {
                    App.Prot0($"HanUpdate({data.HANDELSBEZEICHNUNG}):OK");
                }
            }
        }

        // fügt neue Handelsbezeichnung ein in HANDELSPRODUKTE_WEB 
        private void HanInsert(SdbData data, OracleConnection con, out double fHSPW_ID)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"insert into HANDELSPRODUKTE_WEB(HANDELSBEZEICHNUNG, INTERNET_KNZ, MINERAL, KOERNUNG, 
                                        BESCHICHTUNG, BEMERKUNG)
                                    values(:HANDELSBEZEICHNUNG, :INTERNET_KNZ, :MINERAL, :KOERNUNG, 
                                        :BESCHICHTUNG, :BEMERKUNG)";
                cmd.Parameters.Add(":HANDELSBEZEICHNUNG", OracleDbType.Varchar2).Value = data.HANDELSBEZEICHNUNG;
                cmd.Parameters.Add(":INTERNET_KNZ", OracleDbType.Char).Value = data.INTERNET_KNZ;
                cmd.Parameters.Add(":MINERAL", OracleDbType.Varchar2).Value = data.MINERAL;
                cmd.Parameters.Add(":KOERNUNG", OracleDbType.Varchar2).Value = data.KOERNUNG;
                cmd.Parameters.Add(":BESCHICHTUNG", OracleDbType.Varchar2).Value = data.BESCHICHTUNG;
                cmd.Parameters.Add(":BEMERKUNG", OracleDbType.Varchar2).Value = "Insert by SdblService";

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt == 0)
                {   //insert nicht erfolgt
                    App.EError("E11", $"Interner Fehler bei HanInsert({data.HANDELSBEZEICHNUNG})");
                }
            }
            if (!HanFound(data, con, out fHSPW_ID))
            {
                //insert nicht gefunden
                App.EError("E12", $"Interner Fehler bei HanInsert({data.HANDELSBEZEICHNUNG})");
            }
            App.Prot0($"HanInsert({data.HANDELSBEZEICHNUNG}, {fHSPW_ID}):OK");
        }

        public string UploadSDB(SdbData data)
        {
            string result = "OK";
            double fHSPW_ID;
            try
            {
                App.Prot0("Open Connection");
                using (var con = new OracleConnection(conString))
                {
                    con.Open();

                    if (HanFound(data, con, out fHSPW_ID))
                        HanUpdate(data, con, fHSPW_ID); 
                    else 
                        HanInsert(data, con, out fHSPW_ID);
                }
            }
            catch (Exception ex)
            {
                App.EError("E09", ex.Message);
            }
            App.Prot0("Connection Closed");
            return result;
        } //UploadSDB


        public string DeleteSDB(SdbData data)
        {
            string result = "NOK";
            return result;
        }

        } //class
    }  //namespace
