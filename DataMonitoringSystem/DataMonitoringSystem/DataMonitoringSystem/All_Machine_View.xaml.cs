﻿using DataMonitoringSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
using uPLibrary.Networking.M2Mqtt;

namespace DataMonitoringSystem
{
    /// <summary>
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class All_Machine_View : Page
    {
        public All_Machine_View(List<MainModel> models)
        {
            InitializeComponent();
            MachineListView.DataContext = models;
            //MachineListView.ItemsSource = models;
            MachineListView.PreviewMouseLeftButtonDown += MachineListView_PreviewMouseLeftButtonDown;
        }

   
        private void MachineListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 클릭된 항목 가져오기
            if (e.OriginalSource is DependencyObject source)
            {
                var listViewItem = FindAncestor<ListViewItem>(source);

                if (listViewItem != null)
                {
                    if (listViewItem.DataContext is MainModel selectedModel)
                    {

                        NavigationService?.Navigate(new DetailView1(MainWindow.elecModels[selectedModel.MachineNo], selectedModel.MachineNo));
                        // 이벤트가 다른 곳에 전달되지 않도록 처리
                        e.Handled = true;
                    }
                }
            }
        }

        // 부모 요소를 찾는 유틸리티 메서드
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null && !(current is T))
            {
                current = VisualTreeHelper.GetParent(current);
            }
            return current as T;
        }
    }
}
