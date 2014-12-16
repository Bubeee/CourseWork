using System.Web.Mvc;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductRepository _repo;

    public ProductsController(IProductRepository repo)
    {
      _repo = repo;
    }

    // GET: Products
    public ActionResult Index(int? typeId)
    {
      if (typeId.HasValue)
      {
        var products = _repo.GetProductsByType(typeId.Value);

        return View(products); 
      }

      return new EmptyResult();
    }

    public ActionResult AddProduct()
    {
      return View();
    }

    [HttpPost]
    public ActionResult AddProduct(Product newProduct)
    {
      return View();
    }

    //public ActionResult SearchProducts()
    //{

    //}
  }
}