using Autoflex.Models.RawMaterials;
using Autoflex.Repository.Interfaces;
using Autoflex.Services.Interfaces;

namespace Autoflex.Services
{
	public class RawMaterialService : IRawMaterialService
	{
		private readonly IRawMaterialRepository _rawMaterialRepository;

		public RawMaterialService(IRawMaterialRepository rawMaterialRepository)
		{
			_rawMaterialRepository = rawMaterialRepository;
		}

		public async Task AddAsync(RawMaterial rawMaterial)
		{
			if (rawMaterial == null)
				throw new ArgumentNullException(nameof(rawMaterial));

			await _rawMaterialRepository.AddAsync(rawMaterial);
		}

		public async Task DeleteAsync(int id)
		{
			var existing = await _rawMaterialRepository.GetByIdWithAssociationsAsync(id);
			if (existing == null)
				throw new Exception("Raw Material not found");

			if (existing.ProductRawMaterials != null && existing.ProductRawMaterials.Any())
				throw new Exception("Cannot delete this raw material because it is used in one or more products.");

			await _rawMaterialRepository.DeleteAsync(existing);
		}


		public async Task<IEnumerable<RawMaterial>> GetAllAsync()
		{
			return await _rawMaterialRepository.GetAllAsync();
		}

		public async Task<RawMaterial?> GetByIdAsync(int id)
		{
			return await _rawMaterialRepository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(RawMaterial rawMaterial)
		{
			if (rawMaterial == null)
				throw new ArgumentNullException(nameof(rawMaterial));

			var existing = await _rawMaterialRepository.GetByIdWithAssociationsAsync(rawMaterial.Id);
			if (existing == null)
				throw new Exception("Raw Material not found");

			int minRequiredStock = existing.ProductRawMaterials?.Sum(pr => pr.RequiredQuantity) ?? 0;

			if (rawMaterial.StockQuantity < minRequiredStock)
				throw new Exception($"Cannot set stock below {minRequiredStock}, which is the total required by associated products.");

			existing.Name = rawMaterial.Name;
			existing.StockQuantity = rawMaterial.StockQuantity;

			await _rawMaterialRepository.UpdateAsync(existing);
		}

	}
}
