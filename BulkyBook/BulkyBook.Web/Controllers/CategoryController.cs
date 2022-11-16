using BulkyBook.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IActionResult Index()
    {
        var objCategoryList = _dbContext.Categories;
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
}
