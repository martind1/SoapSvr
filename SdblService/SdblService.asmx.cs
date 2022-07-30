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
        private DataService Data { get; set; }

        public SdblService()
        {
            System.Diagnostics.Debug.WriteLine("New SdblService");
            Data = new DataService();
        }

        [WebMethod]
        public int Add(int a, int b)
        {
            string s;
            s = Data.UploadSDB($"{a} + {b}");
            System.Diagnostics.Debug.WriteLine($"{a} + {b}");
            if (s == "OK")
                return 1;
            else
                return (a + b);
        }
        [WebMethod]
        public System.Single Subtract(System.Single A, System.Single B)
        {
            return (A - B);
        }
        [WebMethod]
        public System.Single Multiply(System.Single A, System.Single B)
        {
            return A * B;
        }
        [WebMethod]
        public System.Single Divide(System.Single A, System.Single B)
        {
            if (B == 0)
            {
                //return -1; 
                // Build the detail element of the SOAP fault.
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlNode node = doc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

                /*
                // Build specific details for the SoapException.
                // Add first child of detail XML element.
                System.Xml.XmlNode details = doc.CreateNode(XmlNodeType.Element, "mySpecialInfo1", "http://dbsoft.de/SoapSvr/SdblService/");
                System.Xml.XmlNode detailsChild = doc.CreateNode(XmlNodeType.Element, "childOfSpecialInfo", "http://dbsoft.de/SoapSvr/SdblService/");
                details.AppendChild(detailsChild);


                // Add second child of detail XML element with an attribute.
                System.Xml.XmlNode details2 = doc.CreateNode(XmlNodeType.Element, "mySpecialInfo2", "http://dbsoft.de/SoapSvr/SdblService/");
                XmlAttribute attr = doc.CreateAttribute("t", "attrName", "http://dbsoft.de/SoapSvr/SdblService/");
                attr.Value = "attrValue";
                details2.Attributes.Append(attr);

                // Append the two child elements to the detail node.
                node.AppendChild(details);
                node.AppendChild(details2);
                */

                //Throw the exception.    
                //SoapException se = new SoapException("Fault occurred", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, node);
                SoapException se = new SoapException("Division durch 0", new XmlQualifiedName("C01"));

                throw se;
            }
            return Convert.ToSingle(A / B);
        }
    }
}
