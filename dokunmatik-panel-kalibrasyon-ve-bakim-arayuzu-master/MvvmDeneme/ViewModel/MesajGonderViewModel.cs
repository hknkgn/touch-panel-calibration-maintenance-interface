using MvvmDeneme.Command;
using MvvmDeneme.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MvvmDeneme.ViewModel
{
    public partial class MesajGonderViewModel : INotifyPropertyChanged
    {
        Socket soket;
        private string _sp;
        private string _tp;
        private string _mesajTextBox;
        private ICommand _tcpBaglaButton;
        private ICommand _mesajGonderButton;
        public event PropertyChangedEventHandler PropertyChanged;
        public MesajGonderViewModel()
        {
            Socket soket2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.soket = soket2;
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<string> Ips { get; set; } = new ObservableCollection<string>()
        {
                "192.168.139.129",
                "IP2",
                "IP3",
                "IP4"
        };
        public string SelectedIps
        {
            get { return _sp; }
            set { _sp = value; NotifyPropertyChanged("SelectedIps"); }
        }
        public string PortSelect
        {
            get { return _tp; }
            set { _tp = value; NotifyPropertyChanged("PortSelect"); }
        }
        public string MesajTextBox
        {
            get { return _mesajTextBox; }
            set { _mesajTextBox = value; NotifyPropertyChanged("MesajTextBox"); }
        }
        public ICommand TcpbaglaButton
        {
            get
            {
                _tcpBaglaButton = new RelayCommand(param => this.TcpBaglanButton());
                return _tcpBaglaButton;
            }
        }
        public ICommand MesajgonderButton
        {
            get
            {
                _mesajGonderButton = new RelayCommand(param => this.MesajGonderButton());
                return _mesajGonderButton;
            }
        }
        private void TcpBaglanButton()
        {
            if (PortSelect == null || SelectedIps == null)
            {
                MessageBox.Show("Bilgileri Eksiksiz Giriniz!");
            }
            else
            {
                int PORT = Convert.ToInt32(PortSelect);
                string uzakBilgisayarIp = SelectedIps.ToString();

                try
                {
                    this.soket.Connect(new IPEndPoint(IPAddress.Parse(uzakBilgisayarIp), PORT));
                    MessageBox.Show("Başarıyla Bağlanıldı. Mesajınızı Girebilirsiniz.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n (X) -> Bağlanmaya çalışırken hata oluştu: " + ex.Message);
                    Application.Current.MainWindow.Close();
                }
            }
        }
        private void MesajGonderButton()
        {
            if (this.soket.Connected)
            {
                string gonder = MesajTextBox.ToString();
                this.soket.Send(Encoding.UTF8.GetBytes(gonder));
                MesajTextBox = "";
                if (gonder == "exit")
                {
                    this.soket.Shutdown(SocketShutdown.Both);
                    this.soket.Close();
                    MessageBox.Show("Bağlantı Koparıldı.");
                    MesajTextBox = "";
                    PortSelect = "";
                    SelectedIps = "";
                    Application.Current.MainWindow.Close();
                }
            }
            else
                MessageBox.Show("Bağlantınız Yok");
        }
    }
}
