using Interfaces.Entities;
using Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalAlexey.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public static string connectionString = @"Data Source=BUMBLEBEE\SQLEXPRESS;Integrated Security=true";
        public static string workDatabaseName = "kur";

        public Category GetById(int id)
        {
            var category = new Category();
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [id],[name],[picture] FROM [category] WHERE [id]=(@categoryId)";
                    command.Parameters.Add(new SqlParameter("@categoryId", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        category.Id = id;
                        category.Name = reader.GetString(1);
                        category.Image = reader.GetString(2);
                    }
                }
            }
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [id],[name],[picture] FROM [category]";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var category = new Category();
                            category.Id = reader.GetInt32(0);
                            category.Name = reader.GetString(1);
                            category.Image = reader.GetString(2);
                            categories.Add(category);
                        }
                    }
                }
            }
            return categories;
        }

        public int Create(Category model)
        {
            int categoryId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {                
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    {
                        command.CommandText = "INSERT INTO [category] ([name],[picture]) VALUES (@name,@picture)";
                        command.Parameters.Add(new SqlParameter("@name", model.Name));
                        command.Parameters.Add(new SqlParameter("@picture", model.Image ?? "Files/1.gif"));

                        categoryId = (int)command.ExecuteNonQuery();

                        model.Id = categoryId;
                    }
                }
            }
            return categoryId;
        }
    }
}
