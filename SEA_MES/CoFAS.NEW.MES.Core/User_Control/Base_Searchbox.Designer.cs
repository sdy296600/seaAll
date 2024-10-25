
namespace CoFAS.NEW.MES.Core
{
    partial class Base_Searchbox
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.button1 = new System.Windows.Forms.Button();
            this._DisplayText = new CoFAS.NEW.MES.Core.ucTextEdit();
            this.Control_Name = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(142)))), ((int)(((byte)(172)))));
            this.panelControl2.Appearance.Options.UseBackColor = true;
            this.panelControl2.AutoSize = true;
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.button1);
            this.panelControl2.Controls.Add(this._DisplayText);
            this.panelControl2.Controls.Add(this.Control_Name);
            this.panelControl2.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(300, 26);
            this.panelControl2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::CoFAS.NEW.MES.Core.Properties.Resources.find;
            this.button1.Location = new System.Drawing.Point(274, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 26);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _SearchText
            // 
            this._DisplayText.AutoSize = true;
            this._DisplayText.CancelButton = null;
            this._DisplayText.CommandButton = null;
            this._DisplayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DisplayText.EditMask = "";
            this._DisplayText.Enabled = false;
            this._DisplayText.Location = new System.Drawing.Point(100, 0);
            this._DisplayText.Margin = new System.Windows.Forms.Padding(0);
            this._DisplayText.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this._DisplayText.MaxLength = 0;
            this._DisplayText.Name = "_SearchText";
            this._DisplayText.NumText = "";
            this._DisplayText.PasswordChar = '\0';
            this._DisplayText.ReadOnly = false;
            this._DisplayText.Size = new System.Drawing.Size(200, 26);
            this._DisplayText.TabIndex = 1;
            this._DisplayText.TextAlignment = DevExpress.Utils.HorzAlignment.Default;
            this._DisplayText.ToolTipt = "";
            this._DisplayText.UseMaskAsDisplayFormat = false;
            // 
            // Control_Name
            // 
            this.Control_Name.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Control_Name.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Control_Name.Appearance.ForeColor = System.Drawing.Color.White;
            this.Control_Name.Appearance.Options.UseBackColor = true;
            this.Control_Name.Appearance.Options.UseFont = true;
            this.Control_Name.Appearance.Options.UseForeColor = true;
            this.Control_Name.Appearance.Options.UseTextOptions = true;
            this.Control_Name.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Control_Name.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Control_Name.Dock = System.Windows.Forms.DockStyle.Left;
            this.Control_Name.Location = new System.Drawing.Point(0, 0);
            this.Control_Name.Name = "Control_Name";
            this.Control_Name.Size = new System.Drawing.Size(100, 26);
            this.Control_Name.TabIndex = 0;
            // 
            // Base_Searchbox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panelControl2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Base_Searchbox";
            this.Size = new System.Drawing.Size(300, 26);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl Control_Name;
        private ucTextEdit _DisplayText;
        private System.Windows.Forms.Button button1;
    }
}
