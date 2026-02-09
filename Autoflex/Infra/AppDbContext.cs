using Autoflex.Models.ProductRawMaterials;
using Autoflex.Models.Products;
using Autoflex.Models.RawMaterials;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options) { }

	public DbSet<Product> Products { get; set; }
	public DbSet<RawMaterial> RawMaterials { get; set; }
	public DbSet<ProductRawMaterial> ProductRawMaterials { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ProductRawMaterial>()
			.HasKey(pr => new { pr.ProductId, pr.RawMaterialId });

		modelBuilder.Entity<ProductRawMaterial>()
			.HasOne(pr => pr.Product)
			.WithMany(p => p.RawMaterials)
			.HasForeignKey(pr => pr.ProductId);

		modelBuilder.Entity<ProductRawMaterial>()
			.HasOne(pr => pr.RawMaterial)
			.WithMany(r => r.ProductRawMaterials)
			.HasForeignKey(pr => pr.RawMaterialId);
	}
}
