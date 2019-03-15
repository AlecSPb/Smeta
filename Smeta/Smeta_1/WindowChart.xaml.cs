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

using System.Windows.Controls.DataVisualization.Charting;
using Smeta_DB;

namespace Smeta_1
{
	/// <summary>
	/// Interaction logic for WindowChart.xaml
	/// </summary>
	public partial class Chart : MetroWindow
	{
		SmetaEntities1 context = new SmetaEntities1();
		
		public Chart()
		{
			InitializeComponent();
			ReDrow();
		}
		public IEnumerable<Объект> GetAllObject()
		{
			return context.Объект.ToArray().Select((Объект ob) =>
			{
				return new Объект
				{
					Шифр = ob.Шифр,
					Адрес = ob.Адрес,
					НаименованиеОбъекта = ob.НаименованиеОбъекта,
					КодПроектировщика = ob.КодПроектировщика,
					Код_коэффициента = ob.Код_коэффициента,
					КодЗаказчик = ob.КодЗаказчик,
					КодСтавки = ob.КодСтавки
				};
			});

		}
		public void ReDrow()
		{
			listBox2.Items.Clear();
			foreach (Объект obj in GetAllObject())
			{
				listBox2.Items.Add(obj);
			}
		}
		private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Объект obj = listBox2.SelectedItem as Объект;
			if (obj != null)
			{

				labelName.Content = obj.НаименованиеОбъекта;
				var Cost1 = context.Локальная_смета
					.Where(c => c.Шифр == obj.Шифр)
					.Select(c => c.СтоимостьРаботы)
					.Sum();

				var Labor1 = context.Локальная_смета
					.Where(c => c.Шифр == obj.Шифр)
					.Select(c => c.ТрудоемкостьРаботы)
					.Sum();
				((PieSeries)mcChart.Series[0]).ItemsSource =
					new KeyValuePair<string, double>[]
					{
					new KeyValuePair<string, double>("Стоимость работ", Convert.ToDouble(Cost1)),
					new KeyValuePair<string, double>("Трудоемкость работ", Convert.ToDouble(Labor1))

					};
			
			}
		}
		
			
	}
		
	
}