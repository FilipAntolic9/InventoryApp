using System;
using System.Threading.Tasks;
using InventoryApp.Application.DTOs;
using InventoryApp.Application.Services;
using InventoryApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApp.Web.Controllers
{
    public class LookupController : Controller
    {
        private readonly ICategoryService _categorySvc;
        private readonly ISupplierService _supplierSvc;

        public LookupController(
            ICategoryService categorySvc,
            ISupplierService supplierSvc)
        {
            _categorySvc = categorySvc;
            _supplierSvc = supplierSvc;
        }

        //  Categories 

        // GET: /Lookup/Categories
        public async Task<IActionResult> Categories()
        {
            var categories = await _categorySvc.GetAllAsync();
            return View(categories);
        }

        // GET: /Lookup/EditCategory/{id?}
        public async Task<IActionResult> EditCategory(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return View(new CategoryEditVm());

            var dto = await _categorySvc.GetByIdAsync(id.Value);
            if (dto == null) return NotFound();

            return View(new CategoryEditVm { Id = dto.Id, Name = dto.Name });
        }

        // POST: /Lookup/EditCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = new CategoryDto { Id = vm.Id, Name = vm.Name };
            if (vm.Id == Guid.Empty)
                await _categorySvc.CreateAsync(dto);
            else
                await _categorySvc.UpdateAsync(dto);

            return RedirectToAction(nameof(Categories));
        }

        // POST: /Lookup/DeleteCategory/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categorySvc.DeleteAsync(id);
            return RedirectToAction(nameof(Categories));
        }

        //  Suppliers 

        // GET: /Lookup/Suppliers
        public async Task<IActionResult> Suppliers()
        {
            var suppliers = await _supplierSvc.GetAllAsync();
            return View(suppliers);
        }

        // GET: /Lookup/EditSupplier/{id?}
        public async Task<IActionResult> EditSupplier(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return View(new SupplierEditVm());

            var dto = await _supplierSvc.GetByIdAsync(id.Value);
            if (dto == null) return NotFound();

            return View(new SupplierEditVm { Id = dto.Id, Name = dto.Name });
        }

        // POST: /Lookup/EditSupplier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSupplier(SupplierEditVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var dto = new SupplierDto { Id = vm.Id, Name = vm.Name };
            if (vm.Id == Guid.Empty)
                await _supplierSvc.CreateAsync(dto);
            else
                await _supplierSvc.UpdateAsync(dto);

            return RedirectToAction(nameof(Suppliers));
        }

        // POST: /Lookup/DeleteSupplier/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            await _supplierSvc.DeleteAsync(id);
            return RedirectToAction(nameof(Suppliers));
        }
    }
}