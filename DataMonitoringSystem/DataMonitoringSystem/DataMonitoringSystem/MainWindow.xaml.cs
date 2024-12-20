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
            private List<MainModel> mainModels;
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


                All_Machine_View machine_View = new All_Machine_View(mainModels);
                MainView.Content = machine_View;

            
            }
        }
    }