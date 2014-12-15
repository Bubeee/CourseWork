using System.Collections.Generic;
using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductType : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> AttributeNames { get; set; }
    public ProductType()
    {
      AttributeNames = new List<string>();
    }
  }
}