using MvvmDeneme.Command;
using MvvmDeneme.View;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace MvvmDeneme.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string host = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        public string pid = string.Empty;
        private string _sp;
        private string _tp;
        private string _sifre;
        private string _cpulbl;
        private string _memlbl;
        private string[] _myListItems;
        private int _selectedIndex;
        public SshClient sshClient;
        public SftpClient sftpClient;
        private ICommand _dosyaPaylasButton; 
        private ICommand _killProcessButton;
        private ICommand _mesajGonderButton;
        private ICommand _baglaButton;
        private ICommand _yenileButton;
        private ICommand _disconnectButton;
        private ICommand _taskManageButton;
        private ICommand _linuxManageButton;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ICommand linuxManageButton
        {
            get
            {
                _linuxManageButton = new RelayCommand(param => this.LinuxManage());
                return _linuxManageButton;
            }
        }
        public ICommand taskManageButton
        {
            get
            {
                _taskManageButton = new RelayCommand(param => this.TaskManage());
                return _taskManageButton;
            }
        }
        public ICommand killProcessButton
        {
            get
            {
                _killProcessButton = new RelayCommand(param => this.ProcessSonlandır());
                return _killProcessButton;
            }
        }
        public ICommand dosyaPaylasButton
        {
            get
            {
                _dosyaPaylasButton = new RelayCommand(param => this.DosyaGonder());
                return _dosyaPaylasButton;
            }
        }
        public ICommand mesajGonderButton
        {
            get
            {
                _mesajGonderButton = new RelayCommand(param => this.MesajGonder());
                return _mesajGonderButton;
            }
        }
        public ICommand baglaButton
        {
            get
            {
                _baglaButton = new RelayCommand(param => this.Baglanti());
                return _baglaButton;
            }
        }
        public ICommand yenileButton
        {
            get
            {
                _yenileButton = new RelayCommand(param => this.Yenile());
                return _yenileButton;
            }
        }
        public ICommand disconnectButton
        {
            get
            {
                _disconnectButton = new RelayCommand(param => this.BaglantiKopar());
                return _disconnectButton;
            }
        }
        public ObservableCollection<string> Ips { get; set; } = new ObservableCollection<string>()
        {
                "192.168.139.129",
                "IP2",
                "IP3",
                "IP4"
        };

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex == value)
                    return;

                _selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }
        public string MemLabel
        {
            get { return _memlbl; }
            set
            {
                _memlbl = value;
                NotifyPropertyChanged("MemLabel");
            }
        }
        public string CpuLabel
        {
            get { return _cpulbl; }
            set
            {
                _cpulbl = value;
                NotifyPropertyChanged("CpuLabel");
            }
        }
        public string SifreSelect
        {
            get { return _sifre; }
            set { _sifre = value; NotifyPropertyChanged("SifreSelect"); }
        }
        public string TextSelect
        {
            get { return _tp; }
            set { _tp = value; NotifyPropertyChanged("TextSelect"); }
        }
        public string SelectedIps
        {
            get { return _sp; }
            set { _sp = value; NotifyPropertyChanged("SelectedIps"); }
        }
        public string[] MyListItems
        {
            get => _myListItems;
            set
            {
                _myListItems = value;
                NotifyPropertyChanged(nameof(MyListItems));
            }
        }
        public void LinuxManage()
        {
            LinuxManager _linuxManagerView = new LinuxManager(this.sshClient);
            Application.Current.MainWindow = _linuxManagerView;
        }
        public void TaskManage()
        {
            TaskManager _taskManagerView = new TaskManager();
            Application.Current.MainWindow = _taskManagerView;
            //Application.Current.MainWindow.Show();
        }
        public void DosyaGonder()
        {
            DosyaPaylas _dosyaGonderView = new DosyaPaylas(host, username, password);
            Application.Current.MainWindow = _dosyaGonderView;
            Application.Current.MainWindow.Show();
        }
        public void MesajGonder()
        {
            MesajGonder _mesajGonderView = new MesajGonder();
            Application.Current.MainWindow = _mesajGonderView;
            Application.Current.MainWindow.Show();
        }
        public void BaglantiKopar()
        {
            string[] arr = new string[] { };
            if (!(this.sshClient == null) && this.sshClient.IsConnected)
            {
                this.sshClient.Disconnect();
                SelectedIps = null;
                TextSelect = string.Empty;
                SifreSelect = string.Empty;
                MyListItems = arr;
                CpuLabel = "";
                MemLabel = "";
                MessageBox.Show("Bağlantı Koptu");
            }
            else
                MessageBox.Show("Bağlantınız yok");
        }
        public void Yenile()
        {
            string result = string.Empty;
            if ((this.sshClient == null) || !(this.sshClient.IsConnected))
            {
                MessageBox.Show("Bağlantınız Bulunmamaktadır.");
            }
            else
            {
                SshCommand cmd = this.sshClient.RunCommand("ps -aux");
                result = cmd.Result;
            }
            string[] subStrings = result.Split('\n');
            MyListItems = subStrings;

            CpuLabel = "";
            MemLabel = "";
        }
        public void ProcessSonlandır()
        {
            try
            {
                string secilen = MyListItems[SelectedIndex].ToString();
                
                string[] parse = secilen.Split(' ');

                int index = 0;
                string pid = string.Empty;
                string cpu = string.Empty;
                string memory = string.Empty;
                foreach (string c in parse)
                {
                    if (c.Length != 0)
                    {
                        if (index == 1)
                        {
                            pid = c;
                        }
                        else if (index == 2)
                        {
                            cpu = c;
                        }
                        else if (index == 3)
                        {
                            memory = c;
                            break;
                        }
                        index++;
                    }
                }
                if (!(secilen == MyListItems[0]))
                {
                    if (secilen != null || secilen == "")
                    {
                        CpuLabel = (pid + " Numaralı Sürecin CPU Kullanımı(%): " + cpu);
                        MemLabel = (pid + " Numaralı Sürecin Memory Kullanımı(%)" + memory);
                    }
                    if (this.host == "" || this.username == "" || this.password == "")
                    {
                        MessageBox.Show("Lütfen IP Kullanıcı Ve Şifreyi Giriniz");
                    }
                    else
                    {
                        KillProcess _killProcessView = new KillProcess(sshClient, pid);
                        Application.Current.MainWindow = _killProcessView;
                        _killProcessView.surecLbl.Content = (pid + " Numaralı Süreci Sonlandırmaya Emin misiniz?");
                        Application.Current.MainWindow.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Yanlış Seçim, Lütfen Bir Süreç Seçiniz.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Bir Süreç Seçiniz");
            }
        }
        public void Baglanti()
        {
            if (TextSelect == null || SifreSelect == null || SelectedIps == null)
            {
                MessageBox.Show("Bilgileri Eksiksiz Giriniz!");
            }
            else
            {
                host = SelectedIps.ToString();
                username = TextSelect.ToString();
                password = SifreSelect.ToString();
                string result = SshBaglanti("ps -aux");//ps -aux
                string[] subStrings = result.Split('\n');
                MyListItems = subStrings;
            }
        }
        private string SshBaglanti(string command)
        {
            string result = string.Empty;
            try
            {
                AuthenticationMethod method = new PasswordAuthenticationMethod(username, password);
                ConnectionInfo connection = new ConnectionInfo(host, username, method); // ip: 192.168.139.129  kullaciadi: hkn  sifre: 123
                sshClient = new SshClient(connection);

                sshClient.Connect();
                SshCommand cmd = sshClient.RunCommand(command);
                result = cmd.Result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (!sshClient.IsConnected)
                {
                    TextSelect = "";
                    SifreSelect = "";
                    MessageBox.Show("Bağlanamadı, Kontrol Ediniz!");
                }
            }
            return result;
        }
    }
}
