using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository;

public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CoverTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        => _applicationDbContext = applicationDbContext;

    public void Update(CoverType coverType) => _applicationDbContext.CoverTypes.Update(coverType);
}
