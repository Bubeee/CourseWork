using Autofac;    
using DalAlexey.Repositories;
using DalUladzimir.Repositories;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.AutoFacModules
{
  public class DbResolutionModule : Module
  {
    public bool LoadAlexey { get; set; }
    protected override void Load(ContainerBuilder builder)
    {
      if (LoadAlexey)
      {
        builder.RegisterType<ProductRepository>().As<IRepository<Product>>();
        builder.RegisterType<ProductTypeRepository>().As<IRepository<ProductType>>();
      }
      else
      {
        builder.RegisterType<ProductsRepository>().As<IProductRepository>();
        builder.RegisterType<TypesRepository>().As<ITypesRepository>();
        builder.RegisterType<CategoriesRepository>().As<IRepository<Category>>();
      }
    }
  }
}