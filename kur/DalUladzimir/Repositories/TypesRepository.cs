using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalUladzimir.Repositories
{
  public class TypesRepository : ITypesRepository
  {
    public IEnumerable<ProductType> GetAll()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<ProductType> GetTypesByCategory(int categoryId)
    {
      var types = new List<ProductType>();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [Id], [Name]" +
                  "FROM [dbo].[Types]" +
                  "WHERE [CategoryId] = @categoryId";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("categoryId", categoryId));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var type = new ProductType()
            {
              Id = (int)reader[0],
              Name = (string)reader[1]
            };

            types.Add(type);
          }
        }
      }

      return types;
    }



    ProductType IRepository<ProductType>.GetById(int id)
    {
      throw new NotImplementedException();
    }
  }
}
