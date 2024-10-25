namespace CoFAS.NEW.MES.Monitoring
{
    partial class ucCorechipsWorkStatus
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_weight = new System.Windows.Forms.Label();
            this.lbl_ratio = new System.Windows.Forms.Label();
            this.lbl_equipment_name = new System.Windows.Forms.Label();
            this.lbl_mold_name = new System.Windows.Forms.Label();
            this.lbl_product_name = new System.Windows.Forms.Label();
            this.lbl_start_work_time = new System.Windows.Forms.Label();
            this.lbl_worker_name = new System.Windows.Forms.Label();
            this.lbl_plan_qty = new System.Windows.Forms.Label();
            this.lbl_all_qty = new System.Windows.Forms.Label();
            this.lbl_Now_qty = new System.Windows.Forms.Label();
            this.lbl_collection_date = new System.Windows.Forms.Label();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 11;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel2.Controls.Add(this.lbl_weight, 11, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_ratio, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_equipment_name, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_mold_name, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_product_name, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_start_work_time, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_worker_name, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_plan_qty, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_all_qty, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_Now_qty, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.lbl_collection_date, 8, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(783, 150);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lbl_weight
            // 
            this.lbl_weight.AutoSize = true;
            this.lbl_weight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_weight.ForeColor = System.Drawing.Color.Black;
            this.lbl_weight.Location = new System.Drawing.Point(702, 1);
            this.lbl_weight.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_weight.Name = "lbl_weight";
            this.lbl_weight.Size = new System.Drawing.Size(80, 148);
            this.lbl_weight.TabIndex = 11;
            this.lbl_weight.Text = "예측 중량";
            this.lbl_weight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ratio
            // 
            this.lbl_ratio.AutoSize = true;
            this.lbl_ratio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ratio.ForeColor = System.Drawing.Color.Black;
            this.lbl_ratio.Location = new System.Drawing.Point(621, 1);
            this.lbl_ratio.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_ratio.Name = "lbl_ratio";
            this.lbl_ratio.Size = new System.Drawing.Size(80, 148);
            this.lbl_ratio.TabIndex = 10;
            this.lbl_ratio.Text = "달성율";
            this.lbl_ratio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_equipment_name
            // 
            this.lbl_equipment_name.AutoSize = true;
            this.lbl_equipment_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_equipment_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_equipment_name.ForeColor = System.Drawing.Color.Black;
            this.lbl_equipment_name.Location = new System.Drawing.Point(1, 1);
            this.lbl_equipment_name.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_equipment_name.Name = "lbl_equipment_name";
            this.lbl_equipment_name.Size = new System.Drawing.Size(50, 148);
            this.lbl_equipment_name.TabIndex = 0;
            this.lbl_equipment_name.Text = "호기";
            this.lbl_equipment_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_mold_name
            // 
            this.lbl_mold_name.AutoSize = true;
            this.lbl_mold_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_mold_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_mold_name.ForeColor = System.Drawing.Color.Black;
            this.lbl_mold_name.Location = new System.Drawing.Point(52, 1);
            this.lbl_mold_name.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_mold_name.Name = "lbl_mold_name";
            this.lbl_mold_name.Size = new System.Drawing.Size(80, 148);
            this.lbl_mold_name.TabIndex = 1;
            this.lbl_mold_name.Text = "금형";
            this.lbl_mold_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_product_name
            // 
            this.lbl_product_name.AutoSize = true;
            this.lbl_product_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_product_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_product_name.ForeColor = System.Drawing.Color.Black;
            this.lbl_product_name.Location = new System.Drawing.Point(133, 1);
            this.lbl_product_name.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_product_name.Name = "lbl_product_name";
            this.lbl_product_name.Size = new System.Drawing.Size(1, 148);
            this.lbl_product_name.TabIndex = 3;
            this.lbl_product_name.Text = "제품명칭";
            this.lbl_product_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_start_work_time
            // 
            this.lbl_start_work_time.AutoSize = true;
            this.lbl_start_work_time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_start_work_time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_start_work_time.ForeColor = System.Drawing.Color.Black;
            this.lbl_start_work_time.Location = new System.Drawing.Point(115, 1);
            this.lbl_start_work_time.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_start_work_time.Name = "lbl_start_work_time";
            this.lbl_start_work_time.Size = new System.Drawing.Size(100, 148);
            this.lbl_start_work_time.TabIndex = 4;
            this.lbl_start_work_time.Text = "시작시간";
            this.lbl_start_work_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_worker_name
            // 
            this.lbl_worker_name.AutoSize = true;
            this.lbl_worker_name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_worker_name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_worker_name.ForeColor = System.Drawing.Color.Black;
            this.lbl_worker_name.Location = new System.Drawing.Point(216, 1);
            this.lbl_worker_name.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_worker_name.Name = "lbl_worker_name";
            this.lbl_worker_name.Size = new System.Drawing.Size(80, 148);
            this.lbl_worker_name.TabIndex = 5;
            this.lbl_worker_name.Text = "작업자";
            this.lbl_worker_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_plan_qty
            // 
            this.lbl_plan_qty.AutoSize = true;
            this.lbl_plan_qty.BackColor = System.Drawing.Color.Transparent;
            this.lbl_plan_qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_plan_qty.ForeColor = System.Drawing.Color.Black;
            this.lbl_plan_qty.Location = new System.Drawing.Point(297, 1);
            this.lbl_plan_qty.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_plan_qty.Name = "lbl_plan_qty";
            this.lbl_plan_qty.Size = new System.Drawing.Size(80, 148);
            this.lbl_plan_qty.TabIndex = 6;
            this.lbl_plan_qty.Text = "계획수량";
            this.lbl_plan_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_all_qty
            // 
            this.lbl_all_qty.AutoSize = true;
            this.lbl_all_qty.BackColor = System.Drawing.Color.Transparent;
            this.lbl_all_qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_all_qty.ForeColor = System.Drawing.Color.Black;
            this.lbl_all_qty.Location = new System.Drawing.Point(378, 1);
            this.lbl_all_qty.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_all_qty.Name = "lbl_all_qty";
            this.lbl_all_qty.Size = new System.Drawing.Size(80, 148);
            this.lbl_all_qty.TabIndex = 7;
            this.lbl_all_qty.Text = "누적수량";
            this.lbl_all_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Now_qty
            // 
            this.lbl_Now_qty.AutoSize = true;
            this.lbl_Now_qty.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Now_qty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Now_qty.ForeColor = System.Drawing.Color.Black;
            this.lbl_Now_qty.Location = new System.Drawing.Point(459, 1);
            this.lbl_Now_qty.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Now_qty.Name = "lbl_Now_qty";
            this.lbl_Now_qty.Size = new System.Drawing.Size(80, 148);
            this.lbl_Now_qty.TabIndex = 8;
            this.lbl_Now_qty.Text = "진행수량";
            this.lbl_Now_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_collection_date
            // 
            this.lbl_collection_date.AutoSize = true;
            this.lbl_collection_date.BackColor = System.Drawing.Color.Transparent;
            this.lbl_collection_date.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_collection_date.ForeColor = System.Drawing.Color.Black;
            this.lbl_collection_date.Location = new System.Drawing.Point(540, 1);
            this.lbl_collection_date.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_collection_date.Name = "lbl_collection_date";
            this.lbl_collection_date.Size = new System.Drawing.Size(80, 148);
            this.lbl_collection_date.TabIndex = 9;
            this.lbl_collection_date.Text = "수집시간";
            this.lbl_collection_date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucCorechipsWorkStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucCorechipsWorkStatus";
            this.Size = new System.Drawing.Size(783, 150);
            this.Load += new System.EventHandler(this.ucCorechipsWorkStatus_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public System.Windows.Forms.Label lbl_equipment_name;
        public System.Windows.Forms.Label lbl_mold_name;
        public System.Windows.Forms.Label lbl_product_name;
        public System.Windows.Forms.Label lbl_start_work_time;
        public System.Windows.Forms.Label lbl_worker_name;
        public System.Windows.Forms.Label lbl_plan_qty;
        public System.Windows.Forms.Label lbl_all_qty;
        public System.Windows.Forms.Label lbl_Now_qty;
        public System.Windows.Forms.Label lbl_collection_date;
        public System.Windows.Forms.Label lbl_ratio;
        public System.Windows.Forms.Label lbl_weight;
    }
}
