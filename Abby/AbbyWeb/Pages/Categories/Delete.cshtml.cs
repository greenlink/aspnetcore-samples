using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
                
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void OnGet(int id)
        {
            Category = _applicationDbContext.Categories.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var category = _applicationDbContext.Categories.Find(Category.Id);
            
            if (category == null) return NotFound();
            
            _applicationDbContext.Categories.Remove(category);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
