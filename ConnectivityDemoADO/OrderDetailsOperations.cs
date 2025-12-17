using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectivityDemoADO
{

    internal class OrderDetails
    {

        public int Orderid { get; set; }
        public string CustID { get; set; }
        public DateTime ORderDate { get; set; }
        public int Discount { get; set; }
        public int ProductID { get; set; }

    }
    internal class OrderDetailsOperations
    {
        string cnstring = "";
        public OrderDetailsOperations()
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();
            cnstring = config.GetConnectionString("NorthwindCnString");
        }
        public List<OrderDetails> ShowCustomerOrders(string custid)
        { 
            string query= "select od.orderid,orderdate,productid,CustomerID,Discount from[Order Details]  as od join orders as o on od.orderid = o.orderid and CustomerID = @customerid";

            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd=new SqlCommand(query,cn);
            cn.Open();
            cmd.Parameters.AddWithValue("@customerid", custid);
            SqlDataReader dr=cmd.ExecuteReader();
           
            List<OrderDetails> orderdetails = new List<OrderDetails>();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    OrderDetails ord_Det = new OrderDetails();
                    ord_Det.Orderid = Convert.ToInt32(dr["orderid"]);
                    ord_Det.ORderDate = Convert.ToDateTime(dr["orderdate"]);
                    ord_Det.ProductID= Convert.ToInt32(dr["productid"]);
                    ord_Det.CustID = dr["CustomerID"].ToString();
                    ord_Det.Discount = Convert.ToInt32(dr["Discount"]);
                    orderdetails.Add(ord_Det);
                }
            }


            cmd.Dispose();
            cn.Close();
            cn.Dispose();
            return orderdetails;


        }

    }
}
