using Azure.Core;
using Azure;
using Microsoft.Extensions.Options;
using RepairPC1.Data;
using RepairPC1.Endpoints;
using RepairPC1.Extensions;
using RepairPC1.Hashing;
using RepairPC1.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
services.AddApiAuthentication(configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();