using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using System.Data;
using Smeta_DB;


namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowAddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : MetroWindow
	{
        // У тебя данные переменные никогда нигде не присваивается. Следовательно проверка на
        //
        //        var existedItem = SmetaContext.Проектная_организация
        //           .Where(n => n.КодПроектировщика == nomer)
        //           .FirstOrDefault();

        //			if (existedItem != null)
        //			{
        //				MessageBox.Show("Проектная организация с данным кодом уже существует!");
        //				return;
        //			}

        //    var existedYNP = SmetaContext.Проектная_организация
        //       .Where(n => n.УНП == YNP)
        //       .FirstOrDefault();
		  
        //			if (existedYNP != null)
        //			{
        //				MessageBox.Show("Проектная организация с данным УНП уже существует!");
        //				return;
        //			}

        //var existedName = SmetaContext.Проектная_организация
        //   .Where(n => n.НаименованиеПроектиров == NameP)
        //   .FirstOrDefault();

        //            if (existedName != null)
        //			{
        //				MessageBox.Show("Проектная организация с данным названием уже существует!");
        //				return;
        //			}

        //			var existedRS = SmetaContext.Проектная_организация
        //               .Where(n => n.Р_с == RS)
        //               .FirstOrDefault();

        //            if (existedRS != null)
        //			{
        //				MessageBox.Show("Проектная организация с данным расчетным счетом уже существует!");
        //				return;
        //			}
        //
        // Работать не будет!!!
        private string YNP;
        private string NameP;
        private string RS;

        public SmetaEntities SmetaContext { get; set; }

		public AddCustomer(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;
        }

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (!int.TryParse(tbProjectCod.Text, out int nomer))
			{
				MessageBox.Show("Некорректный ввод!");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbProjectName.Text))
			{
				MessageBox.Show("Заполните поле Наименование проектной организации");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbProjectAdress.Text))
			{
				MessageBox.Show("Заполните поле Адрес проектной организации");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbProjectYNP.Text))
			{
				MessageBox.Show("Заполните поле УНП");
				return;
			}

            if (tbProjectYNP.Text.Length != 9)
			{
				MessageBox.Show("Поле должно содержать 9 цифр!");
				return;
			}

            if (string.IsNullOrWhiteSpace(tbRS.Text))
			{
				MessageBox.Show("Заполните поле р/с");
				return;
			}

            if (tbRS.Text.Length != 13)
			{
				MessageBox.Show("Поле должно содержать 13 цифр");
				return;
			}

			var existedItem = SmetaContext.Проектная_организация
			   .Where(n => n.КодПроектировщика == nomer)
			   .FirstOrDefault();

			if (existedItem != null)
			{
				MessageBox.Show("Проектная организация с данным кодом уже существует!");
				return;
			}

			var existedYNP = SmetaContext.Проектная_организация
			   .Where(n => n.УНП == YNP)
			   .FirstOrDefault();
		  
			if (existedYNP != null)
			{
				MessageBox.Show("Проектная организация с данным УНП уже существует!");
				return;
			}

			var existedName = SmetaContext.Проектная_организация
			   .Where(n => n.НаименованиеПроектиров == NameP)
			   .FirstOrDefault();

            if (existedName != null)
			{
				MessageBox.Show("Проектная организация с данным названием уже существует!");
				return;
			}

			var existedRS = SmetaContext.Проектная_организация
			   .Where(n => n.Р_с == RS)
			   .FirstOrDefault();

            if (existedRS != null)
			{
				MessageBox.Show("Проектная организация с данным расчетным счетом уже существует!");
				return;
			}

			try
			{
				var addProjectCompany = new Проектная_организация()
				{
					КодПроектировщика = nomer,
					НаименованиеПроектиров = tbProjectName.Text,
					ЮрАдрес = tbProjectName.Text,
					Р_с = tbRS.Text,
					УНП = tbProjectYNP.Text,
					Тел = tbPhone.Text,
					ЭлПочта = tbMail.Text
				};

				SmetaContext.Проектная_организация.Add(addProjectCompany);
				SmetaContext.SaveChanges();
				MessageBox.Show("Проектная организация добавлена в базу");
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
