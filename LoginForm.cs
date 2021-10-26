using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using NoteBook.Contexts;
using NoteBook.Models;

namespace NoteBook
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
        }

        delegate void Registration();
        delegate void Authorizaton();
        public int Login { get; set; }
        private void authButton_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                Authorizaton auth = () =>
                {
                    using (UserContext uc = new UserContext())
                    {
                        string login = ToMd5(textBox1.Text);
                        string password = ToMd5(textBox2.Text);
                        var tmp = uc.Users.Where(p => p.Name == login && p.Password == password).ToList();
                        if (tmp.Count > 0)
                        {
                            this.Login = tmp[0].Id;
                            this.DialogResult = DialogResult.OK;

                        }
                        else
                        {
                            MessageBox.Show("Неверное имя пользователя или пароль!");
                        }
                            Count++;
                    } 
                };
                auth();
            } 
            if(Count == 3)
            {
                MessageBox.Show("Превышены попытки входа");
                Environment.Exit(0);
            }

        }

        private string ToMd5(string str)
        {
            MD5 md5= MD5.Create();
            byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            str = Convert.ToBase64String(result);
            return str;
        }

        private int Count = 0;

        private void regButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0)
            { 
                Registration reg = () => 
                {
                    using (UserContext us = new UserContext())
                    {        
                        string login = ToMd5(textBox1.Text);
                        string password = ToMd5(textBox2.Text);
                        if(us.Users.Any(p => p.Name == login))
                        {
                            MessageBox.Show("Имя пользователя уже занято");
                            return;
                        }
                        else 
                        {
                            User user = new User
                            {
                                Name = login,
                                Password = password
                            };
                            us.Users.Add(user);
                            us.SaveChanges();
                            MessageBox.Show("Пользователь добавлен!");
                            return;
                        }
                    }
                };
                reg.Invoke();
            }
        }
    }
}
