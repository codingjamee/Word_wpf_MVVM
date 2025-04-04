using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using wpf_inf.Models;
using static wpf_inf.CharButtonState;

namespace wpf_inf.ViewModels
{
    public class GameStateViewModel : ViewModelBase
    {
        private readonly GameStateModel _stateModel;
        private readonly GameWordModel _wordModel;

        private string EngWord => _wordModel.EngWord;

        private int _wrong;
        private int _maxWrong;
        private string _stateMessage;
        private List<char> _selectedChars;
        private bool _isGameIng;
        private bool _isWin;
        private ICommand _replay;

        public ObservableCollection<CharButtonState> CharButtons => _wordModel.CharButtons;

        public int Wrong
        {
            get => _wrong;
            set
            {
                SetProperty(ref _wrong, value);
                _stateModel.SetWrong(value);
            }
        }
        public int MaxWrong
        {
            get => _maxWrong;
            set
            {
                SetProperty(ref _maxWrong, value);
                _stateModel.SetWrong(value);
            }
        }
        public string StateMessage
        {
            get => _stateMessage;
            set
            {
                SetProperty(ref _stateMessage, value);
                _stateModel.SetStateMessage(value);
            }
        }
        public List<char> SelectedChars
        {
            get => _stateModel.SelectedChars;
            set
            {
                SetProperty(ref _selectedChars, value);
                _stateModel.SetSelectedChars(value);
            }
        }
        public int AddWrong(int value)
        {
            Wrong += value;
            return Wrong;
        }

        public void SetStateMessage(int wrong, int max)
        {
            StateMessage = $"실패횟수 {max}회 중 {wrong}회";
        }

        public bool IsGameIng
        {
            get => _isGameIng;
            set
            {
                SetProperty(ref _isGameIng, value);
                _stateModel.SetGameIng(value);
            }
        }

        public ICommand Replay
        {
            get
            {
                if (_replay == null)
                {
                    _replay = new RelayCommand(_ => ReplayCommand(), _ => true);
                }
                return _replay;
            }
        }

        public string DisplayWord => _stateModel.DisplayWord;
        public string KorWord => _wordModel.KorWord;

        // 단어 마스크 업데이트
        private void UpdateWordMask(string targetWord)
        {
            if (targetWord == null) return;
            _stateModel.ClearWordMask();
            foreach (char c in targetWord)
            {
                _stateModel.SetWordMask(_stateModel.GuessedLetters.Contains(c) ? c : '*');
            }
            OnPropertyChanged(nameof(DisplayWord));
        }

        public void SelectWrong()
        {
            AddWrong(1);
            //졌는지 판단 지면 end 메서드 실행 및 early return
            bool lose = CheckLose();
            if (lose)
            {
                _stateModel.SetGameIng(false);
                _stateModel.SetGameResult(false);
                MessageBox.Show($"아쉽네요! 정답은 '{EngWord}'입니다.", "게임 패배");
            }

            SetStateMessage(Wrong, MaxWrong);
        }

        public bool CheckLose()
        {
            return Wrong >= MaxWrong;
        }

        public bool CheckWin()
        {
            // Check if all letters of the word have been guessed
            return !_stateModel.DisplayWord.Contains('*');
        }


        private void SelectCorrect(char selectedChar)
        {
            // Add the character to guessed letters
            _stateModel.AddGuessedLetter(selectedChar);

            UpdateWordMask(EngWord);

            bool win = CheckWin();
            if (win)
            {
                _stateModel.SetGameIng(false);
                _stateModel.SetGameResult(true);
                MessageBox.Show($"축하합니다! '{EngWord}' 단어를 맞추셨습니다!", "게임 승리");
            }
        }


        public bool CheckChar(string word)
        {
            char selectedChar = word[0];
            if (EngWord.Contains(selectedChar))
            {
                return true;
            }
            return false;
        }

    
        public void OnClickChar(object parameter)
        {
            if (!IsGameIng) return;

            var charStr = parameter?.ToString();
            if (string.IsNullOrEmpty(charStr)) return;

            char selectedChar = charStr[0];

            // Disable the button
            var button = _wordModel.CharButtons.FirstOrDefault(b => b.Character == selectedChar);
            if (button != null)
            {
                button.IsEnabled = false;
            }

            bool isCorrect = CheckChar(charStr);
            if (isCorrect)
            {
                SelectCorrect(selectedChar);
            }
            else
            {
                SelectWrong();
            }

            SelectedChars.Add(selectedChar);
        }

        public void InitializeGame()
        {
            // Reset all buttons
            foreach (var button in _wordModel.CharButtons)
            {
                button.IsEnabled = true;
                button.OnClickChar = new RelayCommand(OnClickChar);
            }

            // 랜덤 단어 선택
            string[] selectedWord = _wordModel.Words[new Random().Next(0, _wordModel.Words.Count)].Split(',');
            _wordModel.SetKorWord(selectedWord[0]);
            _wordModel.SetEngWord(selectedWord[1]);

            // Reset game state
            Wrong = 0;
            _stateModel.ClearWordMask();
            _stateModel.ClearGuessedLetters();

            // Set initial word mask
            UpdateWordMask(EngWord);

            // Set game status
            IsGameIng = true;
            SetStateMessage(Wrong, MaxWrong);

            OnPropertyChanged(nameof(KorWord));
            OnPropertyChanged(nameof(DisplayWord));
        }

        public void ReplayCommand ()
        {
            InitializeGame();
        }

        public GameStateViewModel()
        {
            //모델 초기화
            _stateModel = new GameStateModel();
            _wordModel = new GameWordModel();

            _wrong = _stateModel.Wrong;
            _maxWrong = _stateModel.MaxWrong;
            _stateMessage = _stateModel.StateMessage;
            _isGameIng = _stateModel.IsGameIng;
            _isWin = _stateModel.IsWin;

        }
    }
}