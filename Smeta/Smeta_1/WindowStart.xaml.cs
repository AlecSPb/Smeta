﻿using System.Windows;
using MahApps.Metro.Controls;
using Smeta_DB;

namespace Smeta_1
{
    /// <summary>
    /// Interaction logic for WindowStart.xaml
    /// </summary>
    public partial class WindowStart : MetroWindow
	{
        public SmetaEntities SmetaContext { get; set; }

        public WindowStart(SmetaEntities context)
		{
			InitializeComponent();
            
            SmetaContext = context;

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
		}

		private void btnOpenObject_Click(object sender, RoutedEventArgs e)
		{
			Object wa = new Object(SmetaContext);
			if (wa.ShowDialog() == true)
			{

			}
		}

		private void btnOpenChart_Click(object sender, RoutedEventArgs e)
		{
			Chart wa = new Chart(SmetaContext);
			if (wa.ShowDialog() == true)
			{

			}
		}

		private void btnOpenDirectory_Click(object sender, RoutedEventArgs e)
		{
			Directory wa = new Directory(SmetaContext);
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
			CreateSmeta wa = new CreateSmeta(SmetaContext);
			if (wa.ShowDialog() == true)
			{

			}
		}
	}
}
