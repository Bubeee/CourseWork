using System.Web.Mvc;
using WebUI.DAL;
using WebUI.DAL.Entities;
using WebUI.DAL.LeshaBd.Repositories;

namespace WebUI.Controllers
{
  public class ProductsController : Controller
  {
    private readonly ProductRepository _repo = new ProductRepository();
    // GET: Products
    public ActionResult Index()
    {
      var product = _repo.GetProductFullManyQueries(1);

      return View(product);
    }

    //public ActionResult AddProduct(Product newProduct)
    //{
      
    //}
  }
}