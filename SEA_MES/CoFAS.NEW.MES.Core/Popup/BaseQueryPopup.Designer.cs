
namespace CoFAS.NEW.MES.Core
{
    partial class BaseQueryPopup
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblClose = new System.Windows.Forms.Label();
            this._Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.fpMain = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpMain_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.SEARCH_PAN = new System.Windows.Forms.Panel();
            this.btnCmd01 = new System.Windows.Forms.Button();
            this.btnCmd02 = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCmd04 = new System.Windows.Forms.Button();
            this.btnCmd03 = new System.Windows.Forms.Button();
            this.btnCmd05 = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblCtl02 = new DevExpress.XtraEditors.LabelControl();
            this.lblCtl01 = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 572);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Controls.Add(this.lblClose, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._Title, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(508, 20);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.lblClose.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(478, 0);
            this.lblClose.Margin = new System.Windows.Forms.Padding(0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(30, 20);
            this.lblClose.TabIndex = 5;
            this.lblClose.Text = "X";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // _Title
            // 
            this._Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this._Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Title.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this._Title.ForeColor = System.Drawing.Color.White;
            this._Title.Location = new System.Drawing.Point(0, 0);
            this._Title.Margin = new System.Windows.Forms.Padding(0);
            this._Title.Name = "_Title";
            this._Title.Size = new System.Drawing.Size(478, 20);
            this._Title.TabIndex = 0;
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.labelControl4, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.fpMain, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 25);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.40406F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.59594F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(502, 543);
            this.tableLayoutPanel4.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl4.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl4.Location = new System.Drawing.Point(0, 76);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(502, 24);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = " - 상세내역 - ";
            this.labelControl4.Click += new System.EventHandler(this.labelControl4_Click);
            // 
            // fpMain
            // 
            this.fpMain._menu_name = null;
            this.fpMain._user_account = null;
            this.fpMain.AccessibleDescription = "";
            this.fpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpMain.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpMain.Location = new System.Drawing.Point(3, 103);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(496, 203);
            this.fpMain.TabIndex = 8;
            this.fpMain.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpMain_CellClick);
            // 
            // fpMain_Sheet1
            // 
            this.fpMain_Sheet1.Reset();
            fpMain_Sheet1.SheetName = "Sheet1";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.SEARCH_PAN, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCmd01, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnCmd02, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(502, 76);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // SEARCH_PAN
            // 
            this.SEARCH_PAN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SEARCH_PAN.Location = new System.Drawing.Point(3, 3);
            this.SEARCH_PAN.Name = "SEARCH_PAN";
            this.tableLayoutPanel3.SetRowSpan(this.SEARCH_PAN, 2);
            this.SEARCH_PAN.Size = new System.Drawing.Size(395, 70);
            this.SEARCH_PAN.TabIndex = 0;
            // 
            // btnCmd01
            // 
            this.btnCmd01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCmd01.Location = new System.Drawing.Point(404, 3);
            this.btnCmd01.Name = "btnCmd01";
            this.btnCmd01.Size = new System.Drawing.Size(95, 32);
            this.btnCmd01.TabIndex = 1;
            this.btnCmd01.Text = "초기화";
            this.btnCmd01.UseVisualStyleBackColor = true;
            this.btnCmd01.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // btnCmd02
            // 
            this.btnCmd02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCmd02.Location = new System.Drawing.Point(404, 41);
            this.btnCmd02.Name = "btnCmd02";
            this.btnCmd02.Size = new System.Drawing.Size(95, 32);
            this.btnCmd02.TabIndex = 2;
            this.btnCmd02.Text = "조회";
            this.btnCmd02.UseVisualStyleBackColor = true;
            this.btnCmd02.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Controls.Add(this.btnCmd04, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnCmd03, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnCmd05, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 312);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.90749F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.09251F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(496, 228);
            this.tableLayoutPanel5.TabIndex = 9;
            // 
            // btnCmd04
            // 
            this.btnCmd04.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCmd04.Location = new System.Drawing.Point(3, 3);
            this.btnCmd04.Name = "btnCmd04";
            this.btnCmd04.Size = new System.Drawing.Size(159, 46);
            this.btnCmd04.TabIndex = 1;
            this.btnCmd04.Text = "저장";
            this.btnCmd04.UseVisualStyleBackColor = true;
            this.btnCmd04.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // btnCmd03
            // 
            this.btnCmd03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCmd03.Location = new System.Drawing.Point(168, 3);
            this.btnCmd03.Name = "btnCmd03";
            this.btnCmd03.Size = new System.Drawing.Size(159, 46);
            this.btnCmd03.TabIndex = 0;
            this.btnCmd03.Text = "검증";
            this.btnCmd03.UseVisualStyleBackColor = true;
            this.btnCmd03.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // btnCmd05
            // 
            this.btnCmd05.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCmd05.Location = new System.Drawing.Point(333, 3);
            this.btnCmd05.Name = "btnCmd05";
            this.btnCmd05.Size = new System.Drawing.Size(160, 46);
            this.btnCmd05.TabIndex = 2;
            this.btnCmd05.Text = "닫기";
            this.btnCmd05.UseVisualStyleBackColor = true;
            this.btnCmd05.Click += new System.EventHandler(this.btnCmd_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel5.SetColumnSpan(this.tableLayoutPanel6, 3);
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.85714F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.14286F));
            this.tableLayoutPanel6.Controls.Add(this.lblCtl01, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lblCtl02, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.labelControl1, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.labelControl2, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 55);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.7929F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.2071F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(490, 170);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl1.Location = new System.Drawing.Point(3, 3);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(399, 19);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = " - 쿼리 검증 - ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.labelControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelControl2.Location = new System.Drawing.Point(408, 3);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(79, 19);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "- 쿼리 결과 - ";
            // 
            // lblCtl02
            // 
            this.lblCtl02.Appearance.BackColor = System.Drawing.Color.White;
            this.lblCtl02.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblCtl02.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblCtl02.Appearance.Options.UseBackColor = true;
            this.lblCtl02.Appearance.Options.UseFont = true;
            this.lblCtl02.Appearance.Options.UseForeColor = true;
            this.lblCtl02.Appearance.Options.UseTextOptions = true;
            this.lblCtl02.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblCtl02.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCtl02.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblCtl02.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCtl02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCtl02.Location = new System.Drawing.Point(408, 28);
            this.lblCtl02.Name = "lblCtl02";
            this.lblCtl02.Size = new System.Drawing.Size(79, 139);
            this.lblCtl02.TabIndex = 10;
            this.lblCtl02.Text = " - 쿼리 결과 - ";
            // 
            // lblCtl01
            // 
            this.lblCtl01.Appearance.BackColor = System.Drawing.Color.White;
            this.lblCtl01.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblCtl01.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblCtl01.Appearance.Options.UseBackColor = true;
            this.lblCtl01.Appearance.Options.UseFont = true;
            this.lblCtl01.Appearance.Options.UseForeColor = true;
            this.lblCtl01.Appearance.Options.UseTextOptions = true;
            this.lblCtl01.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblCtl01.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCtl01.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblCtl01.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCtl01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCtl01.Location = new System.Drawing.Point(3, 28);
            this.lblCtl01.Name = "lblCtl01";
            this.lblCtl01.Size = new System.Drawing.Size(399, 139);
            this.lblCtl01.TabIndex = 11;
            this.lblCtl01.Text = " - 쿼리 검증 - ";
            // 
            // BaseQueryPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(510, 572);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseQueryPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BaseCompanyPopupBox";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _Title;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel SEARCH_PAN;
        private System.Windows.Forms.Button btnCmd01;
        private System.Windows.Forms.Button btnCmd02;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btnCmd03;
        private System.Windows.Forms.Button btnCmd04;
        private System.Windows.Forms.Button btnCmd05;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblCtl01;
        private DevExpress.XtraEditors.LabelControl lblCtl02;
    }
}