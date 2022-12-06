using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var objProductList = _unitOfWork.ProductRepository.GetAll();
        return View(objProductList);
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

        if (id == null || id == 0)
        {
            
            return View(productVM);
        }
        else
        {

        }

        return View(productVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            var wwwRootPath = _webHostEnvironment.WebRootPath;
            if(file != null)
            {
                var fileName =  Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                using var filestream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create);
                file.CopyTo(filestream);
                productVM.Product.ImageUrl = @"images\products\" + fileName + extension;
            }
            _unitOfWork.ProductRepository.Add(productVM.Product);
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
}
