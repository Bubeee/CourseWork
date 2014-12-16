using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface IProductRepository : IRepository<ProductCreate, Product>
  {
    IEnumerable<Product> GetProductsByType(int typeId);
  }
}
