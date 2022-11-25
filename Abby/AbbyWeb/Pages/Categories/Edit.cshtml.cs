using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
                
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void OnGet(int id)
        {
            Category = _applicationDbContext.Categories.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The Display order can't be the same as the name.");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Update(Category);
                await _applicationDbContext.SaveChangesAsync();
                TempData["success"] = "Category updated successfuly.";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
