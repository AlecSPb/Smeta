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

using System.Windows.Controls.DataVisualization.Charting;
using Smeta_DB;

namespace Smeta_1
{
	/// <summary>
	/// Interaction logic for WindowChart.xaml
	/// </summary>
	public partial class Chart : MetroWindow
	{
        public SmetaEntities SmetaContext { get; set; }

        public Chart(SmetaEntities context)
        {
            InitializeComponent();

            SmetaContext = context;

            ReDrow();
        }
        
		public void ReDrow()
		{
			listBox2.Items.Clear();
			foreach (Объект obj in SmetaContext.Объект.ToList())
			{
				listBox2.Items.Add(obj);
			}
		}

		private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            Объект obj = listBox2.SelectedItem as Объект;

            if (obj != null)
            {
                labelName.Content = obj.НаименованиеОбъекта;

                var Cost1 = SmetaContext.Локальная_смета
                    .Where(c => c.Шифр == obj.Шифр)
                    .Select(c => c.СтоимостьРаботы)
                    .Sum();

                var Labor1 = SmetaContext.Локальная_смета
                    .Where(c => c.Шифр == obj.Шифр)
                    .Select(c => c.ТрудоемкостьРаботы)
                    .Sum();

                ((PieSeries)mcChart.Series[0]).ItemsSource =
                    new KeyValuePair<string, double>[]
                    {
                        new KeyValuePair<string, double>("Стоимость работ", Cost1.Value),
                        new KeyValuePair<string, double>("Трудоемкость работ", Labor1.Value)
                    };
            }
        }
	}
}