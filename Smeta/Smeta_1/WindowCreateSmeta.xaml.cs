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
    /// Interaction logic for WindowCreateSmeta.xaml
    /// </summary>
    public partial class CreateSmeta : MetroWindow
	{
		private int categAddCust;
        private int categAddProject;
        private int categAddKof;
        private int categAddStavka;
        private int categAddWorkTypeCode;
        private int categAddWorkCode;

        // У тебя данная переменная никогда нигде не присваивается. Следовательно проверка на
        //
        // var existedDog = SmetaContext.Договор_подряда
        //   .Where(n => n.НомерДог == DogNomer)
        //   .FirstOrDefault();
        //
        // Работать не будет!!!
        private int DogNomer;

        public SmetaEntities SmetaContext { get; set; }

        public CreateSmeta(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            foreach (var item in SmetaContext.Заказчик.ToList())
            {
                cmbCustomers.Items.Add(item);
            }

            foreach (var item in SmetaContext.Проектная_организация.ToList())
            {
                cmbProject.Items.Add(item);
            }

            foreach (var item in SmetaContext.Поправочный_коэффициент_по_типу_ПИР.ToList())
            {
                cmbObjectProject.Items.Add(item);
            }

            foreach (var item in SmetaContext.Ставка_14_го_разряда.ToList())
            {
                cmbStavka.Items.Add(item);
            }

            foreach (var item in SmetaContext.Справочник_видов_работ.ToList())
            {
                cmbWorkType.Items.Add(item);
            }

            foreach (var item in SmetaContext.Справочник_расценок.ToList())
            {
                cmbWorkName.Items.Add(item);
            }
        }

		private void cbSelectCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var cust = cmbCustomers.SelectedItem as Заказчик;
			categAddCust = cust.КодЗаказчик;
		}

		private void cbSelectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var pro = cmbProject.SelectedItem as Проектная_организация;
			categAddProject = pro.КодПроектировщика;
		}

		private void CmbObjectProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var kof = cmbObjectProject.SelectedItem as Поправочный_коэффициент_по_типу_ПИР;
			categAddKof = kof.Код_коэффициента;
		}

		private void CmbStavka_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var stv = cmbStavka.SelectedItem as Ставка_14_го_разряда;
			categAddStavka = stv.КодСтавки;
		}

		private void cbSelectWorkType_SelectionChanged (object sender, SelectionChangedEventArgs e)
		{
			var dir = cmbWorkType.SelectedItem as Справочник_видов_работ;
			categAddWorkTypeCode = dir.КодВидаРабот;
		}

		private void CmbWorkName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var pr = cmbWorkName.SelectedItem as Справочник_расценок;
			categAddWorkCode = pr.КодРаботы;
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txObjectName.Text))
			{
				MessageBox.Show("Заполните поле наименование объекта");
                return;
			}

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

			if (string.IsNullOrWhiteSpace(txAdress.Text))
			{
				MessageBox.Show("Заполните поле адрес");
				return;
			}

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

			var existedItem = SmetaContext.Объект
			   .Where(n => n.Шифр == nomer)
			   .FirstOrDefault();

			if (existedItem != null)
			{
				MessageBox.Show("Объект с данным шифром уже существует!");
				return;
			}

			var existedDog = SmetaContext.Договор_подряда
			   .Where(n => n.НомерДог == DogNomer)
			   .FirstOrDefault();

			if (existedDog != null)
			{
				MessageBox.Show("Данный договор уже существует!");
				return;
			}

			try
			{
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

				};

				SmetaContext.Объект.Add(addObject);
				SmetaContext.Договор_подряда.Add(addDogovor);
				SmetaContext.Локальная_смета.Add(addSmeta);
				SmetaContext.SaveChanges();

				MessageBox.Show("Данные добавлены. Смета создана");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "SQL Error");
                return;
			}

			Close();
		}
	}
}
