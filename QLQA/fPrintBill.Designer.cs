﻿namespace QLQA
{
    partial class fPrintBill
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crystalReportViewerPrintBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(988, 618);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // crystalReportViewerPrintBill
            // 
            this.crystalReportViewerPrintBill.ActiveViewIndex = -1;
            this.crystalReportViewerPrintBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewerPrintBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewerPrintBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewerPrintBill.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewerPrintBill.Name = "crystalReportViewerPrintBill";
            this.crystalReportViewerPrintBill.ShowLogo = false;
            this.crystalReportViewerPrintBill.Size = new System.Drawing.Size(988, 618);
            this.crystalReportViewerPrintBill.TabIndex = 1;
            // 
            // fPrintBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 618);
            this.Controls.Add(this.crystalReportViewerPrintBill);
            this.Controls.Add(this.crystalReportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "fPrintBill";
            this.Text = "Xuất hóa đơn";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewerPrintBill;
    }
}