using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using wpf_inf.Models;

namespace wpf_inf.ViewModels
{
    public class GameStateViewModel : ViewModelBase
    {
        private readonly GameStateModel _stateModel;
        private readonly GameWordModel _wordModel;

        private string _displayWord;

        // 속성
        public string EngWord => _wordModel.EngWord;
        public string KorWord => _wordModel.KorWord;
        public string DisplayWord
        {
            get => _stateModel.DisplayWord;
            set => OnPropertyChanged(nameof(DisplayWord));
        }
        public string StateMessage
        {
            get => _stateModel.StateMessage;
            set
            {
                _stateModel.SetStateMessage(value);
                OnPropertyChanged(nameof(StateMessage));
            }
        }

        // 버튼 클릭 처리 메서드
        public void HandleCharButtonClick(object parameter)
        {
            if (!_stateModel.IsGameIng) return;

            var charStr = parameter?.ToString();
            if (string.IsNullOrEmpty(charStr)) return;

            char selectedChar = charStr[0];

            // 버튼 비활성화
            var button = _wordModel.CharButtons.FirstOrDefault(b => b.Character == selectedChar);
            if (button != null)
            {
                button.IsEnabled = false;
            }

            // 글자 확인
            bool isCorrect = EngWord.Contains(selectedChar);

            if (isCorrect)
            {
                // 맞은 경우
                _stateModel.AddGuessedLetter(selectedChar);
                UpdateWordMask();

                // 승리 확인
                if (!_stateModel.DisplayWord.Contains('*'))
                {
                    _stateModel.SetGameIng(false);
                    _stateModel.SetGameResult(true);
                    MessageBox.Show($"축하합니다! '{EngWord}' 단어를 맞추셨습니다!", "게임 승리");
                }
            }
            else
            {
                // 틀린 경우
                _stateModel.SetWrong(_stateModel.Wrong + 1);
                StateMessage = $"실패횟수 {_stateModel.MaxWrong}회 중 {_stateModel.Wrong}회";

                // 패배 확인
                if (_stateModel.Wrong >= _stateModel.MaxWrong)
                {
                    _stateModel.SetGameIng(false);
                    _stateModel.SetGameResult(false);
                    MessageBox.Show($"아쉽네요! 정답은 '{EngWord}'입니다.", "게임 패배");
                }
            }

            // 선택한 글자 추가
            _stateModel.SelectedChars.Add(selectedChar);
        }

        // 단어 마스크 업데이트
        private void UpdateWordMask()
        {
            _stateModel.ClearWordMask();
            foreach (char c in EngWord)
            {
                _stateModel.SetWordMask(_stateModel.GuessedLetters.Contains(c) ? c : '*');
            }

            OnPropertyChanged(nameof(DisplayWord));
        }

        // 게임 초기화
        public void InitializeGame()
        {
            // 랜덤 단어 선택
            string[] selectedWord = _wordModel.Words[new Random().Next(0, _wordModel.Words.Count)].Split(',');
            _wordModel.SetKorWord(selectedWord[0]);
            _wordModel.SetEngWord(selectedWord[1]);

            // 버튼 초기화
            foreach (var button in _wordModel.CharButtons)
            {
                button.IsEnabled = true;
            }

            // 게임 상태 초기화
            _stateModel.SetWrong(0);
            _stateModel.ClearGuessedLetters();
            _stateModel.ClearWordMask();
            _stateModel.SetGameIng(true);
            _stateModel.SetGameResult(false);
            StateMessage = $"실패횟수 {_stateModel.MaxWrong}회 중 {_stateModel.Wrong}회";

            // 단어 마스크 생성
            UpdateWordMask();

            // 속성 변경 알림
            OnPropertyChanged(nameof(KorWord));
        }

        public GameStateViewModel(GameStateModel stateModel, GameWordModel wordModel)
        {
            _stateModel = stateModel;
            _wordModel = wordModel;
            StateMessage = $"실패횟수 {_stateModel.MaxWrong}회 중 {_stateModel.Wrong}회";

            // 초기 게임 설정
            InitializeGame();
        }
    }
}