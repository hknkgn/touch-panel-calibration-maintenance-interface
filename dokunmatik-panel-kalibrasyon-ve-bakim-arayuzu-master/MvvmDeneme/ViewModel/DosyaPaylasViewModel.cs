using MvvmDeneme.View;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MvvmDeneme.ViewModel
{
    public partial class DosyaPaylasViewModel : INotifyPropertyChanged
    {
        private string host = string.Empty;
        private string username = string.Empty;
        private string password = string.Empty;
        private TreeViewItem _selected_item = null;
        private ObservableCollection<TreeViewItem> _treeView;
        //private TreeView _treeView;
        private string _sp = string.Empty;
        private Visibility _buttonGizleB;
        private Visibility _buttonGizleA;
        private string lblFilePath = string.Empty;
        public DosyaPaylasViewModel(string host, string username, string password)
        {
            this.host = host;
            this.username = username;
            this.password = password;

            treeView = new ObservableCollection<TreeViewItem>();
            _selected_item = new TreeViewItem();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public ObservableCollection<string> ComboBoxDoldur { get; set; } = new ObservableCollection<string>()
        {
                "SUNUCU/İSTEMCİ",
                "İSTEMCİ/SUNUCU"
        };
        public string LblFilePath
        {
            get { return lblFilePath; }
            set
            {
                lblFilePath = value;
                NotifyPropertyChanged("LblFilePath");
            }
        }
        public Visibility ButtonGizleA
        {
            get { return _buttonGizleA; }
            set { _buttonGizleA = value; NotifyPropertyChanged("ButtonGizleA"); }
        }
        public Visibility ButtonGizleB
        {
            get { return _buttonGizleB; }
            set { _buttonGizleB = value; NotifyPropertyChanged("ButtonGizleB"); }
        }
        public ObservableCollection<TreeViewItem> treeView
        {
            get { return _treeView; }
            set
            {
                _treeView = value;
                NotifyPropertyChanged("treeView");
            }
        }
        public TreeViewItem TreeViewSecilen
        {
            get { return _selected_item; }
            private set
            {
                if (_selected_item != value)
                {
                    _selected_item = value;
                }
            }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                    if (_isSelected)
                    {
                        TreeViewSecilen = this._selected_item;
                        //TreeViewSecilenItem();
                    }
                }
            }
        }
        public string SelectServerClient
        {
            get { return _sp; }
            set 
            {
                _sp = value; NotifyPropertyChanged("SelectServerClient");
                comboBox_SelectionChanged();
            }
        }
        private void ListDirectory(ObservableCollection<TreeViewItem> treeView, string path)
        {
            //treeView.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);

            treeView.Add(CreateDirectoryNode(rootDirectoryInfo));
        }
        private static TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Items.Add(CreateDirectoryNode(directory));

            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Items.Add(new TreeViewItem { Header = file.Name });

            return directoryNode;
        }
        private void DownloadDirectoryForLinux(SftpClient client, string remotePath)
        {
            try
            {
                IEnumerable<SftpFile> files = client.ListDirectory(remotePath);//lblFilePath2.Content.ToString()
                foreach (SftpFile file in files)
                {
                    if ((file.Name != ".") && (file.Name != ".."))
                    {
                        string sourceFilePath = remotePath + "/" + file.Name;

                        if (file.IsDirectory)
                        {
                            var directoryNode = new TreeViewItem { Header = "**" + file.Name };
                            treeView.Add(directoryNode);//?
                            DownloadDirectoryForLinux(client, sourceFilePath);
                        }
                        else
                        {
                            var directoryNode = new TreeViewItem { Header = file.Name };
                            treeView.Add(directoryNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void comboBox_SelectionChanged()
        {
            if (host == "" || username == "" || password == "")
            {
                MessageBox.Show("Bağlantı Yok!");
            }
            else
            {
                using (SftpClient client = new SftpClient(this.host, 22, this.username, this.password))
                {
                    client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                    client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
                    client.OperationTimeout = TimeSpan.FromMinutes(180);
                    client.Connect();
                    if (SelectServerClient == "SUNUCU/İSTEMCİ")
                    {
                        ButtonGizleB = Visibility.Hidden;
                        ButtonGizleA = Visibility.Visible;
                        LblFilePath = "";
                        treeView.Clear();
                        ListDirectory(treeView, "C:\\Users\\PC_1770\\Desktop");//C:\\Users\\PC_1770\\Desktop
                    }
                    if (SelectServerClient == "İSTEMCİ/SUNUCU")
                    {
                        ButtonGizleA = Visibility.Hidden;
                        ButtonGizleB = Visibility.Visible;
                        LblFilePath = "";
                        treeView.Clear();
                        DownloadDirectoryForLinux(client, "/home/hkn/Desktop");
                        //RemoteFileButton_Click(this.sftpclient);
                    }
                }
            }
        }
        /*public virtual void TreeViewSecilenItem()
        {
            LblFilePath = TreeViewSecilen.ToString();
        }*/
    }
}
