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
        //intern:
        public double HSPW_ID;
        public double DOKW_ID;
        public byte[] DOKU_DATA;

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
        // ergänzt data.HSPW_ID
        private bool HanFound(ref SdbData data, OracleConnection con)
        {
            bool Result = false;
            data.HSPW_ID = 0;
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
                        data.HSPW_ID = reader.GetDouble(0);
                        App.Prot0($"HanFound({data.HANDELSBEZEICHNUNG}):{Result} {data.HSPW_ID}");
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
        private void HanUpdate(SdbData data, OracleConnection con)
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
                cmd.Parameters.Add(":HSPW_ID", OracleDbType.Double).Value = data.HSPW_ID;
                cmd.Parameters.Add(":INTERNET_KNZ", OracleDbType.Char).Value = data.INTERNET_KNZ;
                cmd.Parameters.Add(":MINERAL", OracleDbType.Varchar2).Value = data.MINERAL;
                cmd.Parameters.Add(":KOERNUNG", OracleDbType.Varchar2).Value = data.KOERNUNG;
                cmd.Parameters.Add(":BESCHICHTUNG", OracleDbType.Varchar2).Value = data.BESCHICHTUNG;

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt > 0) 
                {
                    App.Prot0($"HanUpdate({data.HANDELSBEZEICHNUNG}):OK");
                }
                else
                {
                    //update nicht erfolgt
                    App.EError("E10", $"Interner Fehler bei HanUpdate({data.HANDELSBEZEICHNUNG})");
                }
            }
        } //HanUpdate

        // fügt neue Handelsbezeichnung ein in HANDELSPRODUKTE_WEB 
        private void HanInsert(ref SdbData data, OracleConnection con)
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
            if (!HanFound(ref data, con))
            {
                //insert nicht gefunden
                App.EError("E12", $"Interner Fehler bei HanInsert({data.HANDELSBEZEICHNUNG})");
            }
            App.Prot0($"HanInsert({data.HANDELSBEZEICHNUNG}):OK  {data.HSPW_ID}");
        } //HanInsert

        // löscht Handelsbezeichnung in HANDELSPRODUKTE_WEB 
        private void HanDelete(SdbData data, OracleConnection con)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"delete from HANDELSPRODUKTE_WEB 
                                    WHERE HSPW_ID = :HSPW_ID";
                cmd.Parameters.Add(":HSPW_ID", OracleDbType.Double).Value = data.HSPW_ID;

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt > 0)
                {
                    App.Prot0($"HanDelete({data.HANDELSBEZEICHNUNG}):OK");
                }
                else
                {
                    //delete nicht erfolgt
                    App.EError("E10", $"Interner Fehler bei HanDelete({data.HANDELSBEZEICHNUNG})");
                }
            }
        } //HanDelete

        // ergibt true wenn Dokument in DOKUMENTE_WEB gefunden
        // ergänzt data.DOKW_ID
        private bool DokFound(ref SdbData data, OracleConnection con)
        {
            bool Result = false;
            data.DOKW_ID = 0;
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"SELECT DOKW_ID from DOKUMENTE_WEB 
                                    WHERE DOKW_HSPW_ID = :HSPW_ID
                                    AND SPRACHE = :SPRACHE
                                    AND DOKU_TYP = :DOKU_TYP";
                cmd.Parameters.Add(":HSPW_ID", OracleDbType.Double).Value = data.HSPW_ID;
                cmd.Parameters.Add(":SPRACHE", OracleDbType.Varchar2).Value = data.SPRACHE;
                cmd.Parameters.Add(":DOKU_TYP", OracleDbType.Varchar2).Value = data.DOKU_TYP;

                //cmd.ExecuteNonQuery();
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Result = true;
                        data.DOKW_ID = reader.GetDouble(0);
                        App.Prot0($"DokFound({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP}):{Result} {data.DOKW_ID}");
                    }
                    else
                    {
                        Result = false;
                        App.Prot0($"DokFound({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP}):{Result}");
                    }
                }
            }
            return Result;
        } //DokFound

        // ergibt anzahl Dokumente zu einem Handelsprodukt
        private int DokHanCount(SdbData data, OracleConnection con)
        {
            int Result = 0;
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"SELECT count(*) from DOKUMENTE_WEB 
                                    WHERE DOKW_HSPW_ID = :HSPW_ID";
                cmd.Parameters.Add(":HSPW_ID", OracleDbType.Double).Value = data.HSPW_ID;
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Result = reader.GetInt32(0);
                        App.Prot0($"DokHanCount({data.HANDELSBEZEICHNUNG}):{Result}");
                    }
                    else
                    {
                        App.EError("E32", $"Interner Fehler bei DokHanCount({data.HANDELSBEZEICHNUNG}, {data.HSPW_ID})");
                    }
                }
            }
            return Result;
        } //DokHanCount

        // aktualisiert Dokudata(PDF) in DOKUMENTE_WEB 
        private void DokUpdate(SdbData data, OracleConnection con)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"update DOKUMENTE_WEB 
                                    set DOKU_DATA = :DOKU_DATA
                                    WHERE DOKW_HSPW_ID = :DOKW_ID";
                cmd.Parameters.Add(":DOKW_ID", OracleDbType.Double).Value = data.DOKW_ID;
                cmd.Parameters.Add(":DOKU_DATA", OracleDbType.Blob).Value = data.DOKU_DATA;

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt > 0)
                {   
                    App.Prot0($"DokUpdate({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP}):OK");
                }
                else
                {
                    //update nicht erfolgt
                    App.EError("E20", $"Interner Fehler {rowCnt} bei DokUpdate({data.DOKW_ID}, {data.SPRACHE}, {data.DOKU_TYP})");
                }
            }
        }

        // fügt neue HSPW, Sprache, Dokutyp, Dokudata(PDF) ein in DOKUMENTE_WEB 
        private void DokInsert(ref SdbData data, OracleConnection con)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"insert into DOKUMENTE_WEB(DOKW_HSPW_ID, SPRACHE, DOKU_TYP, DOKU_DATA)
                                    values(:HSPW_ID, :SPRACHE, :DOKU_TYP, :DOKU_DATA)";
                cmd.Parameters.Add(":HSPW_ID", OracleDbType.Double).Value = data.HSPW_ID;
                cmd.Parameters.Add(":DOKU_TYP", OracleDbType.Char).Value = data.DOKU_TYP;
                cmd.Parameters.Add(":SPRACHE", OracleDbType.Varchar2).Value = data.SPRACHE;
                cmd.Parameters.Add(":DOKU_DATA", OracleDbType.Blob).Value = data.DOKU_DATA;
                cmd.Parameters.Add(":BEMERKUNG", OracleDbType.Varchar2).Value = "Insert by SdblService";

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt == 0)
                {   //insert nicht erfolgt
                    App.EError("E21", $"Interner Fehler bei DokInsert({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP})");
                }
            }
            if (!DokFound(ref data, con))
            {
                //insert nicht gefunden
                App.EError("E22", $"Interner Fehler bei DokInsert({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP})");
            }
            App.Prot0($"DokInsert({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP}):OK  {data.DOKW_ID}");
        }

        // löscht Dokument anhand ID in DOKUMENTE_WEB 
        private void DokDelete(SdbData data, OracleConnection con)
        {
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.BindByName = true;
                cmd.CommandText = @"delete from DOKUMENTE_WEB 
                                    WHERE DOKW_ID = :DOKW_ID";
                cmd.Parameters.Add(":DOKW_ID", OracleDbType.Double).Value = data.DOKW_ID;

                var rowCnt = cmd.ExecuteNonQuery();
                if (rowCnt > 0)
                {
                    App.Prot0($"DokDelete({data.HANDELSBEZEICHNUNG}, {data.SPRACHE}, {data.DOKU_TYP}):OK");
                }
                else
                {
                    //delete nicht erfolgt
                    App.EError("E23", $"Interner Fehler {rowCnt} bei DokDelete({data.DOKW_ID})");
                }
            }
        }

        // Hauptfunktion zum Upload:
        public string UploadSDB(SdbData data)
        {
            string result = "OK";
            try
            {
                App.Prot0("Open Connection");
                using (var con = new OracleConnection(conString))
                {
                    con.Open();

                    //Pdf als byte[] anhand Base64 String:
                    data.DOKU_DATA = Convert.FromBase64String(data.SDB_BASE64);

                    if (HanFound(ref data, con))
                        HanUpdate(data, con); 
                    else 
                        HanInsert(ref data, con);

                    //if (DokFound(ref data, con))
                    //    DokUpdate(data, con);
                    //else
                    //    DokInsert(ref data, con);
                    // Bug bei QW Server TestUtf8:
                    if (DokFound(ref data, con))
                        DokDelete(data, con);
                    DokInsert(ref data, con);
                }
            }
            catch (Exception ex) when (!(ex is SoapException))
            {
                App.EError("E09", ex.Message);
            }
            App.Prot0("Connection Closed");
            return result;
        } //UploadSDB


        public string DeleteSDB(SdbData data)
        {
            string result = "NOK";
            try
            {
                App.Prot0("Delete: Open Connection");
                using (var con = new OracleConnection(conString))
                {
                    con.Open();

                    if (HanFound(ref data, con))
                    {
                        if (DokFound(ref data, con))
                        {
                            DokDelete(data, con);
                            result = "OK"; 
                        }
                        if (DokHanCount(data, con) == 0)
                        {
                            //Handelsprodukt löschen wenn keine Dokumente mehr existieren
                            HanDelete(data, con);
                        }

                    }
                    if (result != "OK")
                    {
                        App.EError("E31", "Zu löschendes Dokument nicht gefunden");
                    }
                }
            }
            catch (Exception ex) when (!(ex is SoapException))
            {
                App.EError("E39", ex.Message);
            }
            App.Prot0("Connection Closed");
            return result;
        }  //DeleteSDB

    } //class
    }  //namespace
