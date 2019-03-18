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
	/// Interaction logic for WindowCreateSmeta.xaml
	/// </summary>
	public partial class CreateSmeta : MetroWindow
	{
		static int categAddCust;
		static int categAddProject;
		static int categAddKof;
		static int categAddStavka;
		static int categAddWorkTypeCode;
		static int categAddWorkCode;

        public SmetaEntities SmetaContext { get; set; }

        public CreateSmeta(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            cmbCustomers.Items.Clear();
            cmbObjectProject.Items.Clear();
            cmbProject.Items.Clear();
            cmbStavka.Items.Clear();
            cmbWorkName.Items.Clear();
            //dgDirectory_1.Items.Clear();
            cmbWorkType.Items.Clear();

            foreach (var stw in SmetaContext.Заказчик.ToList())
            {
                cmbCustomers.Items.Add(stw.НаименованиеЗаказчика);
            }

            foreach (var stw in SmetaContext.Проектная_организация.ToList())
            {
                cmbProject.Items.Add(stw.НаименованиеПроектиров);
            }

            foreach (var stw in SmetaContext.Поправочный_коэффициент_по_типу_ПИР.ToList())
            {
                cmbObjectProject.Items.Add(stw.Наименование_коэффициента);
            }

            foreach (var stw in SmetaContext.Ставка_14_го_разряда.ToList())
            {
                cmbStavka.Items.Add(stw.Обоснование);
            }

            foreach (var stw in SmetaContext.Справочник_видов_работ.ToList())
            {
                cmbWorkType.Items.Add(stw.ВидРабот);
            }

            foreach (var stw in SmetaContext.Справочник_расценок.ToList())
            {
                cmbWorkName.Items.Add(stw.ИмяРаботы);
                //dgDirectory_1.Items.Add(stw);
            }
        }

		private void cbSelectCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var cust = cmbCustomers.SelectedItem as Заказчик;
			cust = SmetaContext.Заказчик
				.Where(v => v.НаименованиеЗаказчика == cmbCustomers.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			SmetaContext.Заказчик.Load();
			categAddCust = cust.КодЗаказчик;
		}

		private void cbSelectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var pro = cmbProject.SelectedItem as Проектная_организация;
			pro = SmetaContext.Проектная_организация
				.Where(v => v.НаименованиеПроектиров == cmbProject.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			SmetaContext.Проектная_организация.Load();
			categAddProject = pro.КодПроектировщика;
		}

		private void CmbObjectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var kof = cmbObjectProject.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;
			kof = SmetaContext.Поправочный_коэффициент_по_типу_ПИР
				.Where(v => v.Наименование_коэффициента == cmbObjectProject.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			SmetaContext.Поправочный_коэффициент_по_типу_ПИР.Load();
			categAddKof = kof.Код_коэффициента;
		}

		private void CmbStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var stv = cmbStavka.SelectedItem as Ставка_14_го_разряда;
			stv = SmetaContext.Ставка_14_го_разряда
				.Where(v => v.Обоснование == cmbStavka.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			SmetaContext.Ставка_14_го_разряда.Load();

			categAddStavka = stv.КодСтавки;
		}

		private void cbSelectWorkType_SelectionChanged (object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbWorkType.SelectedItem as Справочник_видов_работ;
			dir = SmetaContext.Справочник_видов_работ
				.Where(v => v.ВидРабот == cmbWorkType.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();
			SmetaContext.Справочник_видов_работ.Load();
			categAddWorkTypeCode = dir.КодВидаРабот;
			//dgDirectory_1.Items.Clear();
			//cat = dir.КодВидаРабот;
			//foreach (Справочник_расценок item in GetAllPrices())
			//{
			//	if (item.КодВидаРабот == cat)
			//	{
			//		dgDirectory_1.Items.Add(item);
			//	}
			//}
		}

		private void CmbWorkName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var pr = cmbWorkName.SelectedItem as Справочник_расценок;
			pr = SmetaContext.Справочник_расценок
				.Where(v => v.ИмяРаботы == cmbWorkName.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();
			SmetaContext.Справочник_расценок.Load();
			categAddWorkCode = pr.КодРаботы;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			//if (cbSelectCustomer.Text.Length == 0 || cbSelectProject.Text.Length == 0 || CmbObjectProject.Text.Length ==0 || CmbStavka.Text.Length == 0)
			//{
			//	MessageBox.Show("Выберите категорию!");
				
			//}
			if (txObjectName.Text.Length < 2 || txObjectName.Text.Length > 60)
			{
				MessageBox.Show("В поле наименование объекта введите от 2 до 60 символов A-Z");
                return;
			}

            if (!int.TryParse(txShifr.Text, out int nomer))
			{
				MessageBox.Show("В поле Шифр введите цифры");
                return;
            }
            //else if (txShifr != null)
            //{
            //	MessageBox.Show("Поле Шифр не может быть пустым");
            //}

            if (txAdress.Text.Length < 2 || txAdress.Text.Length > 60)
			{
				MessageBox.Show("Поле адрес содержит от 2 до 60 символов A-Z");
                return;
            }

            if (!DateTime.TryParse(txDateDog.Text, out DateTime date))
			{
				MessageBox.Show("В поле дата договора введен неверный формат даты");
                return;
            }

            if (!int.TryParse(txNomerDog.Text, out int nomer1))
			{
				MessageBox.Show("В поле номер договора введите цифры");
                return;
            }

            var addObject = new Объект()
            {
                КодЗаказчик = categAddCust,
                КодПроектировщика = categAddProject,
                Код_коэффициента = categAddKof,
                КодСтавки = categAddStavka,
                Шифр = nomer,
                НаименованиеОбъекта = txObjectName.Text,
                Адрес = txAdress.Text
            };

            var addDogovor = new Договор_подряда()
            {
                НомерДог = nomer1,
                ДатаДог = date,
                Код_коэффициента = categAddKof,
                КодСтавки = categAddStavka,
                Шифр = nomer
            };

            var addSmeta = new Локальная_смета()
            {
                Шифр = nomer,
                КодВидаРабот = categAddWorkTypeCode,
                КодРаботы = categAddWorkCode,
                КодСтавки = categAddStavka,
                Код_коэффициента = categAddKof,
                ФизОбъемРабот = double.Parse(txObjem.Text),
                //СтоимостьРаботы = Convert.ToDouble(txSum.Text),
                //ТрудоемкостьРаботы = Convert.ToDouble(txTrud.Text)
            };

            SmetaContext.Объект.Add(addObject);
            SmetaContext.Договор_подряда.Add(addDogovor);
            SmetaContext.Локальная_смета.Add(addSmeta);
            SmetaContext.SaveChanges();
            MessageBox.Show("Данные добавлены. Смета создана");
            Close();
        }
	}
}
//сделать проверку на существование объекта в базе!!!