using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowEditpriceToSmeta.xaml
    /// </summary>
    public partial class EditPriceToSmeta : MetroWindow
	{
        private Object _currentObject;

        public SmetaEntities SmetaContext { get; set; }

		public EditPriceToSmeta(SmetaEntities context, Object currentObject)
		{
			InitializeComponent();

			SmetaContext = context;
            _currentObject = currentObject;
            
            tbValue.Text = _currentObject.oldObjem.ToString();
		}

		private void EditButton_Click(object sender, RoutedEventArgs e)
		{
			if (!double.TryParse(tbValue.Text, out double kof) & kof <=0)
			{
				tbValue.Text = "Некорретный ввод!";
				return;
			}

			var c = SmetaContext.Локальная_смета.SingleOrDefault(
                cl => cl.Шифр == _currentObject.Shifr 
		        & cl.КодВидаРабот == _currentObject.WorkTypeCode
	            & cl.КодРаботы == _currentObject.WorkCode
			    & cl.Код_коэффициента == _currentObject.KofCode
			    & cl.КодСтавки == _currentObject.StavkaCode);
			
            if(c != null)
			    c.ФизОбъемРабот = double.Parse(tbValue.Text);

            try
            {
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

        private void cbSelectStavkakAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
