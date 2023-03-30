using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReportStorage.Data
{
    public class UserModulesTestService
    {
        private string connectionString = @"Data Source=106.109.72.69; 
                Initial Catalog=UserModules;
                Integrated Security=false; 
                User ID=sa; Password=OrchestraSQL;
                Encrypt=True;
                TrustServerCertificate=True";
        public Task<UserModulesTest[]> GetDataTest()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {

                    throw;
                }
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT top 3 * FROM UserModules.[user].TEST";
                SqlDataReader reader = cmd.ExecuteReader();
                List<UserModulesTest> result = new List<UserModulesTest>();
                while(reader.Read()) {
                    result.Add(
                        new UserModulesTest()
                        {
                            id = reader.GetInt32(0),
                            created_by = reader.GetString(1),
                            checkbox = reader.GetString(2),
                            dropdown = reader.GetString(3),
                            number = reader.GetString(4),
                            textbox = reader.GetString(5)
                        });
                }
                return Task.FromResult(result.ToArray());
            }
        }
    }

    public class UserModulesService
    {
        private string ConnectionString = @"Data Source=106.109.72.69; 
                Initial Catalog=UserModules;
                Integrated Security=false; 
                User ID=sa; Password=OrchestraSQL;
                Encrypt=True;
                TrustServerCertificate=True";
        
        public Task<UserModulesTable> GetData()
        {
            UserModulesTable table = new UserModulesTable("[UserModules].[user].[TEST]", ConnectionString);
            table.ConString = ConnectionString;
            table.Select();
            //return Task.FromResult(table.Rows.ToArray());   
            return Task.FromResult(table);
        }
    }
}
