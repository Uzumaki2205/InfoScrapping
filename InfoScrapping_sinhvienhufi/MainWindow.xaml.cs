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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadContent(string id)
        {
            CrawlHelper api = new CrawlHelper();
            api.MainRun(id);

            var List = api.ListSubject;

            foreach (Subject item in List)
            {
                if(item.LinkZoom != null)
                {
                    ItemSubject itemSubject = new ItemSubject();
                    itemSubject.txtTitle.Text = item.NameSubject;
                    itemSubject.txtTime.Text = item.Time;
                    itemSubject.txtTeacher.Text = item.Teacher;
                    itemSubject.txtLinkZoom.Text = item.LinkZoom;

                    stackContent.Children.Add(itemSubject);
                }
            }
        }
    }
}
