namespace Interfaces.Interfaces
{
  public interface IRepository<out T>
  {
    T GetById(int id);
  }
}