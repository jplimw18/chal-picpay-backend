using Microsoft.EntityFrameworkCore;
using PicpayChal.App.Data;

namespace PicpayChal.App;

public static class ServicesExtensions
{
    public static void ConfigurePersistenceApp(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("sql_conn");
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conn));
    }
}
