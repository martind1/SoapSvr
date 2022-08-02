using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;

namespace SdblService
{
    [Flags]
    public enum ProtFlags
    {
        None = 0,
        AddTime = 1,
        ToFile = 2,
        ToListbox = 4,
        ToDatabase = 8,
        ToSmess = 16,
        ToDmess = 32,
        ToHmess = 64,
        Debug = 128
    }

    public enum ProtLevels
    {
        Info, Debug
    }

    public class AppService
    {
        public ProtLevels ProtLevel { get; set; } = ProtLevels.Info;

        public void EError(string faultcode, string faultstring)
        {
            Prot(ProtFlags.AddTime | ProtFlags.ToFile, $"ERROR {faultcode}: {faultstring}");
            throw new SoapException(faultstring, new XmlQualifiedName(faultcode));
        }

        public void Prot(ProtFlags flags, string message)
        {
            if ((flags & ProtFlags.AddTime) == ProtFlags.AddTime)
            {
                message = DateTime.Now.ToString() + ' ' + message;
            }
            if ((flags & ProtFlags.ToFile) == ProtFlags.ToFile)
            {
                //Ausgabe nach Logfile
            }
            //erstmal immer nach Console:
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Prot0(string message)
        {
            //mit Timestamp, nach Logfile(später)
            Prot(ProtFlags.AddTime | ProtFlags.ToFile, message);
        }

        public void ProtA(string message)
        {
            //nur Console
            Prot(ProtFlags.None, message);
        }

        public void Prot0P(string message)
        {
            if (ProtLevel == ProtLevels.Debug)
                Prot0(message);
        }

        public void ProtAP(string message)
        {
            if (ProtLevel == ProtLevels.Debug)
                ProtA(message);
        }
    }
}