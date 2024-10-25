
namespace CoFAS.NEW.MES.POP
{
    partial class 간판POP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(간판POP));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblClose = new System.Windows.Forms.Label();
            this._Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.base_FromtoDateTime1 = new CoFAS.NEW.MES.Core.Base_FromtoDateTime();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_포장수량 = new CoFAS.NEW.MES.Core.ucTextEdit();
            this.lbl_품번 = new System.Windows.Forms.Label();
            this.lbl_품목명 = new System.Windows.Forms.Label();
            this.lbl_지시수량 = new System.Windows.Forms.Label();
            this.lbl_양품수량 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_비고 = new CoFAS.NEW.MES.Core.ucTextEdit();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.fpMain = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpMain_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 1024);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Controls.Add(this.lblClose, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this._Title, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1278, 30);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.lblClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClose.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(1244, 0);
            this.lblClose.Margin = new System.Windows.Forms.Padding(0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(34, 30);
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
            this._Title.Size = new System.Drawing.Size(1244, 30);
            this._Title.TabIndex = 0;
            this._Title.Text = "System Main";
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1272, 985);
            this.panel1.TabIndex = 6;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.fpMain, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1272, 985);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 9;
            this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.54931F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.21417F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.6441224F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 385F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.button4, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.button5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.button7, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.button8, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.base_FromtoDateTime1, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1262, 68);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 68);
            this.label1.TabIndex = 1;
            this.label1.Text = "[간편 라벨 설정]";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button4
            // 
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.Location = new System.Drawing.Point(308, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 62);
            this.button4.TabIndex = 5;
            this.button4.Text = "생성";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.Location = new System.Drawing.Point(227, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 62);
            this.button5.TabIndex = 6;
            this.button5.Text = "조회";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button7.Location = new System.Drawing.Point(1098, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 62);
            this.button7.TabIndex = 2;
            this.button7.Text = "출력";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.Location = new System.Drawing.Point(1179, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(80, 62);
            this.button8.TabIndex = 2;
            this.button8.Text = "닫기";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // base_FromtoDateTime1
            // 
            this.base_FromtoDateTime1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.base_FromtoDateTime1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.base_FromtoDateTime1.EndValue = new System.DateTime(2024, 10, 31, 23, 59, 0, 0);
            this.base_FromtoDateTime1.Location = new System.Drawing.Point(629, 0);
            this.base_FromtoDateTime1.Margin = new System.Windows.Forms.Padding(0);
            this.base_FromtoDateTime1.Name = "base_FromtoDateTime1";
            this.base_FromtoDateTime1.SearchName = "날짜";
            this.base_FromtoDateTime1.Size = new System.Drawing.Size(385, 68);
            this.base_FromtoDateTime1.StartValue = new System.DateTime(2024, 10, 1, 0, 0, 0, 0);
            this.base_FromtoDateTime1.TabIndex = 7;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.txt_포장수량, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.lbl_품번, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_품목명, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbl_지시수량, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.lbl_양품수량, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.txt_비고, 1, 5);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(5, 107);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(500, 432);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(5, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 50);
            this.label3.TabIndex = 1;
            this.label3.Text = "품번";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(5, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 50);
            this.label4.TabIndex = 2;
            this.label4.Text = "품목명";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(5, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 50);
            this.label5.TabIndex = 3;
            this.label5.Text = "지시수량";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(5, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 50);
            this.label6.TabIndex = 4;
            this.label6.Text = "양품수량";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(5, 210);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 50);
            this.label2.TabIndex = 11;
            this.label2.Text = "포장수량";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_포장수량
            // 
            this.txt_포장수량.AutoSize = true;
            this.txt_포장수량.CancelButton = null;
            this.txt_포장수량.CommandButton = null;
            this.txt_포장수량.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_포장수량.EditMask = "";
            this.txt_포장수량.Location = new System.Drawing.Point(206, 215);
            this.txt_포장수량.Margin = new System.Windows.Forms.Padding(5);
            this.txt_포장수량.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_포장수량.MaxLength = 0;
            this.txt_포장수량.Name = "txt_포장수량";
            this.txt_포장수량.NumText = "";
            this.txt_포장수량.PasswordChar = '\0';
            this.txt_포장수량.ReadOnly = false;
            this.txt_포장수량.Size = new System.Drawing.Size(287, 40);
            this.txt_포장수량.TabIndex = 16;
            this.txt_포장수량.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_포장수량.ToolTipt = "";
            this.txt_포장수량.UseMaskAsDisplayFormat = false;
            // 
            // lbl_품번
            // 
            this.lbl_품번.AutoSize = true;
            this.lbl_품번.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_품번.Location = new System.Drawing.Point(204, 2);
            this.lbl_품번.Name = "lbl_품번";
            this.lbl_품번.Size = new System.Drawing.Size(291, 50);
            this.lbl_품번.TabIndex = 17;
            this.lbl_품번.Text = "-";
            this.lbl_품번.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_품목명
            // 
            this.lbl_품목명.AutoSize = true;
            this.lbl_품목명.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_품목명.Location = new System.Drawing.Point(204, 54);
            this.lbl_품목명.Name = "lbl_품목명";
            this.lbl_품목명.Size = new System.Drawing.Size(291, 50);
            this.lbl_품목명.TabIndex = 18;
            this.lbl_품목명.Text = "-";
            this.lbl_품목명.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_지시수량
            // 
            this.lbl_지시수량.AutoSize = true;
            this.lbl_지시수량.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_지시수량.Location = new System.Drawing.Point(204, 106);
            this.lbl_지시수량.Name = "lbl_지시수량";
            this.lbl_지시수량.Size = new System.Drawing.Size(291, 50);
            this.lbl_지시수량.TabIndex = 19;
            this.lbl_지시수량.Text = "-";
            this.lbl_지시수량.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_양품수량
            // 
            this.lbl_양품수량.AutoSize = true;
            this.lbl_양품수량.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_양품수량.Location = new System.Drawing.Point(204, 158);
            this.lbl_양품수량.Name = "lbl_양품수량";
            this.lbl_양품수량.Size = new System.Drawing.Size(291, 50);
            this.lbl_양품수량.TabIndex = 20;
            this.lbl_양품수량.Text = "-";
            this.lbl_양품수량.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(5, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(191, 168);
            this.label8.TabIndex = 11;
            this.label8.Text = "비고";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_비고
            // 
            this.txt_비고.AutoSize = true;
            this.txt_비고.CancelButton = null;
            this.txt_비고.CommandButton = null;
            this.txt_비고.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_비고.EditMask = "";
            this.txt_비고.Location = new System.Drawing.Point(206, 267);
            this.txt_비고.Margin = new System.Windows.Forms.Padding(5);
            this.txt_비고.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_비고.MaxLength = 0;
            this.txt_비고.Name = "txt_비고";
            this.txt_비고.NumText = "";
            this.txt_비고.PasswordChar = '\0';
            this.txt_비고.ReadOnly = false;
            this.txt_비고.Size = new System.Drawing.Size(287, 158);
            this.txt_비고.TabIndex = 22;
            this.txt_비고.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_비고.ToolTipt = "";
            this.txt_비고.UseMaskAsDisplayFormat = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.checkBox2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(513, 547);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(754, 433);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox2.Location = new System.Drawing.Point(5, 5);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(744, 34);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "출력 미리보기";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(5, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(744, 381);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // fpMain
            // 
            this.fpMain._menu_name = null;
            this.fpMain._user_account = null;
            this.fpMain.AccessibleDescription = "";
            this.fpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpMain.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpMain.Location = new System.Drawing.Point(513, 107);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(754, 432);
            this.fpMain.TabIndex = 4;
            // 
            // fpMain_Sheet1
            // 
            this.fpMain_Sheet1.Reset();
            fpMain_Sheet1.SheetName = "Sheet1";
            // 
            // 간판POP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(220)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "간판POP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "간판POP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpMain_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _Title;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private Core.ucTextEdit txt_포장수량;
        private System.Windows.Forms.Label lbl_품번;
        private System.Windows.Forms.Label lbl_품목명;
        private System.Windows.Forms.Label lbl_지시수량;
        private System.Windows.Forms.Label lbl_양품수량;
        private Core.xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private System.Windows.Forms.Label label8;
        private Core.ucTextEdit txt_비고;
        private Core.Base_FromtoDateTime base_FromtoDateTime1;
    }
}