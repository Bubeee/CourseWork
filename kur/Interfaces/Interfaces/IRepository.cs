using System.Collections.Generic;

namespace Interfaces.Interfaces
{
  public interface IRepository<in TR, out T>
  {
    T GetById(int id);

    IEnumerable<T> GetAll();

    int Create<TR>(TR model);
  }
}