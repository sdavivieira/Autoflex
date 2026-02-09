using Autoflex.Models.Products;

namespace Autoflex.Services.Interfaces
{
	public interface IProductionService
	{
		Task<IEnumerable<ProductionSuggestionDto>> GetSuggestionsAsync();
	}

}
