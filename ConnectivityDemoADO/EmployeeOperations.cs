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
        //public bool DeleteEmp(int empid) { }

        //public bool UpdateEmp(int empid,Emp emp) { }

        //public List<Emp> ShowEmpList() { }

        //public Emp FindEmployee(int empid) { }

    }
}
