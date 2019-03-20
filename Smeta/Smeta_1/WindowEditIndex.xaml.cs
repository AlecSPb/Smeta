using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MahApps.Metro.Controls;
using System.Data.Entity;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowEditIndex.xaml
    /// </summary>
    public partial class EditIndex : MetroWindow
	{
        private Directory _currentDirectory;
        public SmetaEntities SmetaContext { get; set; }

        public EditIndex(SmetaEntities context, Directory currentDirectory)
        {
            InitializeComponent();

            SmetaContext = context;
            _currentDirectory = currentDirectory;

            tbKofNameEdit.Text = _currentDirectory.oldNaz;
            tbKofSizeEdit.Text = Convert.ToString(_currentDirectory.oldPrice);
        }

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			double kof;

			if (!double.TryParse(tbKofSizeEdit.Text, out kof))
			{
				tbKofSizeEdit.Text = "Некорретный ввод!";
                return;
			}

            if (tbKofNameEdit.Text.Length < 2 || tbKofNameEdit.Text.Length > 60)
			{
				tbKofNameEdit.Text = "Слишком короткий или длинный!";
                return;
			}

            var c = SmetaContext.Поправочный_коэффициент_по_типу_ПИР
                .SingleOrDefault(cl => cl.Код_коэффициента == _currentDirectory.pk2ID);

            c.Наименование_коэффициента = tbKofNameEdit.Text;
            c.Значение_коэффициента = double.Parse(tbKofSizeEdit.Text);
            SmetaContext.Entry(c).State = EntityState.Modified;

            try
            {
                SmetaContext.SaveChanges();
                MessageBox.Show("Расценка добавлена");
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
