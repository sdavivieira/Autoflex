using Autoflex.Models.Products;
using Autoflex.Repository.Interfaces;
using Autoflex.Services.Interfaces;

namespace Autoflex.Services
{
	public class ProductionService : IProductionService
	{
		private readonly IProductRepository _repository;

		public ProductionService(IProductRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<ProductionSuggestionDto>> GetSuggestionsAsync()
		{
			var products = (await _repository.GetWithRawMaterialsAsync())
				.OrderByDescending(p => p.Price);

			var result = new List<ProductionSuggestionDto>();

			foreach (var product in products)
			{
				int maxProduction = int.MaxValue;

				foreach (var material in product.RawMaterials)
				{
					var available = material.RawMaterial.StockQuantity;
					var possible = available / material.RequiredQuantity;
					maxProduction = Math.Min(maxProduction, possible);
				}

				if (maxProduction > 0 && maxProduction != int.MaxValue)
				{
					result.Add(new ProductionSuggestionDto
					{
						ProductName = product.Name,
						Quantity = maxProduction,
						TotalValue = maxProduction * product.Price
					});
				}
			}

			return result;
		}
	}

}
