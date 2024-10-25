namespace CoFAS.NEW.MES
{

    partial class frmSettingPw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettingPw));
            this._lblTitle = new System.Windows.Forms.Label();
            this._txtPw = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._lblfooter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblTitle
            // 
            this._lblTitle.BackColor = System.Drawing.Color.White;
            this._lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._lblTitle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this._lblTitle.ForeColor = System.Drawing.Color.Black;
            this._lblTitle.Location = new System.Drawing.Point(0, 0);
            this._lblTitle.Name = "_lblTitle";
            this._lblTitle.Size = new System.Drawing.Size(288, 21);
            this._lblTitle.TabIndex = 40;
            this._lblTitle.Text = " 데이터베이스 설정";
            this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _txtPw
            // 
            this._txtPw.Location = new System.Drawing.Point(9, 8);
            this._txtPw.Name = "_txtPw";
            this._txtPw.PasswordChar = '●';
            this._txtPw.Size = new System.Drawing.Size(265, 21);
            this._txtPw.TabIndex = 0;
            this._txtPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this._txtPw_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._txtPw);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 38);
            this.panel1.TabIndex = 44;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(3, 41);
            this.label2.TabIndex = 41;
            // 
            // _lblfooter
            // 
            this._lblfooter.BackColor = System.Drawing.Color.White;
            this._lblfooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._lblfooter.Location = new System.Drawing.Point(3, 59);
            this._lblfooter.Name = "_lblfooter";
            this._lblfooter.Size = new System.Drawing.Size(282, 3);
            this._lblfooter.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(285, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(3, 41);
            this.label1.TabIndex = 42;
            // 
            // frmSettingPw
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(288, 62);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._lblfooter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettingPw";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmSettingPw";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label _lblTitle;
        private System.Windows.Forms.TextBox _txtPw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lblfooter;
        private System.Windows.Forms.Label label1;
    }
}