namespace Interfaces.Interfaces
{
  public interface IRepository<in TR, out T>
  {
    T GetById(int id);

    int Create(TR model);
  }
}