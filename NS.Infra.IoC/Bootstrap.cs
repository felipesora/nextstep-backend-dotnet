using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NS.Infra.Data.AppData;
using NS.Domain.Interfaces;
using NS.Infra.Data.Repositories;
using NS.Application.Interfaces;
using NS.Application.Services;
using NS.Infra.Data.HealthCheck;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NS.Infra.IoC;

public class Bootstrap
{
    public static void AddIoC(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options => {
            options.UseOracle(configuration.GetConnectionString("Oracle"));
        });

        services.AddHealthChecks()
            // Liveness: verifica api “estou no ar”
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
            //Readiness: verifica se o mongo "esta online"
            .AddCheck<OracleHealthCheck>("oracle_ef_query", tags: new[] { "ready" });

        services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        services.AddTransient<IUsuarioService, UsuarioService>();

        services.AddTransient<ITrilhaRepository, TrilhaRepository>();
        services.AddTransient<ITrilhaService, TrilhaService>();

        services.AddTransient<INotaTrilhaRepository, NotaTrilhaRepository>();
        services.AddTransient<INotaTrilhaService, NotaTrilhaService>();

        services.AddTransient<IConteudoRepository, ConteudoRepository>();
        services.AddTransient<IConteudoService, ConteudoService>();

        services.AddTransient<IFormularioRepository, FormularioRepository>();
        services.AddTransient<IFormularioService, FormularioService>();
    }
}
