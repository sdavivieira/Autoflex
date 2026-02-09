using Autoflex.Models.Products;
using Autoflex.Services.Interfaces;
using Autoflex.Models.RawMaterials;
using Microsoft.AspNetCore.Mvc;

namespace Autoflex.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IRawMaterialService _rawMaterialService;

		public ProductController(IProductService productService, IRawMaterialService rawMaterialService)
		{
			_productService = productService;
			_rawMaterialService = rawMaterialService;
		}

		public async Task<IActionResult> Index()
		{
			var products = await _productService.GetAllAsync();
			return View(products);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(Product product)
		{
			if (!ModelState.IsValid)
				return View(product);

			try
			{
				await _productService.AddAsync(product);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			var product = await _productService.GetByIdWithRawMaterialsAsync(id);
			if (product == null)
				return NotFound();

			var rawMaterials = await _rawMaterialService.GetAllAsync();
			ViewBag.AvailableRawMaterials = rawMaterials.ToList();

			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product product)
		{
			if (!ModelState.IsValid)
			{
				var rawMaterials = await _rawMaterialService.GetAllAsync();
				ViewBag.AvailableRawMaterials = rawMaterials.ToList();
				return View(product);
			}

			try
			{
				await _productService.UpdateAsync(product);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				var rawMaterials = await _rawMaterialService.GetAllAsync();
				ViewBag.AvailableRawMaterials = rawMaterials.ToList();
				return View(product);
			}

			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _productService.DeleteAsync(id);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
			}

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> GetRawMaterials(int id)
		{
			var product = await _productService.GetByIdWithRawMaterialsAsync(id);
			if (product is null) return NotFound();

			return PartialView("_RawMaterialsGrid", product);
		}
	}
}
