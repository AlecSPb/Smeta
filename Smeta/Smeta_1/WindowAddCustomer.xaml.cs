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

        public AddCustomer(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;
        }

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(tbProjectCod.Text, out int nomer))
			{
				tbProjectCod.Text = "Некорретный ввод!";
                return;
			}

   //         if (tbProjectName != null)
			//{
			//	tbProjectName.Text = "Поле не может быть пустым";
   //             return;
   //         }

   //         if (tbProjectAdress != null)
			//{
			//	tbProjectAdress.Text = "Поле не может быть пустым";
   //             return;
   //         }

            if (tbProjectYNP.Text.Length != 9)
			{
				tbProjectYNP.Text = "Поле должно содержать 9 цифр!";
                return;
            }

            if (tbRS.Text.Length != 13)
			{
				tbRS.Text = "Поле должно содержать 13 цифр";
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
