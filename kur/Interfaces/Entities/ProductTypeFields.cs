using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductTypeField : IEntity
  {
    [Required]
    public string AttributeName { get; set; }
    [Required]
    public short AttributeType { get; set; }
    public int Id { get; set; }
  }
}