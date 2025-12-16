using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Identity.Client;
namespace ConnectivityDemoADO
{
    internal class DepartmentOperations
    {

        //Operations-Method--Insert,Update,Delete,Find,ListofDepts
        //insert,update,delete,select -where,select * from dept


        string cnstring = "";
        public DepartmentOperations()
        {
            IConfiguration config=new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();
            cnstring = config.GetConnectionString("NorthwindCnString");
        }
        public List<Dept> ShowDepts()
        {
            string s = "select * from Dept";

            SqlConnection cn=new SqlConnection(cnstring);
            SqlCommand cmd = new SqlCommand(s, cn);
            cn.Open();
            List<Dept> deptlist = new List<Dept>();
            try
            {
                SqlDataReader dr=cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Dept obj = new Dept();
                        obj.Deptno =Convert.ToInt32(dr["Deptno"]);
                        obj.Dname = dr["Dname"].ToString();
                        obj.Loc = dr["Loc"].ToString();
                        deptlist.Add(obj);
                    }
                }

            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return deptlist;
        
        
        }

        public Dept FindDept(int dno)
        {
            string s = "select * from Dept where Deptno=@dno";
            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd=new SqlCommand(s, cn);
            cn.Open();
            Dept obj = new Dept();
            try
            {
                cmd.Parameters.AddWithValue("@dno", dno);
                SqlDataReader dr=cmd.ExecuteReader();//select --executereader
               if (dr.HasRows)
                {
                    //if finding the record by pk then only one  row
                    dr.Read();//one read invocation will read one row
                    obj.Deptno=Convert.ToInt32(dr["Deptno"]);
                    obj.Dname = dr["Dname"].ToString();
                    obj.Loc = dr["Loc"].ToString();

               }
           }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();

            }
            return obj;

        }

        public bool EditDept(int deptno,Dept d)
        {
            string s = "update Dept set Dname=@deptname,Loc=@location where Deptno=@deptno";

            SqlConnection cn=new SqlConnection(cnstring);
            SqlCommand updatecmd=new SqlCommand(s, cn);
            cn.Open();
            try
            {
                updatecmd.Parameters.AddWithValue("@deptname", d.Dname);
                updatecmd.Parameters.AddWithValue("@location", d.Loc);
                updatecmd.Parameters.AddWithValue("@deptno", deptno);
                updatecmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                 throw;
            }
            finally
            {
                updatecmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return true;
        }


        public bool AddDept(Dept d)
        {
            //@--means parameters
            string s = "insert into Dept values(@deptname,@location)";
            //string cnstring = "Data Source=DESKTOP-BO7RKUV;Initial Catalog=Northwind;User ID=sa;Password=User$2025;Trust Server Certificate=True";
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
