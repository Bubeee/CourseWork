using Interfaces.Interfaces;
using System.Collections.Generic;

namespace Interfaces.Entities
{
  public class Product : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string ManufacturerInfo { get; set; }
    public int Price { get; set; }
    public string Warranty { get; set; }
    public string Delivery { get; set; }
    public string Picture { get; set; }
    public int? Count { get; set; }
    public string StorageSerial { get; set; }
    public List<string> Attributes { get; set; }
    public ProductType ProductType { get; set; }//!!! How rename?

    public Product()
    {
        Attributes = new List<string>();
    }
    public override string ToString()
    {
      return Id + " " + Name + " " + Price + " " + Warranty + " " + " " + Picture + " " + Count;
    }
  }
}