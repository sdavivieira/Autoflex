using System;
using Autoflex.Models.Products;
using Autoflex.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autoflex.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly AppDbContext _context;

		public ProductRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAllAsync() =>
			await _context.Products.ToListAsync();

		public async Task<Product?> GetByIdAsync(int id) =>
			await _context.Products.FindAsync(id);

		public async Task<Product?> GetByIdWithRawMaterialsAsync(int id) =>
		await _context.Products
			.Include(p => p.RawMaterials)
				.ThenInclude(pr => pr.RawMaterial)
			.FirstOrDefaultAsync(p => p.Id == id);

		public async Task<IEnumerable<Product>> GetWithRawMaterialsAsync()
		{
			return await _context.Products
				.Include(p => p.RawMaterials) 
					.ThenInclude(pr => pr.RawMaterial)
				.ToListAsync();
		}

		public async Task AddAsync(Product product)
		{
			_context.Products.Add(product);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Product product)
		{
			_context.Products.Update(product);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Product product)
		{
			_context.Products.Remove(product);
			await _context.SaveChangesAsync();
		}

    }

}
