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
    }
}
