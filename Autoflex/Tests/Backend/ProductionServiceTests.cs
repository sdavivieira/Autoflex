using Autoflex.Models.ProductRawMaterials;
using Autoflex.Models.Products;
using Autoflex.Models.RawMaterials;
using Autoflex.Repository.Interfaces;
using Autoflex.Services;
using Moq;
using Xunit;

namespace Autoflex.Tests.BackEnd.Services
{
	public class ProductionServiceTests
	{
		[Fact]
		public async Task GetSuggestionsAsync_ShouldReturnCorrectQuantity()
		{
			var repoMock = new Mock<IProductRepository>();
			var products = new List<Product>
			{
				new Product
				{
					Name = "P1",
					Price = 10,
					RawMaterials = new List<ProductRawMaterial>
					{
						new ProductRawMaterial
						{
							RequiredQuantity = 2,
							RawMaterial = new RawMaterial { StockQuantity = 6 }
						}
					}
				}
			};

			repoMock.Setup(r => r.GetWithRawMaterialsAsync()).ReturnsAsync(products);
			var service = new ProductionService(repoMock.Object);

			var result = (await service.GetSuggestionsAsync()).ToList();

			Assert.Single(result);
			Assert.Equal(3, result[0].Quantity);
			Assert.Equal(30, result[0].TotalValue);
		}

		[Fact]
		public async Task GetSuggestionsAsync_ShouldIgnoreProductsWithNoStock()
		{
			var repoMock = new Mock<IProductRepository>();
			var products = new List<Product>
			{
				new Product
				{
					Name = "P1",
					Price = 10,
					RawMaterials = new List<ProductRawMaterial>
					{
						new ProductRawMaterial
						{
							RequiredQuantity = 2,
							RawMaterial = new RawMaterial { StockQuantity = 0 }
						}
					}
				}
			};

			repoMock.Setup(r => r.GetWithRawMaterialsAsync()).ReturnsAsync(products);
			var service = new ProductionService(repoMock.Object);

			var result = await service.GetSuggestionsAsync();

			Assert.Empty(result);
		}
	}
}
