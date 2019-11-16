using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StoryEngine_M
{
    public partial class Form1 : Form
    {
        public static Form1 frm1;
        public static Form2 frm2;
        public static Form3 frm3;
        public static bool FormShow;

        public Form1()
        {
            InitializeComponent();
            frm2 = new Form2();
            frm3 = new Form3();
            frm1 = this;
            frm2.Show();
            ControlExtension.Draggable(frm2, true);
            ControlExtension.Draggable(frm3, true);
            timer2.Start();

            Save.tempPath = @AppDomain.CurrentDomain.BaseDirectory+"Temps" + @"\" + Guid.NewGuid().ToString() + ".txt";
            using (File.Create(Save.tempPath)) { }


        }

        public List<Card> cards = new List<Card>();
        
        
        public static Card SelectedCard;
        public int CardCounter = 0;
        public Card CreateCard()
        {
            Card _card;
            if(CardCounter == 0)
            {
                _card = new Card(frm2.textBox1.Text, frm2.textBox2.Text, frm2.textBox3.Text,
                                frm2.comboBox1.SelectedIndex, frm2.comboBox2.SelectedIndex,
                                new Point(-8, 219), CardCounter, frm2.textBox4.Text, Convert.ToInt32(frm2.textBox5.Text),
                                frm2.checkBox1.Checked, frm2.checkBox2.Checked, frm2.textBox6.Text);
            }
            else
            {
                _card = new Card(frm2.textBox1.Text, frm2.textBox2.Text, frm2.textBox3.Text,
                                frm2.comboBox1.SelectedIndex, frm2.comboBox2.SelectedIndex,
                                SelectedCard.ButtonCard.Location, CardCounter,frm2.textBox4.Text, Convert.ToInt32(frm2.textBox5.Text),
                                frm2.checkBox1.Checked, frm2.checkBox2.Checked, frm2.textBox6.Text);
            }
            SelectedCard = _card;
            groupBox1.Controls.Add(_card.ButtonCard);
            groupBox1.Size += new Size(100, 100);
            CardCounter++;
            cards.Add(_card);
            frm2.checkedListBox2.Items.Add("L", false);
            frm2.checkedListBox3.Items.Add("R", false);
            
            frm2.checkedListBox1.Items.Add((CardCounter-1).ToString(),true);

            return _card;
        }
        public void EditCard()
        {
            SelectedCard.MainText = frm2.textBox1.Text;
            SelectedCard.LeftText = frm2.textBox2.Text;
            SelectedCard.RightText = frm2.textBox3.Text;
            SelectedCard.SpriteID = frm2.comboBox1.SelectedIndex;
            SelectedCard.SoundID = frm2.comboBox2.SelectedIndex;
            SelectedCard.TEXT = frm2.textBox4.Text;
            SelectedCard.StoryNO = Convert.ToInt32(frm2.textBox5.Text);
            SelectedCard.isPlace = frm2.checkBox1.Checked;
            SelectedCard.isComment = frm2.checkBox2.Checked;
            SelectedCard.Comment = frm2.textBox6.Text;
            EditList();
            CheckList();
            
        }
       
        public void CheckList()
        {
            for(int i = 0; i < frm2.checkedListBox2.Items.Count; i++)
            {
                if(frm2.checkedListBox2.GetItemChecked(i))
                {
                    cards[i].LeftNext = SelectedCard;

                    frm2.checkedListBox2.Items.RemoveAt(i);
                    frm2.checkedListBox2.Items.Insert(i, "DOLU");

                    //INFO için
                    cards[SelectedCard.NO].LeftConnections.Add(i);
                }
                if(frm2.checkedListBox3.GetItemChecked(i))
                {
                    cards[i].RightNext = SelectedCard;

                    frm2.checkedListBox3.Items.RemoveAt(i);
                    frm2.checkedListBox3.Items.Insert(i, "DOLU");

                    //INFO için
                    cards[SelectedCard.NO].RightConnections.Add(i);
                }
            }
        }

        public void EditList()
        {
            for(int i = 0; i < frm2.checkedListBox2.Items.Count; i++)
            {
                if (frm2.checkedListBox2.Items[i].ToString() == "DOLU" &&
                   !frm2.checkedListBox2.GetItemChecked(i) &&
                   SelectedCard.LeftConnections.Contains(i))
                {
                    
                    SelectedCard.LeftConnections.Remove(i);
                    cards[i].LeftNext = null;

                    frm2.checkedListBox2.Items.RemoveAt(i);
                    frm2.checkedListBox2.Items.Insert(i, "L");

                }
                if (frm2.checkedListBox3.Items[i].ToString() == "DOLU" &&
                  !frm2.checkedListBox3.GetItemChecked(i) &&
                  SelectedCard.RightConnections.Contains(i))
                {
                    
                    SelectedCard.RightConnections.Remove(i);
                    cards[i].RightNext = null;


                    frm2.checkedListBox3.Items.RemoveAt(i);
                    frm2.checkedListBox3.Items.Insert(i, "L");
                }
            }
        }

        private static int yy = 70;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            frm2.button6.BackColor = Color.Black;
            for(int i = 0; i < cards.Count; i++)
            {
                cards[i].ButtonCard.Location = new Point(cards[i].ButtonCard.Location.X, cards[i].ButtonCard.Location.Y + yy);
            }
            frm2.button6.BackColor = Color.DarkOrange;
            groupBox1.Size = new Size(groupBox1.Width + yy, groupBox1.Height + yy);

            timer1.Stop();

        }

        public void EraseCard()
        {
            int i = SelectedCard.NO;
            if (CardCounter > 1 && i != 0)
            {
                frm2.checkedListBox1.Items.RemoveAt(i);
                frm2.checkedListBox2.Items.RemoveAt(i);
                frm2.checkedListBox3.Items.RemoveAt(i);
                groupBox1.Controls.Remove(SelectedCard.ButtonCard);
                CardCounter--;
                SelectedCard = cards[CardCounter - 1];
                frm2.label11.Text = SelectedCard.NO.ToString();
                frm2.label12.Text = CardCounter.ToString();
                cards.RemoveAt(i);
                GC.Collect();
            }
            else if(CardCounter == 1)
            {
                frm2.checkedListBox1.Items.RemoveAt(0);
                frm2.checkedListBox2.Items.RemoveAt(0);
                frm2.checkedListBox3.Items.RemoveAt(0);
                groupBox1.Controls.Remove(SelectedCard.ButtonCard);
                CardCounter--;
                cards.RemoveAt(i);
                frm2.button1.PerformClick();
                GC.Collect();
            }
            
        }

       
        private void Timer2_Tick(object sender, EventArgs e)
        {
            frm3.richTextBox1.Clear();
           
            for(int i = 0; i < CardCounter; i++)
            {
                if (cards[i].LeftNext == null)
                {
                    frm3.richTextBox1.AppendText(i.ToString() + "  nolu kartın sol bağlantısı yok! \n");
                }
                if (cards[i].RightNext == null)
                {
                    frm3.richTextBox1.AppendText(i.ToString() + "  nolu kartın sağ bağlantısı yok! \n");
                }
            }
            
            
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

            if (!FormShow)
            {
                frm2.Hide();
                FormShow = true;
                button1.BackColor = Color.Red;
            }
            else
            {
                frm2.Show();
                FormShow = false;
                button1.BackColor = Color.Green;

            }
        }
    }
}
