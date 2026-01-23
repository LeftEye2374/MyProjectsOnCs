using Microsoft.AspNetCore.Rewrite;

namespace MyWebAppTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseRewriter(new RewriteOptions().AddRedirect("history", "about"));
            app.MapGet("/", () => "Welcome to Contoso!");
            app.MapGet("/about", () => "Contose was fount in 2000");

            app.Run();
        }
    }
}
