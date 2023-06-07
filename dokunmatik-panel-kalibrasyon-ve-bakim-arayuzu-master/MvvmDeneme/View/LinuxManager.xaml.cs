using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Renci.SshNet;

namespace MvvmDeneme.View
{
    /// <summary>
    /// Interaction logic for LinuıxManage.xaml
    /// </summary>
    public partial class LinuxManager : Window
    {
        Thread th1;
        public LinuxManager(SshClient sshClient)
        {
            InitializeComponent();
            Hide();
            /*th1 = new Thread(() => KomutCalistir(sshClient));
            th1.Start();*/

            if (!(sshClient.IsConnected))
            {
                MessageBox.Show("Ssh Bağlantınız Bulunmamaktadır.");
            }
            else
            {
                LinuxCpuMem form = new LinuxCpuMem(sshClient);
                form.ShowDialog();
            }
            Close();
        }
        /*public void KomutCalistir(SshClient sshClient)
        {
            while ((sshClient.IsConnected))
            {
                string result = string.Empty;
                string cpuDegeri = string.Empty;
                string memDegeri = string.Empty;

                SshCommand cmd = sshClient.RunCommand("top -n 1 -b");
                result = cmd.Result;
                var line = result.Split('\n');
                var cpuLineIndex = 2;
                string cpuLine;
                cpuLine = line[cpuLineIndex];
                var wordCpu = cpuLine.Substring(0, 13);
                //Dispatcher.Invoke(() => { lbl.Content = wordCpu; });
                
                //lblCpu.Text = wordCpu;
                //lblRam.Text = ParseMemory(result);
            }
        }
        public string ParseMemory(string contentOfOutput)
        {
            var line = contentOfOutput.Split('\n');
            var cpuLineIndex = 3;
            string cpuLine;
            cpuLine = line[cpuLineIndex];
            var wordCpu = cpuLine.Substring(0, 54);
            return wordCpu;
        }*/
    }
}
