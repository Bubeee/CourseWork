using System.Collections.Generic;
using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductTypeFields : IEntity
  {
    public string Name { get; set; }
    public short AttributeType { get; set; }
    public List<string> EnumValues { get; set; }
  }
}