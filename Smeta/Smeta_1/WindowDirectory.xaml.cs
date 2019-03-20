using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Data;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for Справочник.xaml
    /// </summary>
    public partial class Directory : MetroWindow
	{
		private int cat;

        public int pk2ID { get; set; }
        public string oldNaz { get; set; }
        public double oldPrice { get; set; }
        public int pkIDStavka { get; set; }
        public string oldNazStavka { get; set; }
        public double oldPriceStavka { get; set; }
        public DateTime oldDateStavka { get; set; }

        public SmetaEntities SmetaContext { get; set; }

        public Directory(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

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

            foreach (var item in SmetaContext.Справочник_расценок.ToList())
            {
                dgDirectory.Items.Add(item);
            }

            foreach (var stw in SmetaContext.Справочник_видов_работ.ToList())
            {
                cbSelectTypeWork.Items.Add(stw);
            }

            foreach (var item in SmetaContext.Поправочный_коэффициент_по_типу_ПИР.ToList())
            {
                dgKof.Items.Add(item);
            }

            foreach (var item in SmetaContext.Ставка_14_го_разряда.ToList())
            {
                dgStavka.Items.Add(item);
            }
        }

		private void cbSelectTypeWork_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var catalWork = cbSelectTypeWork.SelectedItem as Справочник_видов_работ;

            cat = catalWork.КодВидаРабот;

            dgDirectory.Items.Clear();

            foreach (Справочник_расценок item in SmetaContext.Справочник_расценок.Where(n => n.КодВидаРабот == cat).ToList())
			{
				if (item.КодВидаРабот == cat)
				{
					dgDirectory.Items.Add(item);
				}
			}
		}

		private void dg1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            
		}

		private void dgKof_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            
        }

        private void dgStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
           
        }

		private void dg1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
		    var sd = dgKof.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;

			if (sd != null)
			{
				pk2ID = sd.Код_коэффициента;
				oldNaz = sd.Наименование_коэффициента;
				oldPrice = sd.Значение_коэффициента.Value;
				EditIndex b = new EditIndex(SmetaContext, this);
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

				EditStavka b = new EditStavka(SmetaContext, this);
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
