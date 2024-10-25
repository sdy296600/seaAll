namespace CoFAS.NEW.MES.Core
{
    partial class ucTextEdit
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
            this._pTextEdit = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this._pTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _pTextEdit
            // 
            this._pTextEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pTextEdit.Location = new System.Drawing.Point(0, 0);
            this._pTextEdit.Margin = new System.Windows.Forms.Padding(0);
            this._pTextEdit.Name = "_pTextEdit";
            this._pTextEdit.Properties.HideSelection = false;
            this._pTextEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this._pTextEdit.Size = new System.Drawing.Size(150, 20);
            this._pTextEdit.TabIndex = 0;
            // 
            // ucTextEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this._pTextEdit);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucTextEdit";
            this.Size = new System.Drawing.Size(150, 23);
            ((System.ComponentModel.ISupportInitialize)(this._pTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit _pTextEdit;
    }
}
