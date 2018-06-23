using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public bool flag;
        private
        Label firstClicked = null;//первая картинка
        Label secondClicked = null;//вторая картинка
        Random random = new Random();//перемешивание картинок
        Label iconLabel;
        List<string> icons = new List<string>() 
        { 
            "!", "!", "N", "N", ",", ",", "k", "k",// Каждая из этих букв интересный значок
            "b", "b", "v", "v", "w", "w", "z", "z"// В шрифте Webdings
        };

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                /*Label*/ iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
            Wait(1);
            Show_();
        }

        public void Show_()
        {
            Wait(1);
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                //Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }
        } 
           
        private void Wait(int seconds)
        {
             Thread.Sleep(2000);

            //int ticks = System.Environment.TickCount + (int)Math.Round(seconds * 1000.0);
            //while (System.Environment.TickCount < ticks)
            //{
            //    Application.DoEvents();
            //}
            //await Task.Delay(seconds);

        }


        public Form1()
        {
            InitializeComponent();
            flag = false;
            Form2 form2 = new Form2();
            form2.Show();
            AssignIconsToSquares();
        }
        public Form1(bool fl_)
        {
            InitializeComponent();
            flag = fl_;
            AssignIconsToSquares();
            //Show_();
        }

        private void label_Click(object sender, EventArgs e)
        {
           

            if (timer1.Enabled == true)
                return; 
            
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                CheckForWinner();// Проверка, если игрок выиграл.
                if (firstClicked.Text == secondClicked.Text)// Если игрок нажал два соответствующих текста
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
           // Wait(1);
            firstClicked.ForeColor = firstClicked.BackColor;//скрытие двух иконок
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;//обнуление ссылок на иконки
            secondClicked = null;
        }

        private void CheckForWinner()// Проверка, если игрок выиграл.
                                     // Проход по всем меткам в TableLayoutPanel,
                                     // Проверка каждого из них, чтобы увидеть, если его значок подобран.
        {
           
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            MessageBox.Show("Вы открыли все иконки!", "Поздравляем!");
            Close();
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

       public void Form1_Activated(object sender, EventArgs e)
        {
            if (flag == false)
                this.Visible = false;
            else
                this.Visible = true;
            flag = true;
        }
       
    }

}
