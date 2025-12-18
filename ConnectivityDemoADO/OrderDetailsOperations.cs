using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client.AuthScheme.PoP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectivityDemoADO
{

    internal class HigherOrderValues
    {
        public int Orderid { get; set; }
        public double OrderValue { get; set; }

    }
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



        public List<HigherOrderValues> GetAllHighOrders()
        {
            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd = new SqlCommand("sp_HigherValueOrdersThanAvgValueOrders", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            List<HigherOrderValues> orderslist = new List<HigherOrderValues>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    HigherOrderValues obj = new HigherOrderValues();
                    obj.Orderid = Convert.ToInt32(dr["orderid"]);
                    obj.OrderValue = Convert.ToDouble(dr["value"]);
                       orderslist.Add(obj);  
                }

            }

            cn.Close();
            cn.Dispose();
                return orderslist;  

        }


        public int GetOrderCount(string custid)
        {
            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd = new SqlCommand("sp_GetOrderCount", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                     
            cmd.Parameters.AddWithValue("@custid", custid);
            //SqlParameter p1 = new SqlParameter("@orderCount",System.Data.SqlDbType.Int);

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@orderCount";
            p1.SqlDbType = System.Data.SqlDbType.Int;
                            
            p1.Direction = System.Data.ParameterDirection.Output;
            cmd.Parameters.Add(p1);
            cn.Open();
            cmd.ExecuteNonQuery();
            int cnt = 0;

            cnt=Convert.ToInt32(cmd.Parameters["@orderCount"].Value);
              cn.Close();
            cn.Dispose();
            return cnt;
        }
        public List<OrderDetails> ShowCustomerOrders(string custid)
        { 
        //    string query= "select od.orderid,orderdate,productid,CustomerID,Discount from[Order Details]  as od join orders as o on od.orderid = o.orderid and CustomerID = @customerid";

            SqlConnection cn = new SqlConnection(cnstring);
            SqlCommand cmd = new SqlCommand("sp_CustOrders", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
        //    SqlCommand cmd=new SqlCommand(query,cn);
           cn.Open();
            cmd.Parameters.AddWithValue("@customerid", custid);
            SqlDataReader dr=cmd.ExecuteReader();



            List <OrderDetails> orderdetails = new List<OrderDetails>();

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
