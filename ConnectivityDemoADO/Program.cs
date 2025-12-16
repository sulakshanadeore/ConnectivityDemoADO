using ConnectivityDemoADO;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("Menu");
        Console.WriteLine("1.Insert \n2.Update \n4.Find  \n5.ShowList \n8.Exit");
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
                Console.WriteLine(new string('-', 70));
                Console.WriteLine($"{"Deptno",-10}    | {"DeptName",-30}    | {"Location",-30}");
                Console.WriteLine(new string('-', 70));
                foreach (var item in depts)
                {
                    Console.WriteLine($"{item.Deptno,-10}    | {item.Dname,-30}    | {item.Loc,-30}");
                }
                break;


            case 8:
                Environment.Exit(1);
                break;
        }


    }
}