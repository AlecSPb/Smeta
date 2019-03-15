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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.Entity;
using Microsoft.Win32;
using System.Xml.Linq;
using Ninject;
using Smeta_DB;


namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowAddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : MetroWindow
	{
		SmetaEntities1 context = new SmetaEntities1();
		public AddCustomer()
        {
            InitializeComponent();
        }
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			int nomer;
			if (!Int32.TryParse(tbProjectCod.Text, out nomer))
			{
				tbProjectCod.Text = "Некорретный ввод!";
			}
			else if (tbProjectCod != null)
			{

				commentName.Text = "Поле не может быть пустым";
			}
			else if (tbProjectName != null)
			{
				tbProjectName.Text = "Поле не может быть пустым";
			}
			else if (tbProjectAdress != null)
			{
				tbProjectAdress.Text = "Поле не может быть пустым";
			}
			else if (tbProjectYNP.Text.Length != 9)
			{
				tbProjectYNP.Text = "Поле должно содержать 9 цифр!";
			}
			else if (tbRS.Text.Length != 13)
			{
				tbRS.Text = "Поле должно содержать 13 цифр";
			}
			else
			{
				Проектная_организация addProjectCompany = new Проектная_организация()
				{
					КодПроектировщика = Convert.ToInt32(tbProjectCod.Text),
					НаименованиеПроектиров = tbProjectName.Text,
					ЮрАдрес = tbProjectName.Text,
					Р_с = tbRS.Text,
					УНП = tbProjectYNP.Text,
					Тел = tbPhone.Text,
					ЭлПочта = tbMail.Text
					
				};


				context.Проектная_организация.Add(addProjectCompany);
				context.SaveChanges();
				Close();
			}

		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
