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

        public SmetaEntities SmetaContext { get; set; }

        public Directory(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

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

            foreach (Справочник_расценок item in SmetaContext.Справочник_расценок.ToList())
            {
                dgDirectory.Items.Add(item);
            }

            foreach (Справочник_видов_работ stw in SmetaContext.Справочник_видов_работ.ToList())
            {
                cbSelectTypeWork.Items.Add(stw.ВидРабот);
            }

            foreach (Поправочный_коэффициент_по_типу_ПИР item in SmetaContext.Поправочный_коэффициент_по_типу_ПИР.ToList())
            {
                dgKof.Items.Add(item);
            }

            foreach (Ставка_14_го_разряда item in SmetaContext.Ставка_14_го_разряда.ToList())
            {
                dgStavka.Items.Add(item);
            }
        }

		private void cbSelectTypeWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var catalWork = cbSelectTypeWork.SelectedItem as Справочник_видов_работ;

			SmetaContext.Справочник_видов_работ.Load();

			dgDirectory.Items.Clear();
			cat = catalWork.КодВидаРабот;

			foreach (Справочник_расценок item in SmetaContext.Справочник_расценок.ToList())
			{
				if (item.КодВидаРабот == cat)
				{
					dgDirectory.Items.Add(item);
				}
			}
		}

		private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            // Этот код ничего не делает. Удалить!

			//Справочник_расценок catalogPrice = dgDirectory.SelectedItem as Справочник_расценок;

            //if (catalogPrice != null)
			//{
			//	catalogPrice = SmetaContext.Справочник_расценок
			//		.Where(v => v.КодРаботы == catalogPrice.КодРаботы)
			//		.AsEnumerable()
			//		.FirstOrDefault();

			//	SmetaContext.Справочник_расценок.Load();
			//}
		}
		private void dgKof_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            // Этот код ничего не делает. Удалить!

            //Поправочный_коэффициент_по_типу_ПИР kof = dgKof.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;
            //if (kof != null)
            //{
            //	kof = SmetaContext.Поправочный_коэффициент_по_типу_ПИР
            //		.Where(k=> k.Код_коэффициента == kof.Код_коэффициента)
            //		.AsEnumerable()
            //		.FirstOrDefault();


            //	SmetaContext.Поправочный_коэффициент_по_типу_ПИР.Load();

            //}
        }
        private void dgStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            // Этот код ничего не делает. Удалить!

            //Ставка_14_го_разряда stavka = dgStavka.SelectedItem as Ставка_14_го_разряда;
            //if (stavka != null)
            //{
            //	stavka = SmetaContext.Ставка_14_го_разряда
            //		.Where(s => s.КодСтавки == stavka.КодСтавки)
            //		.AsEnumerable()
            //		.FirstOrDefault();


            //	SmetaContext.Ставка_14_го_разряда.Load();

            //}
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
		    var sd = dgKof.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;

			if (sd != null)
			{
				pk2ID = sd.Код_коэффициента;
				oldNaz = sd.Наименование_коэффициента;
				oldPrice = sd.Значение_коэффициента.Value;
				EditIndex b = new EditIndex(SmetaContext);
				b.Owner = this;
				b.ShowDialog();
			}
		}

		private void dg2_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			var sd = dgStavka.SelectedItem as Ставка_14_го_разряда;

			if (sd != null)
			{
				pkIDStavka = sd.КодСтавки;
				oldNazStavka = sd.Обоснование;
				oldPriceStavka = sd.Значение_ставки;
				oldDateStavka = sd.Дата_ставки;

				EditStavka b = new EditStavka(SmetaContext);
				b.Owner = this;
				b.ShowDialog();
			}
		}

		private void MenuItem_Click_OpenChart(object sender, RoutedEventArgs e)
		{
			Chart wa = new Chart(SmetaContext);
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
			AddPrice wa = new AddPrice(SmetaContext);
			wa.Owner = this;
			wa.ShowDialog();
		}

		private void MenuItem_Click_AddIndex(object sender, RoutedEventArgs e)
		{
			AddIndex wa = new AddIndex(SmetaContext);
			wa.Owner = this;
			wa.ShowDialog();
		}

		private void MenuItem_Click_AddStavka(object sender, RoutedEventArgs e)
		{
			AddStavka wa = new AddStavka(SmetaContext);
			wa.Owner = this;
			wa.ShowDialog();
		}
	}
}
