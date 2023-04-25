﻿using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSach.Data;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository categoryRepository { get;  }
        public IProductRepository productRepository { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            categoryRepository = new CategoryRepository(_db);
            productRepository = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
