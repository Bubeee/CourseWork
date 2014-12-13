using System.Web.Mvc;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IRepository<Product> _repo;

    public ProductsController(IRepository<Product> repo)
    {
      _repo = repo;
    }

    // GET: Products
    public ActionResult Index()
    {
      var product = _repo.GetById(1);

      return View(product);
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