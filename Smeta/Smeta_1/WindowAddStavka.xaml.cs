using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using System.Data;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for AddStavka.xaml
    /// </summary>
    public partial class AddStavka : MetroWindow
	{
        // У тебя данная переменная никогда нигде не присваивается. Следовательно проверка на
        //
        // var existedDate = SmetaContext.Ставка_14_го_разряда
        //    .Where(n => n.Дата_ставки == stDate)
        //    .FirstOrDefault();
        //
        // Работать не будет!!!
        private DateTime stDate;

        public SmetaEntities SmetaContext { get; set; }

        public AddStavka(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(tbStavkaCode.Text, out int nomer) & nomer <= 0)
			{
				MessageBox.Show("В поле Код ставки введите положительное число");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbStavkaSize.Text))
			{
				MessageBox.Show("Заполните поле Значение ставки");
				return;
			}

			if (!double.TryParse(tbStavkaSize.Text, out double stavka) & stavka <= 0)
			{
				MessageBox.Show("В поле Значение ставки введите положительное число");
				return;
			}

            if (string.IsNullOrWhiteSpace(tbStavkaName.Text))
            {
                MessageBox.Show("Заполните поле обоснование");
                return;
            }

            if (tbStavkaName.Text.Length < 2 || tbStavkaName.Text.Length > 60)
			{
				MessageBox.Show("В поле обоснование введите от 2 до 60 символов A-Z");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbStavkaDate.Text))
			{
				MessageBox.Show("Заполните поле дата ставки");
				return;
			}

			if (!DateTime.TryParse(tbStavkaDate.Text, out DateTime dateTime))
			{
				MessageBox.Show("В поле дата ставки введен неверный формат даты");
				return;
			}

			var existedItem = SmetaContext.Ставка_14_го_разряда
                .Where(n => n.КодСтавки == nomer)
                .FirstOrDefault();

            if(existedItem != null)
            {
                MessageBox.Show("Ставка с данным кодом уже существует!");
                return;
            }

			var existedDate = SmetaContext.Ставка_14_го_разряда
				.Where(n => n.Дата_ставки == stDate)
				.FirstOrDefault();

			if (existedDate != null)
			{
				MessageBox.Show("Ставка с данной датой уже существует!");
                return;
			}

			try
            {
                var addStavka = new Ставка_14_го_разряда()
                {
                    КодСтавки = nomer,
                    Дата_ставки = dateTime,
                    Значение_ставки = stavka,
                    Обоснование = tbStavkaName.Text
                };

                SmetaContext.Ставка_14_го_разряда.Add(addStavka);
                SmetaContext.SaveChanges();
                MessageBox.Show("Ставка добавлена");
            }
            catch(Exception ex)
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
