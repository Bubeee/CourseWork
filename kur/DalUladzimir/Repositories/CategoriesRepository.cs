using System.Collections.Generic;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalUladzimir.Repositories
{
  public class CategoriesRepository : IRepository<Category>
  {
    public Category GetById(int id)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Category> GetAll()
    {
      throw new System.NotImplementedException();
    }
  }
}
