using SdblDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace SdblService
{
    /// <summary>
    /// Summary description for SdblService
    /// </summary>
    [WebService(Namespace = "http://dbsoft.de/SoapSvr/SdblService/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SdblService : System.Web.Services.WebService
    {
        private AppService App { get; set; }
        private DataService Data { get; set; }

        public SdblService()
        {
            App = new AppService();
            App.Prot0("New SdblService");
            Data = new DataService();
        }

        [WebMethod]
        public string UploadSDB(
            string HANDELSBEZEICHNUNG,
            string SPRACHE,
            string VKORG,
            string DOKU_TYP,
            string LOESCH_KNZ,
            string INTERNET_KNZ,
            string MINERAL,
            string BESCHICHTUNG,
            string KOERNUNG,
            string SDB_BASE64)
        {
            string s;
            App.Prot0($"UploadSDB(Han:{HANDELSBEZEICHNUNG}, Sprache:{SPRACHE} Doktyp:{DOKU_TYP}, Löschknz:{LOESCH_KNZ})");
            if (String.IsNullOrEmpty(HANDELSBEZEICHNUNG))
            {
                App.EError("E01", "Handelsbezeichnung darf nicht leer sein");
            }
            if (String.IsNullOrEmpty(SPRACHE))
            {
                App.EError("E02", "Sprache darf nicht leer sein");
            }
            if (String.IsNullOrEmpty(DOKU_TYP))
            {
                DOKU_TYP = "P";
            }
            if (String.IsNullOrEmpty(LOESCH_KNZ))
            {
                LOESCH_KNZ = "N";
            }
            if (LOESCH_KNZ != "J")
            {
                if (String.IsNullOrEmpty(INTERNET_KNZ))
                {
                    App.EError("E03", "Internet_knz darf nicht leer sein");
                }
                if (String.IsNullOrEmpty(MINERAL))
                {
                    App.EError("E04", "Mineral darf nicht leer sein");
                }
                if (String.IsNullOrEmpty(BESCHICHTUNG))
                {
                    App.EError("E05", "Beschichtung darf nicht leer sein");
                }
                if (String.IsNullOrEmpty(SDB_BASE64))
                {
                    App.EError("E06", "SDB_BASE64 darf nicht leer sein");
                }
                s = Data.UploadSDB(new SdbData( HANDELSBEZEICHNUNG,
                                                SPRACHE,
                                                VKORG,
                                                DOKU_TYP,
                                                LOESCH_KNZ,
                                                INTERNET_KNZ,
                                                MINERAL,
                                                BESCHICHTUNG,
                                                KOERNUNG,
                                                SDB_BASE64));
            } 
            else 
            { 
                s = Data.DeleteSDB(new SdbData(HANDELSBEZEICHNUNG,
                                                SPRACHE,
                                                VKORG,
                                                DOKU_TYP,
                                                LOESCH_KNZ,
                                                INTERNET_KNZ,
                                                MINERAL,
                                                BESCHICHTUNG,
                                                KOERNUNG,
                                                SDB_BASE64));
            }
            return s;
        }
    }
    //SoapException se = new SoapException("Division durch 0", new XmlQualifiedName("C01"));
    //throw se;
}
