
using CoFAS.NEW.MES.Core;

namespace CoFAS.NEW.MES.POP
{
    partial class frmPOPMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOPMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._uc_Clock1 = new _uc_Clock();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_비가동여부 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_최신데이터 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_STOCK_OUT_CODE = new System.Windows.Forms.Label();
            this.lbl_STOCK_NAME = new System.Windows.Forms.Label();
            this.lbl_STOCK_STANDARD = new System.Windows.Forms.Label();
            this.lbl_ProductionInstruct_Qty = new System.Windows.Forms.Label();
            this.lbl_Now_OK_Qty = new System.Windows.Forms.Label();
            this.lbl_Now_NG_Qty = new System.Windows.Forms.Label();
            this.lbl_all_Qty = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_Now_Qty = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_Instruc_date = new System.Windows.Forms.Label();
            this.lbl_Line = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_start_work = new System.Windows.Forms.Button();
            this.dtp_start_work = new CoFAS.NEW.MES.Core.ucDateEdit();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_end_work = new System.Windows.Forms.Button();
            this.dtp_end_work = new CoFAS.NEW.MES.Core.ucDateEdit();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lbl_작업LOT = new System.Windows.Forms.Label();
            this.lbl_최신수집LOT = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fpPOP = new CoFAS.NEW.MES.Core.xFpSpread();
            this.fpPOP_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_end = new System.Windows.Forms.Button();
            this.txt_coment = new System.Windows.Forms.TextBox();
            this.btn_coment = new System.Windows.Forms.Button();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btn_SA_Equipment_Check = new System.Windows.Forms.Button();
            this.btn_Equipment_Check = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_ProductionProgressStatus = new System.Windows.Forms.Button();
            this.btn_ManualResultt = new System.Windows.Forms.Button();
            this.btn_ProductionInstruct_select = new System.Windows.Forms.Button();
            this.btn_PR_Equipment_Check = new System.Windows.Forms.Button();
            this.btn_비가동등록 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpPOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPOP_Sheet1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 1024);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
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
            // _Title
            // 
            this._Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this._Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Title.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._Title.ForeColor = System.Drawing.Color.White;
            this._Title.Location = new System.Drawing.Point(0, 0);
            this._Title.Margin = new System.Windows.Forms.Padding(0);
            this._Title.Name = "_Title";
            this._Title.Size = new System.Drawing.Size(1278, 30);
            this._Title.TabIndex = 0;
            this._Title.Text = "Point of Production System";
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(1, 32);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1278, 960);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.00001F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999999F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._uc_Clock1, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.button1, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1272, 94);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(511, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 94);
            this.label1.TabIndex = 0;
            this.label1.Text = "POP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::CoFAS.NEW.MES.POP.Properties.Resources.logo_edit_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(254, 94);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // _uc_Clock1
            // 
            this._uc_Clock1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.SetColumnSpan(this._uc_Clock1, 2);
            this._uc_Clock1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._uc_Clock1.Location = new System.Drawing.Point(762, 0);
            this._uc_Clock1.Margin = new System.Windows.Forms.Padding(0);
            this._uc_Clock1.Name = "_uc_Clock1";
            this._uc_Clock1.Size = new System.Drawing.Size(381, 94);
            this._uc_Clock1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(1146, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 88);
            this.button1.TabIndex = 3;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Close_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel3.SetColumnSpan(this.tableLayoutPanel5, 2);
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.Controls.Add(this.label19, 2, 6);
            this.tableLayoutPanel5.Controls.Add(this.lbl_비가동여부, 3, 6);
            this.tableLayoutPanel5.Controls.Add(this.label20, 2, 7);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbl_최신데이터, 3, 7);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.lbl_STOCK_OUT_CODE, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_STOCK_NAME, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lbl_STOCK_STANDARD, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.lbl_ProductionInstruct_Qty, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.lbl_Now_OK_Qty, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.lbl_Now_NG_Qty, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.lbl_all_Qty, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.label12, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.lbl_Now_Qty, 1, 7);
            this.tableLayoutPanel5.Controls.Add(this.label14, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.label13, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.label10, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.label11, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.lbl_Instruc_date, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.lbl_Line, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 3, 2);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel9, 3, 3);
            this.tableLayoutPanel5.Controls.Add(this.label15, 2, 5);
            this.tableLayoutPanel5.Controls.Add(this.label21, 2, 4);
            this.tableLayoutPanel5.Controls.Add(this.lbl_작업LOT, 3, 4);
            this.tableLayoutPanel5.Controls.Add(this.lbl_최신수집LOT, 3, 5);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel10, 0, 8);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(10, 110);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 9;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1258, 710);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(631, 363);
            this.label19.Margin = new System.Windows.Forms.Padding(3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(182, 54);
            this.label19.TabIndex = 0;
            this.label19.Text = "비가동 발생:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_비가동여부
            // 
            this.lbl_비가동여부.AutoSize = true;
            this.lbl_비가동여부.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_비가동여부.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_비가동여부.ForeColor = System.Drawing.Color.White;
            this.lbl_비가동여부.Location = new System.Drawing.Point(819, 363);
            this.lbl_비가동여부.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_비가동여부.Name = "lbl_비가동여부";
            this.lbl_비가동여부.Size = new System.Drawing.Size(436, 54);
            this.lbl_비가동여부.TabIndex = 2;
            this.lbl_비가동여부.Text = "-";
            this.lbl_비가동여부.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(631, 423);
            this.label20.Margin = new System.Windows.Forms.Padding(3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(182, 54);
            this.label20.TabIndex = 1;
            this.label20.Text = "최신 데이터:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 54);
            this.label2.TabIndex = 0;
            this.label2.Text = "제품 코드:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 54);
            this.label3.TabIndex = 0;
            this.label3.Text = "제품 명칭:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_최신데이터
            // 
            this.lbl_최신데이터.AutoSize = true;
            this.lbl_최신데이터.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_최신데이터.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_최신데이터.Location = new System.Drawing.Point(819, 423);
            this.lbl_최신데이터.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_최신데이터.Name = "lbl_최신데이터";
            this.lbl_최신데이터.Size = new System.Drawing.Size(436, 54);
            this.lbl_최신데이터.TabIndex = 3;
            this.lbl_최신데이터.Text = "-";
            this.lbl_최신데이터.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 123);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 54);
            this.label4.TabIndex = 0;
            this.label4.Text = "제품 규격:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 183);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(182, 54);
            this.label5.TabIndex = 0;
            this.label5.Text = "계획 수량:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 243);
            this.label6.Margin = new System.Windows.Forms.Padding(3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 54);
            this.label6.TabIndex = 0;
            this.label6.Text = "양품 수량:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 303);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 54);
            this.label7.TabIndex = 0;
            this.label7.Text = "불량 수량:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_STOCK_OUT_CODE
            // 
            this.lbl_STOCK_OUT_CODE.AutoSize = true;
            this.lbl_STOCK_OUT_CODE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_STOCK_OUT_CODE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_STOCK_OUT_CODE.Location = new System.Drawing.Point(191, 3);
            this.lbl_STOCK_OUT_CODE.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_STOCK_OUT_CODE.Name = "lbl_STOCK_OUT_CODE";
            this.lbl_STOCK_OUT_CODE.Size = new System.Drawing.Size(434, 54);
            this.lbl_STOCK_OUT_CODE.TabIndex = 2;
            this.lbl_STOCK_OUT_CODE.Text = "-";
            this.lbl_STOCK_OUT_CODE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_STOCK_NAME
            // 
            this.lbl_STOCK_NAME.AutoSize = true;
            this.lbl_STOCK_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_STOCK_NAME.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_STOCK_NAME.Location = new System.Drawing.Point(191, 63);
            this.lbl_STOCK_NAME.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_STOCK_NAME.Name = "lbl_STOCK_NAME";
            this.lbl_STOCK_NAME.Size = new System.Drawing.Size(434, 54);
            this.lbl_STOCK_NAME.TabIndex = 2;
            this.lbl_STOCK_NAME.Text = "-";
            this.lbl_STOCK_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_STOCK_STANDARD
            // 
            this.lbl_STOCK_STANDARD.AutoSize = true;
            this.lbl_STOCK_STANDARD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_STOCK_STANDARD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_STOCK_STANDARD.Location = new System.Drawing.Point(191, 123);
            this.lbl_STOCK_STANDARD.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_STOCK_STANDARD.Name = "lbl_STOCK_STANDARD";
            this.lbl_STOCK_STANDARD.Size = new System.Drawing.Size(434, 54);
            this.lbl_STOCK_STANDARD.TabIndex = 2;
            this.lbl_STOCK_STANDARD.Text = "-";
            this.lbl_STOCK_STANDARD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ProductionInstruct_Qty
            // 
            this.lbl_ProductionInstruct_Qty.AutoSize = true;
            this.lbl_ProductionInstruct_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ProductionInstruct_Qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ProductionInstruct_Qty.Location = new System.Drawing.Point(191, 183);
            this.lbl_ProductionInstruct_Qty.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_ProductionInstruct_Qty.Name = "lbl_ProductionInstruct_Qty";
            this.lbl_ProductionInstruct_Qty.Size = new System.Drawing.Size(434, 54);
            this.lbl_ProductionInstruct_Qty.TabIndex = 2;
            this.lbl_ProductionInstruct_Qty.Text = "0";
            this.lbl_ProductionInstruct_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Now_OK_Qty
            // 
            this.lbl_Now_OK_Qty.AutoSize = true;
            this.lbl_Now_OK_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Now_OK_Qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Now_OK_Qty.Location = new System.Drawing.Point(191, 243);
            this.lbl_Now_OK_Qty.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Now_OK_Qty.Name = "lbl_Now_OK_Qty";
            this.lbl_Now_OK_Qty.Size = new System.Drawing.Size(434, 54);
            this.lbl_Now_OK_Qty.TabIndex = 2;
            this.lbl_Now_OK_Qty.Text = "0";
            this.lbl_Now_OK_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Now_NG_Qty
            // 
            this.lbl_Now_NG_Qty.AutoSize = true;
            this.lbl_Now_NG_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Now_NG_Qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Now_NG_Qty.Location = new System.Drawing.Point(191, 303);
            this.lbl_Now_NG_Qty.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Now_NG_Qty.Name = "lbl_Now_NG_Qty";
            this.lbl_Now_NG_Qty.Size = new System.Drawing.Size(434, 54);
            this.lbl_Now_NG_Qty.TabIndex = 2;
            this.lbl_Now_NG_Qty.Text = "0";
            this.lbl_Now_NG_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_all_Qty
            // 
            this.lbl_all_Qty.AutoSize = true;
            this.lbl_all_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_all_Qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_all_Qty.Location = new System.Drawing.Point(191, 363);
            this.lbl_all_Qty.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_all_Qty.Name = "lbl_all_Qty";
            this.lbl_all_Qty.Size = new System.Drawing.Size(434, 54);
            this.lbl_all_Qty.TabIndex = 2;
            this.lbl_all_Qty.Text = "0";
            this.lbl_all_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 363);
            this.label8.Margin = new System.Windows.Forms.Padding(3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(182, 54);
            this.label8.TabIndex = 0;
            this.label8.Text = "생산 수량:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(3, 423);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(182, 54);
            this.label12.TabIndex = 0;
            this.label12.Text = "진행 수량:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Now_Qty
            // 
            this.lbl_Now_Qty.AutoSize = true;
            this.lbl_Now_Qty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Now_Qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Now_Qty.Location = new System.Drawing.Point(191, 423);
            this.lbl_Now_Qty.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Now_Qty.Name = "lbl_Now_Qty";
            this.lbl_Now_Qty.Size = new System.Drawing.Size(434, 54);
            this.lbl_Now_Qty.TabIndex = 2;
            this.lbl_Now_Qty.Text = "0";
            this.lbl_Now_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(631, 3);
            this.label14.Margin = new System.Windows.Forms.Padding(3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(182, 54);
            this.label14.TabIndex = 0;
            this.label14.Text = "지시 일자:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(631, 63);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(182, 54);
            this.label13.TabIndex = 0;
            this.label13.Text = "작업 라인:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(631, 123);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(182, 54);
            this.label10.TabIndex = 0;
            this.label10.Text = "작업 시작:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(631, 183);
            this.label11.Margin = new System.Windows.Forms.Padding(3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(182, 54);
            this.label11.TabIndex = 0;
            this.label11.Text = "작업 종료:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Instruc_date
            // 
            this.lbl_Instruc_date.AutoSize = true;
            this.lbl_Instruc_date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Instruc_date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Instruc_date.Location = new System.Drawing.Point(819, 3);
            this.lbl_Instruc_date.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Instruc_date.Name = "lbl_Instruc_date";
            this.lbl_Instruc_date.Size = new System.Drawing.Size(436, 54);
            this.lbl_Instruc_date.TabIndex = 2;
            this.lbl_Instruc_date.Text = "-";
            this.lbl_Instruc_date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Line
            // 
            this.lbl_Line.AutoSize = true;
            this.lbl_Line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Line.Location = new System.Drawing.Point(819, 63);
            this.lbl_Line.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_Line.Name = "lbl_Line";
            this.lbl_Line.Size = new System.Drawing.Size(436, 54);
            this.lbl_Line.TabIndex = 2;
            this.lbl_Line.Text = "-";
            this.lbl_Line.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel8.Controls.Add(this.btn_start_work, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.dtp_start_work, 0, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(819, 123);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(436, 54);
            this.tableLayoutPanel8.TabIndex = 6;
            // 
            // btn_start_work
            // 
            this.btn_start_work.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_start_work.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_start_work.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_start_work.FlatAppearance.BorderSize = 0;
            this.btn_start_work.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start_work.ForeColor = System.Drawing.Color.White;
            this.btn_start_work.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_start_work.Location = new System.Drawing.Point(289, 0);
            this.btn_start_work.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btn_start_work.Name = "btn_start_work";
            this.btn_start_work.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btn_start_work.Size = new System.Drawing.Size(147, 54);
            this.btn_start_work.TabIndex = 1;
            this.btn_start_work.Text = "시작";
            this.btn_start_work.UseVisualStyleBackColor = false;
            this.btn_start_work.Click += new System.EventHandler(this.btn_start_work_Click);
            this.btn_start_work.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_start_work_Paint);
            // 
            // dtp_start_work
            // 
            this.dtp_start_work.Appearance.Options.UseFont = true;
            this.dtp_start_work.DateEdit_Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtp_start_work.DateTime = new System.DateTime(2024, 3, 27, 9, 36, 8, 876);
            this.dtp_start_work.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtp_start_work.Location = new System.Drawing.Point(0, 0);
            this.dtp_start_work.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.dtp_start_work.Name = "dtp_start_work";
            this.dtp_start_work.ReadOnly = false;
            this.dtp_start_work.Size = new System.Drawing.Size(283, 54);
            this.dtp_start_work.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel9.Controls.Add(this.btn_end_work, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.dtp_end_work, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(819, 183);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(436, 54);
            this.tableLayoutPanel9.TabIndex = 8;
            // 
            // btn_end_work
            // 
            this.btn_end_work.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_end_work.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_end_work.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_end_work.FlatAppearance.BorderSize = 0;
            this.btn_end_work.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_end_work.ForeColor = System.Drawing.Color.White;
            this.btn_end_work.Location = new System.Drawing.Point(289, 0);
            this.btn_end_work.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btn_end_work.Name = "btn_end_work";
            this.btn_end_work.Size = new System.Drawing.Size(147, 54);
            this.btn_end_work.TabIndex = 1;
            this.btn_end_work.Text = "종료";
            this.btn_end_work.UseVisualStyleBackColor = false;
            this.btn_end_work.Click += new System.EventHandler(this.btn_end_work_Click);
            this.btn_end_work.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_start_work_Paint);
            // 
            // dtp_end_work
            // 
            this.dtp_end_work.DateEdit_Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtp_end_work.DateTime = new System.DateTime(2024, 3, 27, 9, 36, 36, 577);
            this.dtp_end_work.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtp_end_work.Location = new System.Drawing.Point(0, 0);
            this.dtp_end_work.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.dtp_end_work.Name = "dtp_end_work";
            this.dtp_end_work.ReadOnly = false;
            this.dtp_end_work.Size = new System.Drawing.Size(283, 54);
            this.dtp_end_work.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(631, 303);
            this.label15.Margin = new System.Windows.Forms.Padding(3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(182, 54);
            this.label15.TabIndex = 0;
            this.label15.Text = "최신수집LOT:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(631, 243);
            this.label21.Margin = new System.Windows.Forms.Padding(3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(182, 54);
            this.label21.TabIndex = 0;
            this.label21.Text = "제품LOT:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_작업LOT
            // 
            this.lbl_작업LOT.AutoSize = true;
            this.lbl_작업LOT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_작업LOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_작업LOT.Location = new System.Drawing.Point(819, 243);
            this.lbl_작업LOT.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_작업LOT.Name = "lbl_작업LOT";
            this.lbl_작업LOT.Size = new System.Drawing.Size(436, 54);
            this.lbl_작업LOT.TabIndex = 2;
            this.lbl_작업LOT.Text = "-";
            this.lbl_작업LOT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_최신수집LOT
            // 
            this.lbl_최신수집LOT.AutoSize = true;
            this.lbl_최신수집LOT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_최신수집LOT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_최신수집LOT.Location = new System.Drawing.Point(819, 303);
            this.lbl_최신수집LOT.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_최신수집LOT.Name = "lbl_최신수집LOT";
            this.lbl_최신수집LOT.Size = new System.Drawing.Size(436, 54);
            this.lbl_최신수집LOT.TabIndex = 2;
            this.lbl_최신수집LOT.Text = "-";
            this.lbl_최신수집LOT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel5.SetColumnSpan(this.tableLayoutPanel10, 4);
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel10.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 480);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(1258, 230);
            this.tableLayoutPanel10.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fpPOP);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(748, 224);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "비가동현황";
            // 
            // fpPOP
            // 
            this.fpPOP._menu_name = null;
            this.fpPOP._user_account = null;
            this.fpPOP.AccessibleDescription = "";
            this.fpPOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fpPOP.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.fpPOP.Location = new System.Drawing.Point(3, 17);
            this.fpPOP.Name = "fpPOP";
            this.fpPOP.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpPOP_Sheet1});
            this.fpPOP.Size = new System.Drawing.Size(742, 204);
            this.fpPOP.TabIndex = 0;
            // 
            // fpPOP_Sheet1
            // 
            this.fpPOP_Sheet1.Reset();
            fpPOP_Sheet1.SheetName = "Sheet1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(757, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 224);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "메모";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel7.Controls.Add(this.btn_end, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.txt_coment, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.btn_coment, 1, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(492, 204);
            this.tableLayoutPanel7.TabIndex = 5;
            // 
            // btn_end
            // 
            this.btn_end.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_end.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_end.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_end.FlatAppearance.BorderSize = 0;
            this.btn_end.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_end.ForeColor = System.Drawing.Color.White;
            this.btn_end.Location = new System.Drawing.Point(345, 3);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(144, 96);
            this.btn_end.TabIndex = 1;
            this.btn_end.Text = "강제 종료";
            this.btn_end.UseVisualStyleBackColor = false;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // txt_coment
            // 
            this.txt_coment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_coment.Location = new System.Drawing.Point(0, 0);
            this.txt_coment.Margin = new System.Windows.Forms.Padding(0);
            this.txt_coment.Multiline = true;
            this.txt_coment.Name = "txt_coment";
            this.tableLayoutPanel7.SetRowSpan(this.txt_coment, 2);
            this.txt_coment.Size = new System.Drawing.Size(342, 204);
            this.txt_coment.TabIndex = 0;
            // 
            // btn_coment
            // 
            this.btn_coment.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btn_coment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_coment.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_coment.FlatAppearance.BorderSize = 0;
            this.btn_coment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_coment.ForeColor = System.Drawing.Color.White;
            this.btn_coment.Location = new System.Drawing.Point(345, 105);
            this.btn_coment.Name = "btn_coment";
            this.btn_coment.Size = new System.Drawing.Size(144, 96);
            this.btn_coment.TabIndex = 1;
            this.btn_coment.Text = "메모 저장";
            this.btn_coment.UseVisualStyleBackColor = false;
            this.btn_coment.Click += new System.EventHandler(this.btn_coment_Click);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 8;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49952F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.50328F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.tableLayoutPanel6.Controls.Add(this.label16, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label17, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_SA_Equipment_Check, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.btn_Equipment_Check, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.label9, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.btn_ProductionProgressStatus, 7, 1);
            this.tableLayoutPanel6.Controls.Add(this.btn_ManualResultt, 6, 1);
            this.tableLayoutPanel6.Controls.Add(this.btn_ProductionInstruct_select, 5, 1);
            this.tableLayoutPanel6.Controls.Add(this.btn_PR_Equipment_Check, 3, 1);
            this.tableLayoutPanel6.Controls.Add(this.btn_비가동등록, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.button2, 4, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 833);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1272, 124);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(198)))), ((int)(((byte)(189)))));
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(3, 3);
            this.label16.Margin = new System.Windows.Forms.Padding(3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 34);
            this.label16.TabIndex = 0;
            this.label16.Text = "▣ 비가동 영역";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.tableLayoutPanel6.SetColumnSpan(this.label17, 4);
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(161, 3);
            this.label17.Margin = new System.Windows.Forms.Padding(3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(627, 34);
            this.label17.TabIndex = 1;
            this.label17.Text = "▣ 점검 영역";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_SA_Equipment_Check
            // 
            this.btn_SA_Equipment_Check.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(112)))), ((int)(((byte)(142)))));
            this.btn_SA_Equipment_Check.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_SA_Equipment_Check.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_SA_Equipment_Check.FlatAppearance.BorderSize = 0;
            this.btn_SA_Equipment_Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SA_Equipment_Check.ForeColor = System.Drawing.Color.White;
            this.btn_SA_Equipment_Check.Location = new System.Drawing.Point(161, 43);
            this.btn_SA_Equipment_Check.Name = "btn_SA_Equipment_Check";
            this.btn_SA_Equipment_Check.Size = new System.Drawing.Size(152, 78);
            this.btn_SA_Equipment_Check.TabIndex = 4;
            this.btn_SA_Equipment_Check.Text = "설비안전점검";
            this.btn_SA_Equipment_Check.UseVisualStyleBackColor = false;
            this.btn_SA_Equipment_Check.Click += new System.EventHandler(this.btn_SA_Equipment_Check_Click);
            // 
            // btn_Equipment_Check
            // 
            this.btn_Equipment_Check.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(112)))), ((int)(((byte)(142)))));
            this.btn_Equipment_Check.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Equipment_Check.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Equipment_Check.FlatAppearance.BorderSize = 0;
            this.btn_Equipment_Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Equipment_Check.ForeColor = System.Drawing.Color.White;
            this.btn_Equipment_Check.Location = new System.Drawing.Point(319, 43);
            this.btn_Equipment_Check.Name = "btn_Equipment_Check";
            this.btn_Equipment_Check.Size = new System.Drawing.Size(152, 78);
            this.btn_Equipment_Check.TabIndex = 5;
            this.btn_Equipment_Check.Text = "설비점검";
            this.btn_Equipment_Check.UseVisualStyleBackColor = false;
            this.btn_Equipment_Check.Click += new System.EventHandler(this.btn_Equipment_Check_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(136)))), ((int)(((byte)(208)))));
            this.tableLayoutPanel6.SetColumnSpan(this.label9, 3);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(794, 3);
            this.label9.Margin = new System.Windows.Forms.Padding(3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(475, 34);
            this.label9.TabIndex = 1;
            this.label9.Text = "▣ 작업 영역";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ProductionProgressStatus
            // 
            this.btn_ProductionProgressStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btn_ProductionProgressStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProductionProgressStatus.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_ProductionProgressStatus.FlatAppearance.BorderSize = 0;
            this.btn_ProductionProgressStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ProductionProgressStatus.ForeColor = System.Drawing.Color.White;
            this.btn_ProductionProgressStatus.Location = new System.Drawing.Point(1110, 43);
            this.btn_ProductionProgressStatus.Name = "btn_ProductionProgressStatus";
            this.btn_ProductionProgressStatus.Size = new System.Drawing.Size(159, 78);
            this.btn_ProductionProgressStatus.TabIndex = 2;
            this.btn_ProductionProgressStatus.Text = "작업현황";
            this.btn_ProductionProgressStatus.UseVisualStyleBackColor = false;
            this.btn_ProductionProgressStatus.Click += new System.EventHandler(this.button6_Click);
            // 
            // btn_ManualResultt
            // 
            this.btn_ManualResultt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btn_ManualResultt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ManualResultt.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_ManualResultt.FlatAppearance.BorderSize = 0;
            this.btn_ManualResultt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ManualResultt.ForeColor = System.Drawing.Color.White;
            this.btn_ManualResultt.Location = new System.Drawing.Point(952, 43);
            this.btn_ManualResultt.Name = "btn_ManualResultt";
            this.btn_ManualResultt.Size = new System.Drawing.Size(152, 78);
            this.btn_ManualResultt.TabIndex = 2;
            this.btn_ManualResultt.Text = "수동실적";
            this.btn_ManualResultt.UseVisualStyleBackColor = false;
            this.btn_ManualResultt.Click += new System.EventHandler(this.btn_ManualResultt_Click);
            // 
            // btn_ProductionInstruct_select
            // 
            this.btn_ProductionInstruct_select.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(96)))), ((int)(((byte)(168)))));
            this.btn_ProductionInstruct_select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ProductionInstruct_select.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_ProductionInstruct_select.FlatAppearance.BorderSize = 0;
            this.btn_ProductionInstruct_select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ProductionInstruct_select.ForeColor = System.Drawing.Color.White;
            this.btn_ProductionInstruct_select.Location = new System.Drawing.Point(794, 43);
            this.btn_ProductionInstruct_select.Name = "btn_ProductionInstruct_select";
            this.btn_ProductionInstruct_select.Size = new System.Drawing.Size(152, 78);
            this.btn_ProductionInstruct_select.TabIndex = 2;
            this.btn_ProductionInstruct_select.Text = "작업지시";
            this.btn_ProductionInstruct_select.UseVisualStyleBackColor = false;
            this.btn_ProductionInstruct_select.Click += new System.EventHandler(this.btn_ProductionInstruct_select_Click);
            // 
            // btn_PR_Equipment_Check
            // 
            this.btn_PR_Equipment_Check.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(112)))), ((int)(((byte)(142)))));
            this.btn_PR_Equipment_Check.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_PR_Equipment_Check.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_PR_Equipment_Check.FlatAppearance.BorderSize = 0;
            this.btn_PR_Equipment_Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PR_Equipment_Check.ForeColor = System.Drawing.Color.White;
            this.btn_PR_Equipment_Check.Location = new System.Drawing.Point(477, 43);
            this.btn_PR_Equipment_Check.Name = "btn_PR_Equipment_Check";
            this.btn_PR_Equipment_Check.Size = new System.Drawing.Size(152, 78);
            this.btn_PR_Equipment_Check.TabIndex = 6;
            this.btn_PR_Equipment_Check.Text = "생산점검";
            this.btn_PR_Equipment_Check.UseVisualStyleBackColor = false;
            this.btn_PR_Equipment_Check.Click += new System.EventHandler(this.btn_PR_Equipment_Check_Click);
            // 
            // btn_비가동등록
            // 
            this.btn_비가동등록.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(168)))), ((int)(((byte)(159)))));
            this.btn_비가동등록.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_비가동등록.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_비가동등록.ForeColor = System.Drawing.Color.White;
            this.btn_비가동등록.Location = new System.Drawing.Point(3, 43);
            this.btn_비가동등록.Name = "btn_비가동등록";
            this.btn_비가동등록.Size = new System.Drawing.Size(152, 78);
            this.btn_비가동등록.TabIndex = 7;
            this.btn_비가동등록.Text = "button2";
            this.btn_비가동등록.UseVisualStyleBackColor = false;
            this.btn_비가동등록.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(112)))), ((int)(((byte)(142)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(635, 43);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 78);
            this.button2.TabIndex = 6;
            this.button2.Text = "PM점검";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(1, 993);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(1278, 30);
            this.label18.TabIndex = 7;
            this.label18.Text = "-";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmPOPMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1280, 1024);
            this.MinimumSize = new System.Drawing.Size(1280, 1024);
            this.Name = "frmPOPMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmPOPMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fpPOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpPOP_Sheet1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_ProductionInstruct_select;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btn_SA_Equipment_Check;
        private System.Windows.Forms.Button btn_Equipment_Check;
        private System.Windows.Forms.Button btn_PR_Equipment_Check;
        private System.Windows.Forms.Button btn_ProductionProgressStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_STOCK_OUT_CODE;
        private System.Windows.Forms.Label lbl_STOCK_NAME;
        private System.Windows.Forms.Label lbl_STOCK_STANDARD;
        private System.Windows.Forms.Label lbl_ProductionInstruct_Qty;
        private System.Windows.Forms.Label lbl_Now_OK_Qty;
        private System.Windows.Forms.Label lbl_Now_NG_Qty;
        private System.Windows.Forms.Label lbl_all_Qty;
        private System.Windows.Forms.Label lbl_작업LOT;
        private System.Windows.Forms.Label lbl_Instruc_date;
        private System.Windows.Forms.Label lbl_Line;
        private System.Windows.Forms.Button btn_ManualResultt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txt_coment;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Button btn_start_work;
        private System.Windows.Forms.Button btn_end_work;
        private _uc_Clock _uc_Clock1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_Now_Qty;
        private System.Windows.Forms.Label label18;
        private Core.ucDateEdit dtp_start_work;
        private Core.ucDateEdit dtp_end_work;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbl_비가동여부;
        private System.Windows.Forms.Label lbl_최신데이터;
        private System.Windows.Forms.GroupBox groupBox1;
        private Core.xFpSpread fpPOP;
        private FarPoint.Win.Spread.SheetView fpPOP_Sheet1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lbl_최신수집LOT;
        private System.Windows.Forms.Button btn_비가동등록;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button btn_coment;
    }
}