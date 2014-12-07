using System.Collections.Generic;

namespace WebUI.DAL.Entities
{
  public class ProductType
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Attributes { get; set; }
  }
}