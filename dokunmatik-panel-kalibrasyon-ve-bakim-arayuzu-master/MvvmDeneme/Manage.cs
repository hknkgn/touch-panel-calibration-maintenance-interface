using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;

namespace MvvmDeneme
{
    public partial class Manage : MetroFramework.Forms.MetroForm
    {
        public Manage()
        {
            InitializeComponent();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            metroProgressBar1CPU.Value = (int)fcpu;
            metroProgressBar2RAM.Value = (int)fram;
            lblCPU.Text = string.Format("{0:0.00}%", fcpu);
            lblRAM.Text = string.Format("{0:0.00}%", fram);
            chart1.Series["CPU"].Points.AddY(fcpu);
            chart1.Series["RAM"].Points.AddY(fram);
        }
        private void Manage_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
