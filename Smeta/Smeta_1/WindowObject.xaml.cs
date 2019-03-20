using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using System.Data;
using Smeta_DB;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Input;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for Object.xaml
    /// </summary>
    public partial class Object : MetroWindow
	{
		private int code;
        private object oMissing = System.Reflection.Missing.Value;

        public int WorkTypeCode { get; set; }
		public int Shifr { get; set; }
        public int WorkCode { get; set; }
        public int StavkaCode { get; set; }
        public int KofCode { get; set; }
        public double oldObjem { get; set; }

        public SmetaEntities SmetaContext { get; set; }

        public Object(SmetaEntities context)
		{
			InitializeComponent();
            SmetaContext = context;
            ReDrow();

			if (MainWindow.sRole == "admin")
			{
				menu_addProject.IsEnabled = true;
				menu_saveDogovor.IsEnabled = true;
				menu_Counter.IsEnabled = false;
				btnAddPrice.IsEnabled = false;
			}

			if (MainWindow.sRole == "user")
			{
				menu_addProject.IsEnabled = false;
				menu_saveDogovor.IsEnabled = false;
				menu_Counter.IsEnabled = true;
				btnAddPrice.IsEnabled = true;
			}
		}

		public void ReDrow()
		{
			foreach (var obj in SmetaContext.Объект.ToList())
			{
				listBox1.Items.Add(obj);
			}
		}

		private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var obj = listBox1.SelectedItem as Объект;
			
			if (obj != null)
			{
				txtAd.Text = obj.Адрес;
				txtName.Text = obj.НаименованиеОбъекта;
				
				code = obj.Шифр;

                // Зачем делать 3 одинаковых запросов к БД?
                var listOfSmeta = SmetaContext.Локальная_смета
                    .Where(sm => sm.Шифр == code).ToList();

                dgObject.ItemsSource = listOfSmeta;

				txSumTrud.Text = listOfSmeta
                    .Select(c => c.ТрудоемкостьРаботы)
                    .Sum()
                    .ToString();

				txStoimost.Text = listOfSmeta
                    .Select(c => c.СтоимостьРаботы)
                    .Sum()
                    .ToString();

				var dg = SmetaContext.Договор_подряда
                    .Where(d => d.Шифр == obj.Шифр)
                    .FirstOrDefault();
               
                txtData.Text = dg.ДатаДог.ToString();
                txtNomer.Text = dg.НомерДог.ToString();

                var cu = SmetaContext.Заказчик
                    .Where(c => c.КодЗаказчик == obj.КодЗаказчик)
                    .FirstOrDefault();
				
				txtCustName.Text = cu.НаименованиеЗаказчика;
				txtCustAdress.Text = cu.ЮрАдрес;
				txtCustYNP.Text = cu.УНП;
				txtCustPhone.Text = cu.Тел;
				txtCustMail.Text = cu.ЭлПочта;

				var pr = SmetaContext.Проектная_организация
                    .Where(p => p.КодПроектировщика == obj.КодПроектировщика)
                    .FirstOrDefault();

				txtProectName.Text = pr.НаименованиеПроектиров;
				txtProectAdress.Text = pr.ЮрАдрес;
				txtProectYNP.Text = pr.УНП;
				txtProectPhone.Text = pr.Тел;
				txtProectMail.Text = pr.ЭлПочта;
			}
		}

		private void dgObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            
		}

        private void MenuItem_Click_OpenChart(object sender, RoutedEventArgs e)
		{
            var wa = new Chart(SmetaContext);
            wa.Owner = this;
            wa.ShowDialog();
        }

        private void MenuItem_Click_OpenDirectory(object sender, RoutedEventArgs e)
		{
            var wa = new Directory(SmetaContext);
            wa.Owner = this;
            wa.ShowDialog();
        }

		private void MenuItem_Click_OpenAbout(object sender, RoutedEventArgs e)
		{
			var wa = new About();
			wa.Owner = this;
			wa.ShowDialog();
		}

		private void MenuItem_Click_AddProjectCompany(object sender, RoutedEventArgs e)
		{
			var wa = new AddCustomer(SmetaContext);
			wa.Owner = this;
			wa.ShowDialog();
		}
		private void MenuItem_AddPriceToSmeta(object sender, RoutedEventArgs e)
		{
			var wa = new AddPriceToSmeta (SmetaContext);
			wa.Owner = this;
			wa.ShowDialog();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
		}

		private void MenuItem_Click_SaveDogovor(object sender, RoutedEventArgs e)
		{
            Word.Application word = null;
            Word.Document document = null;

            try
            {
                word = new Word.Application();
                var template = (object)(Environment.CurrentDirectory + "\\template_dogovor.dotx");
                document = word.Documents.Add(ref template, ref oMissing, ref oMissing, ref oMissing);

                SetTemplate(document);

                var date = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}";
                var time = DateTime.Now.ToLongTimeString().Replace(":", ".");
                var documentName = Environment.CurrentDirectory + $"\\Документ от {date} {time}.docx";
                SaveToDisk(document, documentName);
                MessageBox.Show($"Документ создан под именем {documentName}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error");
            }
            finally
            {
                document?.Close(ref oMissing, ref oMissing, ref oMissing);
                word?.Quit(ref oMissing, ref oMissing, ref oMissing);
            }
        }

		private void SetTemplate(Word._Document oDoc)
		{
			object oBookMark = "CustomerName";
			object oBookMark_1 = "DogNomer";
			object oBookMark_2 = "ProectName";
			object oBookMark_3 = "ObjectName";
			object oBookMark_4 = "ObjectAdress";
			object oBookMark_5 = "DogData";
			object oBookMark_6 = "WorkSum";
			object oBookMark_7 = "CustomerAdress";
			object oBookMark_8 = "CustomerYNP";
			object oBookMark_9 = "CustomerPhone";
			object oBookMark_10 = "ProectAdress";
			object oBookMark_11 = "ProectYNP";
			object oBookMark_12 = "ProectPhone";
			object oBookMark_13 = "ProectName_1";
			object oBookMark_14 = "CustomerName_1";
			oDoc.Bookmarks.get_Item(ref oBookMark).Range.Text = txtCustName.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_1).Range.Text = txtNomer.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_2).Range.Text = txtProectName.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_3).Range.Text = txtName.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_4).Range.Text = txtAd.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_5).Range.Text = txtData.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_6).Range.Text = txStoimost.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_7).Range.Text = txtCustAdress.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_8).Range.Text = txtCustYNP.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_9).Range.Text = txtCustPhone.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_10).Range.Text = txtProectAdress.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_11).Range.Text = txtProectYNP.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_12).Range.Text = txtProectPhone.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_13).Range.Text = txtProectName.Text;
			oDoc.Bookmarks.get_Item(ref oBookMark_14).Range.Text = txtCustName.Text;
		}

		private void SaveToDisk(Word._Document oDoc, string filePath)
		{
			object fileName = filePath;
            object fileExtension = Word.WdSaveFormat.wdFormatDocumentDefault;

            oDoc.SaveAs(
                ref fileName,
                ref fileExtension,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing,
                ref oMissing);
		}

		private void AddPriceToSmetaButton_Click(object sender, RoutedEventArgs e)
		{
			var wa = new AddPriceToSmeta(SmetaContext);
			wa.Owner = this;
			wa.ShowDialog();
		}

        private void EditPriceToSmetaButton_Click(object sender, RoutedEventArgs e)
        {
			var sd = dgObject.SelectedItem as Локальная_смета;

			if (sd != null)
			{
				WorkTypeCode = sd.КодВидаРабот;
				Shifr = sd.Шифр;
				WorkCode = sd.КодРаботы;
				StavkaCode = sd.КодСтавки;
				KofCode = sd.Код_коэффициента;
				oldObjem = sd.ФизОбъемРабот.Value;
				EditPriceToSmeta b = new EditPriceToSmeta(SmetaContext, this);
				b.Owner = this;
				b.ShowDialog();
			}
		}


		private void ExportSmetaPIR_Click(object sender, RoutedEventArgs e)
		{
			Excel.Application excel = null;
			Excel._Workbook workbook = null;

			try
			{
				excel = new Excel.Application();

				string myPath = Environment.CurrentDirectory + @"\\shablon_smety.xlsx";
				workbook = excel.Workbooks.Open(myPath);

				var worksheet = excel.Worksheets[(object)"Smeta"];
				var templateTable = worksheet.Names.Item("SmetaTable").RefersToRange;
				var row = templateTable.Row;

				worksheet.Cells[2,5] = txtNomer.Text;
				worksheet.Cells[3, 5] = txtData.Text;
				worksheet.Cells[6, 2] = txtName.Text;
				worksheet.Cells[7, 2] = txtProectName.Text;
				worksheet.Cells[8, 2] = txtCustName.Text;
				worksheet.Cells[7, 5] = txSumTrud.Text;
				worksheet.Cells[8, 5] = txStoimost.Text; 

				 var gridRowCount = dgObject.Items.Count;

				for (int i = 0; i < gridRowCount - 1; i++)
				{
                    templateTable.Insert();
				}

				var targetTable = worksheet.Rows[row].Cells;

				for (int i = 1; i <= gridRowCount; i++)
				{
                    var smeta = dgObject.Items[i - 1] as Локальная_смета;
                    targetTable[i, 1] = smeta.КодВидаРабот;
                    targetTable[i, 2] = smeta.Справочник_расценок.ИмяРаботы;
                    targetTable[i, 4] = smeta.ТрудоемкостьРаботы;
                    targetTable[i, 5] = smeta.СтоимостьРаботы;
				}

				var date = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}";
				var time = DateTime.Now.ToLongTimeString().Replace(":", ".");
				var documentName = Environment.CurrentDirectory + $"\\Смета от {date} {time}.xlsx";

				object fileName = documentName;
				object fileExtension = Excel.XlFileFormat.xlWorkbookDefault;

				workbook.SaveAs(
					fileName,
					fileExtension,
					oMissing,
					oMissing,
					oMissing,
					oMissing,
					Excel.XlSaveAsAccessMode.xlExclusive,
					oMissing,
					oMissing,
					oMissing,
					oMissing);

				MessageBox.Show("Смета создана");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error!");
			}
			finally
			{
				workbook?.Close();
				excel?.Quit();
			}
		}

		private void DgObject_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var sd = dgObject.SelectedItem as Локальная_смета;

			if (sd != null)
			{
				WorkTypeCode = sd.КодВидаРабот;
				Shifr = sd.Шифр;
				WorkCode = sd.КодРаботы;
				StavkaCode = sd.КодСтавки;
				KofCode = sd.Код_коэффициента;
				oldObjem = sd.ФизОбъемРабот.Value;
				EditPriceToSmeta b = new EditPriceToSmeta(SmetaContext, this);
				b.Owner = this;
				b.ShowDialog();
			}
		}
	}
}
