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
        public SmetaEntities SmetaContext { get; set; }
		static string YNP;
		static string NameP;
		static string RS;
		public AddCustomer(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;
        }

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(tbProjectCod.Text, out int nomer))
			{
				MessageBox.Show("Некорректный ввод!");
				return;
			}

			if (tbProjectName.Text == "")
			{
				MessageBox.Show("Заполните поле Наименование проектной организации");
				return;
			}

			if (tbProjectAdress.Text == "")
			{
				MessageBox.Show("Заполните поле Адрес проектной организации");
				return;
			}
			if (tbProjectYNP.Text == "")
			{
				MessageBox.Show("Заполните поле УНП");
				return;
			}
			if (tbProjectYNP.Text.Length != 9)
			{
				MessageBox.Show("Поле должно содержать 9 цифр!");
				return;
			}
			if (tbRS.Text == "")
			{
				MessageBox.Show("Заполните поле р/с");
				return;
			}
			if (tbRS.Text.Length != 13)
			{
				MessageBox.Show("Поле должно содержать 13 цифр");
				return;
			}
			var existedItem = SmetaContext.Проектная_организация
			   .Where(n => n.КодПроектировщика == nomer)
			   .FirstOrDefault();

			if (existedItem != null)
			{
				MessageBox.Show("Проектная организация с данным кодом уже существует!");
				return;
			}
			var existedYNP = SmetaContext.Проектная_организация
			   .Where(n => n.УНП == YNP)
			   .FirstOrDefault();
		  
			if (existedYNP != null)
			{
				MessageBox.Show("Проектная организация с данным УНП уже существует!");
				return;
			}
			var existedName = SmetaContext.Проектная_организация
			   .Where(n => n.НаименованиеПроектиров == NameP)
			   .FirstOrDefault();
			if (existedName != null)
			{
				MessageBox.Show("Проектная организация с данным названием уже существует!");
				return;
			}
			var existedRS = SmetaContext.Проектная_организация
			   .Where(n => n.Р_с == RS)
			   .FirstOrDefault();
			if (existedRS != null)
			{
				MessageBox.Show("Проектная организация с данным расчетным счетом уже существует!");
				return;
			}
			try
			{
				var addProjectCompany = new Проектная_организация()
				{
					КодПроектировщика = nomer,
					НаименованиеПроектиров = tbProjectName.Text,
					ЮрАдрес = tbProjectName.Text,
					Р_с = tbRS.Text,
					УНП = tbProjectYNP.Text,
					Тел = tbPhone.Text,
					ЭлПочта = tbMail.Text
				};

				SmetaContext.Проектная_организация.Add(addProjectCompany);
				SmetaContext.SaveChanges();
				MessageBox.Show("Проектная организация добавлена в базу");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "SQL Error");
			}

			Close();
        }

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
