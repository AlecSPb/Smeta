using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Ninject;
using System.Data.Entity;
using Microsoft.Win32;
using System.Xml.Linq;
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

            //// Удалить после отладки
            //this.Hide();
            //var mw = new WindowStart(SmetaContext);
            //mw.ShowDialog();
            //this.Close();
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
                    this.Hide();
                    MessageBox.Show(sotrudn.ФИО + ", вы вошли в систему как " + sotrudn.Должность);
                    var mw = new WindowStart(SmetaContext);
                    mw.ShowDialog();
                    this.Close();
                }
                if (sRole == "user")
                {
                    this.Hide();
                    MessageBox.Show(sotrudn.ФИО + ", вы вошли в систему как " + sotrudn.Должность);
                    var mw = new WindowStart(SmetaContext);
                    mw.ShowDialog();
                    this.Close();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

        //public event EventHandler ClickAdmin;
        //public event EventHandler ClickUser;
    }
}
