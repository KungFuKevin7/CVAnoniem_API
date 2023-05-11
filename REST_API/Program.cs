using Microsoft.AspNetCore.Mvc;
using REST_API;
using System.Net.Http;

public class Program 
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        //app.Map
        //app.MapGet("/", () => "Hello World!");

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }



}