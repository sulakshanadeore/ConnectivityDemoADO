using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConnectivityDemoADO
{
    internal class DepartmentOperations
    {

        //Operations-Method--Insert,Update,Delete,Find,ListofDepts
        //insert,update,delete,select -where,select * from dept

        public bool AddDept(Dept d)
        {
            //@--means parameters
            string s = "insert into Dept values(@deptname,@location)";
            string cnstring = "Data Source=DESKTOP-BO7RKUV;Initial Catalog=Northwind;User ID=sa;Password=User$2025;Trust Server Certificate=True";
            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd = new SqlCommand(s, cn);
            cn.Open();
            //Insert/Update/Delete(Commands)---ExceuteNonQuery(ExecuteNotQuery)
            try
            {
                cmd.Parameters.AddWithValue("@deptname", d.Dname);
                cmd.Parameters.AddWithValue("@location", d.Loc);


                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally 
            {
            cmd.Dispose();
            cn.Close();
            cn.Dispose();
            }
        return true;
        
        }



    }
}
