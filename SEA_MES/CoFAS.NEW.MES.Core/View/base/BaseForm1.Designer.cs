namespace CoFAS.NEW.MES.Core
{
    partial class BaseForm1
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
            this._PAN_WHERE = new System.Windows.Forms.Panel();
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
            this._pnContent.Size = new System.Drawing.Size(1600, 450);
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
            this.panel1.Controls.Add(this._PAN_WHERE);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1600, 150);
            this.panel1.TabIndex = 6;
            // 
            // _PAN_WHERE
            // 
            this._PAN_WHERE.AutoScroll = true;
            this._PAN_WHERE.BackColor = System.Drawing.Color.White;
            this._PAN_WHERE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._PAN_WHERE.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PAN_WHERE.Location = new System.Drawing.Point(0, 20);
            this._PAN_WHERE.Name = "_PAN_WHERE";
            this._PAN_WHERE.Size = new System.Drawing.Size(1600, 130);
            this._PAN_WHERE.TabIndex = 1;
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
            this.panel4.Size = new System.Drawing.Size(1600, 450);
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
            this.fpMain.Size = new System.Drawing.Size(1600, 430);
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
            this.labelControl4.Appearance.ForeColor = System.Drawing.SystemColors.Window;
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
            // BaseForm1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1600, 600);
            this.Name = "BaseForm1";
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

        public System.Windows.Forms.Panel panel1;
        public DevExpress.XtraEditors.LabelControl labelControl1;
        public System.Windows.Forms.Panel _PAN_WHERE;
        public System.Windows.Forms.Panel panel4;
        public DevExpress.XtraEditors.LabelControl labelControl4;
        public Core.xFpSpread fpMain;
        public FarPoint.Win.Spread.SheetView fpMain_Sheet1;
    }
}