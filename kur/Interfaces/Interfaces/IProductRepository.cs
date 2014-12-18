using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface IProductRepository : IRepository<ProductCreate, Product>
  {
    IEnumerable<Product> GetProductsByType(int typeId);

    void AddManufacturer(string manufacturerName, string manufacturerInfo);
    void AddDelivery(string delivery);

    Dictionary<int, string> GetManuf();
    Dictionary<int, string> GetDeliveries();
    Dictionary<int, string> GetStorages();
  }
}
