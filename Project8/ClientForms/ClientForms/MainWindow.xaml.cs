using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Net.Http;

using System.Windows;
using ClientForms.objects;
using System.Windows.Controls;
using System.ComponentModel;
using System.Threading.Tasks;



namespace ClientForms
{

	public partial class MainWindow : Window
	{
		User selectedUser;

		public MainWindow()
		{
			InitializeComponent();
			Users.getUsers();
		}

		private async void getAction(object sender, EventArgs e)
		{
			await Task.Delay(500);
			Users.getUsers();
		}

		private void addAction(object sender, RoutedEventArgs e)
		{
			var addUsers = new AddUser();
			addUsers.Show();
			addUsers.Closed += getAction;
		}

		private void delAction(object sender, RoutedEventArgs e)
		{
			var state = MessageBox.Show("Удалить Элемент?", "Удаление элемента", MessageBoxButton.YesNo);
			if(state == MessageBoxResult.Yes)
			{
				HttpMethods.delUser(selectedUser.id);
				getAction(sender, e);
			}

		}

		private void updAction(object sender, RoutedEventArgs e)
		{
			var addUsers = new AddUser(selectedUser);
			addUsers.Show();
			addUsers.Closed += getAction;
		}

		private void focusElement(object sender, SelectionChangedEventArgs e)
		{
			if (Users.SelectedItem is User user)
				selectedUser = user;
		}
	}
}
