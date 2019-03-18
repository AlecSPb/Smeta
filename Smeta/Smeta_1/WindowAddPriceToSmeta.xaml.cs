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
	/// Interaction logic for WindowAddPriceToSmeta.xaml
	/// </summary>
	public partial class AddPriceToSmeta : MetroWindow
	{
		static int categSelectShifr;
		static int categAddWorkType;
		static int categAddWorkCode;
		static int categSelectObjectTypeCode;
		static int categSelectStavkaCode;
		public SmetaEntities SmetaContext { get; set; }
		public AddPriceToSmeta(SmetaEntities context)
		{
			
			InitializeComponent();
			SmetaContext = context;
			cmbObjectName.Items.Clear();
			cmbWorkName.Items.Clear();
			foreach (Объект stw in SmetaContext.Объект.ToList())
			{
				cmbObjectName.Items.Add(stw.НаименованиеОбъекта);
			}
			foreach (Справочник_расценок stw in SmetaContext.Справочник_расценок.ToList())
			{
				cmbWorkName.Items.Add(stw.ИмяРаботы);
			}
			
		}
		private void CmbObjectName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbObjectName.SelectedItem as Объект;
			dir = SmetaContext.Объект
				.Where(v => v.НаименованиеОбъекта == cmbObjectName.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			SmetaContext.Объект.Load();
			categSelectShifr = dir.Шифр;
			categSelectObjectTypeCode = dir.Код_коэффициента;
			categSelectStavkaCode = dir.КодСтавки;
			

		}
		
		private void CmbWorkName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbWorkName.SelectedItem as Справочник_расценок;
			dir = SmetaContext.Справочник_расценок
				.Where(v => v.ИмяРаботы == cmbWorkName.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			SmetaContext.Справочник_расценок.Load();
			categAddWorkCode = dir.КодРаботы;
			categAddWorkType = dir.КодВидаРабот;
		}
		
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{

			if (!double.TryParse(txObjem.Text, out double TrudObjem))
			{
				MessageBox.Show("В поле Объем введите число");
				return;
			}

			if (TrudObjem <= 0)
			{
				MessageBox.Show("В поле Объем введите положительно число");
				return;
			}

			else
			{
				var addPriceToSmeta = new Локальная_смета()
				{
					КодВидаРабот = categAddWorkType,
					КодРаботы = categAddWorkCode,
					Шифр = categSelectShifr,
					Код_коэффициента = categSelectObjectTypeCode,
					КодСтавки = categSelectStavkaCode,
					ФизОбъемРабот = TrudObjem

				};

				SmetaContext.Локальная_смета.Add(addPriceToSmeta);
				SmetaContext.SaveChanges();
				MessageBox.Show("Расценка добавлена в смету");
			}

			Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
