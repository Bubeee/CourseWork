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
    public ActionResult Index(int? typeId, string typeName)
    {
      if (typeId.HasValue)
      {
        var products = _repo.GetProductsByType(typeId.Value);
        ViewBag.Type = typeName;
        return View(products);
      }

      return new EmptyResult();
    }

    public ActionResult AddProduct()
    {
      var newProd = new ProductCreate { Manufacturers = new SelectList(_repo.GetManuf(), "Key", "Value") };

      return View(newProd);
    }

    [HttpPost]
    public ActionResult AddProduct(ProductCreate newProduct)
    {
      _repo.Create(newProduct);
      return View();
    }

    //public ActionResult SearchProducts()
    //{

    //}

    public ActionResult Details(int id)
    {
      return View(_repo.GetById(id));
    }
  }
}