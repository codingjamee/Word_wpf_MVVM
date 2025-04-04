using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using wpf_inf.Models;

namespace wpf_inf.ViewModels
{
    public class GameWordViewModel : ViewModelBase
    {
        private readonly GameWordModel _gameWordModel;
        private readonly GameStateModel _stateModel;
        private string _korWord;
        private string _engWord;
        private List<string> Words => _gameWordModel.Words;

        public int Wrong => _stateModel.Wrong;
        public int MaxWrong => _stateModel.MaxWrong;

        public string DisplayWord => _stateModel.DisplayWord;

        public string KorWord
        {
            get => _gameWordModel.KorWord;
            set
            {
                SetProperty(ref _korWord, value);
                _gameWordModel.SetKorWord(value);
            }
        }
        public string EngWord
        {
            get => _gameWordModel.EngWord;
            set
            {
                SetProperty(ref _engWord, value);
                _gameWordModel.SetEngWord(value);
            }
        }

        private string[] GetRandomWordPair()
        {
            return Words[new Random().Next(0, Words.Count)].Split(',');
        }

        private void AssignWordPair(string[] wordPair)
        {
            KorWord = wordPair[0];
            EngWord = wordPair[1];
        }

        public void InitializeGame()
        {
            //초기화 메서드
            //선택한 문자열 초기화
            //랜덤글자를 선택
            //랜덤글자 할당
            //화면 업데이트
            string[] selectedWord = GetRandomWordPair();
            AssignWordPair(selectedWord);
            
        }
        public GameWordViewModel()
        {
            _gameWordModel = new GameWordModel();
            _stateModel = new GameStateModel();
            _korWord = _gameWordModel.KorWord;
            _engWord = _gameWordModel.EngWord;
            InitializeGame();
        }

    }
}
