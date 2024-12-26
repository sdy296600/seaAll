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
            WindowStyle = WindowStyle.None; //Window의 타이틀, 버튼(Minimized, Maximized 등) 제거
            WindowState = WindowState.Maximized; // 모니터의 해상도 크기로 변경
            //ResizeMode = ResizeMode.NoResize; // Window의 크기를 변경 불가
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // 다른 화면일 경우 Back 버튼 보이기
            BackButton.Visibility = Visibility.Collapsed;

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
                this.TitleContent.Content = page.Title;
                
                if (MainView.Content is All_Machine_View)
                {
                    // 다른 화면일 경우 Back 버튼 보이기
                    BackButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // 다른 화면일 경우 Back 버튼 보이기
                    BackButton.Visibility = Visibility.Visible;
                }
            }

        }

      
        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 뒤로 가기 동작
            if (MainView.CanGoBack)
            {
                MainView.GoBack();
            }
        }

    }
}