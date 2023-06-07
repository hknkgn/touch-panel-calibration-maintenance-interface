using MvvmDeneme.ViewModel;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ShareFile.xaml
    /// </summary>
    public partial class DosyaPaylas : Window
    {
        private TreeViewItem _selected_item;
        private string host;
        private string username;
        private string password;
        public TreeViewItem selected_item
        {
            get { return _selected_item; }
            set { _selected_item = value; }
        }
        public DosyaPaylas(string host, string username, string password)
        {
            this.host = host;
            this.username = username;
            this.password = password;

            DosyaPaylasViewModel _dosyaPaylasViewModel = new DosyaPaylasViewModel(host, username, password);
            this.DataContext = _dosyaPaylasViewModel;
            InitializeComponent();
        }
        private void DowloadFileButton_Click(object sender, RoutedEventArgs e)
        {
            string localDir = @"C:\Users\PC_1770\Desktop";
            var remoteDir = "/home/hkn/Desktop";

            using (SftpClient client = new SftpClient(this.host, this.username, this.password))
            {
                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
                client.OperationTimeout = TimeSpan.FromMinutes(180);
                client.Connect();
                bool connected = client.IsConnected;
                if (connected == true)
                    MessageBox.Show("SFTP Bağlantısı Sağlandı");

                client.ChangeDirectory(remoteDir);

                DownloadDirectory(client, remoteDir, localDir);

                MessageBox.Show("Dosyanız Server İle Paylaşılmıştır.");

                client.Disconnect();
            }
        }
        private void ShareFileButton_Click(object sender, RoutedEventArgs e)
        {
            string localDir = @"C:\Users\PC_1770\Desktop";
            var remoteDir = "/home/hkn/Desktop";

            using (SftpClient client = new SftpClient(this.host, 22, this.username, this.password))
            {
                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(180);
                client.OperationTimeout = TimeSpan.FromMinutes(180);
                client.Connect();
                bool connected = client.IsConnected;
                if (connected == true)
                    MessageBox.Show("SFTP Bağlantısı Sağlandı.");

                client.ChangeDirectory(remoteDir);
                //MessageBox.Show(client.WorkingDirectory);

                UploadDirectory(client, localDir, remoteDir);
                MessageBox.Show("Dosyanız Client İle Paylaşılmıştır.");

                client.Disconnect();
            }
        }
        private void UploadDirectory(SftpClient client, string localPath, string remotePath)
        {
            if (lblFilePath.Content.ToString() == "")
            {
                MessageBox.Show("Lütfen Paylaşım Yapacağınız Dosyayı Seçiniz!!!");
            }
            try
            {
                IEnumerable<FileSystemInfo> infos = new DirectoryInfo(localPath).EnumerateFileSystemInfos(lblFilePath.Content.ToString());
                foreach (FileSystemInfo info in infos)
                {
                    if (info.Attributes.HasFlag(FileAttributes.Directory))
                    {
                        string subPath = remotePath + "/" + info.Name;
                        if (!client.Exists(subPath))
                        {
                            client.CreateDirectory(subPath);
                        }
                        UploadDirectory(client, info.FullName, remotePath + "/" + info.Name);
                    }
                    else
                    {
                        using (Stream fileStream = new FileStream(info.FullName, FileMode.Open))
                        {
                            Console.WriteLine("Uploading {0} ({1:N0} bytes)", info.FullName, ((FileInfo)info).Length);

                            client.UploadFile(fileStream, remotePath + "/" + info.Name);
                        }
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void DownloadDirectory(SftpClient client, string remotePath, string localPath)
        {
            if (lblFilePath.Content.ToString() == "")
            {
                MessageBox.Show("Lütfen Paylaşım Yapacağınız Dosyayı Seçiniz!!!");
            }
            try
            {
                Directory.CreateDirectory(localPath);
                IEnumerable<SftpFile> files = client.ListDirectory(remotePath);//lblFilePath2.Content.ToString()
                string destFilePath = System.IO.Path.Combine(localPath, lblFilePath.Content.ToString());

                string a = "/home/hkn/Desktop/" + lblFilePath.Content.ToString();
                using (Stream fileStream = File.Create(destFilePath))
                {
                    if (lblFilePath.HasContent)
                        client.DownloadFile(a, fileStream);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selected_item = treeView.SelectedItem as TreeViewItem;
            lblFilePath.Content = selected_item.Header.ToString();
        }
    }
}
