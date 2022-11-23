using AbbyWeb.Data;
using AbbyWeb.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IEnumerable<Category> Categories { get; private set; }

        public IndexModel(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public void OnGet()
        {
            Categories = _applicationDbContext.Categories;
        }
    }
}
