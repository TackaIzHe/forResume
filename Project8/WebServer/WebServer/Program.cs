using WebServer;
using WebServer.data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

DBContext dbContext = new DBContext();
dbContext.AplicatioConfig();

app.addEndPoints();
app.Run();






