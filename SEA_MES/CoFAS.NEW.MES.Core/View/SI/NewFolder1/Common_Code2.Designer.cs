namespace CoFAS.NEW.MES.Core
{
    partial class Common_Code2
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._lv3 = new System.Windows.Forms.Label();
            this._lv2 = new System.Windows.Forms.Label();
            this._lv1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fpMain = new NEW.MES.Core.xFpSpread();
            this.fpMain_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fpSub1 = new NEW.MES.Core.xFpSpread();
            this.sheetView1 = new FarPoint.Win.Spread.SheetView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.fpSub2 = new NEW.MES.Core.xFpSpread();
            this.fpSub2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).BeginInit();
            this._pnContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).BeginInit();
            this._pnHeader.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSub1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpSub2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSub2_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // _pnContent
            // 
            this._pnContent.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnContent.Appearance.Options.UseBackColor = true;
            this._pnContent.Controls.Add(this.tableLayoutPanel1);
            this._pnContent.Location = new System.Drawing.Point(0, 64);
            this._pnContent.Margin = new System.Windows.Forms.Padding(0);
            this._pnContent.Size = new System.Drawing.Size(800, 336);
            // 
            // _pnHeader
            // 
            this._pnHeader.Appearance.BackColor = System.Drawing.Color.Transparent;
            this._pnHeader.Appearance.Options.UseBackColor = true;
            this._pnHeader.Controls.Add(this.panel3);
            this._pnHeader.Size = new System.Drawing.Size(800, 64);
            this._pnHeader.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._lv3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this._lv2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this._lv1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 336);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // _lv3
            // 
            this._lv3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this._lv3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lv3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._lv3.ForeColor = System.Drawing.Color.White;
            this._lv3.Location = new System.Drawing.Point(653, 0);
            this._lv3.Name = "_lv3";
            this._lv3.Size = new System.Drawing.Size(144, 20);
            this._lv3.TabIndex = 18;
            this._lv3.Text = " - 소분류 - ";
            this._lv3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lv3.Click += new System.EventHandler(this.lbl_Click);
            // 
            // _lv2
            // 
            this._lv2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this._lv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lv2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._lv2.ForeColor = System.Drawing.Color.White;
            this._lv2.Location = new System.Drawing.Point(303, 0);
            this._lv2.Name = "_lv2";
            this._lv2.Size = new System.Drawing.Size(344, 20);
            this._lv2.TabIndex = 17;
            this._lv2.Text = " - 중분류 - ";
            this._lv2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lv2.Click += new System.EventHandler(this.lbl_Click);
            // 
            // _lv1
            // 
            this._lv1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this._lv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lv1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._lv1.ForeColor = System.Drawing.Color.White;
            this._lv1.Location = new System.Drawing.Point(3, 0);
            this._lv1.Name = "_lv1";
            this._lv1.Size = new System.Drawing.Size(294, 20);
            this._lv1.TabIndex = 16;
            this._lv1.Text = " - 대분류 - ";
            this._lv1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lv1.Click += new System.EventHandler(this.lbl_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fpMain);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 310);
            this.panel1.TabIndex = 12;
            // 
            // fpMain
            // 
            this.fpMain.AccessibleDescription = "";
            this.fpMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpMain.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpMain.Location = new System.Drawing.Point(0, 0);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(294, 310);
            this.fpMain.TabIndex = 9;
            // 
            // fpMain_Sheet1
            // 
            this.fpMain_Sheet1.Reset();
            fpMain_Sheet1.SheetName = "Sheet1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpSub1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(303, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(344, 310);
            this.panel2.TabIndex = 13;
            // 
            // fpSub1
            // 
            this.fpSub1.AccessibleDescription = "";
            this.fpSub1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSub1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSub1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpSub1.Location = new System.Drawing.Point(0, 0);
            this.fpSub1.Name = "fpSub1";
            this.fpSub1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.sheetView1});
            this.fpSub1.Size = new System.Drawing.Size(344, 310);
            this.fpSub1.TabIndex = 11;
            // 
            // sheetView1
            // 
            this.sheetView1.Reset();
            sheetView1.SheetName = "Sheet1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.fpSub2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(653, 23);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(144, 310);
            this.panel4.TabIndex = 14;
            // 
            // fpSub2
            // 
            this.fpSub2.AccessibleDescription = "";
            this.fpSub2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpSub2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpSub2.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpSub2.Location = new System.Drawing.Point(0, 0);
            this.fpSub2.Name = "fpSub2";
            this.fpSub2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpSub2_Sheet1});
            this.fpSub2.Size = new System.Drawing.Size(144, 310);
            this.fpSub2.TabIndex = 12;
            // 
            // fpSub2_Sheet1
            // 
            this.fpSub2_Sheet1.Reset();
            fpSub2_Sheet1.SheetName = "Sheet1";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 64);
            this.panel3.TabIndex = 13;
            // 
            // Common_Code22
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Name = "Common_Code22";
            this.Text = "AuthInformation";
            this.WindowName = "AuthInformation";
            ((System.ComponentModel.ISupportInitialize)(this._pnContent)).EndInit();
            this._pnContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pnHeader)).EndInit();
            this._pnHeader.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSub1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sheetView1)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpSub2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpSub2_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NEW.MES.Core.xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private NEW.MES.Core.xFpSpread fpSub1;
        private FarPoint.Win.Spread.SheetView sheetView1;
        private System.Windows.Forms.Label _lv3;
        private System.Windows.Forms.Label _lv2;
        private System.Windows.Forms.Label _lv1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private NEW.MES.Core.xFpSpread fpSub2;
        private FarPoint.Win.Spread.SheetView fpSub2_Sheet1;
        private System.Windows.Forms.Panel panel3;
    }
}