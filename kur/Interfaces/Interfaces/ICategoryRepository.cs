using System.Collections.Generic;
using Interfaces.Entities;

namespace Interfaces.Interfaces
{
  public interface ICategoriesRepository : IRepository<Category, Category>
  {
    IEnumerable<Category> GetAllCategories();
  }
}