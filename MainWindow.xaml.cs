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

            // 모델 생성
            var wordModel = new Models.GameWordModel();
            var stateModel = new Models.GameStateModel();

            // 뷰모델 생성 및 버튼 커맨드 연결
            var stateViewModel = new ViewModels.GameStateViewModel(stateModel, wordModel);

            // 버튼에 커맨드 할당
            foreach (var charButton in wordModel.CharButtons)
            {
                charButton.OnClickChar = new CharButtonState.DelegateCommand(
                    param => stateViewModel.HandleCharButtonClick(param));
            }

            // 데이터 컨텍스트 설정
            this.DataContext = stateViewModel;
            ic.ItemsSource = wordModel.CharButtons;

            // 게임 초기화
            stateViewModel.InitializeGame();
        }
    }
}