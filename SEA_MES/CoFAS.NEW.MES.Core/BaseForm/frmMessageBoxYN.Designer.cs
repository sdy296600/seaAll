namespace CoFAS.NEW.MES.Core
{
    partial class frmMessageBoxYN
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
            this.panel2 = new System.Windows.Forms.Panel();
            this._No = new System.Windows.Forms.Button();
            this._Yes = new System.Windows.Forms.Button();
            this._Message = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this._Title = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this._No);
            this.panel2.Controls.Add(this._Yes);
            this.panel2.Controls.Add(this._Message);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 185);
            this.panel2.TabIndex = 3;
            // 
            // _No
            // 
            this._No.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._No.DialogResult = System.Windows.Forms.DialogResult.No;
            this._No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._No.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._No.ForeColor = System.Drawing.Color.Black;
            this._No.Location = new System.Drawing.Point(247, 148);
            this._No.Name = "_No";
            this._No.Size = new System.Drawing.Size(104, 30);
            this._No.TabIndex = 4;
            this._No.Text = "No";
            this._No.UseVisualStyleBackColor = true;
            this._No.Click += new System.EventHandler(this.btn_Click);
            // 
            // _Yes
            // 
            this._Yes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this._Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._Yes.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._Yes.ForeColor = System.Drawing.Color.Black;
            this._Yes.Location = new System.Drawing.Point(139, 148);
            this._Yes.Name = "_Yes";
            this._Yes.Size = new System.Drawing.Size(102, 30);
            this._Yes.TabIndex = 3;
            this._Yes.Text = "Yes";
            this._Yes.UseVisualStyleBackColor = true;
            this._Yes.Click += new System.EventHandler(this.btn_Click);
            // 
            // _Message
            // 
            this._Message.Dock = System.Windows.Forms.DockStyle.Top;
            this._Message.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this._Message.ForeColor = System.Drawing.Color.Black;
            this._Message.Location = new System.Drawing.Point(0, 0);
            this._Message.Name = "_Message";
            this._Message.Size = new System.Drawing.Size(494, 145);
            this._Message.TabIndex = 1;
            this._Message.Text = "Message";
            this._Message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this._Title);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 22);
            this.panel1.TabIndex = 2;
            // 
            // _Title
            // 
            this._Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this._Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Title.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this._Title.ForeColor = System.Drawing.Color.White;
            this._Title.Location = new System.Drawing.Point(0, 0);
            this._Title.Name = "_Title";
            this._Title.Size = new System.Drawing.Size(496, 22);
            this._Title.TabIndex = 0;
            this._Title.Text = "Title";
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // frmMessageBoxYN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(496, 207);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMessageBoxYN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmMessageBoxYN";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Button _No;
        public System.Windows.Forms.Button _Yes;
        public System.Windows.Forms.Label _Message;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label _Title;
    }
}