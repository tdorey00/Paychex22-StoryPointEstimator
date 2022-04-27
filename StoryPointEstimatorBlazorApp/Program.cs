using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SqlDataAccessLib;
using MudBlazor.Services;
using StoryPointEstimatorBlazorApp.Models;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.ResponseCompression;
using StoryPointEstimatorBlazorApp.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>(); //SQL Access Class
builder.Services.AddTransient<IRoomDataAccess, RoomDataAccess>(); //Uses SQL Access Class to put relevant data into the database
builder.Services.AddMudServices(); //Mudblazor Services
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
               new[] { "application/octet-stream" });
});



var app = builder.Build();
app.UseResponseCompression();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapHub<VotingHub>("VotingHub"); //SignalR Hub users are connected to

app.Run();
