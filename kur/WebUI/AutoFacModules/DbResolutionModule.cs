using Autofac;    
using DalAlexey.Repositories;
using DalUladzimir.Repositories;
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
        builder.RegisterType<ProductRepository>().As<IProductRepository>();
        builder.RegisterType<ProductTypeRepository>().As<ITypesRepository>();
        builder.RegisterType<DalAlexey.Repositories.CategoriesRepository>().As<ICategoriesRepository>();
      }
      else
      {
        builder.RegisterType<ProductsRepository>().As<IProductRepository>();
        builder.RegisterType<TypesRepository>().As<ITypesRepository>();
        builder.RegisterType<DalUladzimir.Repositories.CategoriesRepository>().As<ICategoriesRepository>();
      }
    }
  }
}