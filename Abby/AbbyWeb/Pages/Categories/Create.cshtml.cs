using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
                
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The Display order can't be the same as the name.");
            }
            if (ModelState.IsValid)
            {
                await _applicationDbContext.Categories.AddAsync(Category);
                await _applicationDbContext.SaveChangesAsync();
                TempData["success"] = "Category created successfuly.";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
