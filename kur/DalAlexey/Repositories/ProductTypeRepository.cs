using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Entities;

namespace DalAlexey.Repositories
{
  public class ProductTypeRepository
    {
        public static string connectionString = @"Data Source=BUMBLEBEE\SQLEXPRESS;Integrated Security=true";
        public static string workDatabaseName = "kur";

        public ProductType GetProductType(int id)
        {
            var productType = new ProductType();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                DataTable dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [view_name] FROM [field] WHERE [type_product_id] = (@producttypeid) ORDER BY [id]";
                    command.Parameters.Add(new SqlParameter("@producttypeid", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productType.AttributeNames.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return productType;
        }
        public void CreateProductType(ProductTypeCreate productTypeCreate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                DataTable dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    try
                    {
                        connection.BeginTransaction();

                        //создание типа товара
                        command.CommandText = "INSERT INTO [type_product] " +
                                                "(category_id,view_name) " +
                                                "OUTPUT INSERTED.[id] " +
                                                "VALUES " +
                                                "((@categoryId),(@productTypeName)) ";
                        command.Parameters.Add(new SqlParameter("@categoryId", productTypeCreate.CategoryId));
                        command.Parameters.Add(new SqlParameter("@productTypeName", productTypeCreate.TypeName));

                        int productTypeId = (int)command.ExecuteScalar();

                        //создание таблицы типа товара
                        command.CommandText = "CREATE TABLE [type_product" + productTypeId + "]" +
                                                "(" +
                                                    "product_id int," +
                                                    CreateProductTypeTableQuery(productTypeCreate.Attributes) +
                                                ")";
                        command.Parameters[0].ParameterName = "@productTypeId";
                        command.Parameters[0].Value = productTypeId;
                        command.Parameters.RemoveAt(1);
                        command.ExecuteNonQuery();

                        command.CommandText = "INSERT INTO [field] ([type_product_id],[view_name],[field_type]) VALUES " +
                                                "(" +
                                                    CreateProductTypeFieldsQuery(productTypeId, productTypeCreate.Attributes) +
                                                ")";
                        command.Parameters.RemoveAt(0);
                        command.ExecuteNonQuery();

                        command.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        command.Transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private string CreateProductTypeTableQuery(List<ProductTypeFields> attributes)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < attributes.Count; i++)
            {
                sb.Append("col");
                sb.Append(i.ToString());
                //--string - 1
                //--int - 2
                //--float - 3
                //--enum - 4
                switch (attributes[i].AttributeType)
                {
                    case 1:
                        sb.Append(" nvarchar(30)");
                        break;
                    case 2:
                        sb.Append(" int");
                        break;
                    case 3:
                        sb.Append(" float");
                        break;
                    case 4:
                        sb.Append(" nvarchar(30)");
                        break;
                }
                if (i < (attributes.Count - 1))
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        private string CreateProductTypeFieldsQuery(int productTypeId, List<ProductTypeFields> attributes)
        {
            //INSERT INTO [field] 
            //([type_product_id],[view_name],[field_type])
            //VALUES
            //(1,N'вес',2),
            //(1,N'цвет',1)
            var sb = new StringBuilder();
            for (int i = 0; i < attributes.Count; i++)
            {
                sb.Append("(");
                sb.Append(productTypeId);
                sb.Append(",N'");
                sb.Append(attributes[i].Name);
                sb.Append("',");
                sb.Append(attributes[i].AttributeType);
                sb.Append(")");
                if (i < (attributes.Count - 1))
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
    }
}
