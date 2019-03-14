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
	/// Interaction logic for AddPrice.xaml
	/// </summary>
	/// 
	public partial class AddPrice : MetroWindow
	{
		SmetaEntities context = new SmetaEntities();
		public AddPrice()
		{
			InitializeComponent();
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			int nomer;
			int nomer1;
			double TrudObjem;
			if (!Int32.TryParse(tbWorkTypeCode.Text, out nomer))
			{
				tbWorkTypeCode.Text = "Некорретный ввод!";
			}
			else if (tbWorkTypeCode != null)
			{
				tbWorkTypeCode.Text = "Поле не может быть пустым";
			}
			//else if (tbWorkTypeCode.Text.Contains != 81)
			//	&& tbWorkTypeCode.Text != 82 && tbWorkTypeCode.Text != 83
			//tbWorkTypeCode.Text != 85 && tbWorkTypeCode.Text != 31 && tbWorkTypeCode.Text != 32 && tbWorkTypeCode.Text != 51)
			//{
			//	tbWorkTypeCode.Text = "Не существует такого вида работ";
			//}
			//else if (tbTrudObjem < 0)
			//{
			//	tbTrudObjem.Text = "Некорретный ввод!";
			//}
			else if (tbWorkCode != null)
			{
				tbWorkCode.Text = "Поле не может быть пустым";
			}
			else if (!Int32.TryParse(tbWorkCode.Text, out nomer1))
			{
				tbWorkCode.Text = "Некорретный ввод!";
			}
			else if (!double.TryParse(tbTrudObjem.Text, out TrudObjem))
			{
				tbTrudObjem.Text = "Некорретный ввод!";
			}
			else if (tbWorkName.Text.Length < 2 || tbWorkName.Text.Length > 50)
			{
				tbWorkName.Text = "Слишком короткий или длинный!";
			}
			else
			{
				Справочник_расценок addPrice = new Справочник_расценок()
				{
					КодВидаРабот = Convert.ToInt32(tbWorkTypeCode.Text),
					КодРаботы = Convert.ToInt32(tbWorkCode.Text),
					ЗатратыТрудаЕдОбъема = Convert.ToDouble(tbTrudObjem.Text),
					ИмяРаботы = tbWorkName.Text
					

				};


				context.Справочник_расценок.Add(addPrice);
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
