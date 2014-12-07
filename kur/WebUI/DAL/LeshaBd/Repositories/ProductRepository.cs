using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebUI.DAL.Entities;

namespace WebUI.DAL.LeshaBd.Repositories
{
  public class ProductRepository
  {
    public Product GetProductFullManyQueries(int productId)
    {
      var product = new Product();
      using (var connection = new SqlConnection((ConfigurationManager.ConnectionStrings["kur"].ConnectionString)))
      {
        connection.Open();

        var dataTable = new DataTable();

        using (var command = new SqlCommand())
        {
          command.Connection = connection;
          command.CommandText = "SELECT [id],[type_id],[model],[price],[warranty],[delivery_id],[picture],[count] " +
                                "FROM [product] WHERE [id]=(@productid)";

          command.Parameters.Add(new SqlParameter("@productid", productId));//Здесь это id товара
          dataTable.Load(command.ExecuteReader());

          product.Id = dataTable.Rows[0].Field<int>("id");
          product.Model = dataTable.Rows[0].Field<string>("model");
          product.Price = dataTable.Rows[0].Field<int>("price");
          product.Warranty = dataTable.Rows[0].Field<int>("warranty");
          product.DeliveryId = dataTable.Rows[0].Field<int>("delivery_id");
          product.Picture = dataTable.Rows[0].Field<string>("picture");
          product.Count = dataTable.Rows[0].Field<int>("count");

          var productTypeId = dataTable.Rows[0].Field<int>("type_id");

          //Название таблицы с этим типом товара
          command.CommandText = "SELECT [table_name] FROM [type_product] WHERE id = (@producttypeid);";
          command.Parameters[0].ParameterName = "@producttypeid";
          command.Parameters[0].Value = productTypeId;
          var productTableName = (string)command.ExecuteScalar();

          //значения дополнительных полей товара
          command.CommandText = "SELECT * FROM " + productTableName + " AS [t] WHERE [t].[product_id]= @productid";
          command.Parameters[0].ParameterName = "@productid";
          command.Parameters[0].Value = productId;
          //MessageBox.Show(command.Parameters.Count.ToString());

          using (SqlDataReader reader = command.ExecuteReader())
          {
            product.Attributes = new string[2][];
            product.Attributes[0] = new string[reader.FieldCount - 1];//-1 потому что первый столбец это id товара
            product.Attributes[1] = new string[reader.FieldCount - 1];
            reader.Read();
            for (int i = 0; i < product.Attributes[1].Length; i++)
            {
              product.Attributes[1][i] = reader.GetValue(i + 1).ToString();
            }
          }

          //названия дополнительных полей товара
          command.CommandText = "SELECT [view_name] FROM [field] WHERE [type_product_id] = (@producttypeid) ORDER BY [id]";
          command.Parameters[0].ParameterName = "@producttypeid";
          command.Parameters[0].Value = productTypeId;

          using (SqlDataReader reader = command.ExecuteReader())
          {
            for (int i = 0; i < product.Attributes[0].Length; i++)
            {
              reader.Read();
              product.Attributes[0][i] = reader.GetString(0);
            }
          }
        }
      }

      return product;
    }
  }
}