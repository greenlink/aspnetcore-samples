using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository;
internal interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
    void Save();
}
