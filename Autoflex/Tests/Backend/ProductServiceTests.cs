using Autoflex.Models.Products;
using Autoflex.Repository.Interfaces;
using Autoflex.Services;
using Moq;
using Xunit;

namespace Autoflex.Tests.BackEnd.Services
{
	public class ProductServiceTests
	{
		private readonly Mock<IProductRepository> _repoMock;
		private readonly ProductService _service;

		public ProductServiceTests()
		{
			_repoMock = new Mock<IProductRepository>();
			_service = new ProductService(_repoMock.Object);
		}

		[Fact]
		public async Task AddAsync_ShouldCallRepositoryAdd()
		{
			var product = new Product { Name = "Teste", Price = 100 };
			await _service.AddAsync(product);
			_repoMock.Verify(r => r.AddAsync(product), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_ShouldThrowException_WhenProductNotFound()
		{
			_repoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);
			await Assert.ThrowsAsync<Exception>(() => _service.DeleteAsync(1));
		}

		[Fact]
		public async Task UpdateAsync_ShouldCallRepositoryUpdate()
		{
			var product = new Product { Id = 1, Name = "Produto", Price = 50 };
			await _service.UpdateAsync(product);
			_repoMock.Verify(r => r.UpdateAsync(product), Times.Once);
		}

		[Fact]
		public async Task GetAllAsync_ShouldReturnProducts()
		{
			_repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product> { new Product() });
			var result = await _service.GetAllAsync();
			Assert.Single(result);
		}
	}
}
