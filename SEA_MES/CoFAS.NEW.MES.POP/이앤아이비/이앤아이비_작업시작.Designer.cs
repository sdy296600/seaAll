
namespace CoFAS.NEW.MES.POP
{
    partial class 이앤아이비_작업시작
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
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.de_시간 = new CoFAS.NEW.MES.Core.ucDateEdit();
            this.txt_작업자 = new CoFAS.NEW.MES.Core.ucTextEdit();
            this.lbl_시간 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 150);
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(798, 30);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lblClose
            // 
            this.lblClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(68)))), ((int)(((byte)(97)))));
            this.lblClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClose.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblClose.ForeColor = System.Drawing.Color.White;
            this.lblClose.Location = new System.Drawing.Point(768, 0);
            this.lblClose.Margin = new System.Windows.Forms.Padding(0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(30, 30);
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
            this._Title.Size = new System.Drawing.Size(768, 30);
            this._Title.TabIndex = 0;
            this._Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._Title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tspMain_MouseDown);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.de_시간, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.txt_작업자, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_시간, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 35);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(792, 111);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(645, 3);
            this.button1.Name = "button1";
            this.tableLayoutPanel3.SetRowSpan(this.button1, 2);
            this.button1.Size = new System.Drawing.Size(144, 105);
            this.button1.TabIndex = 0;
            this.button1.Text = "저장";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // de_시간
            // 
            this.de_시간.Appearance.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold);
            this.de_시간.Appearance.Options.UseFont = true;
            this.de_시간.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.de_시간.DateEdit_Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold);
            this.de_시간.DateTime = new System.DateTime(2024, 4, 4, 11, 26, 36, 419);
            this.de_시간.Dock = System.Windows.Forms.DockStyle.Fill;
            this.de_시간.Location = new System.Drawing.Point(200, 0);
            this.de_시간.Margin = new System.Windows.Forms.Padding(0);
            this.de_시간.Name = "de_시간";
            this.de_시간.ReadOnly = false;
            this.de_시간.Size = new System.Drawing.Size(442, 55);
            this.de_시간.TabIndex = 1;
            // 
            // txt_작업자
            // 
            this.txt_작업자.AutoSize = true;
            this.txt_작업자.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_작업자.CancelButton = null;
            this.txt_작업자.CommandButton = null;
            this.txt_작업자.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_작업자.EditMask = "";
            this.txt_작업자.Location = new System.Drawing.Point(200, 55);
            this.txt_작업자.Margin = new System.Windows.Forms.Padding(0);
            this.txt_작업자.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txt_작업자.MaxLength = 0;
            this.txt_작업자.Name = "txt_작업자";
            this.txt_작업자.NumText = "";
            this.txt_작업자.PasswordChar = '\0';
            this.txt_작업자.ReadOnly = false;
            this.txt_작업자.Size = new System.Drawing.Size(442, 56);
            this.txt_작업자.TabIndex = 2;
            this.txt_작업자.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.txt_작업자.ToolTipt = "";
            this.txt_작업자.UseMaskAsDisplayFormat = false;
            // 
            // lbl_시간
            // 
            this.lbl_시간.AutoSize = true;
            this.lbl_시간.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_시간.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_시간.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold);
            this.lbl_시간.Location = new System.Drawing.Point(3, 3);
            this.lbl_시간.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_시간.Name = "lbl_시간";
            this.lbl_시간.Size = new System.Drawing.Size(194, 49);
            this.lbl_시간.TabIndex = 3;
            this.lbl_시간.Text = "시작시간";
            this.lbl_시간.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 24F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 50);
            this.label2.TabIndex = 4;
            this.label2.Text = "작업자";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 이앤아이비_작업시작
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 150);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "이앤아이비_작업시작";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "이앤아이비_작업시작";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _Title;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button1;
        public Core.ucDateEdit de_시간;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lbl_시간;
        public Core.ucTextEdit txt_작업자;
    }
}