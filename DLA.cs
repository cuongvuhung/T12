using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic.FileIO;

///////////////////////////////////////////////////////////////////////////////////////////////////
//THIS CLASS FOR EXECUTE SQL 
//2 OPTION: SQLExecute for non query and SQLQuery for query
////SQLExecute 
// (Insert, $tblname, $value1, $value 2, $value 3,......)
//                  = Insert into tblname values (value1,value2,....)
// (Update, $tblname, $id, $field1, $value1, $field2, $value2,....)
//                  = Update tblname set field1 = value1, field2 = value2,.... where id = id
// (Delete, $tblname, $field1, $value1, $field 2, $value2,....) 
//                  = Delete from tblname where field1 = value 1 and field2 = value 2
////SQLQuery
// (Select, $tblname, $field1, $value1, $field 2, $value2,....) 
//                  = Select * from tblname where field1 = value 1 and field2 = value 2
///////////////////////////////////////////////////////////////////////////////////////////////////

namespace T12
{
    internal class DLA
    {
        // Excute SQLExecute
        private DBConnect dbconnect = new();
        public int SQLExecute(string str)
        {
            try
            {
                string sql;
                SqlCommand cmd = dbconnect.Cnn.CreateCommand();
                // INSERT            
                if (str.Split(",")[0] == "Insert")
                {
                    // sql = Insert into tablename values 
                    sql = $"Insert into {str.Split(",")[1]} values (";
                    for (int i = 2; i < str.Split(",").Length - 1; i++)
                    {
                        // + (@0,@1,....
                        sql += "@" + i + ",";
                    } 
                    // + @n)
                    sql += "@" + (str.Split(",").Length - 1) + ")";

                    // Set parameter @2 = str.Split(",")[2]) .v.v.
                    cmd = new(sql, dbconnect.Cnn);
                    for (int i = 2; i < str.Split(",").Length; i++)
                    {
                        cmd.Parameters.AddWithValue("" + i + "", str.Split(",")[i]);
                    }
                }

                // UPDATE
                if (str.Split(",")[0] == "Update")
                {
                    // sql = Update tablename set 
                    sql = $"Update {str.Split(",")[1]} Set ";

                    // + field3 = @4, field5= @6,..... 
                    
                    for (int i = 3; i < str.Split(",").Length - 3; i++)
                    {
                        sql += str.Split(",")[i] + "=@" + (i + 1) + ",";
                        i++;
                    }
                    // + fieldn = @n where id =@id
                    sql += (str.Split(",")[str.Split(",").Length - 2]) + "=@" + (str.Split(",").Length - 1);
                    sql += " where id = @id";

                    // Set parameter @5 = str.ToArrayString()[5] ........ @id=str.ToArrayString()[2]
                    cmd = new(sql, dbconnect.Cnn);
                    for (int i = 3; i < str.Split(",").Length; i++)
                    {
                        cmd.Parameters.AddWithValue("" + (i + 1) + "", str.Split(",")[i + 1]);
                        i++;
                    }
                    cmd.Parameters.AddWithValue("id", str.Split(",")[2]);
                }

                // DELETE
                if (str.Split(",")[0] == "Delete")
                {
                    // sql = Delete tablename where 
                    sql = $"Delete from {str.Split(",")[1]} where ";
                    // + field2 = @3 and field4 = @5 ......
                    for (int i = 2; i < str.Split(",").Length - 3; i++)
                    {
                        sql += str.Split(",")[i] + "=@" + (i + 1) + "and";
                        i++;
                    }
                    sql += (str.Split(",")[str.Split(",").Length - 2]) + "=@" + (str.Split(",").Length - 1);
                    // Set parameter @0=str.ToArrayString()[2],@2=str.ToArrayString()[4]
                    cmd = new(sql, dbconnect.Cnn);
                    for (int i = 2; i < str.Split(",").Length; i++)
                    {
                        cmd.Parameters.AddWithValue("" + (i + 1) + "", str.Split(",")[i + 1]);
                        i++;
                    }

                }
                
                int result = cmd.ExecuteNonQuery();
                Console.Write("Execute successful! -- "+ result + " completed!"); Console.ReadLine();
                return result;
            }
            catch
            {
                Console.WriteLine("Cannot execute!"); Console.ReadKey();
                return 0;
            }
            
        }

        // Excute SQLQuery
        public List<string> SQLQuery(string str)
        {
            // SELECT            
            try 
            {
                string sql;
                SqlCommand cmd;
                List<string> result = new();

                //sql= Select * from tablename 
                sql = $"Select * from {str.Split(",")[1]}";
                // + where field1 = @3 and field2 = @5 and ......
                if (str.Split(",").Length > 3)
                {
                    sql += " where ";

                    for (int i = 2; i < str.Split(",").Length - 2; i++)
                    {
                        sql += str.Split(",")[i] + "=@" + (i + 1) + " and ";
                        i++;
                    }
                    sql += str.Split(",")[str.Split(",").Length - 2] + "=@" + (str.Split(",").Length - 1);

                    //Set parameter @3=str.ToArrayString()[3],@5=str.ToArrayString()[5]
                    cmd = new(sql, dbconnect.Cnn);
                    for (int i = 2; i < str.Split(",").Length; i++)
                    {
                        cmd.Parameters.AddWithValue("" + (i + 1) + "", str.Split(",")[i + 1]);
                        i++;
                    }
                }
                else
                {
                    // No condition Query
                    cmd = new(sql, dbconnect.Cnn);
                }

                // Reader to list string
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string line = "";
                    for (int j = 0; j < rdr.FieldCount; j++) 
                    {
                        line += rdr[j].ToString().Trim() + ",";
                    }
                    result.Add(line);
                }
                
                rdr.Close();
                //Console.Write("Query successful! -- " + result.Count + " found!"); Console.ReadLine();
                return result;
            }
            catch 
            {
                Console.Write("CANNOT QUERY!"); Console.ReadKey();
                return new List<string>();
            }
            
        }

    }
}
