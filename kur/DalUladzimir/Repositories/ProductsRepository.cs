using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
  }
}
