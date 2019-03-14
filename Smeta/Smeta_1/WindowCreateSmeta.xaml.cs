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
	/// Interaction logic for WindowCreateSmeta.xaml
	/// </summary>
	public partial class CreateSmeta : MetroWindow
	{
		SmetaEntities context = new SmetaEntities();
		public CreateSmeta()
		{
			InitializeComponent();
			cmbCustomers.Items.Clear();
			cmbObjectProject.Items.Clear();
			cmbProject.Items.Clear();
			cmbStavka.Items.Clear();

			foreach (Заказчик stw in GetAllCustomer())
			{
				cmbCustomers.Items.Add(stw.НаименованиеЗаказчика);

			}
			foreach (Проектная_организация stw in GetAllGen())
			{
				cmbProject.Items.Add(stw.НаименованиеПроектиров);

			}
			foreach (Поправочный_коэффициент_по_типу_ПИР stw in GetAllKoef())
			{
				cmbObjectProject.Items.Add(stw.Наименование_коэффициента);

			}
			foreach (Ставка_14_го_разряда stw in GetAllStavka())
			{
				cmbStavka.Items.Add(stw.Обоснование);

			}
		}
		public IEnumerable<Заказчик> GetAllCustomer()
		{
			return context.Заказчик.ToArray().Select((Заказчик cust) =>
			{
				return new Заказчик
				{
					КодЗаказчик = cust.КодЗаказчик,
					НаименованиеЗаказчика = cust.НаименованиеЗаказчика,
					Расчетный_счет = cust.Расчетный_счет,
					Тел = cust.Тел,
					УНП = cust.УНП,
					ЭлПочта = cust.ЭлПочта,
					ЮрАдрес = cust.ЮрАдрес,

				};
			});
		}
		public IEnumerable<Проектная_организация> GetAllGen()
		{
			return context.Проектная_организация.ToArray().Select((Проектная_организация pro) =>
			{
				return new Проектная_организация
				{
					КодПроектировщика = pro.КодПроектировщика,
					НаименованиеПроектиров = pro.НаименованиеПроектиров,
					Р_с = pro.Р_с,
					Тел = pro.Тел,
					УНП = pro.УНП,
					ЭлПочта = pro.ЭлПочта,
					ЮрАдрес = pro.ЮрАдрес,

				};
			});
		}
		public IEnumerable<Поправочный_коэффициент_по_типу_ПИР> GetAllKoef()
		{
			return context.Поправочный_коэффициент_по_типу_ПИР.ToArray().Select((Поправочный_коэффициент_по_типу_ПИР kof) =>
			{
				return new Поправочный_коэффициент_по_типу_ПИР
				{
					Наименование_коэффициента = kof.Наименование_коэффициента,
					Код_коэффициента = kof.Код_коэффициента,
					Значение_коэффициента = kof.Значение_коэффициента
				};
			});
		}
		public IEnumerable<Ставка_14_го_разряда> GetAllStavka()
		{
			return context.Ставка_14_го_разряда.ToArray().Select((Ставка_14_го_разряда stavka) =>
			{
				return new Ставка_14_го_разряда
				{
					Дата_ставки = stavka.Дата_ставки,
					Значение_ставки = stavka.Значение_ставки,
					Обоснование = stavka.Обоснование,
					КодСтавки = stavka.КодСтавки
				};
			});
		}
		private void cbSelectCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Заказчик cust = cmbCustomers.SelectedItem as Заказчик;

			cust = context.Заказчик
				.Where(v => v.НаименованиеЗаказчика == cmbCustomers.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Заказчик.Load();
		}
		private void cbSelectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Проектная_организация pro = cmbProject.SelectedItem as Проектная_организация;

			pro = context.Проектная_организация
				.Where(v => v.НаименованиеПроектиров == cmbProject.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Проектная_организация.Load();
		}

		private void CmbObjectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Поправочный_коэффициент_по_типу_ПИР kof = cmbObjectProject.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;

			kof = context.Поправочный_коэффициент_по_типу_ПИР
				.Where(v => v.Наименование_коэффициента == cmbObjectProject.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Поправочный_коэффициент_по_типу_ПИР.Load();
		}

		private void CmbStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Ставка_14_го_разряда stv = cmbStavka.SelectedItem as Ставка_14_го_разряда;

			stv = context.Ставка_14_го_разряда
				.Where(v => v.Обоснование == cmbStavka.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Ставка_14_го_разряда.Load();
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
