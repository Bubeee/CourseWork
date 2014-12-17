using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductTypeField : IEntity
  {
    public string AttributeName { get; set; }
    public short AttributeType { get; set; }
  }
}