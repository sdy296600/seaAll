
namespace CoFAS.NEW.MES.POP
{
    partial class 원재료간판POP
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_생성 = new System.Windows.Forms.Button();
            this.btn_조회 = new System.Windows.Forms.Button();
            this.btn_출력 = new System.Windows.Forms.Button();
            this.btn_닫기 = new System.Windows.Forms.Button();
            this.base_FromtoDateTime1 = new CoFAS.NEW.MES.Core.Base_FromtoDateTime();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_총중량 = new CoFAS.NEW.MES.Core.ucTextEdit();
            this.txt_번들수 = new CoFAS.NEW.MES.Core.ucTextEdit();
            this._품목콤보 = new CoFAS.NEW.MES.Core._LookupEdit();
            this._고객사 = new CoFAS.NEW.MES.Core._LookupEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_lot = new CoFAS.NEW.MES.Core.ucTextEdit();
            this.label7 = new System.Windows.Forms.Label();
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
            this._Title.Text = "원재료 간판 POP";
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
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107F));
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
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.7154213F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.94754F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.306336F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_생성, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_조회, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_출력, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_닫기, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.base_FromtoDateTime1, 4, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1266, 66);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 66);
            this.label1.TabIndex = 1;
            this.label1.Text = "원재료 라벨 발행";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_생성
            // 
            this.btn_생성.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_생성.Location = new System.Drawing.Point(433, 3);
            this.btn_생성.Name = "btn_생성";
            this.btn_생성.Size = new System.Drawing.Size(109, 60);
            this.btn_생성.TabIndex = 5;
            this.btn_생성.Text = "생성";
            this.btn_생성.UseVisualStyleBackColor = true;
            this.btn_생성.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_조회
            // 
            this.btn_조회.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_조회.Location = new System.Drawing.Point(318, 3);
            this.btn_조회.Name = "btn_조회";
            this.btn_조회.Size = new System.Drawing.Size(109, 60);
            this.btn_조회.TabIndex = 6;
            this.btn_조회.Text = "조회";
            this.btn_조회.UseVisualStyleBackColor = true;
            this.btn_조회.Click += new System.EventHandler(this.조회버튼_Click);
            // 
            // btn_출력
            // 
            this.btn_출력.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_출력.Location = new System.Drawing.Point(1037, 3);
            this.btn_출력.Name = "btn_출력";
            this.btn_출력.Size = new System.Drawing.Size(109, 60);
            this.btn_출력.TabIndex = 2;
            this.btn_출력.Text = "출력";
            this.btn_출력.UseVisualStyleBackColor = true;
            this.btn_출력.Click += new System.EventHandler(this.btn_출력_Click);
            // 
            // btn_닫기
            // 
            this.btn_닫기.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_닫기.Location = new System.Drawing.Point(1152, 3);
            this.btn_닫기.Name = "btn_닫기";
            this.btn_닫기.Size = new System.Drawing.Size(111, 60);
            this.btn_닫기.TabIndex = 2;
            this.btn_닫기.Text = "닫기";
            this.btn_닫기.UseVisualStyleBackColor = true;
            this.btn_닫기.Click += new System.EventHandler(this.닫기_Click);
            // 
            // base_FromtoDateTime1
            // 
            this.base_FromtoDateTime1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.base_FromtoDateTime1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.base_FromtoDateTime1.EndValue = new System.DateTime(2024, 11, 30, 23, 59, 0, 0);
            this.base_FromtoDateTime1.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.base_FromtoDateTime1.Location = new System.Drawing.Point(553, 0);
            this.base_FromtoDateTime1.Margin = new System.Windows.Forms.Padding(0);
            this.base_FromtoDateTime1.Name = "base_FromtoDateTime1";
            this.base_FromtoDateTime1.SearchName = "날짜";
            this.base_FromtoDateTime1.Size = new System.Drawing.Size(333, 66);
            this.base_FromtoDateTime1.StartValue = new System.DateTime(2024, 11, 1, 0, 0, 0, 0);
            this.base_FromtoDateTime1.TabIndex = 9;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txt_총중량, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.txt_번들수, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this._품목콤보, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this._고객사, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.txt_lot, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.txt_비고, 1, 5);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 110);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(502, 433);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 50);
            this.label3.TabIndex = 1;
            this.label3.Text = "품목명";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(4, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 50);
            this.label4.TabIndex = 2;
            this.label4.Text = "고객사";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(4, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 50);
            this.label5.TabIndex = 3;
            this.label5.Text = "총 중량";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(4, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 50);
            this.label6.TabIndex = 4;
            this.label6.Text = "번들 수";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_총중량
            // 
            this.txt_총중량.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.txt_총중량.Appearance.Options.UseFont = true;
            this.txt_총중량.AutoSize = true;
            this.txt_총중량.CancelButton = null;
            this.txt_총중량.CommandButton = null;
            this.txt_총중량.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_총중량.EditMask = "";
            this.txt_총중량.Location = new System.Drawing.Point(207, 110);
            this.txt_총중량.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txt_총중량.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_총중량.MaxLength = 0;
            this.txt_총중량.Name = "txt_총중량";
            this.txt_총중량.NumText = "";
            this.txt_총중량.PasswordChar = '\0';
            this.txt_총중량.ReadOnly = false;
            this.txt_총중량.Size = new System.Drawing.Size(288, 36);
            this.txt_총중량.TabIndex = 11;
            this.txt_총중량.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_총중량.ToolTipt = "";
            this.txt_총중량.UseMaskAsDisplayFormat = false;
            // 
            // txt_번들수
            // 
            this.txt_번들수.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.txt_번들수.Appearance.Options.UseFont = true;
            this.txt_번들수.AutoSize = true;
            this.txt_번들수.CancelButton = null;
            this.txt_번들수.CommandButton = null;
            this.txt_번들수.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_번들수.EditMask = "";
            this.txt_번들수.Location = new System.Drawing.Point(207, 161);
            this.txt_번들수.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txt_번들수.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_번들수.MaxLength = 0;
            this.txt_번들수.Name = "txt_번들수";
            this.txt_번들수.NumText = "";
            this.txt_번들수.PasswordChar = '\0';
            this.txt_번들수.ReadOnly = false;
            this.txt_번들수.Size = new System.Drawing.Size(288, 36);
            this.txt_번들수.TabIndex = 12;
            this.txt_번들수.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_번들수.ToolTipt = "";
            this.txt_번들수.UseMaskAsDisplayFormat = false;
            // 
            // _품목콤보
            // 
            this._품목콤보.Dock = System.Windows.Forms.DockStyle.Fill;
            this._품목콤보.ItemIndex = -1;
            this._품목콤보.Location = new System.Drawing.Point(207, 8);
            this._품목콤보.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this._품목콤보.Name = "_품목콤보";
            this._품목콤보.ReadOnly = false;
            this._품목콤보.Size = new System.Drawing.Size(288, 36);
            this._품목콤보.TabIndex = 9;
            // 
            // _고객사
            // 
            this._고객사.Dock = System.Windows.Forms.DockStyle.Fill;
            this._고객사.ItemIndex = -1;
            this._고객사.Location = new System.Drawing.Point(207, 59);
            this._고객사.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this._고객사.Name = "_고객사";
            this._고객사.ReadOnly = false;
            this._고객사.Size = new System.Drawing.Size(288, 36);
            this._고객사.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 50);
            this.label2.TabIndex = 11;
            this.label2.Text = "LOT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_lot
            // 
            this.txt_lot.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.txt_lot.Appearance.Options.UseFont = true;
            this.txt_lot.AutoSize = true;
            this.txt_lot.CancelButton = null;
            this.txt_lot.CommandButton = null;
            this.txt_lot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_lot.EditMask = "";
            this.txt_lot.Location = new System.Drawing.Point(207, 212);
            this.txt_lot.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txt_lot.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_lot.MaxLength = 0;
            this.txt_lot.Name = "txt_lot";
            this.txt_lot.NumText = "";
            this.txt_lot.PasswordChar = '\0';
            this.txt_lot.ReadOnly = false;
            this.txt_lot.Size = new System.Drawing.Size(288, 36);
            this.txt_lot.TabIndex = 13;
            this.txt_lot.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_lot.ToolTipt = "";
            this.txt_lot.UseMaskAsDisplayFormat = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(4, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(193, 176);
            this.label7.TabIndex = 11;
            this.label7.Text = "비고";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_비고
            // 
            this.txt_비고.AutoSize = true;
            this.txt_비고.CancelButton = null;
            this.txt_비고.CommandButton = null;
            this.txt_비고.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_비고.EditMask = "";
            this.txt_비고.Location = new System.Drawing.Point(207, 263);
            this.txt_비고.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.txt_비고.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_비고.MaxLength = 0;
            this.txt_비고.Name = "txt_비고";
            this.txt_비고.NumText = "";
            this.txt_비고.PasswordChar = '\0';
            this.txt_비고.ReadOnly = false;
            this.txt_비고.Size = new System.Drawing.Size(288, 162);
            this.txt_비고.TabIndex = 14;
            this.txt_비고.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_비고.ToolTipt = "";
            this.txt_비고.UseMaskAsDisplayFormat = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Controls.Add(this.checkBox2, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(511, 549);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(758, 433);
            this.tableLayoutPanel6.TabIndex = 3;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox2.Location = new System.Drawing.Point(3, 3);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(752, 34);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "출력 미리보기";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(752, 387);
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
            this.fpMain.Location = new System.Drawing.Point(511, 110);
            this.fpMain.Name = "fpMain";
            this.fpMain.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.fpMain_Sheet1});
            this.fpMain.Size = new System.Drawing.Size(758, 433);
            this.fpMain.TabIndex = 4;
            // 
            // fpMain_Sheet1
            // 
            this.fpMain_Sheet1.Reset();
            fpMain_Sheet1.SheetName = "Sheet1";
            // 
            // 원재료간판POP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(220)))), ((int)(((byte)(229)))));
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "원재료간판POP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "세아간판POP";
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
        private System.Windows.Forms.Button btn_생성;
        private System.Windows.Forms.Button btn_조회;
        private System.Windows.Forms.Button btn_출력;
        private System.Windows.Forms.Button btn_닫기;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Core.ucTextEdit txt_총중량;
        private Core.ucTextEdit txt_번들수;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private Core._LookupEdit _품목콤보;
        private Core._LookupEdit _고객사;
        private System.Windows.Forms.Label label2;
        private Core.ucTextEdit txt_lot;
        private System.Windows.Forms.Label label7;
        private Core.ucTextEdit txt_비고;
        private Core.xFpSpread fpMain;
        private FarPoint.Win.Spread.SheetView fpMain_Sheet1;
        private Core.Base_FromtoDateTime base_FromtoDateTime1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}