using Interfaces.Interfaces;
using System.Collections.Generic;

namespace Interfaces.Entities
{
  public class Product : IEntity
  {
    public int Id { get; set; }
    public string Model { get; set; }
    public int ManufacturerId { get; set; }
    public string Manufacturer { get; set; }
    public int Price { get; set; }
    public int Warranty { get; set; }
    public int DeliveryId { get; set; }
    public string Delivery { get; set; }
    public string Picture { get; set; }
    public int Count { get; set; }
    public List<string> Attributes { get; set; }
    public ProductType productType { get; set; }//!!! How rename?

    public Product()
    {
        Attributes = new List<string>();
    }
    public override string ToString()
    {
      return Id + " " + Model + " " + Price + " " + Warranty + " " + DeliveryId + " " + Picture + " " + Count;
    }
  }
}