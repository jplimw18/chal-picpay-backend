using Microsoft.EntityFrameworkCore;
using PicpayChal.App.Data;
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
}
