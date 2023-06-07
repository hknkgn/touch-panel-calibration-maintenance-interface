using MvvmDeneme.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MvvmDeneme.Command;
using System.Windows.Input;
using Renci.SshNet;

namespace MvvmDeneme.ViewModel
{
    public partial class KillProcessViewModel : INotifyPropertyChanged
    {
        private ICommand _pidSonlandirButton;
        private SshClient sshclient;
        private string pidSayisi = string.Empty;
        private string _tp;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public string TextSelect
        {
            get { return _tp; }
            set { _tp = value; NotifyPropertyChanged("TextSelect"); }
        }
        public KillProcessViewModel(SshClient sshClient, string pidSayisi)
        {
            this.sshclient = sshClient;
            this.pidSayisi = pidSayisi;
        }

        public ICommand pidSonlandirButton
        {
            get
            {
                _pidSonlandirButton = new RelayCommand(param => this.ProcessSonlandirPid());
                return _pidSonlandirButton;
            }
        }
        public void ProcessSonlandirPid()
        {
            string command = ("kill -9 " + this.pidSayisi);
            SshCommand cmd = sshclient.RunCommand(command);
            if (cmd.Error == null)
            {
                TextSelect = (this.pidSayisi + " numaralı sürec sonlandirilmiştir.");
            }
            else
            {
                MessageBox.Show(cmd.Error);
                Application.Current.MainWindow.Close();
            }
        }
    }
}
