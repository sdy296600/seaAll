namespace CoFAS.NEW.MES.Core
{
    partial class _LookupEdit
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
            this._pLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this._pLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _pLookUpEdit
            // 
            this._pLookUpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pLookUpEdit.Location = new System.Drawing.Point(0, 0);
            this._pLookUpEdit.Name = "_pLookUpEdit";
            this._pLookUpEdit.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._pLookUpEdit.Properties.Appearance.Options.UseFont = true;
            this._pLookUpEdit.Properties.AppearanceDropDown.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._pLookUpEdit.Properties.AppearanceDropDown.Options.UseFont = true;
            this._pLookUpEdit.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this._pLookUpEdit.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this._pLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._pLookUpEdit.Size = new System.Drawing.Size(119, 22);
            this._pLookUpEdit.TabIndex = 0;
            // 
            // _LookupEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pLookUpEdit);
            this.Name = "_LookupEdit";
            this.Size = new System.Drawing.Size(119, 22);
            ((System.ComponentModel.ISupportInitialize)(this._pLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit _pLookUpEdit;
    }
}
