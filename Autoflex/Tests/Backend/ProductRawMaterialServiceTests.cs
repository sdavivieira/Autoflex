using Autoflex.Models.ProductRawMaterials;
using Autoflex.Models.RawMaterials;
using Autoflex.Repository.Interfaces;
using Autoflex.Services;
using Moq;
using Xunit;

namespace Autoflex.Tests.BackEnd.Services
{
	public class ProductRawMaterialServiceTests
	{
		private readonly Mock<IProductRawMaterialRepository> _repoMock;
		private readonly Mock<IRawMaterialRepository> _rawRepoMock;
		private readonly ProductRawMaterialService _service;

		public ProductRawMaterialServiceTests()
		{
			_repoMock = new Mock<IProductRawMaterialRepository>();
			_rawRepoMock = new Mock<IRawMaterialRepository>();
			_service = new ProductRawMaterialService(_repoMock.Object, _rawRepoMock.Object);
		}

		[Fact]
		public async Task AddAsync_ShouldReduceStockAndAdd()
		{
			var raw = new RawMaterial { Id = 1, StockQuantity = 10 };
			_rawRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(raw);

			var assoc = new ProductRawMaterial { ProductId = 1, RawMaterialId = 1, RequiredQuantity = 5 };
			await _service.AddAsync(assoc);

			_rawRepoMock.Verify(r => r.UpdateAsync(It.Is<RawMaterial>(rm => rm.StockQuantity == 5)), Times.Once);
			_repoMock.Verify(r => r.AddAsync(assoc), Times.Once);
		}

		[Fact]
		public async Task AddAsync_ShouldThrow_WhenQuantityExceedsStock()
		{
			var raw = new RawMaterial { Id = 1, StockQuantity = 2 };
			_rawRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(raw);

			var assoc = new ProductRawMaterial { ProductId = 1, RawMaterialId = 1, RequiredQuantity = 5 };
			await Assert.ThrowsAsync<Exception>(() => _service.AddAsync(assoc));
		}

		[Fact]
		public async Task UpdateAsync_ShouldAdjustStock()
		{
			var existing = new ProductRawMaterial { ProductId = 1, RawMaterialId = 1, RequiredQuantity = 2 };
			var raw = new RawMaterial { Id = 1, StockQuantity = 10 };

			_repoMock.Setup(r => r.GetAsync(1, 1)).ReturnsAsync(existing);
			_rawRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(raw);

			var updated = new ProductRawMaterial { ProductId = 1, RawMaterialId = 1, RequiredQuantity = 4 };
			await _service.UpdateAsync(updated);

			_rawRepoMock.Verify(r => r.UpdateAsync(It.Is<RawMaterial>(rm => rm.StockQuantity == 8)), Times.Once);
			_repoMock.Verify(r => r.UpdateAsync(existing), Times.Once);
		}

		[Fact]
		public async Task DeleteAsync_ShouldRestoreStock()
		{
			var existing = new ProductRawMaterial { ProductId = 1, RawMaterialId = 1, RequiredQuantity = 3 };
			var raw = new RawMaterial { Id = 1, StockQuantity = 5 };

			_repoMock.Setup(r => r.GetAsync(1, 1)).ReturnsAsync(existing);
			_rawRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(raw);

			await _service.DeleteAsync(1, 1);

			_rawRepoMock.Verify(r => r.UpdateAsync(It.Is<RawMaterial>(rm => rm.StockQuantity == 8)), Times.Once);
			_repoMock.Verify(r => r.DeleteAsync(existing), Times.Once);
		}
	}
}
