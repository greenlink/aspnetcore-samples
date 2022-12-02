using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
        var objProductList = _unitOfWork.ProductRepository.GetAll();
        return View(objProductList);
    }

    public IActionResult Upsert(int? id)
    {
        var product = new Product();
        IEnumerable<SelectListItem> categoryList = _unitOfWork.CategoryRepository.GetAll()
            .Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() });
        IEnumerable<SelectListItem> coverTypeList = _unitOfWork.CoverTypeRepository.GetAll()
            .Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

        if (id == null || id == 0)
        {
            ViewBag.CategoryList = categoryList;
            ViewBag.CoverTypeList = coverTypeList;
            return View(product);
        }
        else
        {

        }

        return View(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product updated successfuly";
            return RedirectToAction("Index");
        }
        return View(product);
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
