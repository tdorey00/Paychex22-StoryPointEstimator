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
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IRoomDataAccess, RoomDataAccess>();
builder.Services.AddMudServices();
//builder.Services.AddBlazoredSessionStorage();
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

app.MapHub<VotingHub>("VotingHub");

app.Run();
