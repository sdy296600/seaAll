namespace CoFAS.NEW.MES.Core
{
    partial class ucDateEdit
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
            this._pDateEdit = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this._pDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _pDateEdit
            // 
            this._pDateEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pDateEdit.EditValue = null;
            this._pDateEdit.Location = new System.Drawing.Point(0, 0);
            this._pDateEdit.Margin = new System.Windows.Forms.Padding(0);
            this._pDateEdit.Name = "_pDateEdit";
            this._pDateEdit.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._pDateEdit.Properties.Appearance.Options.UseFont = true;
            this._pDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._pDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._pDateEdit.Properties.DisplayFormat.FormatString = "G";
            this._pDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this._pDateEdit.Properties.EditFormat.FormatString = "G";
            this._pDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this._pDateEdit.Properties.Mask.EditMask = "G";
            this._pDateEdit.Size = new System.Drawing.Size(131, 22);
            this._pDateEdit.TabIndex = 0;
            // 
            // ucDateEdit
            // 
            this.Appearance.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pDateEdit);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDateEdit";
            this.Size = new System.Drawing.Size(131, 24);
            ((System.ComponentModel.ISupportInitialize)(this._pDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.DateEdit _pDateEdit;
    }
}
