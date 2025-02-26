using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace messServer
{
	public static class Endpoints
	{
		public static Dictionary<string,string> list = new Dictionary<string,string>();
		public static void endpoints(this IEndpointRouteBuilder app)
		{
			app.MapGet("/", async (context) =>
			{
				context.Response.ContentType = "text/html";
				await context.Response.SendFileAsync("Index.html");

			});
			app.MapPost("/", async (context) => 
			{
				var email = context.Request.Form["email"].ToString();
				con(email);
				context.Response.Cookies.Append("email", email.Replace('@','0'));
				context.Response.Redirect("/writecode");

			});

			app.MapGet("/writecode", async (context) => 
			{
				context.Response.ContentType = "text/html";
				await context.Response.SendFileAsync("WriteCode.html");
				get();
			});
			app.MapPost("/writecode", async (context) =>
			{
				var code = context.Request.Form["code"].ToString();
				var code1 = list.ToList().Find(x => x.Key== context.Request.Cookies["email"]).Value;
				if(code == code1)
				{
					context.Response.Redirect("/OK");
				}
				Console.WriteLine(context.Request.Form["code"]);
			});
			app.MapGet("/OK",async (context) =>
			{
				context.Response.ContentType="text/html; charset=utf8";
				await context.Response.WriteAsync("<h1>Удачно</h1>");
			});
		}

		async static void con(string email)
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();

			await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
				arguments: null);

			string message = email;
			var body = Encoding.UTF8.GetBytes(message);

			await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
			Console.WriteLine($" [x] Sent {message}");

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}

		async static void get()
		{
			var factory = new ConnectionFactory { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();

			await channel.QueueDeclareAsync(queue: "code", durable: false, exclusive: false, autoDelete: false,
				arguments: null);

			Console.WriteLine(" [*] Waiting for messages.");

			var consumer = new AsyncEventingBasicConsumer(channel);
			consumer.ReceivedAsync += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				var code =JsonSerializer.Deserialize<code>(message);

				list.Add(code.Key,code.Value);

				Console.WriteLine($" [x] Received {message}");
				return Task.CompletedTask;
			};

			await channel.BasicConsumeAsync("code", autoAck: true, consumer: consumer);

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}
	}

	class code
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}
}
