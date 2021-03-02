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

namespace InfoScrapping_sinhvienhufi
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            Closed += Login_Closed;
        }

        private void Login_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        void ProcessMessageLink(string social, string link)
        {
            MessageBoxResult res = MessageBox.Show($"Bạn có muốn chuyển hướng đến {social} của tôi không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
                System.Diagnostics.Process.Start(link);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag.Equals("facebook"))
                ProcessMessageLink("facebook", "https://www.facebook.com/son.auz1/");
            else if (btn.Tag.Equals("instagram"))
                ProcessMessageLink("instagram", "");
            else ProcessMessageLink("github", "https://github.com/Uzumaki2205");
        }
        
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CrawlHelper api = new CrawlHelper();
            if (api.Login(txtUserName.Text) == false)
                MessageBox.Show("UserName không tồn tại!!", "Thất bại");
            else {
                MessageBox.Show("Login thành công", "Thành công");
                this.Visibility = Visibility.Hidden;

                MainWindow fr = new MainWindow();
                fr.Show();
                fr.LoadContent(txtUserName.Text);
                fr.Closed += Fr_Closed;
            } 
        }

        private void Fr_Closed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
        }
    }
}
