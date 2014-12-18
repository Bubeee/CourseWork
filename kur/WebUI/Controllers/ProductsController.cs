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
      var newProd = new ProductCreate
      {
        Manufacturers = new SelectList(_repo.GetManuf(), "Key", "Value"),
        Deliveries = new SelectList(_repo.GetDeliveries(), "Key", "Value")
      };
      if (typeId != 0)
      {
        newProd.ProductType = _typesRepository.GetProductTypeCreateById(typeId);
        foreach (var item in newProd.ProductType.AttributeDescriptions)
        {
          newProd.Attributes.Add(string.Empty);
        }
      }

      return View(newProd);
    }

    [HttpPost]
    public ActionResult AddProduct(ProductCreate newProduct)
    {
      if (ModelState.IsValid)
      {
        _repo.Create(newProduct);

        return RedirectToAction("Index");
      }

      newProduct.Manufacturers = new SelectList(_repo.GetManuf(), "Key", "Value");
      newProduct.Deliveries = new SelectList(_repo.GetDeliveries(), "Key", "Value");
      if (newProduct.TypeId != 0)
      {
        newProduct.ProductType = _typesRepository.GetProductTypeCreateById(newProduct.TypeId);
      }

      return View(newProduct);
    }

    public ActionResult Details(int id)
    {
      return View(_repo.GetById(id));
    }

    public ActionResult Buy(int productId)
    {
      var user = new UserBuying
      {
        ProductId = productId
      };

      return View();
    }

    [HttpPost]
    public ActionResult Buy(UserBuying user)
    {

      return View();
    }
  }
}