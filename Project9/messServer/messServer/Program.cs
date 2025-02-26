using messServer;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.endpoints();

app.Run();
