using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form2 : Form
    {
        public List<Registration> regestrations_ = new List<Registration>();
        Registration reg = new Registration();
        bool t = true;
        string log, parol;
        public Form2()
        {
            InitializeComponent();
            Form1 form1 = new Form1(t);
            regestrations_ = new List<Registration>();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void btnEnter_Click(object sender, EventArgs e)//кнопка проверки авторизации
        {
            List<Registration> r_ = new List<Registration>();
            
            if (!string.IsNullOrEmpty(txtBoxLogin.Text))
            {
               log = txtBoxLogin.Text;
            }

            if (!string.IsNullOrEmpty(txtBoxParol.Text))
            {
                parol = txtBoxParol.Text;
            }

            if (txtBoxLogin.Text.Equals("") || txtBoxParol.Text.Equals(""))
            {
                MessageBox.Show("Поле пустое, введите Логин или Пароль");
            }
            else
            {
                r_ = ReadingBinnary();//возврат текущего файла
                foreach (var item in r_)
                {
                    var found = r_.FindAll(p => p.Login == log && p.Parol == parol);
                     if(found.Count != 0)
                    {
                        Form1 form1 = new Form1(t);
                        form1.Activate();
                        form1.Show();
                       // form1.Show_();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Вы не зарегестрированы, зарегестрируйтесь и играйте");
                    }

                }
            }
        }

        private void btnRegistration_Click(object sender, EventArgs e)//кнопка регистрации
        {
            if (!string.IsNullOrEmpty(txtBoxLogin.Text))
            {
                reg.Login = txtBoxLogin.Text;
            }

            if (!string.IsNullOrEmpty(txtBoxParol.Text))
            {
                reg.Parol = txtBoxParol.Text;
            }

            if (txtBoxLogin.Text.Equals("") || txtBoxParol.Text.Equals(""))
            {
                MessageBox.Show("Вы не смогли зарегестрироваться, введите Логин или Пароль");
            }
            else
            {
                try
                {
                    regestrations_.Add(reg);
                        RecordBinnary(regestrations_);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

             }
            }
        public void RecordBinnary(List<Registration> buff)//запись в файл
        {
            try
            {
                BinaryFormatter binnarryFormat = new BinaryFormatter();
                FileStream stream = new FileStream("lp.dat", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                binnarryFormat.Serialize(stream, buff);
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         }
        public List<Registration> ReadingBinnary()//считывание из файла
        {
            List<Registration> data;
            //try
            //{
                FileStream fs = new FileStream("lp.dat", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                data = (List<Registration>)formatter.Deserialize(fs);
                fs.Close();
           // }
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            
             return data;
        }
        public override string ToString()
        {
          return Convert.ToString( MessageBox.Show(reg.Login, reg.Parol));
        }
    }
}
