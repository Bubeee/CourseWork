using System.Collections.Generic;

namespace WebUI.DAL.Entities
{
  public class ProductTypeFields
  {
    public string Name { get; set; }
    public short AttributeType { get; set; }
    public List<string> EnumValues { get; set; }
  }
}