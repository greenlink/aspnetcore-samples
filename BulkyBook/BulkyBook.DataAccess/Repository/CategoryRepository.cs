using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        => _applicationDbContext = applicationDbContext;

    public void Update(Category category) => _applicationDbContext.Categories.Update(category);
}
