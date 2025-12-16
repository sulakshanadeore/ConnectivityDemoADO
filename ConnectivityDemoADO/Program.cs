using ConnectivityDemoADO;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("Menu");
        Console.WriteLine("1.Insert \n2.Update \n8.Exit");
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
            default:
                break;
        }


    }
}