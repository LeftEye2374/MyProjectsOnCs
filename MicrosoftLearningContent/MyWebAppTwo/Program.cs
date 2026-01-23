using Microsoft.AspNetCore.Rewrite;

namespace MyWebAppTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Response.StatusCode}");
                await next();
            });

            app.UseRewriter(new RewriteOptions().AddRedirect("history", "about"));

            app.MapGet("/", () => "Hello World!");
            app.MapGet("/about", () => "Contoso was founded in 2000.");

            app.Run();
        }
    }
}
