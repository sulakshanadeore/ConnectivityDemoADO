using ConnectivityDemoADO;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("Menu");
        Console.WriteLine("1.Insert \n8.Exit");
        int userchoice = Convert.ToInt32(Console.ReadLine());
        DepartmentOperations operations = new DepartmentOperations();
        switch (userchoice)
        {
            case 1:
                Dept d = new Dept();
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
            default:
                break;
        }


    }
}