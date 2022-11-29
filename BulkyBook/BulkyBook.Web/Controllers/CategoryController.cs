using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

    public IActionResult Index()
    {
        var objCategoryList = _categoryRepository.GetAll();
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
        if(category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _categoryRepository.Add(category);
            _categoryRepository.Save();
            TempData["success"] = "Category created successfuly";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Edit(int? id)
    {
        if(id == null || id == 0)
            return NotFound();
        
        var category = _categoryRepository.GetFirstOrDefault(category => category.Id == id);
        
        if(category == null)
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
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            TempData["success"] = "Category updated successfuly";
            return RedirectToAction("Index");
        }
        return View(category);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var category = _categoryRepository.GetFirstOrDefault(category => category.Id == id);

        if (category == null)
            return NotFound();

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category category)
    {
        _categoryRepository.Remove(category);
        _categoryRepository.Save();
        TempData["success"] = "Category deleted successfuly";
        return RedirectToAction("Index");
    }
}
