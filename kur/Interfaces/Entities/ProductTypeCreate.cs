using System.Collections.Generic;
using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductTypeCreate : IEntity
  {
    public string TypeName { get; set; }
    public int CategoryId { get; set; }
    public List<ProductTypeField> AttributeDescriptions { get; set; }
  }
}