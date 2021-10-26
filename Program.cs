using System;
using System.Windows.Forms;

namespace NoteBook
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool loginSuccess = false;
            int login = 0;
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    loginSuccess = true;
                    login = loginForm.Login;
                }
            }
            if (!loginSuccess) return;
            Application.Run(new NoteBookForm(login));
        }
    }
}
