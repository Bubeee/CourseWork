using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Interfaces.Interfaces;

namespace Interfaces.Entities
{
  public class ProductTypeField : IEntity
  {
    public ProductTypeField()
    {
      TypesList = new Dictionary<int, string> { { 1, "string" }, { 2, "int" }, { 3, "float" } };
    }
    [Required]
    public string AttributeName { get; set; }
    [Required]
    public short AttributeType { get; set; }

    public Dictionary<int, string> TypesList { get; set; }
    public int Id { get; set; }
  }
}