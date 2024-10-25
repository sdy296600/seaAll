
namespace CoFAS.NEW.MES.Core
{
    partial class Base_FromtoDateTime
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Control_Name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_Start = new CoFAS.NEW.MES.Core.ucDateEdit();
            this.dtp_End = new CoFAS.NEW.MES.Core.ucDateEdit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Control_Name, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtp_Start, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtp_End, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 26);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Control_Name
            // 
            this.Control_Name.AutoSize = true;
            this.Control_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.Control_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_Name.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Control_Name.ForeColor = System.Drawing.Color.White;
            this.Control_Name.Location = new System.Drawing.Point(0, 0);
            this.Control_Name.Margin = new System.Windows.Forms.Padding(0);
            this.Control_Name.Name = "Control_Name";
            this.Control_Name.Size = new System.Drawing.Size(100, 26);
            this.Control_Name.TabIndex = 0;
            this.Control_Name.Text = "label1";
            this.Control_Name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(192, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "~";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_Start
            // 
            this.dtp_Start.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.dtp_Start.Appearance.Options.UseFont = true;
            this.dtp_Start.DateEdit_Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtp_Start.DateTime = new System.DateTime(2024, 9, 3, 9, 31, 21, 578);
            this.dtp_Start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtp_Start.Location = new System.Drawing.Point(100, 0);
            this.dtp_Start.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_Start.Name = "dtp_Start";
            this.dtp_Start.ReadOnly = false;
            this.dtp_Start.Size = new System.Drawing.Size(92, 26);
            this.dtp_Start.TabIndex = 4;
            // 
            // dtp_End
            // 
            this.dtp_End.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.dtp_End.Appearance.Options.UseFont = true;
            this.dtp_End.DateEdit_Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dtp_End.DateTime = new System.DateTime(2024, 9, 3, 9, 31, 23, 726);
            this.dtp_End.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtp_End.Location = new System.Drawing.Point(207, 0);
            this.dtp_End.Margin = new System.Windows.Forms.Padding(0);
            this.dtp_End.Name = "dtp_End";
            this.dtp_End.ReadOnly = false;
            this.dtp_End.Size = new System.Drawing.Size(93, 26);
            this.dtp_End.TabIndex = 5;
            // 
            // Base_FromtoDateTime
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Base_FromtoDateTime";
            this.Size = new System.Drawing.Size(300, 26);
            this.Load += new System.EventHandler(this.Base_FromtoDateTime_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Control_Name;
        private System.Windows.Forms.Label label1;
        private ucDateEdit dtp_Start;
        private ucDateEdit dtp_End;
    }
}
