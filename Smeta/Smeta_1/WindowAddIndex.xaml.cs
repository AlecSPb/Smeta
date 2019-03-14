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
	/// Interaction logic for AddIndex.xaml
	/// </summary>
	public partial class AddIndex : MetroWindow
	{
		SmetaEntities context = new SmetaEntities();
		public AddIndex()
		{
			InitializeComponent();
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			int nomer;
			double kof;
			if (!Int32.TryParse(tbKofCode.Text, out nomer))
			{
				tbKofCode.Text = "Некорретный ввод!";
			}
			else if (tbKofCode != null)
			{
				tbKofCode.Text = "Поле не может быть пустым";
			}
			else if (!double.TryParse(tbKofSize.Text, out kof))
			{
				tbKofSize.Text = "Некорретный ввод!";
			}
			else if (tbKofName.Text.Length < 2 || tbKofName.Text.Length > 60)
			{
				tbKofName.Text = "Слишком короткий или длинный!";
			}
			else
			{
				Поправочный_коэффициент_по_типу_ПИР addIndex = new Поправочный_коэффициент_по_типу_ПИР()
				{
					Код_коэффициента = Convert.ToInt32(tbKofCode.Text),
					Наименование_коэффициента = tbKofName.Text,
					Значение_коэффициента = Convert.ToDouble(tbKofName.Text)
			
				};


				context.Поправочный_коэффициент_по_типу_ПИР.Add(addIndex);
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
