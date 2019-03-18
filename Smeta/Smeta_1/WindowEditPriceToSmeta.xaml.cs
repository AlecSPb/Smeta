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
	/// Interaction logic for WindowEditpriceToSmeta.xaml
	/// </summary>
	public partial class EditPriceToSmeta : MetroWindow
	{
		static int categAddWorkType;
		static int categAddWork;
		public SmetaEntities SmetaContext { get; set; }

		public EditPriceToSmeta(SmetaEntities context)
		{
			InitializeComponent();

			SmetaContext = context;

			//cmbWorkType.Items.Clear();
			//cmbWorkName.Items.Clear();
			tbValue.Text = Convert.ToString(Object.oldObjem);
		//	foreach (Справочник_видов_работ stw in SmetaContext.Справочник_видов_работ.ToList())
		//	{
		//		cmbWorkType.Items.Add(stw.ВидРабот);
		//	}
		//	foreach (Справочник_расценок stw in SmetaContext.Справочник_расценок.ToList())
		//	{
		//		cmbWorkName.Items.Add(stw.ИмяРаботы);
		//	}


		}
		//private void cbSelectWorkTypeAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	var dir = cmbWorkType.SelectedItem as Справочник_видов_работ;
		//	dir = SmetaContext.Справочник_видов_работ
		//		.Where(v => v.ВидРабот == cmbWorkType.SelectedItem)
		//		.AsEnumerable()
		//		.FirstOrDefault();

		//	SmetaContext.Справочник_видов_работ.Load();
		//	categAddWorkType = dir.КодВидаРабот;
		//}
		//private void cbSelectWorkAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	var dir = cmbWorkName.SelectedItem as Справочник_расценок;
		//	dir = SmetaContext.Справочник_расценок
		//		.Where(v => v.ИмяРаботы == cmbWorkName.SelectedItem)
		//		.AsEnumerable()
		//		.FirstOrDefault();

		//	SmetaContext.Справочник_расценок.Load();
		//	categAddWork = dir.КодРаботы;
		//}
		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (!double.TryParse(tbValue.Text, out double kof) & kof <=0)
			{
				tbValue.Text = "Некорретный ввод!";
				return;
			}

			var c = SmetaContext.Локальная_смета.SingleOrDefault(cl => cl.Шифр == Object.Shifr 
			& cl.КодВидаРабот == Object.WorkTypeCode
			& cl.КодРаботы == Object.WorkCode
			& cl.Код_коэффициента == Object.KofCode
			& cl.КодСтавки == Object.StavkaCode);
			
			c.ФизОбъемРабот = double.Parse(tbValue.Text);
			
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

        private void cbSelectStavkakAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
