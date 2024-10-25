namespace CoFAS.NEW.MES.Core
{
    partial class BaseForm2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SEARCH_PAN = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.fpMain = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpMain_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).BeginInit();
            this._pnContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).BeginInit();
            this._pnHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // _pnContent
            // 
            this._pnContent.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnContent.Appearance.Options.UseBackColor = true;
            this._pnContent.Controls.Add(this.panel4);
            this._pnContent.Location = new System.Drawing.Point(0, 150);
            this._pnContent.Size = new System.Drawing.Size(1600, 364);
            // 
            // _pnHeader
            // 
            this._pnHeader.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnHeader.Appearance.Options.UseBackColor = true;
            this._pnHeader.Controls.Add(this.panel1);
            this._pnHeader.Size = new System.Drawing.Size(1600, 150);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SEARCH_PAN);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1600, 150);
            this.panel1.TabIndex = 6;
            // 
            // SEARCH_PAN
            // 
            this.SEARCH_PAN.AutoScroll = true;
            this.SEARCH_PAN.BackColor = System.Drawing.Color.White;
            this.SEARCH_PAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEARCH_PAN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SEARCH_PAN.Location = new System.Drawing.Point(0, 20);
            this.SEARCH_PAN.Name = "SEARCH_PAN";
            this.SEARCH_PAN.Size = new System.Drawing.Size(1600, 130);
            this.SEARCH_PAN.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(1600, 20);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = " - 조회조건 - ";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.fpMain);
            this.panel4.Controls.Add(this.labelControl4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1600, 364);
            this.panel4.TabIndex = 6;
            // 
            // fpMain
            // 
            this.fpMain._menu_name = null;
            this.fpMain._user_account = null;
            this.fpMain.AccessibleDescription = "";
            this.fpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpMain.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpMain.Location = new System.Drawing.Point(0, 20);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(1600, 344);
            this.fpMain.TabIndex = 5;
            // 
            // fpMain_Sheet1
            // 
            this.fpMain_Sheet1.Reset();
            fpMain_Sheet1.SheetName = "Sheet1";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl4.Location = new System.Drawing.Point(0, 0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(1600, 20);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = " - 상세내역 - ";
          
            // 
            // BaseForm2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1600, 514);
            this.Name = "BaseForm2";
            this.Text = "UserInformation";
            this.WindowName = "UserInformation";
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).EndInit();
            this._pnContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).EndInit();
            this._pnHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Core.xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private System.Windows.Forms.Panel SEARCH_PAN;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}