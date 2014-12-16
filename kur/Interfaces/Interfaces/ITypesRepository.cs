using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface ITypesRepository : IRepository<ProductCreate, ProductType>
  {
    IEnumerable<ProductType> GetTypesByCategory(int categoryId);
  }
}
