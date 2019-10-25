using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoryEngine_M
{
    public partial class Form2 : Form
    {

        bool done;
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            checkedListBox1.Enabled = false;
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
        }

        public void CleanAllData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label7.Text = "";
            label8.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
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
    }
}

