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
    public partial class Form2 : Form
    {

        bool done;
        public Form2()
        {
            InitializeComponent();
            

            string[] sprites = Directory.GetFiles(Save.paths[1], "*.png");
            string[] sounds = Directory.GetFiles(Save.paths[2], "*.mp3");
            foreach(string sprite in sprites)
            {
                comboBox1.Items.Add(SharpText(sprite));
            }
            foreach(string sound in sounds)
            {
                comboBox2.Items.Add(SharpText(sound));
            }

            
            
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            checkedListBox1.Enabled = false;
        }

        private string SharpText(string s)
        {
            string[] ss = { "\\" };
            string[] words = s.Split(ss, System.StringSplitOptions.RemoveEmptyEntries);
            return words[words.Length - 1];
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(!done)
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                done = true;
            }
            Form1.frm1.CreateCard();
            Form1.frm1.CheckList();
            CleanAllData();
            label11.Text = Form1.SelectedCard.ButtonCard.Name;
            label12.Text = Form1.frm1.CardCounter.ToString();

            Save.SaveTemp(Form1.frm1.cards);
        }

        public void CleanAllData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            label7.Text = "";
            label8.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            checkBox1.Checked = false;
            for(int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
                checkedListBox3.SetItemChecked(i, false);
            }
        }
         
        private void Button2_Click(object sender, EventArgs e)
        {
            Form1.frm1.EditCard();
            CleanAllData();

            Save.SaveTemp(Form1.frm1.cards);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            CleanAllData();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            Form1.frm1.timer1.Start();
        }

        public static bool FormShow;

        private void Button7_Click(object sender, EventArgs e)
        {
            if (FormShow)
            {
                Form1.frm3.Hide();
                FormShow = false;
                button7.BackColor = Color.White;
            }
            else
            {
                Form1.frm3.Show();
                FormShow = true;
                button7.BackColor = Color.Gray;

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            CleanAllData();
            Form1.frm1.EraseCard();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Save.Savee(Form1.frm1.cards);
            MessageBox.Show("Kayıt tamamlandı!");
        }

        private void TextBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
           
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = button7;
            }
        }
    }
}

