using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
	{
		public static string sRole; // Роль пользователя, вошедшего в систему
		public static int idSotrudn; // ID пользователя, вошедшего в систему

        public SmetaEntities SmetaContext { get; set; }

        public MainWindow()
		{
			InitializeComponent();

            SmetaContext = new SmetaEntities();
        }

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
            var localDate = DateTime.Now;

            var sotrudn = SmetaContext.Исполнитель
                   .Where(v => v.Логин == textBoxLogin.Text && v.Пароль == passwordBox1.Text)
                   .AsEnumerable()
                   .FirstOrDefault();

            if (sotrudn is null)
            {
                lbLogin.Content = "неверное имя или пароль";
            }
            else
            {
                sRole = sotrudn.Роль;
                idSotrudn = sotrudn.КодИсполнителя;
                lbLogin.Content = sotrudn.ФИО + ", вы вошли в систему как " + sotrudn.Должность;

                if (sRole == "admin")
                {
                    Hide();
                    MessageBox.Show(sotrudn.ФИО + ", вы вошли в систему как " + sotrudn.Должность);
                    var mw = new WindowStart(SmetaContext);
                    mw.ShowDialog();
                    Close();
                }
                if (sRole == "user")
                {
                    Hide();
                    MessageBox.Show(sotrudn.ФИО + ", вы вошли в систему как " + sotrudn.Должность);
                    var mw = new WindowStart(SmetaContext);
                    mw.ShowDialog();
                    Close();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
    }
}
