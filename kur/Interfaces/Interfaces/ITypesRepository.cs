using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface ITypesRepository : IRepository<ProductType>
  {
    IEnumerable<ProductType> GetTypesByCategory(int categoryId);
  }
}
