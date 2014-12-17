using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface ITypesRepository : IRepository<ProductTypeCreate, ProductType>
  {
    IEnumerable<ProductType> GetTypesByCategory(int categoryId);

    ProductTypeCreate GetPrTypeById(int typeId);
  }
}