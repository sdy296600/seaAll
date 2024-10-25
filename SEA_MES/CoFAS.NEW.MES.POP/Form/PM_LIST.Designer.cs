
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.POP
{
    partial class PM_LIST
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
            this.fpMain = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpMain_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.xFpSpread1 = new CoFAS.NEW.MES.Core.xFpSpread();
            this.xFpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFpSpread1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFpSpread1_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.fpMain, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.xFpSpread1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1170, 578);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1168, 20);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.lblClose.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(1138, 0);
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
            this._Title.Size = new System.Drawing.Size(1138, 20);
            this._Title.TabIndex = 0;
            this._Title.Text = "PM_LIST";
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
         
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // fpMain
            // 
            this.fpMain._menu_name = null;
            this.fpMain._user_account = null;
            this.fpMain.AccessibleDescription = "";
            this.fpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpMain.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpMain.Location = new System.Drawing.Point(4, 56);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(1162, 496);
            this.fpMain.TabIndex = 6;
            // 
            // fpMain_Sheet1
            // 
            this.fpMain_Sheet1.Reset();
            fpMain_Sheet1.SheetName = "Sheet1";
            // 
            // xFpSpread1
            // 
            this.xFpSpread1._menu_name = null;
            this.xFpSpread1._user_account = null;
            this.xFpSpread1.AccessibleDescription = "";
            this.xFpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xFpSpread1.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.xFpSpread1.Location = new System.Drawing.Point(4, 570);
            this.xFpSpread1.Name = "xFpSpread1";
            this.xFpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.xFpSpread1_Sheet1});
            this.xFpSpread1.Size = new System.Drawing.Size(1162, 4);
            this.xFpSpread1.TabIndex = 7;
            // 
            // xFpSpread1_Sheet1
            // 
            this.xFpSpread1_Sheet1.Reset();
            xFpSpread1_Sheet1.SheetName = "Sheet1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1168, 30);
            this.label5.TabIndex = 24;
            this.label5.Text = "▣현황 영역";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // PM_LIST
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1170, 578);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PM_LIST";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PM_LIST";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFpSpread1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xFpSpread1_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _Title;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private xFpSpread xFpSpread1;
        private FarPoint.Win.Spread.SheetView xFpSpread1_Sheet1;
        private System.Windows.Forms.Label label5;
    }
}