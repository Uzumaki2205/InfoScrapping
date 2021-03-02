using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InfoScrapping_sinhvienhufi
{
    /// <summary>
    /// Interaction logic for ItemSubject.xaml
    /// </summary>
    public partial class ItemSubject : UserControl
    {
        public ItemSubject()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public TimeSpan ActivateStoryboardDuration { get; private set; }
        private void btnCoppy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtLinkZoom.Text);
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Bạn chắc chắn muốn vào học?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(res == MessageBoxResult.Yes)System.Diagnostics.Process.Start(txtLinkZoom.Text);
        }
    }
}
