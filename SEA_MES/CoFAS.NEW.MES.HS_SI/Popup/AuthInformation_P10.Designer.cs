
namespace CoFAS.NEW.MES.HT_SI
{
    partial class AuthInformation_P10
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
            this._Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.fpLeft = new Core.xFpSpread();
            this.fpLeft_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this._lblAccount = new System.Windows.Forms.Label();
            this._lblName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.fpRight = new Core.xFpSpread();
            this.fpRight_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel3 = new System.Windows.Forms.Panel();
            this._AuthCopy = new DevExpress.XtraEditors.SimpleButton();
            this._Close = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft_Sheet1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRight_Sheet1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 2F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this._Title, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 469);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _Title
            // 
            this._Title.BackColor = System.Drawing.Color.Silver;
            this._Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this._Title, 3);
            this._Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Title.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this._Title.Location = new System.Drawing.Point(1, 1);
            this._Title.Margin = new System.Windows.Forms.Padding(0);
            this._Title.Name = "_Title";
            this._Title.Size = new System.Drawing.Size(798, 20);
            this._Title.TabIndex = 0;
            this._Title.Text = "label1";
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fpLeft);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 2);
            this.panel1.Size = new System.Drawing.Size(397, 402);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Linen;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "- 원본사용자(1명만 선택) -";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fpLeft
            // 
            this.fpLeft.AccessibleDescription = "";
            this.fpLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpLeft.Location = new System.Drawing.Point(0, 18);
            this.fpLeft.Name = "fpLeft";
            this.fpLeft.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpLeft_Sheet1});
            this.fpLeft.Size = new System.Drawing.Size(397, 384);
            this.fpLeft.TabIndex = 1;
            // 
            // fpLeft_Sheet1
            // 
            this.fpLeft_Sheet1.Reset();
            fpLeft_Sheet1.SheetName = "Sheet1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel2.Controls.Add(this._lblName, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this._lblAccount, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(402, 22);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(397, 100);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Linen;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.tableLayoutPanel2.SetRowSpan(this.label2, 2);
            this.label2.Size = new System.Drawing.Size(98, 98);
            this.label2.TabIndex = 1;
            this.label2.Text = "원본사용자";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblAccount
            // 
            this._lblAccount.BackColor = System.Drawing.Color.Linen;
            this._lblAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lblAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblAccount.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this._lblAccount.Location = new System.Drawing.Point(100, 1);
            this._lblAccount.Margin = new System.Windows.Forms.Padding(0);
            this._lblAccount.Name = "_lblAccount";
            this._lblAccount.Size = new System.Drawing.Size(296, 48);
            this._lblAccount.TabIndex = 2;
            this._lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblName
            // 
            this._lblName.BackColor = System.Drawing.Color.Linen;
            this._lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._lblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lblName.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this._lblName.Location = new System.Drawing.Point(100, 50);
            this._lblName.Margin = new System.Windows.Forms.Padding(0);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(296, 49);
            this._lblName.TabIndex = 3;
            this._lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fpRight);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(402, 123);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(397, 301);
            this.panel2.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Linen;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(397, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "- 복사대상자 -";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fpRight
            // 
            this.fpRight.AccessibleDescription = "";
            this.fpRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fpRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpRight.Location = new System.Drawing.Point(0, 18);
            this.fpRight.Name = "fpRight";
            this.fpRight.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpRight_Sheet1});
            this.fpRight.Size = new System.Drawing.Size(397, 283);
            this.fpRight.TabIndex = 2;
            // 
            // fpRight_Sheet1
            // 
            this.fpRight_Sheet1.Reset();
            fpRight_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.fpRight_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpRight_Sheet1.ColumnFooterSheetCornerStyle.NoteIndicatorColor = System.Drawing.Color.Red;
            this.fpRight_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 3);
            this.panel3.Controls.Add(this._Close);
            this.panel3.Controls.Add(this._AuthCopy);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 425);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(798, 43);
            this.panel3.TabIndex = 1;
            // 
            // _AuthCopy
            // 
            this._AuthCopy.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._AuthCopy.Appearance.Options.UseFont = true;
            this._AuthCopy.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this._AuthCopy.Location = new System.Drawing.Point(321, 9);
            this._AuthCopy.Name = "_AuthCopy";
            this._AuthCopy.Size = new System.Drawing.Size(75, 23);
            this._AuthCopy.TabIndex = 1;
            this._AuthCopy.Text = "권한복사";
            this._AuthCopy.Click += new System.EventHandler(this.SimpleButton_Click);
            // 
            // _Close
            // 
            this._Close.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._Close.Appearance.Options.UseFont = true;
            this._Close.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this._Close.Location = new System.Drawing.Point(400, 9);
            this._Close.Name = "_Close";
            this._Close.Size = new System.Drawing.Size(75, 23);
            this._Close.TabIndex = 2;
            this._Close.Text = "닫기";
            this._Close.Click += new System.EventHandler(this.SimpleButton_Click);
            // 
            // AuthInformation_P10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 469);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AuthInformation_P10";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AuthInformation_P10";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpLeft_Sheet1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpRight_Sheet1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _Title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Core.xFpSpread fpLeft;
        private FarPoint.Win.Spread.SheetView fpLeft_Sheet1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblAccount;
        private System.Windows.Forms.Panel panel2;
        private Core.xFpSpread fpRight;
        private FarPoint.Win.Spread.SheetView fpRight_Sheet1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton _Close;
        private DevExpress.XtraEditors.SimpleButton _AuthCopy;
    }
}