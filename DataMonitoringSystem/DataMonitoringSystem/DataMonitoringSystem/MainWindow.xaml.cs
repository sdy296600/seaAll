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
        public static Dictionary<int, ElecModel2> elecModels2;


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

            mainModels =
            [
                new MainModel(13),
                new MainModel(21),
                new MainModel(22),
                new MainModel(23),
                new MainModel(24),
                new MainModel(25)
            ];

            elecModels = new Dictionary<int, ElecModel>();
            elecModels2 = new Dictionary<int, ElecModel2>();

            foreach (MainModel model in mainModels) 
            {
                elecModels.Add(model.MachineNo,new ElecModel(model.MachineNo));
                elecModels2.Add(model.MachineNo, new ElecModel2(model.MachineNo));
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

                if (MainView.Content is iButtonProperty btnIntface)
                {
                    // ButtonPropertyInterface로 성공적으로 캐스팅된 경우
                    BackButton.Visibility = btnIntface.BackVisibilty;
                    NextButton.Visibility = btnIntface.NextVisibilty;
                }
                else
                {
                    // 기본값 설정 (예: 버튼 숨기기)
                    BackButton.Visibility = Visibility.Collapsed;
                    NextButton.Visibility = Visibility.Collapsed;
                }
            }

        }

      
        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 뒤로 가기 동작
            MainView.Navigate(new All_Machine_View(mainModels));
        }

        private void NextButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MainView.Content is DetailView1 model)
            {
                int machine_no = model.machine_no;
                MainView.Navigate(new DetailView2(elecModels2[machine_no], machine_no));
            }
            else if(MainView.Content is DetailView2 model2) 
            {

                int machine_no = model2.machine_no;
                MainView.Navigate(new DetailView1(elecModels[machine_no], machine_no));
            }
        }
    }
}