
namespace CoFAS.NEW.MES.POP
{
    partial class 공정선택
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(공정선택));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this._lab_TOP = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_사출 = new System.Windows.Forms.Button();
            this.btn_압출 = new System.Windows.Forms.Button();
            this.btn_조립 = new System.Windows.Forms.Button();
            this.btn_포장 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this._lab_TOP, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1280, 1024);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(1, 998);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1278, 25);
            this.label1.TabIndex = 0;
            // 
            // _lab_TOP
            // 
            this._lab_TOP.AutoSize = true;
            this._lab_TOP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this._lab_TOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lab_TOP.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._lab_TOP.ForeColor = System.Drawing.Color.White;
            this._lab_TOP.Location = new System.Drawing.Point(1, 1);
            this._lab_TOP.Margin = new System.Windows.Forms.Padding(0);
            this._lab_TOP.Name = "_lab_TOP";
            this._lab_TOP.Size = new System.Drawing.Size(1278, 25);
            this._lab_TOP.TabIndex = 1;
            this._lab_TOP.Text = "Point of Production System";
            this._lab_TOP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lab_TOP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_사출, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_압출, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_조립, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_포장, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1272, 964);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btn_사출
            // 
            this.btn_사출.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_사출.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_사출.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_사출.ForeColor = System.Drawing.Color.White;
            this.btn_사출.Location = new System.Drawing.Point(10, 10);
            this.btn_사출.Margin = new System.Windows.Forms.Padding(10);
            this.btn_사출.Name = "btn_사출";
            this.btn_사출.Size = new System.Drawing.Size(616, 462);
            this.btn_사출.TabIndex = 0;
            this.btn_사출.Text = "사출";
            this.btn_사출.UseVisualStyleBackColor = false;
            this.btn_사출.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_압출
            // 
            this.btn_압출.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_압출.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_압출.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_압출.ForeColor = System.Drawing.Color.White;
            this.btn_압출.Location = new System.Drawing.Point(646, 10);
            this.btn_압출.Margin = new System.Windows.Forms.Padding(10);
            this.btn_압출.Name = "btn_압출";
            this.btn_압출.Size = new System.Drawing.Size(616, 462);
            this.btn_압출.TabIndex = 1;
            this.btn_압출.Text = "압출";
            this.btn_압출.UseVisualStyleBackColor = false;
            this.btn_압출.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_조립
            // 
            this.btn_조립.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_조립.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_조립.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_조립.ForeColor = System.Drawing.Color.White;
            this.btn_조립.Location = new System.Drawing.Point(10, 492);
            this.btn_조립.Margin = new System.Windows.Forms.Padding(10);
            this.btn_조립.Name = "btn_조립";
            this.btn_조립.Size = new System.Drawing.Size(616, 462);
            this.btn_조립.TabIndex = 2;
            this.btn_조립.Text = "조립";
            this.btn_조립.UseVisualStyleBackColor = false;
            this.btn_조립.Click += new System.EventHandler(this.btn_Click);
            // 
            // btn_포장
            // 
            this.btn_포장.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btn_포장.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_포장.Font = new System.Drawing.Font("맑은 고딕", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_포장.ForeColor = System.Drawing.Color.White;
            this.btn_포장.Location = new System.Drawing.Point(646, 492);
            this.btn_포장.Margin = new System.Windows.Forms.Padding(10);
            this.btn_포장.Name = "btn_포장";
            this.btn_포장.Size = new System.Drawing.Size(616, 462);
            this.btn_포장.TabIndex = 3;
            this.btn_포장.Text = "포장";
            this.btn_포장.UseVisualStyleBackColor = false;
            this.btn_포장.Click += new System.EventHandler(this.btn_Click);
            // 
            // 공정선택
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1280, 1024);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1280, 1024);
            this.Name = "공정선택";
            this.Text = "Point of Production System";
            this.Load += new System.EventHandler(this.공정선택_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _lab_TOP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_사출;
        private System.Windows.Forms.Button btn_압출;
        private System.Windows.Forms.Button btn_조립;
        private System.Windows.Forms.Button btn_포장;
    }
}