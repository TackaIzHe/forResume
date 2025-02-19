using ClientForms.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClientForms
{
	public static class HttpMethods
	{
		static HttpClient connect = new HttpClient();


		public async static void getUsers(this ListView listUsers)
		{

			var getAsync = await connect.GetAsync("http://localhost:5111/Users/");
			string s = getAsync.Content.ReadAsStringAsync().Result;

			Console.WriteLine(getAsync.StatusCode);

			List<User> users = JsonSerializer.Deserialize<List<User>>(s);
			listUsers.ItemsSource = users;

		}

		public async static void postUser(
			string name,
			string surname,
			string patronymic,
			string birthday,
			string address,
			string department,
			string aboutMe
			)
		{
			User user = new User
			{
				name = name,
				surname = surname,
				patronymic = patronymic,
				birthday = birthday,
				address = address,
				department = department,
				aboutMe = aboutMe

			};
			var jsonUser = JsonSerializer.Serialize(user);
			var httpContent = new StringContent(jsonUser);
			httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
			var postUser = await connect.PostAsync("http://localhost:5111/AddUser/", httpContent);
			Console.WriteLine(postUser.StatusCode);
		}

		public async static void putUser(
			int id,
			string name,
			string surname,
			string patronymic,
			string birthday,
			string address,
			string department,
			string aboutMe
			)
		{
			User user = new User
			{
				id = id,
				name = name,
				surname = surname,
				patronymic = patronymic,
				birthday = birthday,
				address = address,
				department = department,
				aboutMe = aboutMe

			};
			var jsonUser = JsonSerializer.Serialize(user);
			var httpContent = new StringContent(jsonUser);
			httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
			var putUser = await connect.PutAsync("http://localhost:5111/UpdateUser/", httpContent);
			Console.WriteLine(putUser.StatusCode);
		}

		public async static void delUser(int userId)
		{
			var delUser = await connect.DeleteAsync($"http://localhost:5111/DeleteUser/{userId}");
		}
	}
}
