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
		SmetaEntities1 context = new SmetaEntities1();
		public EditStavka()
		{
			InitializeComponent();
			tbStavkaNameEdit.Text = Directory.oldNazStavka;
			tbStavkaSizeEdit.Text = Convert.ToString(Directory.oldPriceStavka);
			tbStavkaDateEdit.Text = Convert.ToString(Directory.oldDateStavka);
		}
		private void EditButton_Click(object sender, RoutedEventArgs e)
		{


			double kof;
			if (!double.TryParse(tbStavkaSizeEdit.Text, out kof))
			{
				tbStavkaSizeEdit.Text = "Некорретный ввод!";
			}
			else if (tbStavkaNameEdit.Text.Length < 2 || tbStavkaNameEdit.Text.Length > 60)
			{
				tbStavkaNameEdit.Text = "Слишком короткий или длинный!";
			}
			else
			{
				Ставка_14_го_разряда c = context.Ставка_14_го_разряда.SingleOrDefault(cl => cl.КодСтавки == Directory.pkIDStavka);
				c.Обоснование = tbStavkaNameEdit.Text;
				c.Значение_ставки = Convert.ToDouble(tbStavkaSizeEdit.Text);
				c.Дата_ставки = Convert.ToDateTime(tbStavkaDateEdit.Text);
				context.SaveChanges();
				context.Entry(c).State = System.Data.Entity.EntityState.Modified;
				Close();

			}

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
