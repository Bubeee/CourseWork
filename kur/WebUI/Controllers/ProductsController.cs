using System.Web.Mvc;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.Controllers
{
  public class ProductsController : Controller
  {
    private readonly IProductRepository _repo;

    private readonly ITypesRepository _typesRepository;

    public ProductsController(
      IProductRepository repo, 
      ITypesRepository typesRepository)
    {
      _repo = repo;
      _typesRepository = typesRepository;
    }

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

    public ActionResult AddProduct(int typeId)
    {
      var newProd = new ProductCreate { Manufacturers = new SelectList(_repo.GetManuf(), "Key", "Value") };
      if (typeId != 0)
      {
        var type = _typesRepository.GetProductTypeCreateById(typeId);
        newProd.ProductType.AttributeDescriptions = _repo.GetAttributes(typeId);
      }

      return View(newProd);
    }

    [HttpPost]
    public ActionResult AddProduct(ProductCreate newProduct)
    {
      _repo.Create(newProduct);
      return View();
    }

    public ActionResult Details(int id)
    {
      return View(_repo.GetById(id));
    }
  }
}