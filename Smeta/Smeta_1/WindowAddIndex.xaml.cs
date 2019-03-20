using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using System.Data;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for AddIndex.xaml
    /// </summary>
    public partial class AddIndex : MetroWindow
	{
        public SmetaEntities SmetaContext { get; set; }

        public AddIndex(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(tbKofCode.Text))
			{
				MessageBox.Show("Заполните поле Код коэффициента");
				return;
			}

			if (!int.TryParse(tbKofCode.Text, out int nomer) & nomer <= 0)
			{
				MessageBox.Show("В поле Код коэффициента введите цифры больше 0");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbKofSize.Text))
			{
				MessageBox.Show("Заполните поле Значение коэффициента");
				return;
			}

			if (!double.TryParse(tbKofSize.Text, out double kof) & kof <= 0)
			{
				MessageBox.Show("В поле Значение введите положительное число");
				return;
			}

			if (string.IsNullOrWhiteSpace(tbKofName.Text))
			{
				MessageBox.Show("Заполните поле Наименование коэффициента");
                return;
			}

			if (tbKofName.Text.Length < 2 || tbKofName.Text.Length > 60)
			{
				MessageBox.Show("Поле Наименование коэффициента должно содеражать от 2 до 60 символов A-Z");
                return;
			}

			var existedItem = SmetaContext.Поправочный_коэффициент_по_типу_ПИР
			   .Where(n => n.Код_коэффициента == nomer)
			   .FirstOrDefault();

			if (existedItem != null)
			{
				MessageBox.Show("Коэффициент с данным кодом уже существует!");
				return;
			}

			try
			{
				var addIndex = new Поправочный_коэффициент_по_типу_ПИР()
				{
					Код_коэффициента = Convert.ToInt32(tbKofCode.Text),
					Наименование_коэффициента = tbKofName.Text,
					Значение_коэффициента = Convert.ToDouble(tbKofSize.Text)
				};

				SmetaContext.Поправочный_коэффициент_по_типу_ПИР.Add(addIndex);
				SmetaContext.SaveChanges();
				MessageBox.Show("Коэффициент добавлен в базу");
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
