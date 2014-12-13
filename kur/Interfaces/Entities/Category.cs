using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class Category : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
  }
}