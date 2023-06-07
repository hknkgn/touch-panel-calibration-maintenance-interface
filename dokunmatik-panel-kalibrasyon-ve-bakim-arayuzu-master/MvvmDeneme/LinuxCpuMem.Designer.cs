namespace MvvmDeneme
{
    partial class LinuxCpuMem
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.metroUserControl1 = new MetroFramework.Controls.MetroUserControl();
            this.lblCpu = new System.Windows.Forms.Label();
            this.lblRam = new System.Windows.Forms.Label();
            this.myChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelChart = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.myChart)).BeginInit();
            this.panelChart.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroUserControl1
            // 
            this.metroUserControl1.Location = new System.Drawing.Point(23, 54);
            this.metroUserControl1.Name = "metroUserControl1";
            this.metroUserControl1.Size = new System.Drawing.Size(384, 150);
            this.metroUserControl1.TabIndex = 0;
            this.metroUserControl1.UseSelectable = true;
            // 
            // lblCpu
            // 
            this.lblCpu.AutoSize = true;
            this.lblCpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCpu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblCpu.Location = new System.Drawing.Point(32, 84);
            this.lblCpu.Name = "lblCpu";
            this.lblCpu.Size = new System.Drawing.Size(51, 16);
            this.lblCpu.TabIndex = 1;
            this.lblCpu.Text = "label1";
            // 
            // lblRam
            // 
            this.lblRam.AutoSize = true;
            this.lblRam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblRam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblRam.Location = new System.Drawing.Point(32, 149);
            this.lblRam.Name = "lblRam";
            this.lblRam.Size = new System.Drawing.Size(51, 16);
            this.lblRam.TabIndex = 2;
            this.lblRam.Text = "label1";
            // 
            // myChart
            // 
            this.myChart.BorderSkin.BackColor = System.Drawing.Color.SkyBlue;
            this.myChart.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.FrameThin2;
            chartArea1.Name = "ChartArea1";
            this.myChart.ChartAreas.Add(chartArea1);
            legend1.HeaderSeparatorColor = System.Drawing.Color.Maroon;
            legend1.ItemColumnSeparatorColor = System.Drawing.Color.Maroon;
            legend1.Name = "Legend1";
            this.myChart.Legends.Add(legend1);
            this.myChart.Location = new System.Drawing.Point(6, 18);
            this.myChart.Name = "myChart";
            this.myChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Fuchsia;
            series1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "CPU";
            series1.ShadowColor = System.Drawing.Color.Silver;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.myChart.Series.Add(series1);
            this.myChart.Size = new System.Drawing.Size(629, 300);
            this.myChart.TabIndex = 3;
            this.myChart.Text = "chart1";
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.myChart);
            this.panelChart.Location = new System.Drawing.Point(35, 168);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(638, 345);
            this.panelChart.TabIndex = 4;
            // 
            // LinuxCpuMem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 526);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.lblRam);
            this.Controls.Add(this.lblCpu);
            this.Controls.Add(this.metroUserControl1);
            this.Name = "LinuxCpuMem";
            this.Text = "CPU&&RAM LİNUX";
            ((System.ComponentModel.ISupportInitialize)(this.myChart)).EndInit();
            this.panelChart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroUserControl metroUserControl1;
        private System.Windows.Forms.Label lblCpu;
        private System.Windows.Forms.Label lblRam;
        private System.Windows.Forms.DataVisualization.Charting.Chart myChart;
        private System.Windows.Forms.Panel panelChart;
    }
}