using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Interfaces.Entities
{
  public class ProductCreate
  {
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int ManufacturerId { get; set; }
    public SelectList Manufacturers { get; set; }
    [Required]
    public int Price { get; set; }
    [Required]
    public string Warranty { get; set; }
    [Required]
    public int DeliveryId { get; set; }
    public SelectList Deliveries { get; set; }
    public string Picture { get; set; }
    [Required]
    public int Count { get; set; }
    public int TypeId { get; set; }
    public string StorageSerial { get; set; }
    public List<string> Attributes { get; set; }
    public ProductTypeCreate ProductType { get; set; }

    public ProductCreate()
    {
      Attributes = new List<string>();
    }
    public override string ToString()
    {
      return Id + " " + Name + " " + Price + " " + Warranty + " " + " " + Picture + " " + Count;
    }
  }
}
