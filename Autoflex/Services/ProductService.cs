using Autoflex.Models.Products;
using Autoflex.Repository.Interfaces;
using Autoflex.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autoflex.Services
{
    public class ProductService : IProductService
	{
		private readonly IProductRepository _repository;

		public ProductService(IProductRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Product>> GetAllAsync() =>
			await _repository.GetAllAsync();

		public async Task<Product?> GetByIdAsync(int id) =>
			await _repository.GetByIdAsync(id);

		public async Task UpdateAsync(Product product) =>
			await _repository.UpdateAsync(product);

        public async Task AddAsync(Product entity)
        {
			await _repository.AddAsync(entity);
        }

		public async Task DeleteAsync(int id)
		{
			var product = await _repository.GetByIdAsync(id);
			if (product is null)
				throw new Exception("Product not found");

			await _repository.DeleteAsync(product);
		}

		public async Task<IEnumerable<Product?>> GetByIdWithRawMaterialsAsync()
		{
			var product = await _repository.GetWithRawMaterialsAsync();
			return product;

		}
		public async Task<Product?> GetByIdWithRawMaterialsAsync(int id)
		{
			var product = await _repository.GetByIdWithRawMaterialsAsync(id);

			return product;
		}

	}

}
