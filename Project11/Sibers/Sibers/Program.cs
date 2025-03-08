using Microsoft.AspNetCore.Mvc;
using Sibers.Data;
using Sibers.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var configuration = builder.Configuration;

services.AddRazorPages();
services.AddAuthentication(configuration);
services.AddAuthorization(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.map();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

new DBcontext();

app.Run();

// the Data folder contains the database configuration,
// the Hashing folder contains the creation of Claims and password hashing,
// the Objects folder contains the representation of objects,
// the Pages folder contains the site pages,
// the Services folder contains services such as authorization, authentication and points for ajax requests