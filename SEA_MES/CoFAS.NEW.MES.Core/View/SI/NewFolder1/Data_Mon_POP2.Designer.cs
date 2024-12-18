
namespace CoFAS.NEW.MES.POP
{
    partial class Data_Mon_POP2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Data_Mon_POP2));
            this.xTableLayoutPanel1 = new CoFAS.NEW.MES.Core.xTableLayoutPanel();
            this.SuspendLayout();
            // 
            // xTableLayoutPanel1
            // 
            this.xTableLayoutPanel1.ColumnCount = 2;
            this.xTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xTableLayoutPanel1.GraColorA = System.Drawing.Color.Empty;
            this.xTableLayoutPanel1.GraColorB = System.Drawing.Color.Empty;
            this.xTableLayoutPanel1.GradientFillStyle = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.xTableLayoutPanel1.Location = new System.Drawing.Point(360, 187);
            this.xTableLayoutPanel1.Name = "xTableLayoutPanel1";
            this.xTableLayoutPanel1.RowCount = 2;
            this.xTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xTableLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.xTableLayoutPanel1.TabIndex = 0;
            // 
            // Data_Mon_POP2
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(30)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.xTableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Data_Mon_POP2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "간판POP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private Core.xTableLayoutPanel xTableLayoutPanel1;
    }
}