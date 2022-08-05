namespace SdblClient
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnUploadSDB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHANDELSBEZEICHNUNG = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtErgebnis = new System.Windows.Forms.TextBox();
            this.LaSoapURL = new System.Windows.Forms.LinkLabel();
            this.txtSPRACHE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLOESCH_KNZ = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDOKU_TYP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtKOERNUNG = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBESCHICHTUNG = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMINERAL = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtINTERNET_KNZ = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSdblFilename = new System.Windows.Forms.TextBox();
            this.BtnSdblFilename = new System.Windows.Forms.Button();
            this.BtnLoadFromIni = new System.Windows.Forms.Button();
            this.BtnSaveToIni = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnUploadSDB
            // 
            this.BtnUploadSDB.Location = new System.Drawing.Point(27, 280);
            this.BtnUploadSDB.Name = "BtnUploadSDB";
            this.BtnUploadSDB.Size = new System.Drawing.Size(146, 49);
            this.BtnUploadSDB.TabIndex = 0;
            this.BtnUploadSDB.Text = "UploadSDB";
            this.BtnUploadSDB.UseVisualStyleBackColor = true;
            this.BtnUploadSDB.Click += new System.EventHandler(this.BtnUploadSDB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "HANDELSBEZEICHNUNG";
            // 
            // txtHANDELSBEZEICHNUNG
            // 
            this.txtHANDELSBEZEICHNUNG.Location = new System.Drawing.Point(166, 17);
            this.txtHANDELSBEZEICHNUNG.Name = "txtHANDELSBEZEICHNUNG";
            this.txtHANDELSBEZEICHNUNG.Size = new System.Drawing.Size(362, 20);
            this.txtHANDELSBEZEICHNUNG.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ergebnis";
            // 
            // txtErgebnis
            // 
            this.txtErgebnis.Location = new System.Drawing.Point(143, 349);
            this.txtErgebnis.Name = "txtErgebnis";
            this.txtErgebnis.ReadOnly = true;
            this.txtErgebnis.Size = new System.Drawing.Size(385, 20);
            this.txtErgebnis.TabIndex = 4;
            // 
            // LaSoapURL
            // 
            this.LaSoapURL.AutoSize = true;
            this.LaSoapURL.Location = new System.Drawing.Point(217, 297);
            this.LaSoapURL.Name = "LaSoapURL";
            this.LaSoapURL.Size = new System.Drawing.Size(55, 13);
            this.LaSoapURL.TabIndex = 5;
            this.LaSoapURL.TabStop = true;
            this.LaSoapURL.Text = "linkLabel1";
            this.LaSoapURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LaSoapURL_LinkClicked);
            // 
            // txtSPRACHE
            // 
            this.txtSPRACHE.Location = new System.Drawing.Point(166, 43);
            this.txtSPRACHE.Name = "txtSPRACHE";
            this.txtSPRACHE.Size = new System.Drawing.Size(362, 20);
            this.txtSPRACHE.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "SPRACHE";
            // 
            // txtLOESCH_KNZ
            // 
            this.txtLOESCH_KNZ.Location = new System.Drawing.Point(166, 97);
            this.txtLOESCH_KNZ.Name = "txtLOESCH_KNZ";
            this.txtLOESCH_KNZ.Size = new System.Drawing.Size(362, 20);
            this.txtLOESCH_KNZ.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "LOESCH_KNZ";
            // 
            // txtDOKU_TYP
            // 
            this.txtDOKU_TYP.Location = new System.Drawing.Point(166, 71);
            this.txtDOKU_TYP.Name = "txtDOKU_TYP";
            this.txtDOKU_TYP.Size = new System.Drawing.Size(362, 20);
            this.txtDOKU_TYP.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "DOKU_TYP";
            // 
            // txtKOERNUNG
            // 
            this.txtKOERNUNG.Location = new System.Drawing.Point(166, 206);
            this.txtKOERNUNG.Name = "txtKOERNUNG";
            this.txtKOERNUNG.Size = new System.Drawing.Size(362, 20);
            this.txtKOERNUNG.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "KOERNUNG";
            // 
            // txtBESCHICHTUNG
            // 
            this.txtBESCHICHTUNG.Location = new System.Drawing.Point(166, 180);
            this.txtBESCHICHTUNG.Name = "txtBESCHICHTUNG";
            this.txtBESCHICHTUNG.Size = new System.Drawing.Size(362, 20);
            this.txtBESCHICHTUNG.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "BESCHICHTUNG";
            // 
            // txtMINERAL
            // 
            this.txtMINERAL.Location = new System.Drawing.Point(166, 152);
            this.txtMINERAL.Name = "txtMINERAL";
            this.txtMINERAL.Size = new System.Drawing.Size(362, 20);
            this.txtMINERAL.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "MINERAL";
            // 
            // txtINTERNET_KNZ
            // 
            this.txtINTERNET_KNZ.Location = new System.Drawing.Point(166, 126);
            this.txtINTERNET_KNZ.Name = "txtINTERNET_KNZ";
            this.txtINTERNET_KNZ.Size = new System.Drawing.Size(362, 20);
            this.txtINTERNET_KNZ.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "INTERNET_KNZ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "PDF Filename";
            // 
            // txtSdblFilename
            // 
            this.txtSdblFilename.Location = new System.Drawing.Point(166, 235);
            this.txtSdblFilename.Name = "txtSdblFilename";
            this.txtSdblFilename.Size = new System.Drawing.Size(362, 20);
            this.txtSdblFilename.TabIndex = 21;
            // 
            // BtnSdblFilename
            // 
            this.BtnSdblFilename.Image = global::SdblClient.Properties.Resources.OPA_LOV;
            this.BtnSdblFilename.Location = new System.Drawing.Point(534, 233);
            this.BtnSdblFilename.Name = "BtnSdblFilename";
            this.BtnSdblFilename.Size = new System.Drawing.Size(28, 23);
            this.BtnSdblFilename.TabIndex = 22;
            this.BtnSdblFilename.UseVisualStyleBackColor = true;
            this.BtnSdblFilename.Click += new System.EventHandler(this.BtnSdblFilename_Click);
            // 
            // BtnLoadFromIni
            // 
            this.BtnLoadFromIni.Location = new System.Drawing.Point(39, 409);
            this.BtnLoadFromIni.Name = "BtnLoadFromIni";
            this.BtnLoadFromIni.Size = new System.Drawing.Size(134, 23);
            this.BtnLoadFromIni.TabIndex = 23;
            this.BtnLoadFromIni.Text = "Einstellungen laden";
            this.BtnLoadFromIni.UseVisualStyleBackColor = true;
            this.BtnLoadFromIni.Click += new System.EventHandler(this.BtnLoadFromIni_Click);
            // 
            // BtnSaveToIni
            // 
            this.BtnSaveToIni.Location = new System.Drawing.Point(197, 409);
            this.BtnSaveToIni.Name = "BtnSaveToIni";
            this.BtnSaveToIni.Size = new System.Drawing.Size(146, 23);
            this.BtnSaveToIni.TabIndex = 24;
            this.BtnSaveToIni.Text = "Einstellungen speichern";
            this.BtnSaveToIni.UseVisualStyleBackColor = true;
            this.BtnSaveToIni.Click += new System.EventHandler(this.BtnSaveToIni_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnSaveToIni);
            this.Controls.Add(this.BtnLoadFromIni);
            this.Controls.Add(this.BtnSdblFilename);
            this.Controls.Add(this.txtSdblFilename);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtKOERNUNG);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBESCHICHTUNG);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMINERAL);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtINTERNET_KNZ);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtLOESCH_KNZ);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDOKU_TYP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSPRACHE);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LaSoapURL);
            this.Controls.Add(this.txtErgebnis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHANDELSBEZEICHNUNG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnUploadSDB);
            this.Name = "FrmMain";
            this.Text = "SdblClient";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnUploadSDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHANDELSBEZEICHNUNG;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtErgebnis;
        private System.Windows.Forms.LinkLabel LaSoapURL;
        private System.Windows.Forms.TextBox txtSPRACHE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLOESCH_KNZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDOKU_TYP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtKOERNUNG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBESCHICHTUNG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMINERAL;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtINTERNET_KNZ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSdblFilename;
        private System.Windows.Forms.Button BtnSdblFilename;
        private System.Windows.Forms.Button BtnLoadFromIni;
        private System.Windows.Forms.Button BtnSaveToIni;
    }
}

