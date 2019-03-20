using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Data;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for AddPrice.xaml
    /// </summary>
    /// 
    public partial class AddPrice : MetroWindow
	{
        private int categAddWorkType;

        // У тебя данная переменная никогда нигде не присваивается. Следовательно проверка на
        //
        // var existedPriceName = SmetaContext.Справочник_расценок
		//      .Where(n => n.ИмяРаботы == PriceName)
		//      .FirstOrDefault();
        //
        // Работать не будет!!!

        private string PriceName;

        public SmetaEntities SmetaContext { get; set; }

        public AddPrice(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            foreach (var item in SmetaContext.Справочник_видов_работ.ToList())
            {
                cmbWorkType.Items.Add(item);
            }
        }

		private void cbSelectWorkTypeAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbWorkType.SelectedItem as Справочник_видов_работ;
			categAddWorkType = dir.КодВидаРабот;
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbWorkCode.Text))
			{
				MessageBox.Show("Заполните поле Код работы");
				return;
			}

			if (!int.TryParse(tbWorkCode.Text, out int nomer1) & nomer1 <= 0)
			{
				MessageBox.Show("В поле Код работы введите положительное число");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbTrudObjem.Text))
			{
				MessageBox.Show("Заполните поле Затраты Труда за ед-ну объема");
				return;
			}

			if (!double.TryParse(tbTrudObjem.Text, out double TrudObjem))
			{
				MessageBox.Show("В поле Затраты Труда за ед-ну объема введите число");
				return;
			}

			if (TrudObjem <= 0)
			{
				MessageBox.Show("В поле Затраты Труда за ед-цу объема введите положительно число");
				return;
			}

            if (string.IsNullOrWhiteSpace(tbWorkName.Text))
            {
                MessageBox.Show("Заполните поле Наименование работы");
                return;
            }

            if (tbWorkName.Text.Length < 2 || tbWorkName.Text.Length > 50)
			{
				MessageBox.Show("Поле Наименование работы содержит от 2 до 50 символов A-Z");
				return;
			}

			var existedItem = SmetaContext.Справочник_расценок
			   .Where(n => n.КодРаботы == nomer1)
			   .FirstOrDefault();

			if (existedItem != null)
			{
				MessageBox.Show("Расценка с данным кодом уже существует!");
				return;
			}

			var existedPriceName = SmetaContext.Справочник_расценок
			   .Where(n => n.ИмяРаботы == PriceName)
			   .FirstOrDefault();

			if (existedPriceName != null)
			{
				MessageBox.Show("Расценка с данным названием уже существует!");
				return;
			}

			try
			{
				var addPrice = new Справочник_расценок()
				{
					КодВидаРабот = categAddWorkType,
					КодРаботы = nomer1,
					ЗатратыТрудаЕдОбъема = TrudObjem,
					ИмяРаботы = tbWorkName.Text
				};

				SmetaContext.Справочник_расценок.Add(addPrice);
				SmetaContext.SaveChanges();
				MessageBox.Show("Расценка добавлена");
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