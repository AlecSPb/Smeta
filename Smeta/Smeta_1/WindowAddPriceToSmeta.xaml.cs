using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Smeta_DB;
using System;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowAddPriceToSmeta.xaml
    /// </summary>
    public partial class AddPriceToSmeta : MetroWindow
	{
        private int categSelectShifr;
        private int categAddWorkType;
        private int categAddWorkCode;
        private int categSelectObjectTypeCode;
        private int categSelectStavkaCode;

		public SmetaEntities SmetaContext { get; set; }

		public AddPriceToSmeta(SmetaEntities context)
		{
			InitializeComponent();

			SmetaContext = context;

			foreach (Объект item in SmetaContext.Объект.ToList())
			{
				cmbObjectName.Items.Add(item);
			}

			foreach (Справочник_расценок item in SmetaContext.Справочник_расценок.ToList())
			{
				cmbWorkName.Items.Add(item);
			}
		}

		private void CmbObjectName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbObjectName.SelectedItem as Объект;
			
			categSelectShifr = dir.Шифр;
			categSelectObjectTypeCode = dir.Код_коэффициента;
			categSelectStavkaCode = dir.КодСтавки;
		}
		
		private void CmbWorkName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbWorkName.SelectedItem as Справочник_расценок;

			categAddWorkCode = dir.КодРаботы;
			categAddWorkType = dir.КодВидаРабот;
		}
		
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txObjem.Text))
			{
				MessageBox.Show("Заполните поле Объем");
				return;
			}

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

            var addPriceToSmeta = new Локальная_смета()
            {
                КодВидаРабот = categAddWorkType,
                КодРаботы = categAddWorkCode,
                Шифр = categSelectShifr,
                Код_коэффициента = categSelectObjectTypeCode,
                КодСтавки = categSelectStavkaCode,
                ФизОбъемРабот = TrudObjem
            };

            try
            {
                SmetaContext.Локальная_смета.Add(addPriceToSmeta);
                SmetaContext.SaveChanges();
                MessageBox.Show("Расценка добавлена в смету");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SQL Error");
                return;
            }

            Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}