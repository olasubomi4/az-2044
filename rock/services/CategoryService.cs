using System;
using System.Data.SqlClient;
using rock.Models;

namespace rock.services
{
    public class CategoryService : ICategoryService
    {
         /*private static string db_source = "rock2.database.windows.net";
          private static string db_user = "subomi";
          private static string db_password = "Theflash456";
          private static string db_database = "rock2";
        */

        private readonly IConfiguration _configuration;

        public CategoryService(IConfiguration configuration)
        {

            _configuration = configuration;

        }
      
        private SqlConnection GetConnection()
        {/*)
              var _builder = new SqlConnectionStringBuilder();
               _builder.DataSource = db_source;
               _builder.UserID = db_user;
               _builder.Password = db_password;
               _builder.InitialCatalog = db_database;
               
            return new SqlConnection(_builder.ConnectionString);*/
            
             return new SqlConnection(_configuration.GetConnectionString("SQL"));

        }


        public List<Category> GetCategories()
        {
            SqlConnection conn = GetConnection();

            List<Category> categories_lst = new List<Category>();

            string statement = "SELECT Id,Name,DisplayOrder,CreatedDateTime FROM[rock2].[dbo].[categories]";

            conn.Open();

            SqlCommand cmd = new SqlCommand(statement, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        DisplayOrder = reader.GetInt32(2),
                        CreatedDateTime = reader.GetDateTime(3)


                    };

                    categories_lst.Add(category);
                }

            }

            conn.Close();
            return categories_lst;


        }
        public void insert(Category category)
        {
            SqlConnection conn = GetConnection();
            string statement = "Insert Into dbo.categories (Name,DisplayOrder,CreatedDateTime) " +
                     "VALUES (@N, @DO, @CT) ";

            // instance connection and command
            SqlTransaction transaction = null;


            conn.Open();

            using (SqlCommand cmd = new SqlCommand(statement, conn))
            {
                transaction = conn.BeginTransaction();
                // add parameters and their values
                // cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int, 100).Value = category.Id;
                cmd.Parameters.Add("@N", System.Data.SqlDbType.NVarChar, 100).Value = category.Name;
                cmd.Parameters.Add("@DO", System.Data.SqlDbType.Int, 100).Value = category.DisplayOrder;
                cmd.Parameters.Add("@CT", System.Data.SqlDbType.DateTime2, 7).Value = category.CreatedDateTime;



                transaction.Commit();
        
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        
           
        }
      
        }
    }


