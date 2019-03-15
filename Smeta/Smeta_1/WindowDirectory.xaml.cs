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
	/// Interaction logic for Справочник.xaml
	/// </summary>
	public partial class Directory : MetroWindow
	{
		static int cat;
		SmetaEntities1 context = new SmetaEntities1();
		public Directory()
		{
			InitializeComponent();
			cbSelectTypeWork.Items.Clear();
			dgDirectory.Items.Clear();
			dgKof.Items.Clear();
			dgStavka.Items.Clear();
			if (MainWindow.sRole == "admin")
			{
				add_index.IsEnabled = true;
				add_price.IsEnabled = true;
				add_stavka.IsEnabled = true;
				menuEdit.IsEnabled = true;
				

			}
			if (MainWindow.sRole == "user")
			{
				add_index.IsEnabled = false;
				add_price.IsEnabled = false;
				add_stavka.IsEnabled = false;
				menuEdit.IsEnabled = false;

			}

			foreach (Справочник_расценок item in GetAllPrices())
			{
				dgDirectory.Items.Add(item);
			}
			foreach (Справочник_видов_работ stw in GetAllTypeWork())
			{
				cbSelectTypeWork.Items.Add(stw.ВидРабот);

			}
			foreach (Поправочный_коэффициент_по_типу_ПИР item in GetAllKoef())
			{
				dgKof.Items.Add(item);
			}
			foreach (Ставка_14_го_разряда item in GetAllStavka())
			{
				dgStavka.Items.Add(item);
			}
		}
		public IEnumerable<Справочник_расценок> GetAllPrices()
		{
			return context.Справочник_расценок.ToArray().Select((Справочник_расценок catw) =>
			{
				return new Справочник_расценок
				{
					КодРаботы = catw.КодРаботы,
					ИмяРаботы = catw.ИмяРаботы,
					ЗатратыТрудаЕдОбъема = catw.ЗатратыТрудаЕдОбъема,
					КодВидаРабот = catw.КодВидаРабот
				};
			});
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
		private void cbSelectTypeWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Справочник_видов_работ catalWork = cbSelectTypeWork.SelectedItem as Справочник_видов_работ;

			catalWork = context.Справочник_видов_работ
				.Where(v => v.ВидРабот == cbSelectTypeWork.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Справочник_видов_работ.Load();

			dgDirectory.Items.Clear();
			cat = catalWork.КодВидаРабот;
			foreach (Справочник_расценок item in GetAllPrices())
			{
				if (item.КодВидаРабот == cat)
				{
					dgDirectory.Items.Add(item);
				}

			}


		}
		private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Справочник_расценок catalogPrice = dgDirectory.SelectedItem as Справочник_расценок;
			if (catalogPrice != null)
			{
				catalogPrice = context.Справочник_расценок
					.Where(v => v.КодРаботы == catalogPrice.КодРаботы)
					.AsEnumerable()
					.FirstOrDefault();

				context.Справочник_расценок.Load();
			}
		}
		private void dgKof_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Поправочный_коэффициент_по_типу_ПИР kof = dgKof.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;
			if (kof != null)
			{
				kof = context.Поправочный_коэффициент_по_типу_ПИР
					.Where(k=> k.Код_коэффициента == kof.Код_коэффициента)
					.AsEnumerable()
					.FirstOrDefault();


				context.Поправочный_коэффициент_по_типу_ПИР.Load();

			}
			
		}
		private void dgStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Ставка_14_го_разряда stavka = dgStavka.SelectedItem as Ставка_14_го_разряда;
			if (stavka != null)
			{
				stavka = context.Ставка_14_го_разряда
					.Where(s => s.КодСтавки == stavka.КодСтавки)
					.AsEnumerable()
					.FirstOrDefault();


				context.Ставка_14_го_разряда.Load();

			}

		}
		public static int pk2ID;// вторичный ключ
		public static string oldNaz;
		public static double oldPrice;
		public static int pkIDStavka;
		public static string oldNazStavka;
		public static double oldPriceStavka;
		public static DateTime oldDateStavka;

		private void dg1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			 
		    Поправочный_коэффициент_по_типу_ПИР sd = dgKof.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;
			if (sd != null)
			{
				
				pk2ID = sd.Код_коэффициента;
				oldNaz = sd.Наименование_коэффициента;
				oldPrice = Convert.ToDouble(sd.Значение_коэффициента);
				EditIndex b = new EditIndex();
				b.Owner = this;
				b.ShowDialog();
				
			}
		}
		private void dg2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{

			Ставка_14_го_разряда sd = dgStavka.SelectedItem as Ставка_14_го_разряда;
			if (sd != null)
			{

				pkIDStavka = sd.КодСтавки;
				oldNazStavka = sd.Обоснование;
				oldPriceStavka = Convert.ToDouble(sd.Значение_ставки);
				oldDateStavka = Convert.ToDateTime(sd.Дата_ставки);
				EditStavka b = new EditStavka();
				b.Owner = this;
				b.ShowDialog();

			}
		}
		private void MenuItem_Click_OpenChart(object sender, RoutedEventArgs e)
		{
			Chart wa = new Chart();
			wa.Owner = this;
			wa.ShowDialog();

		}
		private void MenuItem_Click_OpenAbout(object sender, RoutedEventArgs e)
		{
			About wa = new About();
			wa.Owner = this;
			wa.ShowDialog();

		}
		private void MenuItem_Click_AddPrice(object sender, RoutedEventArgs e)
		{
			AddPrice wa = new AddPrice();
			wa.Owner = this;
			wa.ShowDialog();

		}
		private void MenuItem_Click_AddIndex(object sender, RoutedEventArgs e)
		{
			AddIndex wa = new AddIndex();
			wa.Owner = this;
			wa.ShowDialog();

		}
		private void MenuItem_Click_AddStavka(object sender, RoutedEventArgs e)
		{
			AddStavka wa = new AddStavka();
			wa.Owner = this;
			wa.ShowDialog();

		}
	}
}
