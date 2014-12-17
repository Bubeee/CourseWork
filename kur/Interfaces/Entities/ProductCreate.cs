using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Interfaces.Entities
{
  public class ProductCreate
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int ManufacturerId { get; set; }
    public SelectList Manufacturers { get; set; }
    public int Price { get; set; }
    public string Warranty { get; set; }
    public int DeliveryId { get; set; }
    public string Picture { get; set; }
    public int Count { get; set; }
    public int TypeId { get; set; }
    public string StorageSerial { get; set; }//???
    public List<int> AttributesDescriptionIds { get; set; }
    public Dictionary<string, string> Attributes { get; set; }
    public ProductTypeCreate ProductType { get; set; }

    public ProductCreate()
    {
      Attributes = new Dictionary<string, string>();
    }
    public override string ToString()
    {
      return Id + " " + Name + " " + Price + " " + Warranty + " " + " " + Picture + " " + Count;
    }
  }
}
