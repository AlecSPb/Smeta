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
	/// Interaction logic for Object.xaml
	/// </summary>
	public partial class Object : MetroWindow
	{
		static int code;
		//static int code1;
		SmetaEntities context = new SmetaEntities();

		public Object()
		{

			InitializeComponent();
			ReDrow();
			dgObject.Items.Clear();

			//foreach (Локальная_смета item in GetAllSmeta())
			//{
			//	dgObject.Items.Add(item);
			//}
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
		public IEnumerable<Договор_подряда> GetAllDogovor()
		{
			return context.Договор_подряда.ToArray().Select((Договор_подряда dog) =>
			{
				return new Договор_подряда
				{
					ДатаДог = dog.ДатаДог,
					НомерДог = dog.НомерДог,
					Шифр = dog.Шифр,
					Код_коэффициента = dog.Код_коэффициента,
					КодСтавки = dog.КодСтавки

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
		public void ReDrow()
		{
			listBox1.Items.Clear();
			foreach (Объект obj in GetAllObject())
			{
				listBox1.Items.Add(obj);
			}



		}

		private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Объект obj = listBox1.SelectedItem as Объект;
			
			if (obj != null)
			{
				labelAd.Content = obj.Адрес;
				labelName.Content = obj.НаименованиеОбъекта;
				code = obj.Шифр;

                dgObject.ItemsSource = context.Локальная_смета.
                    Where(sm => sm.Шифр == code).
                    Select(l => new
                    {
                        Код = l.КодРаботы,
                        Наименование = l.Справочник_расценок.ИмяРаботы,
                        Объем = l.ФизОбъемРабот,
                        Трудоемкость = l.ТрудоемкостьРаботы,
                        Стоимость = l.СтоимостьРаботы
                    }).ToList();

                txSumTrud.Text = context.Локальная_смета
                    .Where(c => c.Шифр == obj.Шифр)
                    .Select(c => c.ТрудоемкостьРаботы)
                    .Sum().ToString();

                txStoimost.Text = context.Локальная_смета
                    .Where(c => c.Шифр == obj.Шифр)
                    .Select(c => c.СтоимостьРаботы)
                    .Sum().ToString();

                Договор_подряда dg = context.Договор_подряда.Where(d => d.Шифр == obj.Шифр).FirstOrDefault();
			    labelData.Content = dg.ДатаДог;
			    labelNomer.Content = dg.НомерДог;

			    Заказчик cu = context.Заказчик.Where(c => c.КодЗаказчик == obj.КодЗаказчик).FirstOrDefault();
			    labelCustName.Content = cu.НаименованиеЗаказчика;
			    labelCustAdress.Content = cu.ЮрАдрес;
			    labelCustYNP.Content = cu.УНП;
			    labelCustPhone.Content = cu.Тел;
			    labelCustMail.Content = cu.ЭлПочта;

			    Проектная_организация pr = context.Проектная_организация.Where(p => p.КодПроектировщика == obj.КодПроектировщика).FirstOrDefault();
			    labelProectName.Content = pr.НаименованиеПроектиров;
			    labelProectAdress.Content = pr.ЮрАдрес;
			    labelProectYNP.Content = pr.УНП;
			    labelProectPhone.Content = pr.Тел;
			    labelProectMail.Content = pr.ЭлПочта;
			}
		}

		private void dgObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			Локальная_смета ls = dgObject.SelectedItem as Локальная_смета;
			if (ls != null)
			{
				ls = context.Локальная_смета
					.Where(v => v.Шифр == ls.Шифр)
					.AsEnumerable()
					.FirstOrDefault();
			
			
			}
			
			//code1 = ls.КодРаботы;

			//foreach (Справочник_расценок sr in GetAllPrices())
			//{
			//	if (sr.КодРаботы == code1)
			//	{
			//		dgObject.Items.Add(sr);
			//	}
			//}

			
			
			context.Локальная_смета.Load();
			context.Справочник_расценок.Load();
			//dgObject.ItemsSource = query.ToList();
		}





			//SqlConnection ThisConnection = new SqlConnection(@"Data Source=E520-THINK\SQLSERVER;Initial Catalog= Smeta_1");
			//ThisConnection.Open();
			//SqlCommand thisCommand = ThisConnection.CreateCommand();
			//thisCommand.CommandText = "select sum([ТрудоемкостьРаботы]) as [ОбщаяТрудоемкостьРаботы] FROM [dbo].[Локальная_смета] where [dbo].[Локальная_смета].Шифр= [dbo].[Объект].Шифр ";
			//SqlDataReader thisReader = thisCommand.ExecuteReader();
			//string res = string.Empty;
			//while (thisReader.Read())
			//{
			//	res += thisReader["ОбщаяТрудоемкостьРаботы"];
			//}
			//thisReader.Close();
			//ThisConnection.Close();
			//txSumTrud.Text = res.ToString();

			//var l = context.Локальная_смета
			//   .Where(v => v.Шифр == 33)
			//   .Select(v => v.ТрудоемкостьРаботы)
			//   .Sum();
			//txSumTrud.Text = l.ToString();
		

		private void MenuItem_Click_OpenChart(object sender, RoutedEventArgs e)
		{
				Chart wa = new Chart();
				wa.Owner = this;
				wa.ShowDialog();
			
		}

		private void MenuItem_Click_OpenDirectory(object sender, RoutedEventArgs e)
		{
			Directory wa = new Directory();
			wa.Owner = this;
			wa.ShowDialog();

		}

		private void MenuItem_Click_OpenAbout(object sender, RoutedEventArgs e)
		{
			About wa = new About();
			wa.Owner = this;
			wa.ShowDialog();

		}

		private void MenuItem_Click_AddProjectCompany(object sender, RoutedEventArgs e)
		{
			AddCustomer wa = new AddCustomer();
			wa.Owner = this;
			wa.ShowDialog();
		}
	}
}