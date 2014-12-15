using System;
using System.Collections.Generic;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalUladzimir.Repositories
{
  public class TypesRepository : IRepository<ProductTypeCreate>
  {
    public IEnumerable<ProductTypeCreate> GetAll()
    {
      throw new NotImplementedException();
    }

    ProductTypeCreate IRepository<ProductTypeCreate>.GetById(int id)
    {
      throw new NotImplementedException();
    }
  }
}
