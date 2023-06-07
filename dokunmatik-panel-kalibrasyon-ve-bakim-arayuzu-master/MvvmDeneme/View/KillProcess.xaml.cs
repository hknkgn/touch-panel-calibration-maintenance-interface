using MvvmDeneme.ViewModel;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MvvmDeneme.View
{
    /// <summary>
    /// Interaction logic for KillProcessPopUp.xaml
    /// </summary>
    public partial class KillProcess : Window
    {
        public KillProcess(SshClient sshClient, string pid)
        {
            KillProcessViewModel _killProcessViewModel = new KillProcessViewModel(sshClient, pid);
            this.DataContext = _killProcessViewModel;
            InitializeComponent();
        }
    }
}
