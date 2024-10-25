
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace CoFAS.NEW.MES.Monitoring.MY_UserControl
{
    partial class 달성율_그래프
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, "75,0,0,0");
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, "25,0,0,0");
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Plan = new System.Windows.Forms.Label();
            this.Act = new System.Windows.Forms.Label();
            this.제목 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this._unit1 = new System.Windows.Forms.Label();
            this._unit2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel5.Controls.Add(this.chart1, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.Plan, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.Act, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.제목, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.label5, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.label6, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this._unit1, 3, 1);
            this.tableLayoutPanel5.Controls.Add(this._unit2, 3, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(612, 259);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            this.chart1.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            this.chart1.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            this.chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            chartArea1.Area3DStyle.Inclination = 5;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(283, 3);
            this.chart1.Name = "chart1";
            this.tableLayoutPanel5.SetRowSpan(this.chart1, 3);
            series1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            series1.BackSecondaryColor = System.Drawing.Color.Transparent;
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(45)))), ((int)(((byte)(51)))));
            series1.CustomProperties = "CollectedSliceExploded=True, MinimumRelativePieSize=40, DoughnutRadius=45, PieDra" +
    "wingStyle=SoftEdge";
            series1.MarkerSize = 3;
            series1.Name = "Series1";
            dataPoint1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(131)))), ((int)(((byte)(139)))));
            dataPoint1.Label = "";
            dataPoint2.Color = System.Drawing.Color.Blue;
            dataPoint2.Label = "";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.YValuesPerPoint = 4;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(326, 253);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "고슴도치";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea1";
            title1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            title1.ForeColor = System.Drawing.Color.White;
            title1.Name = "Title1";
            title1.Position.Auto = false;
            title1.Position.Height = 50F;
            title1.Position.Width = 50F;
            title1.Position.X = 25F;
            title1.Position.Y = 35F;
            title1.Text = "달성률";
            title2.DockedToChartArea = "ChartArea1";
            title2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            title2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(230)))), ((int)(((byte)(6)))));
            title2.Name = "Title2";
            title2.Position.Auto = false;
            title2.Position.Height = 50F;
            title2.Position.Width = 50F;
            title2.Position.X = 25F;
            title2.Position.Y = 35F;
            title2.Text = "100%";
            this.chart1.Titles.Add(title1);
            this.chart1.Titles.Add(title2);
            // 
            // Plan
            // 
            this.Plan.AutoSize = true;
            this.Plan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plan.ForeColor = System.Drawing.Color.White;
            this.Plan.Location = new System.Drawing.Point(143, 86);
            this.Plan.Name = "Plan";
            this.Plan.Size = new System.Drawing.Size(104, 86);
            this.Plan.TabIndex = 2;
            this.Plan.Text = "62.1%";
            this.Plan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Act
            // 
            this.Act.AutoSize = true;
            this.Act.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Act.ForeColor = System.Drawing.Color.White;
            this.Act.Location = new System.Drawing.Point(143, 172);
            this.Act.Name = "Act";
            this.Act.Size = new System.Drawing.Size(104, 87);
            this.Act.TabIndex = 3;
            this.Act.Text = "62.1%";
            this.Act.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 제목
            // 
            this.제목.AutoSize = true;
            this.tableLayoutPanel5.SetColumnSpan(this.제목, 3);
            this.제목.Dock = System.Windows.Forms.DockStyle.Fill;
            this.제목.ForeColor = System.Drawing.Color.White;
            this.제목.Location = new System.Drawing.Point(33, 0);
            this.제목.Name = "제목";
            this.제목.Size = new System.Drawing.Size(244, 86);
            this.제목.TabIndex = 1;
            this.제목.Text = "OEE";
            this.제목.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(33, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 86);
            this.label5.TabIndex = 2;
            this.label5.Text = "Plan";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(33, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 87);
            this.label6.TabIndex = 3;
            this.label6.Text = "Act";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(255)))), ((int)(((byte)(51)))));
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(5, 5);
            this.panel9.Margin = new System.Windows.Forms.Padding(5);
            this.panel9.Name = "panel9";
            this.tableLayoutPanel5.SetRowSpan(this.panel9, 3);
            this.panel9.Size = new System.Drawing.Size(20, 249);
            this.panel9.TabIndex = 4;
            // 
            // _unit1
            // 
            this._unit1.AutoSize = true;
            this._unit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._unit1.ForeColor = System.Drawing.Color.White;
            this._unit1.Location = new System.Drawing.Point(250, 86);
            this._unit1.Margin = new System.Windows.Forms.Padding(0);
            this._unit1.Name = "_unit1";
            this._unit1.Size = new System.Drawing.Size(30, 86);
            this._unit1.TabIndex = 5;
            this._unit1.Text = "EA";
            this._unit1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _unit2
            // 
            this._unit2.AutoSize = true;
            this._unit2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._unit2.ForeColor = System.Drawing.Color.White;
            this._unit2.Location = new System.Drawing.Point(250, 172);
            this._unit2.Margin = new System.Windows.Forms.Padding(0);
            this._unit2.Name = "_unit2";
            this._unit2.Size = new System.Drawing.Size(30, 87);
            this._unit2.TabIndex = 6;
            this._unit2.Text = "EA";
            this._unit2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 달성율_그래프
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel5);
            this.Name = "달성율_그래프";
            this.Size = new System.Drawing.Size(612, 259);
            this.FontChanged += new System.EventHandler(this.달성율_그래프_FontChanged);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        public System.Windows.Forms.Label Plan;
        public System.Windows.Forms.Label Act;
        public System.Windows.Forms.Label 제목;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label _unit1;
        private System.Windows.Forms.Label _unit2;
    }
}
