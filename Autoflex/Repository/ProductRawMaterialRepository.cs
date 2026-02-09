using Autoflex.Models.ProductRawMaterials;
using Autoflex.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autoflex.Repository
{
	public class ProductRawMaterialRepository : IProductRawMaterialRepository
	{
		private readonly AppDbContext _context;

		public ProductRawMaterialRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(ProductRawMaterial entity)
		{
			_context.ProductRawMaterials.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(ProductRawMaterial entity)
		{
			_context.ProductRawMaterials.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ProductRawMaterial>> GetAllAsync()
		{
			return await _context.ProductRawMaterials
				.Include(pr => pr.Product)
				.Include(pr => pr.RawMaterial)
				.ToListAsync();
		}

		public async Task<ProductRawMaterial?> GetAsync(int productId, int rawMaterialId)
		{
			return await _context.ProductRawMaterials
				.Include(pr => pr.Product)
				.Include(pr => pr.RawMaterial)
				.FirstOrDefaultAsync(pr => pr.ProductId == productId && pr.RawMaterialId == rawMaterialId);
		}

		public async Task<ProductRawMaterial?> GetByIdAsync(int id)
		{
			return await _context.ProductRawMaterials
				.Include(pr => pr.Product)
				.Include(pr => pr.RawMaterial)
				.FirstOrDefaultAsync(pr => pr.RawMaterialId == id);
		}

		public async Task UpdateAsync(ProductRawMaterial entity)
		{
			_context.ProductRawMaterials.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
