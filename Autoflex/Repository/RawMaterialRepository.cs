using System;
using Autoflex.Models.RawMaterials;
using Autoflex.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autoflex.Repository
{
	public class RawMaterialRepository : IRawMaterialRepository
	{
		private readonly AppDbContext _context;

		public RawMaterialRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<RawMaterial>> GetAllAsync() =>
			await _context.RawMaterials.ToListAsync();

		public async Task<RawMaterial?> GetByIdAsync(int id) =>
			await _context.RawMaterials.FindAsync(id);

		public async Task AddAsync(RawMaterial rawMaterial)
		{
			_context.RawMaterials.Add(rawMaterial);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(RawMaterial rawMaterial)
		{
			_context.RawMaterials.Update(rawMaterial);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(RawMaterial rawMaterial)
		{
			_context.RawMaterials.Remove(rawMaterial);
			await _context.SaveChangesAsync();
		}
		public async Task<RawMaterial?> GetByIdWithAssociationsAsync(int id)
		{
			return await _context.RawMaterials
				.Include(rm => rm.ProductRawMaterials)
				.FirstOrDefaultAsync(rm => rm.Id == id);
		}

	}

}
