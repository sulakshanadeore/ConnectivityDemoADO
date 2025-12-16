using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConnectivityDemoADO
{
    internal class EmployeeOperations
    {
        string cnstring = "";
        DataSet ds = new DataSet();
        SqlDataAdapter da;
        public EmployeeOperations()
        {
              
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();
            cnstring = config.GetConnectionString("NorthwindCnString");
            SqlConnection cn = new SqlConnection(cnstring);
            da = new SqlDataAdapter("select * from Emp", cn);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            da.Fill(ds, "Emp");//disconnects from the db server.
        }
        public bool AddEmp(Emp emp)
        {
            DataRow newrow=ds.Tables["Emp"].NewRow();
            newrow["FirstName"] = emp.FirstName;
            newrow["City"] = emp.City;
            newrow["BirthDate"] = emp.BirthDate;

            try
            {
                ds.Tables["Emp"].Rows.Add(newrow);

                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                da.Update(ds,"Emp");

            }
            catch (Exception)
            {

                throw;
            }
           return true;
            


        
        }
        public bool DeleteEmp(int empid) {
            bool status = false;
            DataRow found = ds.Tables["Emp"].Rows.Find(empid);
            try
            {
                if (found != null)
                {
                    found.Delete();

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Update(ds, "Emp");
                    status = true;
                }
            }
            catch (Exception)
            { 
                throw; 
            }
            return status;   
    

        }

        public bool UpdateEmp(int empid,Emp emp) {

            bool status = false;
            DataRow found = ds.Tables["Emp"].Rows.Find(empid);
            try
            {
                if (found != null)
                {
                    found["FirstName"] = emp.FirstName;
                    found["City"] = emp.City;
                    found["BirthDate"] = emp.BirthDate;

                    SqlCommandBuilder builder = new SqlCommandBuilder(da);
                    da.Update(ds, "Emp");
                    status = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return status;


        }

        public List<Emp> ShowEmpList() {
            DataTable dt=ds.Tables["Emp"];
            List<Emp> AllEmpdata = dt.AsEnumerable().Select(row=>new Emp
                {
            EmployeeId=row.Field<int>("EmployeeId"),
                FirstName = row.Field<string>("FirstName"),
                City=row.Field<string>("City"),
                BirthDate=row.Field<DateTime>("BirthDate")
            }).ToList();
            return AllEmpdata;
        
        }

        public Emp FindEmployee(int empid) 
        {
            DataRow found = ds.Tables["Emp"].Rows.Find(empid);
            Emp emp = new Emp();
            if (found != null)
            {
               
                emp.EmployeeId = Convert.ToInt32(found["EmployeeId"]);
                emp.FirstName = found["FirstName"].ToString();
                emp.City = found["City"].ToString();
                emp.BirthDate = Convert.ToDateTime(found["BirthDate"]);
               
            }
            return emp;

        }

    }
}
