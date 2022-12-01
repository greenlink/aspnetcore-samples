using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository;
public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        => _applicationDbContext = applicationDbContext;

    public void Update(Product product)
    {
        var productFromDb = _applicationDbContext.Products.FirstOrDefault(p => p.Id == product.Id);
        
        if(productFromDb != null)
        {
            productFromDb.Title = product.Title;
            productFromDb.Description = product.Description;
            productFromDb.ISBN = product.ISBN;
            productFromDb.Author = product.Author;
            productFromDb.ListPrice = product.ListPrice;
            productFromDb.Price = product.Price;
            productFromDb.Price50 = product.Price50;
            productFromDb.Price100 = product.Price100;
            productFromDb.CategoryId = product.CategoryId;
            productFromDb.CoverTypeId = product.CoverTypeId;

            if(product.ImageUrl != null)
                productFromDb.ImageUrl = product.ImageUrl;
        }
    }
}
