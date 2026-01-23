using MyWebApp.Service;

namespace MyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IWelcomeService,WelcomeService>();

            var app = builder.Build();

            app.MapGet("/", (IWelcomeService service) => service.GetWelcomeMessage());
            

            app.Run();
        }
    }
}
