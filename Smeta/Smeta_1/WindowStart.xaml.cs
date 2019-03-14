﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Ninject;
using System.Data.Entity;
using Microsoft.Win32;
using System.Xml.Linq;
using Smeta_DB;

namespace Smeta_1
{
	/// <summary>
	/// Interaction logic for WindowStart.xaml
	/// </summary>
	public partial class WindowStart : MetroWindow
	{
		//static string role = MainWindow.sRole; // Код заявки
		SmetaEntities context = new SmetaEntities();


		public WindowStart()
		{
			InitializeComponent();
			if (MainWindow.sRole == "admin")
			{
				btnObject.IsEnabled = true;
				btnChart.IsEnabled = true;
				btnDirectory.IsEnabled = true;
				btnAbout.IsEnabled = true;
				btnSmeta.IsEnabled = false;

			}
			if (MainWindow.sRole == "user")
			{
				btnObject.IsEnabled = true;
				btnChart.IsEnabled = true;
				btnDirectory.IsEnabled = true;
				btnAbout.IsEnabled = true;
				btnSmeta.IsEnabled = true;
			}
			//var Mainwindow = new MainWindow();

			//Mainwindow.ClickAdmin += MainWindow_ClickAdmin;
			//Mainwindow.ClickUser += MainWindow_ClickUser;
			//Mainwindow.Show();

		}
		//private void MainWindow_ClickAdmin(object sender, EventArgs e)
		//{
		//	MessageBox.Show("Вы вошли в систему с ролью Администратор");
		//	btnObject.IsEnabled = true;
		//	btnChart.IsEnabled = true;
		//	btnDirectory.IsEnabled = true;
		//	btnAbout.IsEnabled = true;
		//	btnSmeta.IsEnabled = false;


		//	//Directory mw = new Directory();
		//	//mw.Show();
		//	//Close();

		//}
		//private void MainWindow_ClickUser(object sender, EventArgs e)
		//{
		//	MessageBox.Show("Вы вошли в систему с ролью Пользователь");
		//	btnObject.IsEnabled = true;
		//	btnChart.IsEnabled = true;
		//	btnDirectory.IsEnabled = true;
		//	btnAbout.IsEnabled = true;
		//	btnSmeta.IsEnabled = true;

		//	//About ab = new About();
		//	//ab.Show();
		//	//Close();

		//}
		//private void Create_Click(object sender, RoutedEventArgs e)
		//{

		//}

		private void btnOpenObject_Click(object sender, RoutedEventArgs e)
		{
			Object wa = new Object();
			if (wa.ShowDialog() == true)
			{

			}
		}
		private void btnOpenChart_Click(object sender, RoutedEventArgs e)
		{

			Chart wa = new Chart();
			if (wa.ShowDialog() == true)
			{

			}
		}
		private void btnOpenDirectory_Click(object sender, RoutedEventArgs e)
		{

			Directory wa = new Directory();
			if (wa.ShowDialog() == true)
			{

			}
		}
		private void btnOpenAbout_Click(object sender, RoutedEventArgs e)
		{

			About wa = new About();
			if (wa.ShowDialog() == true)
			{

			}
		}
		private void btnCreateSmeta_Click(object sender, RoutedEventArgs e)
		{

			CreateSmeta wa = new CreateSmeta();
			if (wa.ShowDialog() == true)
			{

			}
		}

	}
}