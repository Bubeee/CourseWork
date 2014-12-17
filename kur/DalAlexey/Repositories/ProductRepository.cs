using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalAlexey.Repositories
{
  public class ProductRepository : IProductRepository
  {
      public static string connectionString = @"Data Source=BUMBLEBEE\SQLEXPRESS;Integrated Security=true";
      public static string workDatabaseName = "kur";

    public Product GetById(int productId)
      {
          Product product = new Product();
          using (SqlConnection connection = new SqlConnection(connectionString))
          {
              connection.Open();
              connection.ChangeDatabase(workDatabaseName);

              DataTable dataTable = new DataTable();

              using (SqlCommand command = new SqlCommand())
              {
                  command.Connection = connection;
                  command.CommandText = "SELECT [p].[id],[p].[type_id],[p].model,[p].[price],[p].[warranty],[p].[picture]," +
                      "[p].[count],[p].[delivery_id],[d].name AS [delivery],[p].[manufacturer_id],[m].name AS [manufacturer] FROM [product] AS [p] JOIN [manufacturer] AS [m] ON [manufacturer_id]=[m].[id] JOIN [delivery]AS [d] ON [delivery_id]=[d].[id] WHERE [p].[id]=(@productid)";
                  command.Parameters.Add(new SqlParameter("@productid", productId));//Здесь это id товара
                  dataTable.Load(command.ExecuteReader());

                  product.Id = dataTable.Rows[0].Field<int>("id");
                  product.Name = dataTable.Rows[0].Field<string>("model");
                  product.Manufacturer = dataTable.Rows[0].Field<string>("manufacturer");
                  product.Price = dataTable.Rows[0].Field<int>("price");
                  product.Warranty = dataTable.Rows[0].Field<string>("warranty");
                  product.Delivery = dataTable.Rows[0].Field<string>("delivery");
                  product.Picture = dataTable.Rows[0].Field<string>("picture");
                  product.Count = dataTable.Rows[0].Field<int>("count");

                  int productTypeId;
                  productTypeId = dataTable.Rows[0].Field<int>("type_id");

                  //Название таблицы с этим типом товара
                  command.CommandText = "SELECT [table_name] FROM [type_product] WHERE id = (@producttypeid);";
                  command.Parameters[0].ParameterName = "@producttypeid";
                  command.Parameters[0].Value = productTypeId;
                  string productTableName = (string)command.ExecuteScalar();

                  //значения дополнительных полей товара
                  command.CommandText = "SELECT * FROM " + productTableName + " AS [t] WHERE [t].[product_id]= @productid";
                  command.Parameters[0].ParameterName = "@productid";
                  command.Parameters[0].Value = productId;
                  //MessageBox.Show(command.Parameters.Count.ToString());

                  using (SqlDataReader reader = command.ExecuteReader())
                  {
                      reader.Read();
                      for (int i = 0; i < reader.FieldCount - 1; i++)//-1 потому что первый столбец это id товара
                      {
                          product.Attributes.Add(reader.GetValue(i + 1).ToString());
                      }
                  }
              }
          }
          return product;
      }

    public int Create(ProductCreate model)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Product> GetProductsByType(int typeId)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
      throw new System.NotImplementedException();
    }


    public void AddManufacturer(string manufacturerName, string manufacturerInfo)
    {
      throw new System.NotImplementedException();
    }

    public void AddDelivery(string delivery)
    {
      throw new System.NotImplementedException();
    }

    public Dictionary<int, string> GetManuf()
    {
      throw new System.NotImplementedException();
    }
  }
}