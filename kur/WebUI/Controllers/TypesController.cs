using System.Collections.Generic;
using System.Web.Mvc;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.Controllers
{
  public class TypesController : Controller
  {
    private readonly ITypesRepository _repository;

    public TypesController(ITypesRepository repository)
    {
      _repository = repository;
    }

    public TypesController()
    {

    }

    // GET: Types
    public ActionResult Index(int? categoryId)
    {
      if (categoryId.HasValue)
      {
        var types = new List<ProductType>();
        types.AddRange(_repository.GetTypesByCategory(categoryId.Value));

        return View(types);
      }

      return new EmptyResult();
    }
  }
}