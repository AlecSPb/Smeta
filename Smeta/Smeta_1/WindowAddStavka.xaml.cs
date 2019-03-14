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
			if (!Int32.TryParse(tbStavkaCode.Text, out nomer))
			{
				tbStavkaCode.Text = "Некорретный ввод!";
			}
			else if (tbStavkaCode != null)
			{
				tbStavkaCode.Text = "Поле не может быть пустым";
			}
			else if (!double.TryParse(tbStavkaSize.Text, out stavka))
			{
				tbStavkaSize.Text = "Некорретный ввод!";
			}
			else if (tbStavkaName.Text.Length < 2 || tbStavkaName.Text.Length > 60)
			{
				tbStavkaName.Text = "Слишком короткий или длинный!";
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
				Close();
			}

		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
