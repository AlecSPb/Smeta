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
	/// Interaction logic for AddPrice.xaml
	/// </summary>
	/// 
	public partial class AddPrice : MetroWindow
	{
		static int categAddWorkType;

        public SmetaEntities SmetaContext { get; set; }

        public AddPrice(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            cmbWorkType.Items.Clear();

            foreach (Справочник_видов_работ stw in SmetaContext.Справочник_видов_работ.ToList())
            {
                cmbWorkType.Items.Add(stw.ВидРабот);
            }
        }

		private void cbSelectWorkTypeAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbWorkType.SelectedItem as Справочник_видов_работ;
			categAddWorkType = dir.КодВидаРабот;
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			//else if (tbTrudObjem < 0)
			//{
			//	tbTrudObjem.Text = "Некорретный ввод!";
			//}
			if (tbWorkCode != null)
			{
				MessageBox.Show("Поле Код работы не может быть пустым");
                return;
			}

            if (!int.TryParse(tbWorkCode.Text, out int nomer1) & nomer1<=0)
			{
				MessageBox.Show("В поле Код работы введите положительное число");
                return;
            }

            if (!double.TryParse(tbTrudObjem.Text, out double TrudObjem))
			{
				MessageBox.Show("В поле Затраты Труда за ед-ну объема введите число");
                return;
            }

            if (TrudObjem <=0)
			{
				MessageBox.Show("В поле Затраты Труда за ед-цу объема введите положительно число");
                return;
            }

            if (tbWorkName.Text.Length < 2 || tbWorkName.Text.Length > 50)
			{
				MessageBox.Show("Поле Наименование работы содержит от 2 до 50 символов A-Z");
                return;
            }

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
            Close();
        }

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}