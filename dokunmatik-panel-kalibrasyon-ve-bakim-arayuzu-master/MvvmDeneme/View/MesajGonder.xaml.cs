using MvvmDeneme.ViewModel;
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
    /// Interaction logic for MesajGonder.xaml
    /// </summary>
    public partial class MesajGonder : Window
    {
        public MesajGonder()
        {
            MesajGonderViewModel _mesajGonderViewModel = new MesajGonderViewModel();
            this.DataContext = _mesajGonderViewModel;
            InitializeComponent();
        }
    }
}
