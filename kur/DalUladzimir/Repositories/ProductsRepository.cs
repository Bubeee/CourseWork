using System;
using System.Collections.Generic;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalUladzimir.Repositories
{
  class ProductsRepository : IRepository<Product>
  {
    public Product GetById(int id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
      throw new NotImplementedException();
    }
  }
}
