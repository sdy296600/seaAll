using DataMonitoringSystem.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataMonitoringSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<MainModel> mainModels;
        public static Dictionary<int,ElecModel> elecModels;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainModels = new List<MainModel>()
            {
                new MainModel(13),
                new MainModel(21),
                new MainModel(22),
                new MainModel(23),
                new MainModel(24),
                new MainModel(25)
            };
            elecModels = new Dictionary<int, ElecModel>();
            foreach (MainModel model in mainModels) 
            {
                elecModels.Add(model.MachineNo,new ElecModel(model.MachineNo));
            }
            MainView.ContentRendered += MainView_ContentRendered;
            All_Machine_View machine_View = new All_Machine_View(mainModels);
            MainView.Content = machine_View;
        }
        private void MainView_ContentRendered(object? sender, EventArgs e)
        {
            // 또는 MainView에 설정된 Page의 Title 변경
            if (MainView.Content is Page page)
            {
                this.Title.Content = page.Title;
            }
        }
    }
}