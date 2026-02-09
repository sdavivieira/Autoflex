using System;
using Autoflex.Models.ProductRawMaterials;
using Autoflex.Repository.Interfaces;
using Autoflex.Services.Interfaces;

namespace Autoflex.Services
{
	public class ProductRawMaterialService : IProductRawMaterialService
	{
		private readonly IProductRawMaterialRepository _repository;
		private readonly IRawMaterialRepository _rawMaterialRepository;

		public ProductRawMaterialService(
			IProductRawMaterialRepository repository,
			IRawMaterialRepository rawMaterialRepository)
		{
			_repository = repository;
			_rawMaterialRepository = rawMaterialRepository;
		}

		public async Task AddAsync(ProductRawMaterial entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var rawMaterial = await _rawMaterialRepository.GetByIdAsync(entity.RawMaterialId);
			if (rawMaterial == null)
				throw new Exception("Raw material not found");

			if (entity.RequiredQuantity > rawMaterial.StockQuantity)
				throw new Exception($"Cannot assign {entity.RequiredQuantity}. Only {rawMaterial.StockQuantity} available in stock.");

			rawMaterial.StockQuantity -= entity.RequiredQuantity;
			await _rawMaterialRepository.UpdateAsync(rawMaterial);

			await _repository.AddAsync(entity);
		}

		public async Task UpdateAsync(ProductRawMaterial entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));

			var existing = await _repository.GetAsync(entity.ProductId, entity.RawMaterialId);
			if (existing == null)
				throw new Exception("Association not found");

			var rawMaterial = await _rawMaterialRepository.GetByIdAsync(entity.RawMaterialId);
			if (rawMaterial == null)
				throw new Exception("Raw material not found");

			int diff = entity.RequiredQuantity - existing.RequiredQuantity;

			if (diff > 0 && diff > rawMaterial.StockQuantity)
				throw new Exception($"Cannot increase quantity by {diff}. Only {rawMaterial.StockQuantity} available in stock.");

			rawMaterial.StockQuantity -= diff;
			await _rawMaterialRepository.UpdateAsync(rawMaterial);

			existing.RequiredQuantity = entity.RequiredQuantity;
			await _repository.UpdateAsync(existing);
		}

		public async Task DeleteAsync(int productId, int rawMaterialId)
		{
			var entity = await _repository.GetAsync(productId, rawMaterialId);
			if (entity == null)
				throw new Exception("Association not found");

			var rawMaterial = await _rawMaterialRepository.GetByIdAsync(rawMaterialId);
			if (rawMaterial != null)
			{
				rawMaterial.StockQuantity += entity.RequiredQuantity;
				await _rawMaterialRepository.UpdateAsync(rawMaterial);
			}

			await _repository.DeleteAsync(entity);
		}

		public Task<IEnumerable<ProductRawMaterial>> GetAllAsync() =>
			_repository.GetAllAsync();

		public Task<ProductRawMaterial?> GetByIdAsync(int id) =>
			_repository.GetByIdAsync(id);
	}
}
