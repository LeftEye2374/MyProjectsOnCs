using WebAppForBoilerplate.Service;

namespace WebAppForBoilerplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IPersonService, PersonService>();

            var app = builder.Build();

            app.MapGet("/",
                (IPersonService service) =>
                {
                    return $"Hello, {service.GetPersonName()}!";
                });

            app.Run();
        }
    }
}
