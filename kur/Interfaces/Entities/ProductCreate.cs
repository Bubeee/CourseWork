using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Entities
{
    public class ProductCreate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public int Price { get; set; }
        public string Warranty { get; set; }
        public int DeliveryId { get; set; }
        public string Picture { get; set; }
        public int? Count { get; set; }
        public string StorageSerial { get; set; }//???
        public List<string> Attributes { get; set; }
        public ProductTypeFields ProductType { get; set; }

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
