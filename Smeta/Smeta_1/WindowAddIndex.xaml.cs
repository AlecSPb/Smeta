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
		SmetaEntities1 context = new SmetaEntities1();
		public AddIndex()
		{
			InitializeComponent();
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			int nomer;
			double kof;
			string code, name, size;
			code = tbKofCode.Text;
			name = tbKofName.Text;
			size = tbKofSize.Text;


			//if (prowSuchs(code, name, size, Context.Поправочный_коэффициент_по_типу_ПИР) == true)
			//{
			//	MessageBox.Show("Такой коэффициет уже имеется в базе");
			//}
			//else

			if (!Int32.TryParse(tbKofCode.Text, out nomer) & nomer <=0)
			{
				MessageBox.Show("В поле Код коэффициента введите цифры больше 0");
			}
			else if (tbKofCode != null)
			{
				MessageBox.Show("Поле Код коэффициента не может быть пустым");
			}
			else if (!double.TryParse(tbKofSize.Text, out kof) & kof <=0)
			{
				MessageBox.Show("В поле Значение введите положительное число");
			}
			else if (tbKofName.Text.Length < 2 || tbKofName.Text.Length > 60)
			{
				MessageBox.Show("Поле Наименование коэффициента должно содеражать от 2 до 60 символов A-Z");
			}
			else
			{
				Поправочный_коэффициент_по_типу_ПИР addIndex = new Поправочный_коэффициент_по_типу_ПИР()
				{
					Код_коэффициента = Convert.ToInt32(tbKofCode.Text),
					Наименование_коэффициента = tbKofName.Text,
					Значение_коэффициента = Convert.ToDouble(tbKofSize.Text)
			
				};


				context.Поправочный_коэффициент_по_типу_ПИР.Add(addIndex);
				context.SaveChanges();
				MessageBox.Show("Коэффициент добавлен в базу");
				Close();
			}

		}


		//private bool prowSuchs(string code, string name, string size, DataTable Поправочный_коэффициент_по_типу_ПИР)
		//{

		//	var query = from order in Поправочный_коэффициент_по_типу_ПИР.AsEnumerable()
		//				where order.Field<string>("Код_коэффициента") == code & order.Field<string>("Наименование_коэффициента") == name & order.Field<string>("Значение_коэффициента") == size
		//				select new
		//				{
		//					ID = order.Field<string>("Код_коэффициента")
		//				};
		//	if (query.Count() == 0)
		//	{
		//		return false;
		//	}
		//	else
		//	{
		//		return true;
		//	}



		//}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
