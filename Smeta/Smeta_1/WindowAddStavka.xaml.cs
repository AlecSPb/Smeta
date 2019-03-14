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
		SmetaEntities context = new SmetaEntities();
		public AddStavka()
		{
			InitializeComponent();
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			int nomer;
			double stavka;
			DateTime dt;
			if (!Int32.TryParse(tbStavkaCode.Text, out nomer) & nomer <=0)
			{
				MessageBox.Show("В поле Код ставки введите положительное число");
			}
			else if (tbStavkaCode != null)
			{
				MessageBox.Show("Поле Код Ставки не может быть пустым");
			}
			else if (!double.TryParse(tbStavkaSize.Text, out stavka) & stavka <=0)
			{
				MessageBox.Show("В поле Значение ставки введите положительное число");
			}
			else if (tbStavkaName.Text.Length < 2 || tbStavkaName.Text.Length > 60)
			{
				MessageBox.Show("В поле наименование объекта введите от 2 до 60 символов A-Z");
			}
			else if (!DateTime.TryParse(tbStavkaDate.Text, out dt))
			{
				MessageBox.Show("В поле дата ставки введен неверный формат даты");
			}
			else
			{
				Ставка_14_го_разряда addStavka = new Ставка_14_го_разряда()
				{
					КодСтавки = Convert.ToInt32(tbStavkaCode.Text),
					Дата_ставки = Convert.ToDateTime(tbStavkaDate.Text),
					Значение_ставки = Convert.ToDouble(tbStavkaSize.Text),
					Обоснование = tbStavkaName.Text

				};


				context.Ставка_14_го_разряда.Add(addStavka);
				context.SaveChanges();
				MessageBox.Show("Ставка добавлена");
				Close();
			}

		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
