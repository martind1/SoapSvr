using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SdblClient
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnUploadSDB_Click(object sender, EventArgs e)
        {
            //Call Webservice
            using (SdblSvcRef.SdblServiceSoapClient client = new SdblSvcRef.SdblServiceSoapClient("SdblServiceSoap"))
            {
                try
                {
                    //Read the contents of the file into a stream
                    /*  var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }*/
                    byte[] buff = File.ReadAllBytes(txtSdblFilename.Text);
                    string SdblBase64 = Convert.ToBase64String(buff);

                    string Ergebnis = client.UploadSDB(txtHANDELSBEZEICHNUNG.Text,
                                                       txtSPRACHE.Text,
                                                       txtDOKU_TYP.Text,
                                                       txtLOESCH_KNZ.Text,
                                                       txtINTERNET_KNZ.Text,
                                                       txtMINERAL.Text,
                                                       txtBESCHICHTUNG.Text,
                                                       txtKOERNUNG.Text,
                                                       SdblBase64);
                    //update the UI:
                    txtErgebnis.Text = Ergebnis;
                }
                catch (System.Exception ex)
                {
                    txtErgebnis.Text = ex.Message;
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //<client> <endpoint address="http://192.168.178.48:50577/SdblService.asmx"
            var serviceModel = ServiceModelSectionGroup.GetSectionGroup(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
            var endpoints = serviceModel.Client.Endpoints;
            foreach (ChannelEndpointElement ep in endpoints)
            {
                if (ep.Name == "SdblServiceSoap")
                {
                    LaSoapURL.Text = ep.Address.ToString();
                }
            }
        }

        private void LaSoapURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            LaSoapURL.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start(LaSoapURL.Text);
        }

        private void BtnSdblFilename_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    txtSdblFilename.Text = openFileDialog.FileName;
                }
            }
        }

        private static string IniFilename { get; set; } = @".\SdblClient.ini.json";

        private void BtnSaveToIni_Click(object sender, EventArgs e)
        {
            var d = new SdbData(txtHANDELSBEZEICHNUNG.Text,
                                                txtSPRACHE.Text,
                                                txtDOKU_TYP.Text,
                                                txtLOESCH_KNZ.Text,
                                                txtINTERNET_KNZ.Text,
                                                txtMINERAL.Text,
                                                txtBESCHICHTUNG.Text,
                                                txtKOERNUNG.Text,
                                                txtSdblFilename.Text);
            string jsonString = JsonSerializer.Serialize(d, new JsonSerializerOptions() { WriteIndented = true });
            using (StreamWriter outputFile = new StreamWriter(IniFilename))
            {
                outputFile.WriteLine(jsonString);
            }
        }

        private void BtnLoadFromIni_Click(object sender, EventArgs e)
        {
            var d = new SdbData();
            using (StreamReader r = new StreamReader(IniFilename))
            {
                string json = r.ReadToEnd();
                d = JsonSerializer.Deserialize<SdbData>(json);
                txtHANDELSBEZEICHNUNG.Text = d.HANDELSBEZEICHNUNG;
                txtSPRACHE.Text = d.SPRACHE;
                txtDOKU_TYP.Text = d.DOKU_TYP;
                txtLOESCH_KNZ.Text = d.LOESCH_KNZ;
                txtINTERNET_KNZ.Text = d.INTERNET_KNZ;
                txtMINERAL.Text = d.MINERAL;
                txtBESCHICHTUNG.Text = d.BESCHICHTUNG;
                txtKOERNUNG.Text = d.KOERNUNG;
                txtSdblFilename.Text = d.SdblFilename;
            }
        }
    }

    public class SdbData
    {
        public string HANDELSBEZEICHNUNG { get; set; }
        public string SPRACHE { get; set; }
        public string DOKU_TYP { get; set; }
        public string LOESCH_KNZ { get; set; }
        public string INTERNET_KNZ { get; set; }
        public string MINERAL { get; set; }
        public string BESCHICHTUNG { get; set; }
        public string KOERNUNG { get; set; }
        public string SdblFilename { get; set; }

        public SdbData(string aHANDELSBEZEICHNUNG,
                        string aSPRACHE,
                        string aDOKU_TYP,
                        string aLOESCH_KNZ,
                        string aINTERNET_KNZ,
                        string aMINERAL,
                        string aBESCHICHTUNG,
                        string aKOERNUNG,
                        string aSdblFilename)
        {
            HANDELSBEZEICHNUNG = aHANDELSBEZEICHNUNG;
            SPRACHE = aSPRACHE;
            DOKU_TYP = aDOKU_TYP;
            LOESCH_KNZ = aLOESCH_KNZ;
            INTERNET_KNZ = aINTERNET_KNZ;
            MINERAL = aMINERAL;
            BESCHICHTUNG = aBESCHICHTUNG;
            KOERNUNG = aKOERNUNG;
            SdblFilename = aSdblFilename;
        }

        public SdbData()
        {
        }
    }

}
