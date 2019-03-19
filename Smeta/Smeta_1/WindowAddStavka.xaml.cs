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
	/// Interaction logic for AddStavka.xaml
	/// </summary>
	public partial class AddStavka : MetroWindow
	{
        public SmetaEntities SmetaContext { get; set; }
		static DateTime stDate;
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
			if (tbStavkaSize.Text == "")
			{
				MessageBox.Show("Заполните поле Значение ставки");
				return;
			}
			if (!double.TryParse(tbStavkaSize.Text, out double stavka) & stavka <= 0)
			{
				MessageBox.Show("В поле Значение ставки введите положительное число");
				return;
			}

			if (tbStavkaName.Text.Length < 2 || tbStavkaName.Text.Length > 60)
			{
				MessageBox.Show("В поле обоснование введите от 2 до 60 символов A-Z");
				return;
			}
			if (tbStavkaName.Text == " ")
			{
				MessageBox.Show("Заполните поле обоснование");
				return;
			}
			if (tbStavkaDate.Text == "")
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
            }
			var existedDate = SmetaContext.Ставка_14_го_разряда
				.Where(n => n.Дата_ставки == stDate)
				.FirstOrDefault();

			if (existedDate != null)
			{
				MessageBox.Show("Ставка с данной датой уже существует!");
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
