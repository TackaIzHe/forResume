using ClientForms.objects;
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

namespace ClientForms
{
	/// <summary>
	/// Логика взаимодействия для AddUser.xaml
	/// </summary>
	public partial class AddUser : Window
	{
		int id;
		public AddUser()
		{
			InitializeComponent();
			
		}

		public AddUser(User user)
		{
			InitializeComponent();
			id = user.id;
			name.Text = user.name;
			surname.Text = user.surname;
			patronymic.Text = user.patronymic;
			birthday.Text = user.birthday;
			address.Text = user.address;
			department.Text = user.department;
			aboutMe.Text = user.aboutMe;
			button.Content = "Обновить";
		}

		private void submit(object sender, RoutedEventArgs e)
		{
			if (button.Content.ToString() == "Добавить")
			{
				addAction();
			}
			else
			{
				updateAction();
			}
			
		}

		void updateAction()
		{
			var a = MessageBox.Show("Обновить данные?", "Обновление данных", MessageBoxButton.YesNo);
			if (a == MessageBoxResult.Yes)
			{
				HttpMethods.putUser(
					id,
					name.Text,
					surname.Text,
					patronymic.Text,
					birthday.Text,
					address.Text,
					department.Text,
					aboutMe.Text
					);
				this.Close();

			}
		}

		void addAction()
		{
			var a = MessageBox.Show("Добавить данные?", "Добавление данных", MessageBoxButton.YesNo);
			if (a == MessageBoxResult.Yes)
			{
				HttpMethods.postUser(
					name.Text,
					surname.Text,
					patronymic.Text,
					birthday.Text,
					address.Text,
					department.Text,
					aboutMe.Text
					);
				this.Close();

			}
		}
		
    }
}
