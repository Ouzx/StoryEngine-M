using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoryEngine_M
{
    public class Card
    {
        public Card LeftNext { get; set; }
        public Card RightNext { get; set; }

        public List<int> LeftConnections = new List<int>();
        public List<int> RightConnections = new List<int>();


        public static byte OFFSET_X = 55;
        public static byte OFFSET_Y = 29;
        public static byte WIDTH = 66;
        public static byte HEIGHT = 23;
        public int StoryNO { get; set; }
        public int NO { get; set; }
        public string MainText { get; set; }
        public string LeftText { get; set; }
        public string RightText { get; set; }
        public int SpriteID { get; set; }
        public int SoundID { get; set; }
        public bool isPlace { get; set; }
        public Point LastCardPos { get; set; }
        public int Counter { get; set; }
        public Button ButtonCard { get; set; }
        public string TEXT { get; set; }

        public Card(string _main, string _left, string _right,
                    int _sprite, int _sound,
                    Point _lastCardPos, int _counter,string _text,int _storyNO, bool _isPlace)
        {
            MainText = _main; LeftText = _left; RightText = _right;
            SpriteID = _sprite; SoundID = _sound;
            LastCardPos = _lastCardPos; Counter = _counter;
            TEXT = _text; StoryNO = _storyNO; isPlace = _isPlace;
            CreateButton();
        }

        public Button CreateButton()
        {
            ButtonCard = new Button()
            {
                Location = SumPoint(LastCardPos),
                Size = new Size(WIDTH, HEIGHT),
                Text = Counter.ToString(),
                Name = Counter.ToString(),
                ForeColor = Color.Black
            };
            NO = Counter;
            ButtonCard.Click += new EventHandler(INFO);
            ControlExtension.Draggable(ButtonCard, true); 
            return ButtonCard;
        }

        public void INFO(object sender, EventArgs e)
        {
            Form1.frm2.CleanAllData();
            Form1.frm2.textBox1.Text = MainText;
            Form1.frm2.textBox2.Text = LeftText;
            Form1.frm2.textBox3.Text = RightText;
            Form1.frm2.textBox4.Text = TEXT;
            Form1.frm2.comboBox1.SelectedIndex = SpriteID;
            Form1.frm2.comboBox2.SelectedIndex = SoundID;
            Form1.frm2.textBox5.Text = StoryNO.ToString();
            Form1.frm2.checkBox1.Checked = isPlace;
            
                if(LeftNext != null)Form1.frm2.label7.Text = LeftNext.ButtonCard.Name;
                if(RightNext != null) Form1.frm2.label8.Text = RightNext.ButtonCard.Name;
            

            Form1.SelectedCard = this;
            Form1.frm2.label11.Text = Form1.SelectedCard.ButtonCard.Name;
            Form1.frm2.label12.Text = Form1.frm1.CardCounter.ToString();

            for(int i = 0; i < Form1.frm1.CardCounter; i++)
            {
                if (LeftConnections.Count>i)
                {
                    Form1.frm2.checkedListBox2.SetItemChecked(LeftConnections[i], true);
                }
                if (RightConnections.Count > i)
                {
                    Form1.frm2.checkedListBox3.SetItemChecked(RightConnections[i], true);
                }
            }

            //KAYIT KISMI İÇİN
            /*int i = Convert.ToInt32(ButtonCard.Name);
            
                if(Form1.frm1.cards[i].LeftNext != null)
                {
                    Form1.frm2.checkedListBox2.SetItemChecked(i, true);
                }
                if (Form1.frm1.cards[i].RightNext != null)
                {
                    Form1.frm2.checkedListBox3.SetItemChecked(i, true);

                }
           */
        }

        private Point SumPoint(Point _p1) => new Point(_p1.X + OFFSET_X, OFFSET_Y + _p1.Y);
        
    }
}
