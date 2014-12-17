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
        command.Parameters.Add(new SqlParameter("@categoryId", categoryId));
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

    public ProductTypeCreate GetProductTypeCreateById(int typeId)
    {
      var productTypeCreate = new ProductTypeCreate();
      productTypeCreate.AttributeDescriptions = new List<ProductTypeField>();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [Name], [CategoryId] " +
                  "FROM [dbo].[Types]" +
                  "WHERE [Id] = @typeId";

      var query2 = "SELECT [Name], [Id] " +
                  "FROM [dbo].[Types]" +
                  "WHERE [TypeId] = @typeId";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@typeId", typeId));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            productTypeCreate.TypeName = (string)reader[0];
            productTypeCreate.CategoryId = (int)reader[1];
          }
        }

        command = new SqlCommand(query2, connection);
        //command.Parameters.Add(new SqlParameter("@typeId", typeId));
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            productTypeCreate.AttributeDescriptions.Add(
              new ProductTypeField
            {
              AttributeName = (string)reader[0],
              Id = (int)reader[1]
            });
          }
        }
      }

      return productTypeCreate;
    }

    public int Create(ProductTypeCreate model)
    {
      int newProdId;

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "INSERT INTO [dbo].[Types] ([Name], [CategoryId]) " +
                  "VALUES (@typeName, @categoryId)";

      using (var connection = new SqlConnection(conString))
      {
        using (var command = new SqlCommand(query, connection))
        {
          command.Parameters.Add(new SqlParameter("@categoryId", model.CategoryId));
          command.Parameters.Add(new SqlParameter("@typeName", model.TypeName));
          connection.Open();
          newProdId = (int)command.ExecuteScalar();
        }
      }

      return newProdId;
    }

    public ProductType GetById(int id)
    {
      var type = new ProductType();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [Id], [Name]" +
                  "FROM [dbo].[Types]" +
                  "WHERE [Id] = @typeId";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@typeId", id));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            type.Id = (int)reader[0];
            type.Name = (string)reader[1];
          }
        }
      }

      return type;
    }
  }
}
