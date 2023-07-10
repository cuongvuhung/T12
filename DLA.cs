using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using Microsoft.VisualBasic.FileIO;
//THIS CLASS FOR EXECUTE SQL
//2 OPTION: SQLExecute for non query and SQLQuery for query
////SQLExecute 
// (Insert, tblname, value1, value 2, value 3,......)
//                  = Insert into tblname values (value1,value2,....)
// (Update, tblname, id, field1, value1, field2, value2,....)
//                  = Update tblname set field1 = value1, field2 = value2,.... where id = id
// (Delete, tblname, field1, value1, field 2, value2,....) 
//                  = Delete from tblname where field1 = value 1 and field2 = value 2
////SQLQuery
//
namespace T12
{
    internal class DLA
    {
        // MS SQL Worker
        // Excute SQL non querry
        private DBConnect dbconnect = new();
        public int SQLExecute(string str)
        {
            // get sql template
            string sql;
            SqlCommand cmd = dbconnect.Cnn.CreateCommand();
// INSERT            
            if (str.Split()[0] == "Insert")
            {
                // sql = Insert into tablename values 
                sql = $"Insert into {str.Split()[1]} values (";
                for (int i = 2; i < str.Count() - 1; i++)
                {
                    // (@0,@1,....
                    sql += "@" + i + ",";                   
                }
                    // @n)
                sql += "@" + (str.Count() - 1) + ")";
                
                // Set parameter @2 = str.Split()[2]) .v.v.
                cmd = new(sql, dbconnect.Cnn);
                for (int i = 2; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + i + "", str.Split()[i]);
                }
            }

// UPDATE
            if (str.Split()[0] == "Update")
            {
                // sql = Update tablename set 
                sql = $"Update {str.Split()[1]} Set ";

                // field3 = @4, field5= @6,..... 
                for (int i = 3; i < str.Count() - 2; i++)
                {
                    sql += str.Split()[i] + "=@" + (i + 1) + ",";
                    i++;
                }
                // fieldn = @n where id =@id
                sql += (str.Split()[str.Count() - 2]) + "=@" + (str.Count() - 1);
                sql += " where id = @id";

                // @5 = str.ToArrayString()[5] ........ @id=str.ToArrayString()[2]
                cmd = new(sql, dbconnect.Cnn);
                for (int i = 3; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + (i + 1) + "", str.Split()[i + 1]);
                    i++;
                }
                cmd.Parameters.AddWithValue("id", str.Split()[2]);
            }

// DELETE
            if (str.Split()[0] == "Delete")
            {
                // sql = Delete tablename where 
                sql = $"Delete from {str.Split()[1]} where ";
                // field2 = @3 and field4 = @5 ......
                for (int i = 2; i < str.Count() - 2; i++)
                {
                    sql += str.Split()[i] + "=@" + (i + 1) + "and";
                    i++;
                }
                sql += (str.Split()[str.Count() - 2]) + "=@" + (str.Count() - 1);
                // @0=str.ToArrayString()[2],@2=str.ToArrayString()[4]
                //Console.WriteLine(sql);Console.ReadLine();
                cmd = new(sql, dbconnect.Cnn);
                for (int i = 2; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + (i + 1) + "", str.Split()[i + 1]);
                    i++;
                }
            }
            int result = cmd.ExecuteNonQuery();
            //Console.Write("Execute successful!"); Console.ReadLine();
            return result;
        }

        // Excute SQL SELECT 
        public List<SqlDataReader> SQLQuery(string str)
        // ('Select', 'Table name', Condition Field1, Val1, Condition Field2, Val2,.....)        
        {
// SELECT            
            string sql;
            SqlCommand cmd;
            List<SqlDataReader> result = new();

            //sql= Select * from tablename where field1 = @3 and field2 = @5 and ......
            sql = $"Select * from {str.Split()[1]}";
            if (str.Count() > 3)
            {
                sql += " where ";

                for (int i = 2; i < str.Count() - 2; i++)
                {
                    sql += str.Split()[i] + "=@" + (i + 1) + " and ";
                    i++;
                }
                sql += str.Split()[str.Count() - 2] + "=@" + (str.Count() - 1);
                
                //@3=str.ToArrayString()[3],@5=str.ToArrayString()[5]
                cmd = new(sql, dbconnect.Cnn);
                for (int i = 2; i < str.Count(); i++)
                {
                    cmd.Parameters.AddWithValue("" + (i + 1) + "", str.Split()[i + 1]);
                    i++;
                }
            }
            else
            {
                cmd = new(sql, dbconnect.Cnn);
            }
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {                                
                result.Add(rdr);
            }            
            return result;
        }

    }
}
