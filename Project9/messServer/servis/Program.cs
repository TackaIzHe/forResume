using MimeKit;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

using MailKit.Net.Smtp;
using System.Text.Json;

class Program 
{ 
	static void Main(string[] args)
	{
		get();
		Console.ReadLine();
		
	}

	async static void get()
	{
		var factory = new ConnectionFactory { HostName = "localhost" };
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
			arguments: null);

		Console.WriteLine(" [*] Waiting for messages.");

		var consumer = new AsyncEventingBasicConsumer(channel);
		consumer.ReceivedAsync += (model, ea) =>
		{
			var body = ea.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);

			SendEmailAsync(message);
			Console.WriteLine($" [x] Received {message}");
			return Task.CompletedTask;
		};

		await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();
	}

	async static void send(string code)
	{
		var factory = new ConnectionFactory { HostName = "localhost" };
		using var connection = await factory.CreateConnectionAsync();
		using var channel = await connection.CreateChannelAsync();

		await channel.QueueDeclareAsync(queue: "code", durable: false, exclusive: false, autoDelete: false,
			arguments: null);

		string message = code;
		var body = Encoding.UTF8.GetBytes(message);

		await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "code", body: body);
		Console.WriteLine($" [x] Sent {message}");

		Console.WriteLine(" Press [enter] to exit.");
		Console.ReadLine();
	}


	async static void SendEmailAsync(string email)
	{

		string smtpServer = "smtp.yandex.ru";
		int smtpPort = 25;
		string smtpUsername = ""; //loggin
		string smtpPassword = ""; //password
		string code = new Random().Next(1000,9999).ToString();

		using var emailMessage = new MimeMessage();

		emailMessage.From.Add(new MailboxAddress("Администрация сайта", smtpUsername));
		emailMessage.To.Add(new MailboxAddress("", email)); //получатель
		emailMessage.Subject = "Код подтверждения";
		emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
		{
			Text = code
		};

		using (var client = new SmtpClient())
		{
			await client.ConnectAsync(smtpServer, smtpPort, false);
			await client.AuthenticateAsync(smtpUsername, smtpPassword);
			await client.SendAsync(emailMessage);

			await client.DisconnectAsync(true);
			Console.WriteLine("OK");
		}
		serialize(email, code);
	}

	static void serialize(string email,string code)
	{
		email = email.Replace('@','0');
		code q = new code { Key = email, Value = code };
		var a =JsonSerializer.Serialize(q);
		Console.WriteLine(a);
		send(a);
	}
	class code
	{
		public string Key { get; set; }
		public string Value { get; set; }
	}


}