﻿using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    public ICategoryRepository CategoryRepository { get; private set; }
    public ICoverTypeRepository CoverTypeRepository { get; private set; }
    public IProductRepository ProductRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        CategoryRepository = new CategoryRepository(_applicationDbContext);
        CoverTypeRepository = new CoverTypeRepository(_applicationDbContext);
        ProductRepository = new ProductRepository(_applicationDbContext);
    }

    public void Save() => _applicationDbContext.SaveChanges();
}
