using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_inf.Models
{
    //게임 단어 로직
    public class GameWordModel
    {
        //비공개 필드
        private string _korWord;
        private string _engWord;

        //읽기전용 속성
        public string KorWord => _korWord;
        public string EngWord => _engWord;


        //값 설정 메서드
        public void SetKorWord(string value) => _korWord = value;
        public void SetEngWord(string value) => _engWord = value;
        public  ObservableCollection<CharButtonState> CharButtons;


        public readonly List<string> Words = new List<string>()
        {
            "거위,goose",
            "참새,sparrow",
            "비둘기,pigion",
            "오리,duck",
            "펭귄,penguin"
        };

        private void InitializeCharBtns()
        {

            string chars = "abcdefghijklmnopqrstuvwxyz";
            foreach (char ch in chars)
            {
                //문자열 각각 보면서 CharButtonState에 매개변수를 넣어 초기화
                CharButtons.Add(new CharButtonState(ch));
            }
        }

        public GameWordModel()
        {

            CharButtons = new ObservableCollection<CharButtonState>();
            InitializeCharBtns();
           // ic.ItemsSource = CharButtons;
        }

    }
}
