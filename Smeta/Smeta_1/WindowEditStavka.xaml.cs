using System;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowEditStavka.xaml
    /// </summary>
    public partial class EditStavka : MetroWindow
	{
        private Directory _currentDirectory;
        public SmetaEntities SmetaContext { get; set; }

        public EditStavka(SmetaEntities context, Directory currentDirectory)
        {
            InitializeComponent();

            SmetaContext = context;
            _currentDirectory = currentDirectory;

            tbStavkaNameEdit.Text = _currentDirectory.oldNazStavka;
            tbStavkaSizeEdit.Text = _currentDirectory.oldPriceStavka.ToString();
            tbStavkaDateEdit.Text = _currentDirectory.oldDateStavka.ToString();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (!double.TryParse(tbStavkaSizeEdit.Text, out double kof))
			{
				tbStavkaSizeEdit.Text = "Некорретный ввод!";
                return;
			}

            if (tbStavkaNameEdit.Text.Length < 2 || tbStavkaNameEdit.Text.Length > 60)
			{
				tbStavkaNameEdit.Text = "Слишком короткий или длинный!";
                return;
			}

            var c = SmetaContext.Ставка_14_го_разряда.SingleOrDefault(cl => cl.КодСтавки == _currentDirectory.pkIDStavka);

            if(c != null)
            {
                c.Обоснование = tbStavkaNameEdit.Text;
                c.Значение_ставки = double.Parse(tbStavkaSizeEdit.Text);
                c.Дата_ставки = DateTime.Parse(tbStavkaDateEdit.Text);
            }

            try
            {
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
