using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

namespace ReportStorage.Data
{
    public class UserModulesTest
    {
        public int id { get; set; }
        public string? created_by { get; set; }
        public string? checkbox { get; set; }
        public string? dropdown { get; set; }
        public string? number { get; set; }
        public string? textbox { get; set; }
    }

    public class UserModulesField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Value { get; set; }
        public bool IsChanged { get; set; }
        public UserModulesField() {
            IsChanged = false;
            Id = 0;
            Name = "";
            Type = "TEXT"; //default
            Value = "";
        }
    }

    public class UserModulesHeaderFields : UserModulesField
    {
        public string SortOrder { get; set; }   
        public string Filter { get; set; }  
        }

    public class UserModulesRow
    {
        public int Id { get; set; }
        public bool IsUpdated { get; set; }
        public bool IsDeleted { get; set; }
        public List<UserModulesField> Fields { get; set; }
        public  UserModulesRow()
        {
            Fields = new List<UserModulesField>();  
            IsUpdated = false;
            IsDeleted = false;
            Id = 0;
        }


        public void Select() { }
        public void Update() { }    
        public void Delete() { }    
    }


    public class UserModulesTable
    {
        public string ConString { get; set; }   
        public int Id { get; set; }
        public string Name { get; set; }   
        public DateTime LastUpdated { get; }
        public List<String> Columns { get; set; }
        public List<UserModulesHeaderFields> HeaderRow { get; set; }    
        public List<UserModulesRow> Rows { get; set;}

        public UserModulesTable(string name, string constring) {
            Name = name;
            ConString = constring;
            Columns = new List<string>();
            HeaderRow = new List<UserModulesHeaderFields>();
            Rows = new List<UserModulesRow>();
            Id = 0;
        }
        public void Select() { 
            using(SqlConnection con = new SqlConnection(ConString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TOP 100 * FROM " + Name;
                SqlDataReader reader = cmd.ExecuteReader(); 
                while (reader.Read()) {
                    UserModulesRow row = new()
                    {
                        Id = reader.GetInt32(0)
                    };
                    UserModulesField FieldId = new()
                    {
                        Name = "ID",
                        Value = row.Id.ToString()
                    };
                    row.Fields.Add(FieldId);
                    for (int i = 1; i < reader.FieldCount - 1; i++)
                    {
                        UserModulesField field = new()
                        {
                            Name = reader.GetName(i),
                            Value = reader.GetString(i),
                            Id = row.Id
                        };
                        row.Fields.Add(field);
                    }
                    Rows.Add(row);
                }
                reader.Close();
            }
        }
        public void Update() { }    
        public void Delete() { }    
        public void Insert(UserModulesRow NewRow)
        {

        }
        
    }
}
