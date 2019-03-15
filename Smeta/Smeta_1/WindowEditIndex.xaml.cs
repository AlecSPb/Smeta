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
	/// Interaction logic for WindowEditIndex.xaml
	/// </summary>
	public partial class EditIndex : MetroWindow
	{
        public SmetaEntities SmetaContext { get; set; }

        public EditIndex(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            tbKofNameEdit.Text = Directory.oldNaz;
            tbKofSizeEdit.Text = Convert.ToString(Directory.oldPrice);
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
                .SingleOrDefault(cl => cl.Код_коэффициента == Directory.pk2ID);

            c.Наименование_коэффициента = tbKofNameEdit.Text;
            c.Значение_коэффициента = double.Parse(tbKofSizeEdit.Text);
            SmetaContext.SaveChanges();
            SmetaContext.Entry(c).State = EntityState.Modified;
            Close();
        }

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		public void Update<TEntity>(IEnumerable<TEntity> entities, DbContext context) where TEntity : class
		{
			// Отключаем отслеживание и проверку изменений для оптимизации вставки множества полей
			context.Configuration.AutoDetectChangesEnabled = false;
			context.Configuration.ValidateOnSaveEnabled = false;

			context.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s));

			foreach (TEntity entity in entities)
				context.Entry<TEntity>(entity).State = EntityState.Modified;
			context.SaveChanges();

			context.Configuration.AutoDetectChangesEnabled = true;
			context.Configuration.ValidateOnSaveEnabled = true;
		}
	}
}
