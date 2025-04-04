using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_inf.Models
{
    public class GameStateModel 
    {
        //비공개 필드
        private int _wrong = 0;
        private int _maxWrong = 3;
        private string _stateMessage = $"실패횟수 {0}회 중 {0}회";
        private List<char> _selectedChars;
        private HashSet<char> _guessedLetters = new HashSet<char>(); 
        private List<char> _currentWordMask = new List<char>();
        private bool _isGameIng = false;
        private bool _isWin = false;

        //읽기전용 속성
        public int Wrong => _wrong;
        public int MaxWrong => _maxWrong;
        public string StateMessage => _stateMessage;
        public List<char> SelectedChars => _selectedChars;
        public IReadOnlyCollection<char> GuessedLetters => _guessedLetters;
        public IReadOnlyList<char> CurrentWordDisplay => _currentWordMask;
        public string DisplayWord => string.Join(" ", _currentWordMask);
        public bool IsGameIng => _isGameIng;
        public bool IsWin => _isWin;

        //값 설정 메서드
        public void SetWrong(int value) => _wrong = value;
        public void SetMaxWrong(int value) => _maxWrong = value;
        public void SetStateMessage(string value) => _stateMessage = value;
        public void SetSelectedChars(List<char> value) => value.ForEach(val => _selectedChars.Add(val));
        public void ClearWordMask() { _currentWordMask.Clear(); }
        public void SetWordMask(char value) { _currentWordMask.Add(value); }
        public void SetGameIng(bool value) { _isGameIng = value; }
        public void SetGameResult(bool value) { _isWin = value; }
        public void AddGuessedLetter(char letter) => _guessedLetters.Add(letter);
        public void ClearGuessedLetters() => _guessedLetters.Clear();

        public GameStateModel ()
        {
          
            _stateMessage = $"실패횟수 {MaxWrong}회 중 {Wrong}회";
            _selectedChars = new List<char>();
        }
    }
}
