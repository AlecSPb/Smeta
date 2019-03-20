using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Data;

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

                var listOfSmeta = SmetaContext.Локальная_смета
                    .Where(c => c.Шифр == obj.Шифр)
                    .ToList();
                
                var Cost1 = listOfSmeta
                    .Select(c => c.СтоимостьРаботы)
                    .Sum();

                var Labor1 = listOfSmeta
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