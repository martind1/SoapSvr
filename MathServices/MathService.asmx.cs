using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace MathServices
{
    /// <summary>
    /// Summary description for MathService
    /// </summary>
    [WebService(Namespace = "http://dbsoft.de/SoapSvr/MathServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MathService : System.Web.Services.WebService
    {
        [WebMethod]
        public int Add(int a, int b)
        {
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
                System.Xml.XmlNode details = doc.CreateNode(XmlNodeType.Element, "mySpecialInfo1", "http://dbsoft.de/SoapSvr/MathServices/");
                System.Xml.XmlNode detailsChild = doc.CreateNode(XmlNodeType.Element, "childOfSpecialInfo", "http://dbsoft.de/SoapSvr/MathServices/");
                details.AppendChild(detailsChild);


                // Add second child of detail XML element with an attribute.
                System.Xml.XmlNode details2 = doc.CreateNode(XmlNodeType.Element, "mySpecialInfo2", "http://dbsoft.de/SoapSvr/MathServices/");
                XmlAttribute attr = doc.CreateAttribute("t", "attrName", "http://dbsoft.de/SoapSvr/MathServices/");
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
