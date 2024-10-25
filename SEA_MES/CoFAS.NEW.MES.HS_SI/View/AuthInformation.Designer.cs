namespace CoFAS.NEW.MES.HS_SI
{
    partial class AuthInformation
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
            this.panel2 = new System.Windows.Forms.Panel();
            this._AuthCopy = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.fpLeft = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpLeft_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.fpMain = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpMain_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this._Authority_name = new CoFAS.NEW.MES.Core.ucTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).BeginInit();
            this._pnContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).BeginInit();
            this._pnHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // _pnContent
            // 
            this._pnContent.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnContent.Appearance.Options.UseBackColor = true;
            this._pnContent.Controls.Add(this.splitContainerControl1);
            this._pnContent.Location = new System.Drawing.Point(0, 90);
            this._pnContent.Size = new System.Drawing.Size(800, 360);
            // 
            // _pnHeader
            // 
            this._pnHeader.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnHeader.Appearance.Options.UseBackColor = true;
            this._pnHeader.Controls.Add(this.panel1);
            this._pnHeader.Size = new System.Drawing.Size(800, 90);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 90);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this._AuthCopy);
            this.panel2.Controls.Add(this.panelControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 68);
            this.panel2.TabIndex = 1;
            // 
            // _AuthCopy
            // 
            this._AuthCopy.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._AuthCopy.Appearance.Options.UseFont = true;
            this._AuthCopy.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this._AuthCopy.Location = new System.Drawing.Point(421, 5);
            this._AuthCopy.Name = "_AuthCopy";
            this._AuthCopy.Size = new System.Drawing.Size(75, 26);
            this._AuthCopy.TabIndex = 1;
            this._AuthCopy.Text = "권한복사";
            this._AuthCopy.Visible = false;
            this._AuthCopy.Click += new System.EventHandler(this.SimpleButton_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this._Authority_name);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Location = new System.Drawing.Point(5, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(300, 26);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(0, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 26);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "권한명";
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
            this.labelControl1.Size = new System.Drawing.Size(800, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = " - 조회조건 - ";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.fpLeft);
            this.splitContainerControl1.Panel1.Controls.Add(this.labelControl3);
            this.splitContainerControl1.Panel1.MinSize = 300;
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.fpMain);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl4);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(800, 360);
            this.splitContainerControl1.SplitterPosition = 258;
            this.splitContainerControl1.TabIndex = 2;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // fpLeft
            // 
            this.fpLeft._menu_name = null;
            this.fpLeft._user_account = null;
            this.fpLeft.AccessibleDescription = "";
            this.fpLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpLeft.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpLeft.Location = new System.Drawing.Point(0, 22);
            this.fpLeft.Name = "fpLeft";
            this.fpLeft.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpLeft_Sheet1});
            this.fpLeft.Size = new System.Drawing.Size(300, 338);
            this.fpLeft.TabIndex = 3;
            // 
            // fpLeft_Sheet1
            // 
            this.fpLeft_Sheet1.Reset();
            fpLeft_Sheet1.SheetName = "Sheet1";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl3.Location = new System.Drawing.Point(0, 0);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(300, 22);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = " - 권한 목록 - ";
            // 
            // fpMain
            // 
            this.fpMain._menu_name = null;
            this.fpMain._user_account = null;
            this.fpMain.AccessibleDescription = "";
            this.fpMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpMain.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpMain.Location = new System.Drawing.Point(0, 22);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(495, 338);
            this.fpMain.TabIndex = 2;
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
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl4.Location = new System.Drawing.Point(0, 0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(495, 22);
            this.labelControl4.TabIndex = 1;
            this.labelControl4.Text = " - 권한 상세 - ";
            // 
            // _Authority_name
            // 
            this._Authority_name.AutoSize = true;
            this._Authority_name.CancelButton = null;
            this._Authority_name.CommandButton = null;
            this._Authority_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Authority_name.EditMask = "";
            this._Authority_name.Location = new System.Drawing.Point(70, 0);
            this._Authority_name.Margin = new System.Windows.Forms.Padding(0);
            this._Authority_name.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this._Authority_name.MaxLength = 0;
            this._Authority_name.Name = "_Authority_name";
            this._Authority_name.NumText = "";
            this._Authority_name.PasswordChar = '\0';
            this._Authority_name.ReadOnly = false;
            this._Authority_name.Size = new System.Drawing.Size(230, 26);
            this._Authority_name.TabIndex = 1;
            this._Authority_name.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this._Authority_name.ToolTipt = "";
            this._Authority_name.UseMaskAsDisplayFormat = false;
            // 
            // AuthInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AuthInformation";
            this.Text = "AuthInformation";
            this.WindowName = "AuthInformation";
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).EndInit();
            this._pnContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).EndInit();
            this._pnHeader.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Core.xFpSpread fpLeft;
        private FarPoint.Win.Spread.SheetView fpLeft_Sheet1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Core.xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton _AuthCopy;
        private Core.ucTextEdit _Authority_name;
    }
}