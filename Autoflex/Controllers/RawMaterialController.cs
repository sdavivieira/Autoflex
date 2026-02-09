using Autoflex.Models.RawMaterials;
using Autoflex.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Autoflex.Controllers
{
	public class RawMaterialController : Controller
	{
		private readonly IRawMaterialService _service;

		public RawMaterialController(IRawMaterialService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var materials = await _service.GetAllAsync();
			return View(materials);
		}

		public IActionResult Create() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(RawMaterial rawMaterial)
		{
			if (!ModelState.IsValid)
				return View(rawMaterial);

			try
			{
				await _service.AddAsync(rawMaterial);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Create));
			}
		}

		public async Task<IActionResult> Edit(int id)
		{
			var rawMaterial = await _service.GetByIdAsync(id);
			if (rawMaterial is null)
				return NotFound();

			return View(rawMaterial);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(RawMaterial rawMaterial)
		{
			if (!ModelState.IsValid)
				return View(rawMaterial);

			try
			{
				await _service.UpdateAsync(rawMaterial);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Edit), new { id = rawMaterial.Id });
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _service.DeleteAsync(id);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}
		}
	}
}
