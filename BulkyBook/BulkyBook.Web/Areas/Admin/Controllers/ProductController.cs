using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility.SaveFile.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISaveFile _saveFile;

    public ProductController(IUnitOfWork unitOfWork, ISaveFile saveFile)
    {
        _unitOfWork = unitOfWork;
        _saveFile = saveFile;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        var productVM = new ProductVM
        {
            Product = new Product(),
            CategoryList = _unitOfWork.CategoryRepository.GetAll()
            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }),
            CoverTypeList = _unitOfWork.CoverTypeRepository.GetAll()
            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() })
        };

        if (id != null && id != 0)
            productVM.Product = _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id);

        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM productVM, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            if(imageFile != null)
            {
                var uploadPath = @"images\products";
                
                if (!string.IsNullOrWhiteSpace(productVM.Product.ImageUrl))
                    _saveFile.Update(imageFile, productVM.Product.ImageUrl, uploadPath);
                else
                    _saveFile.Save(imageFile, uploadPath);

                productVM.Product.ImageUrl = _saveFile.GetLastSavedFilePath();
            }

            if(productVM.Product.Id == 0)
                _unitOfWork.ProductRepository.Add(productVM.Product);
            else
                _unitOfWork.ProductRepository.Update(productVM.Product);

            _unitOfWork.Save();
            TempData["success"] = "Product created successfuly";
            
            return RedirectToAction("Index");
        }

        return View(productVM);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var product = _unitOfWork.ProductRepository.GetFirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Product product)
    {
        _unitOfWork.ProductRepository.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "Product deleted successfuly";
        return RedirectToAction("Index");
    }

    #region API Calls

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _unitOfWork.ProductRepository.GetAll("Category,CoverType");
        return Json(new {data = products});
    }

    #endregion
}
