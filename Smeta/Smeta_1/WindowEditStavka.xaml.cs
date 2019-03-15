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
	/// Interaction logic for WindowEditStavka.xaml
	/// </summary>
	public partial class EditStavka : MetroWindow
	{
        public SmetaEntities SmetaContext { get; set; }

        public EditStavka(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            tbStavkaNameEdit.Text = Directory.oldNazStavka;
            tbStavkaSizeEdit.Text = Convert.ToString(Directory.oldPriceStavka);
            tbStavkaDateEdit.Text = Convert.ToString(Directory.oldDateStavka);
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

            var c = SmetaContext.Ставка_14_го_разряда.SingleOrDefault(cl => cl.КодСтавки == Directory.pkIDStavka);
            c.Обоснование = tbStavkaNameEdit.Text;
            c.Значение_ставки = double.Parse(tbStavkaSizeEdit.Text);
            c.Дата_ставки = DateTime.Parse(tbStavkaDateEdit.Text);
            SmetaContext.SaveChanges();
            Close();
        }

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		public void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
		{
            // Отключаем отслеживание и проверку изменений для оптимизации вставки множества полей
            SmetaContext.Configuration.AutoDetectChangesEnabled = false;
            SmetaContext.Configuration.ValidateOnSaveEnabled = false;

            SmetaContext.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));

			foreach (TEntity entity in entities)
                SmetaContext.Entry(entity).State = EntityState.Modified;
            SmetaContext.SaveChanges();

            SmetaContext.Configuration.AutoDetectChangesEnabled = true;
            SmetaContext.Configuration.ValidateOnSaveEnabled = true;
		}
	}
}
