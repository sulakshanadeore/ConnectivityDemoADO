using ConnectivityDemoADO;
using System.Collections.Concurrent;
using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("Menu");
        Console.WriteLine("1.Connected \n2. Disconnected");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                ConnectedDemo();
                break;
            case 2:
                DisconnectedDemo();
                break;
        }

      

    }

    private static void DisconnectedDemo()
    {
        Console.WriteLine("------EmpMenu------");
        Console.WriteLine("1.Insert \n2.Update \n3.Delete \n4.Find  \n5.ShowList  \n8.Exit");
        Console.WriteLine("Enter choice between 1 to 5, 8 to Exit..");
        int userchoice = Convert.ToInt32(Console.ReadLine());
        EmployeeOperations empoperations = new EmployeeOperations();
        Emp empobj = new Emp();
        switch (userchoice)
        {
            case 1:
                Console.WriteLine("Enter First name");
                empobj.FirstName = Console.ReadLine();
                Console.WriteLine("Enter City");
                empobj.City = Console.ReadLine();
                Console.WriteLine("Enter Birthdate Pls enter in format mm/dd/yyyy");
                empobj.BirthDate = Convert.ToDateTime(Console.ReadLine());
                bool status = false;
                try
                {
                     status = empoperations.AddEmp(empobj);
                    if (status)
                    {
                        Console.WriteLine("Employee added successfully..");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }


           
                break;
            case 2:
                Console.WriteLine("Enter empoyeeid to find and update");
                empobj.EmployeeId= Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter First Name");
                empobj.FirstName = Console.ReadLine();
                Console.WriteLine("Enter City");
                empobj.City = Console.ReadLine();
                Console.WriteLine("Enter birthdate mm/dd/yyyy format");
                empobj.BirthDate = Convert.ToDateTime(Console.ReadLine());

                bool updateStatus=empoperations.UpdateEmp(empobj.EmployeeId, empobj);
                if (updateStatus) {
                    Console.WriteLine("Updated Successfully....");
                }

                break;
            case 3:
                Console.WriteLine("Enter the Employeeid to Delete");
                int empid=Convert.ToInt32(Console.ReadLine());
                bool deletestatus=empoperations.DeleteEmp(empid);
                if (deletestatus)
                {
                    Console.WriteLine("Record deleted successfully...");
                }
                break;
            case 4:
                Console.WriteLine("Enter employeeid to find");
                 empid = Convert.ToInt32(Console.ReadLine());
                empobj=empoperations.FindEmployee(empid);
                if (empobj.EmployeeId != 0)
                {
                    Console.WriteLine(empobj.EmployeeId);
                    Console.WriteLine(empobj.FirstName);
                    Console.WriteLine(empobj.City);
                    Console.WriteLine(empobj.BirthDate);
                }
                else
                    Console.WriteLine("Check the employee id");
                    break;
            case 5:
                List<Emp> emplist=empoperations.ShowEmpList();
                foreach (var item in emplist)
                {
                    Console.WriteLine(item.EmployeeId + "  "+ item.FirstName + " "+ item.City + "  "+ item.BirthDate);
                }

                break;
            case 8: break;
        }


    }

    private static void ConnectedDemo()
    {
        Console.WriteLine("---DeptMenu----");
        Console.WriteLine("1.Insert \n2.Update \n4.Find  \n5.ShowList \n6.OrderList of Customers \n7.Show HighValueOrder \n8.Exit");
        Console.WriteLine("Enter choice between 1 to 5, 8 to Exit..");
        int userchoice = Convert.ToInt32(Console.ReadLine());
        DepartmentOperations operations = new DepartmentOperations();
        Dept d = new Dept();
        switch (userchoice)
        {
            case 1:
                Console.WriteLine("Enter Dept Name");
                d.Dname = Console.ReadLine();
                Console.WriteLine("Enter Location");
                d.Loc = Console.ReadLine();
                try
                {
                    bool insertStatus = operations.AddDept(d);
                    if (insertStatus)
                    {
                        Console.WriteLine("Record inserted successfully....");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

                break;
            case 2:

                Console.WriteLine("Enter Deptno");
                int deptno = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Dept name");
                d.Dname = Console.ReadLine();
                Console.WriteLine("Enter Location");
                d.Loc = Console.ReadLine();
                try
                {
                    bool status = operations.EditDept(deptno, d);
                    if (status)
                    {
                        Console.WriteLine("Record updated Successfully...");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }


                break;
            case 4:
                Console.WriteLine("Enter deptno");
                deptno = Convert.ToInt32(Console.ReadLine());
                try
                {
                    d = operations.FindDept(deptno);
                    if (d.Deptno != 0)
                    {
                        Console.WriteLine($"Deptno={d.Deptno} | DeptName={d.Dname} | Location={d.Loc}");
                    }
                    else
                    {
                        Console.WriteLine("This deptno doesn't exists...");
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 5:
                List<Dept> depts = operations.ShowDepts();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(new string('-', 70));
                Console.WriteLine($"{"Deptno",-10}    | {"DeptName",-30}    | {"Location",-30}");
                Console.WriteLine(new string('-', 70));
                Console.ForegroundColor = ConsoleColor.White;

                foreach (var item in depts)
                {
                    Console.WriteLine($"{item.Deptno,-10}    | {item.Dname,-30}    | {item.Loc,-30}");
                }
                break;
            case 6:
                OrderDetailsOperations odo = new OrderDetailsOperations();
                Console.WriteLine("Enter custid");
                string customerid = Console.ReadLine();
                List<OrderDetails> details=odo.ShowCustomerOrders(customerid);
                foreach (var item in details)
                {
                    Console.WriteLine($"{item.Orderid} | {item.ORderDate}   |  {item.CustID} | {item.ProductID}   |  {item.Discount}");
                }

                break;

            case 7:
                OrderDetailsOperations od = new OrderDetailsOperations();
                List<HigherOrderValues> orderlist=od.GetAllHighOrders();
                foreach (var item in orderlist)
                {
                    Console.WriteLine($"{item.Orderid} | {item.OrderValue}");
                }

                break;
            case 8:
                Environment.Exit(1);
                break;
        }
    }
}