using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductTypeCreate : IEntity
  {
    [Required]
    public string TypeName { get; set; }
    public int CategoryId { get; set; }
    [Required]
    public List<ProductTypeField> AttributeDescriptions { get; set; }
  }
}