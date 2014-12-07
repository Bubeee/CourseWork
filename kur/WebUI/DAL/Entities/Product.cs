namespace WebUI.DAL.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Model { get; set; }
    public int ManufacturerId { get; set; }
    public int Price { get; set; }
    public int Warranty { get; set; }
    public int DeliveryId { get; set; }
    public string Picture { get; set; }
    public int Count { get; set; }
    public string[][] Attributes { get; set; }

    public override string ToString()
    {
      return Id + " " + Model + " " + Price + " " + Warranty + " " + DeliveryId + " " + Picture + " " + Count;
    }
  }
}