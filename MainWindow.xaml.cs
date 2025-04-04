using System;
using System.Windows;
using System.Windows.Controls;

namespace wpf_inf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var stateViewModel = new ViewModels.GameStateViewModel();

            // DataContext 설정
            this.DataContext = stateViewModel;
            ic.ItemsSource = stateViewModel.CharButtons;

            // 게임 초기화
            stateViewModel.InitializeGame();
        }
    }
}