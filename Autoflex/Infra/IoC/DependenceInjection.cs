using Autoflex.Repository;
using Autoflex.Repository.Interfaces;
using Autoflex.Services;
using Autoflex.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Autoflex.Infra.IoC
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(
			configuration.GetConnectionString("PostgresConnection")
		));


			services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IRawMaterialRepository, RawMaterialRepository>();
			services.AddScoped<IProductRawMaterialRepository, ProductRawMaterialRepository>();

			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IRawMaterialService, RawMaterialService>();
			services.AddScoped<IProductRawMaterialService, ProductRawMaterialService>();
			services.AddScoped<IProductionService, ProductionService>();

			return services;
		}
	}
}
