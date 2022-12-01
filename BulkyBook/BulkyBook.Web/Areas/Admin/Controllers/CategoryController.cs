using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
        var objCategoryList = _unitOfWork.CategoryRepository.GetAll();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfuly";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var category = _unitOfWork.CategoryRepository.GetFirstOrDefault(category => category.Id == id);

        if (category == null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfuly";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var category = _unitOfWork.CategoryRepository.GetFirstOrDefault(category => category.Id == id);

        if (category == null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category category)
    {
        _unitOfWork.CategoryRepository.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfuly";
        return RedirectToAction("Index");
    }
}
