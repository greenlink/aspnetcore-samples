using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
        var objCoverTypeList = _unitOfWork.CoverTypeRepository.GetAll();
        return View(objCoverTypeList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverTypeRepository.Add(coverType);
            _unitOfWork.Save();
            TempData["success"] = "Cover type created successfuly";
            return RedirectToAction("Index");
        }
        return View(coverType);
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var coverType = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(coverType => coverType.Id == id);

        if (coverType == null)
            return NotFound();

        return View(coverType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverTypeRepository.Update(coverType);
            _unitOfWork.Save();
            TempData["success"] = "Cover type updated successfuly";
            return RedirectToAction("Index");
        }
        return View(coverType);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
            return NotFound();

        var coverType = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(coverType => coverType.Id == id);

        if (coverType == null)
            return NotFound();

        return View(coverType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(CoverType coverType)
    {
        _unitOfWork.CoverTypeRepository.Remove(coverType);
        _unitOfWork.Save();
        TempData["success"] = "Cover type deleted successfuly";
        return RedirectToAction("Index");
    }
}
