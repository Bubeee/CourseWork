using System.Collections.Generic;

namespace WebUI.DAL.Entities
{
  public class ProductTypeCreate
  {
    public string TypeName { get; set; }
    public List<ProductTypeFields> Attributes { get; set; }
  }
}