namespace CoFAS.NEW.MES.Core
{
    partial class frmBaseNone
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaseNone));
            this._pnContent = new DevExpress.XtraEditors.PanelControl();
            this._pnHeader = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // _pnContent
            // 
            this._pnContent.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnContent.Appearance.Options.UseBackColor = true;
            this._pnContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnContent.Location = new System.Drawing.Point(0, 108);
            this._pnContent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pnContent.Name = "_pnContent";
            this._pnContent.Size = new System.Drawing.Size(1231, 521);
            this._pnContent.TabIndex = 4;
            this._pnContent.Paint += new System.Windows.Forms.PaintEventHandler(this._pnContent_Paint);
            // 
            // _pnHeader
            // 
            this._pnHeader.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnHeader.Appearance.Options.UseBackColor = true;
            this._pnHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this._pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this._pnHeader.Location = new System.Drawing.Point(0, 0);
            this._pnHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._pnHeader.Name = "_pnHeader";
            this._pnHeader.Size = new System.Drawing.Size(1231, 108);
            this._pnHeader.TabIndex = 3;
            // 
            // frmBaseNone
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1231, 629);
            this.Controls.Add(this._pnContent);
            this.Controls.Add(this._pnHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBaseNone";
            this.Text = "frmBaseNone";
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.PanelControl _pnContent;
        protected DevExpress.XtraEditors.PanelControl _pnHeader;
    }
}