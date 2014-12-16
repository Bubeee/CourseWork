using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface ITypesRepository : IRepository<ProductCreate, ProductType>
  {
    IEnumerable<ProductType> GetTypesByCategory(int categoryId);

    void AddManufacturer(string manufacturerName,string manufacturerInfo);
    void AddDelivery(string delivery);
  }
}
