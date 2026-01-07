using MassTransit;
using Microsoft.EntityFrameworkCore;
using PicpayChal.App.Data;
using PicpayChal.App.Messaging.Consumer;
using PicpayChal.App.Repositories;
using PicpayChal.App.Repositories.Interfaces;
using PicpayChal.App.Services.External;
using Refit;

namespace PicpayChal.App;

public static class ServicesExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("sql_conn");
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void ConfigureRefitClient(this IServiceCollection services)
    {
        var authApiUri = new Uri("https://util.devi.tools/api");
        var notifyApiUri = new Uri("https://util.devi.tools/api/");

        services.AddRefitClient<IAuthorizationApi>()
            .ConfigureHttpClient(c => c.BaseAddress = authApiUri);

        services.AddRefitClient<INotificationApi>()
            .ConfigureHttpClient(c => c.BaseAddress = notifyApiUri);
    }


    public static void ConfigureMessagingQueue(this IServiceCollection services)
    {
         services.AddMassTransit(opt =>
         {
            opt.AddConsumer<NotificationConsumer>();

            opt.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                   h.Username("guest");
                   h.Password("guest"); 
                });

                cfg.UseMessageRetry(r => r.Interval(10, TimeSpan.FromSeconds(5)));

                cfg.ConfigureEndpoints(context);
            }); 
         });
    }
}
