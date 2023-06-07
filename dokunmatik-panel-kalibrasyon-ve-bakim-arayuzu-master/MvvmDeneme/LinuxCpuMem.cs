using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace MvvmDeneme
{
    public partial class LinuxCpuMem : MetroFramework.Forms.MetroForm
    {
        Thread t1;
        string result = string.Empty;
        List<DateTime> TimeList = new List<DateTime>();
        public LinuxCpuMem(SshClient sshClient)
        {
            InitializeComponent();
            t1 = new Thread(() => KomutCalistir(sshClient));
            t1.Start();
            myChart.ChartAreas[0].AxisX.LabelStyle.Format = "hh:mm";
            panelChart.AutoScroll = true;

            myChart.ChartAreas[0].AxisX.ScaleView.Size = 100;
            myChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            myChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
        }
        public void KomutCalistir(SshClient sshClient)
        {
            while ((sshClient.IsConnected))
            {
                //Thread.Sleep(500);
                SshCommand cmd = sshClient.RunCommand("top -n 1 -b");
                result = cmd.Result;

                string cpuDegeri = ParseCpu(result);
                string cpu = cpuDegeri.Remove(0, 9);
                double cpuInt = Double.Parse(cpu, CultureInfo.InvariantCulture);

                int width = myChart.Width;
                int height = myChart.Height;

                DateTime now = DateTime.Now;
                TimeList.Add(now);
                //myChart.Size = new Size(width++, height++);

                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => 
                    {
                        /*if(myChart.ChartAreas[0].AxisX.Maximum > myChart.ChartAreas[0].AxisX.ScaleView.Size)
                        { 
                            myChart.ChartAreas[0].AxisX.ScaleView.Scroll(myChart.ChartAreas[0].AxisX.Maximum); 
                        } */
                        myChart.ChartAreas[0].AxisX.ScaleView.Position = myChart.ChartAreas[0].AxisX.Maximum - 100;
                        lblCpu.Text = cpuDegeri;
                        lblRam.Text = ParseMemory(result);
                        //myChart.ChartAreas[0].AxisX.Maximum = 100;
                        myChart.Series["CPU"].Points.AddXY(now, cpuInt/10);
                    }));
                }
                else
                {
                    t1.Abort();
                }
            }
        }
        public string ParseMemory(string contentOfOutput)
        {
            var line = contentOfOutput.Split('\n');
            string cpuLine;
            cpuLine = line[3];
            var wordCpu = cpuLine.Substring(0, 54);
            return wordCpu;
        }
        public string ParseCpu(string contentOfOutput)
        {
            var line = contentOfOutput.Split('\n');
            string cpuLine;
            cpuLine = line[2];
            var wordCpu = cpuLine.Substring(0, 13);
            return wordCpu;
        }
    }
}
