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
		static int categAddWorkType;
		SmetaEntities context = new SmetaEntities();
		public AddPrice()
		{
			InitializeComponent();
			cmbWorkType.Items.Clear();
			foreach (Справочник_видов_работ stw in GetAllTypeWork())
			{
				cmbWorkType.Items.Add(stw.ВидРабот);

			}
		}
		public IEnumerable<Справочник_видов_работ> GetAllTypeWork()
		{
			return context.Справочник_видов_работ.ToArray().Select((Справочник_видов_работ typew) =>
			{
				return new Справочник_видов_работ
				{
					КодВидаРабот = typew.КодВидаРабот,
					ВидРабот = typew.ВидРабот,
					ЕдИзмОб = typew.ЕдИзмОб,
					ЕдИзмТруд = typew.ЕдИзмТруд
				};
			});
		}
		private void cbSelectWorkTypeAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Справочник_видов_работ dir = cmbWorkType.SelectedItem as Справочник_видов_работ;

			dir = context.Справочник_видов_работ
				.Where(v => v.ВидРабот == cmbWorkType.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Справочник_видов_работ.Load();
			categAddWorkType = dir.КодВидаРабот;
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			
			int nomer1;
			double TrudObjem;
			
			
			//else if (tbTrudObjem < 0)
			//{
			//	tbTrudObjem.Text = "Некорретный ввод!";
			//}
			if (tbWorkCode != null)
			{
				MessageBox.Show("Поле Код работы не может быть пустым");
			}
			else if (!Int32.TryParse(tbWorkCode.Text, out nomer1) & nomer1<=0)
			{
				MessageBox.Show("В поле Код работы введите положительное число");
			}
			else if (!double.TryParse(tbTrudObjem.Text, out TrudObjem))
			{
				MessageBox.Show("В поле Затраты Труда за ед-ну объема введите число");
			}
			else if (TrudObjem <=0)
			{
				MessageBox.Show("В поле Затраты Труда за ед-цу объема введите положительно число");
			}
			else if (tbWorkName.Text.Length < 2 || tbWorkName.Text.Length > 50)
			{
				MessageBox.Show("Поле Наименование работы содержит от 2 до 50 символов A-Z");
			}
			else
			{
				Справочник_расценок addPrice = new Справочник_расценок()
				{
					КодВидаРабот = categAddWorkType,
					КодРаботы = Convert.ToInt32(tbWorkCode.Text),
					ЗатратыТрудаЕдОбъема = Convert.ToDouble(tbTrudObjem.Text),
					ИмяРаботы = tbWorkName.Text
					

				};


				context.Справочник_расценок.Add(addPrice);
				context.SaveChanges();
				MessageBox.Show("Расценка добавлена");
				Close();
			}

		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
