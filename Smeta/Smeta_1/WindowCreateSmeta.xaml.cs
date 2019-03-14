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
		//static int cat;
		SmetaEntities context = new SmetaEntities();
		public CreateSmeta()
		{
			InitializeComponent();
			cmbCustomers.Items.Clear();
			cmbObjectProject.Items.Clear();
			cmbProject.Items.Clear();
			cmbStavka.Items.Clear();
			cmbWorkName.Items.Clear();
			//dgDirectory_1.Items.Clear();
			cmbWorkType.Items.Clear();

			foreach (Заказчик stw in GetAllCustomer())
			{
				cmbCustomers.Items.Add(stw.НаименованиеЗаказчика);

			}
			foreach (Проектная_организация stw in GetAllGen())
			{
				cmbProject.Items.Add(stw.НаименованиеПроектиров);

			}
			foreach (Поправочный_коэффициент_по_типу_ПИР stw in GetAllKoef())
			{
				cmbObjectProject.Items.Add(stw.Наименование_коэффициента);

			}
			foreach (Ставка_14_го_разряда stw in GetAllStavka())
			{
				cmbStavka.Items.Add(stw.Обоснование);

			}
			foreach (Справочник_видов_работ stw in GetAllTypeWork())
			{
				cmbWorkType.Items.Add(stw.ВидРабот);
			}
			foreach (Справочник_расценок stw in GetAllPrices())
			{
				cmbWorkName.Items.Add(stw.ИмяРаботы);
				//dgDirectory_1.Items.Add(stw);
			}
		}
		public IEnumerable<Заказчик> GetAllCustomer()
		{
			return context.Заказчик.ToArray().Select((Заказчик cust) =>
			{
				return new Заказчик
				{
					КодЗаказчик = cust.КодЗаказчик,
					НаименованиеЗаказчика = cust.НаименованиеЗаказчика,
					Расчетный_счет = cust.Расчетный_счет,
					Тел = cust.Тел,
					УНП = cust.УНП,
					ЭлПочта = cust.ЭлПочта,
					ЮрАдрес = cust.ЮрАдрес,

				};
			});
		}
		public IEnumerable<Проектная_организация> GetAllGen()
		{
			return context.Проектная_организация.ToArray().Select((Проектная_организация pro) =>
			{
				return new Проектная_организация
				{
					КодПроектировщика = pro.КодПроектировщика,
					НаименованиеПроектиров = pro.НаименованиеПроектиров,
					Р_с = pro.Р_с,
					Тел = pro.Тел,
					УНП = pro.УНП,
					ЭлПочта = pro.ЭлПочта,
					ЮрАдрес = pro.ЮрАдрес,

				};
			});
		}
		public IEnumerable<Поправочный_коэффициент_по_типу_ПИР> GetAllKoef()
		{
			return context.Поправочный_коэффициент_по_типу_ПИР.ToArray().Select((Поправочный_коэффициент_по_типу_ПИР kof) =>
			{
				return new Поправочный_коэффициент_по_типу_ПИР
				{
					Наименование_коэффициента = kof.Наименование_коэффициента,
					Код_коэффициента = kof.Код_коэффициента,
					Значение_коэффициента = kof.Значение_коэффициента
				};
			});
		}
		public IEnumerable<Ставка_14_го_разряда> GetAllStavka()
		{
			return context.Ставка_14_го_разряда.ToArray().Select((Ставка_14_го_разряда stavka) =>
			{
				return new Ставка_14_го_разряда
				{
					Дата_ставки = stavka.Дата_ставки,
					Значение_ставки = stavka.Значение_ставки,
					Обоснование = stavka.Обоснование,
					КодСтавки = stavka.КодСтавки
				};
			});
		}
		public IEnumerable<Объект> GetAllObject()
		{
			return context.Объект.ToArray().Select((Объект ob) =>
			{
				return new Объект
				{
					Шифр = ob.Шифр,
					Адрес = ob.Адрес,
					НаименованиеОбъекта = ob.НаименованиеОбъекта,
					КодПроектировщика = ob.КодПроектировщика,
					Код_коэффициента = ob.Код_коэффициента,
					КодЗаказчик = ob.КодЗаказчик,
					КодСтавки = ob.КодСтавки
				};
			});

		}
		public IEnumerable<Справочник_расценок> GetAllPrices()
		{
			return context.Справочник_расценок.ToArray().Select((Справочник_расценок catw) =>
			{
				return new Справочник_расценок
				{
					КодРаботы = catw.КодРаботы,
					ИмяРаботы = catw.ИмяРаботы,
					ЗатратыТрудаЕдОбъема = catw.ЗатратыТрудаЕдОбъема,
					КодВидаРабот = catw.КодВидаРабот
				};
			});
		}
		public IEnumerable<Справочник_видов_работ> GetAllTypeWork()
		{
			return context.Справочник_видов_работ.ToArray().Select((Справочник_видов_работ typew) =>
			{
				return new Справочник_видов_работ
				{
					КодВидаРабот = typew.КодВидаРабот,
					ВидРабот = typew.ВидРабот,
					ЕдИзмОб = typew.ЕдИзмОб,
					ЕдИзмТруд = typew.ЕдИзмТруд
				};
			});
		}
		public IEnumerable<Локальная_смета> GetAllSmeta()
		{
			return context.Локальная_смета.ToArray().Select((Локальная_смета locsmeta) =>
			{
				return new Локальная_смета
				{
					КодВидаРабот = locsmeta.КодВидаРабот,
					КодРаботы = locsmeta.КодРаботы,
					Шифр = locsmeta.Шифр,
					Код_коэффициента = locsmeta.Код_коэффициента,
					КодСтавки = locsmeta.КодСтавки,
					ФизОбъемРабот = locsmeta.ФизОбъемРабот,
					ТрудоемкостьРаботы = locsmeta.ТрудоемкостьРаботы,
					СтоимостьРаботы = locsmeta.СтоимостьРаботы
				};
			});
		}
		private void cbSelectCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Заказчик cust = cmbCustomers.SelectedItem as Заказчик;

			cust = context.Заказчик
				.Where(v => v.НаименованиеЗаказчика == cmbCustomers.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Заказчик.Load();
			categAddCust = cust.КодЗаказчик;
		}
		private void cbSelectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Проектная_организация pro = cmbProject.SelectedItem as Проектная_организация;

			pro = context.Проектная_организация
				.Where(v => v.НаименованиеПроектиров == cmbProject.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Проектная_организация.Load();
			categAddProject = pro.КодПроектировщика;
		}

		private void CmbObjectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Поправочный_коэффициент_по_типу_ПИР kof = cmbObjectProject.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;

			kof = context.Поправочный_коэффициент_по_типу_ПИР
				.Where(v => v.Наименование_коэффициента == cmbObjectProject.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Поправочный_коэффициент_по_типу_ПИР.Load();
			categAddKof = kof.Код_коэффициента;
		}

		private void CmbStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Ставка_14_го_разряда stv = cmbStavka.SelectedItem as Ставка_14_го_разряда;

			stv = context.Ставка_14_го_разряда
				.Where(v => v.Обоснование == cmbStavka.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();

			context.Ставка_14_го_разряда.Load();
			categAddStavka = stv.КодСтавки;
		}
		private void cbSelectWorkType_SelectionChanged (object sender, SelectionChangedEventArgs e)
		{
			Справочник_видов_работ dir = cmbWorkType.SelectedItem as Справочник_видов_работ;
			dir = context.Справочник_видов_работ
				.Where(v => v.ВидРабот == cmbWorkType.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();
			context.Справочник_видов_работ.Load();
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
			Справочник_расценок pr = cmbWorkName.SelectedItem as Справочник_расценок;
			pr = context.Справочник_расценок
				.Where(v => v.ИмяРаботы == cmbWorkName.SelectedItem)
				.AsEnumerable()
				.FirstOrDefault();
			context.Справочник_расценок.Load();
			categAddWorkCode = pr.КодРаботы;

		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			int nomer;
			DateTime date;
			int nomer1;
			//if (cbSelectCustomer.Text.Length == 0 || cbSelectProject.Text.Length == 0 || CmbObjectProject.Text.Length ==0 || CmbStavka.Text.Length == 0)
			//{
			//	MessageBox.Show("Выберите категорию!");
				
			//}
			if (txObjectName.Text.Length < 2 || txObjectName.Text.Length > 60)
			{
				MessageBox.Show("В поле наименование объекта введите от 2 до 60 символов A-Z");
			}
			else if (!Int32.TryParse(txShifr.Text, out nomer))
			{
				MessageBox.Show("В поле Шифр введите цифры");
			}
			//else if (txShifr != null)
			//{
			//	MessageBox.Show("Поле Шифр не может быть пустым");
			//}
			else if (txAdress.Text.Length < 2 || txAdress.Text.Length > 60)
			{
				MessageBox.Show("Поле адрес содержит от 2 до 60 символов A-Z");
			}
			else if (!DateTime.TryParse(txDateDog.Text, out date))
			{
				MessageBox.Show("В поле дата договора введен неверный формат даты");
			}
			else if (!Int32.TryParse(txNomerDog.Text, out nomer1))
			{
				MessageBox.Show("В поле номер договора введите цифры");
			}
			else
			{
				Объект addObject = new Объект()
				{
					КодЗаказчик = categAddCust,
					КодПроектировщика = categAddProject,
					Код_коэффициента = categAddKof,
					КодСтавки = categAddStavka,
					Шифр = Convert.ToInt32(txShifr.Text),
					НаименованиеОбъекта = txObjectName.Text,
					Адрес = txAdress.Text
				};

				
				Договор_подряда addDogovor = new Договор_подряда()
				{
					НомерДог = Convert.ToInt32(txNomerDog.Text),
					ДатаДог = Convert.ToDateTime(txDateDog.Text),
					Код_коэффициента = categAddKof,
					КодСтавки = categAddStavka,
					Шифр = Convert.ToInt32(txShifr.Text)

				};

				Локальная_смета addSmeta = new Локальная_смета()
				{
					Шифр = Convert.ToInt32(txShifr.Text),
					КодВидаРабот = categAddWorkTypeCode,
					КодРаботы = categAddWorkCode,
					КодСтавки = categAddStavka,
					Код_коэффициента = categAddKof,
					ФизОбъемРабот = Convert.ToDouble(txObjem.Text),
					//СтоимостьРаботы = Convert.ToDouble(txSum.Text),
					//ТрудоемкостьРаботы = Convert.ToDouble(txTrud.Text)

				};
				context.Объект.Add(addObject);
				context.Договор_подряда.Add(addDogovor);
				context.Локальная_смета.Add(addSmeta);
				context.SaveChanges();
				MessageBox.Show("Данные добавлены. Смета создана");
				Close();
			}

		}

		
	}
}
//сделать проверку на существование объекта в базе!!!