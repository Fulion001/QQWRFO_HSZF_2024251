using Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using QQWRFO_HSZF_2024251.Application;
using QQWRFO_HSZF_2024251.Model;
using QQWRFO_HSZF_2024251.Presistance.MsSql;

namespace QQWRFO_HSZF_2024251
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting application...");
            //Servvices
            using var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<AppDbContext>();
                    services.AddSingleton<IPersonService, PersonService>();
                    services.AddSingleton<IPersonCreate, PersonService>();
                    services.AddSingleton<IPersonDataProvider, PersonDataProvider>();
                    services.AddSingleton<IPersonAdd, PersonDataProvider>();
                    services.AddSingleton<ITransactinDataProvider, TransactinDataProvider>();
                    services.AddSingleton<ITransactionService, TransactionService>();
                })
                .Build();

            host.Start();

            //Call services
            using (var serviceScope = host.Services.CreateScope())
            {
                var transactionService = serviceScope.ServiceProvider.GetRequiredService<ITransactionService>();
                var personService = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();
                var personcreate = serviceScope.ServiceProvider.GetRequiredService<IPersonCreate>();

                // Use services...
            }


            //Display
            Program p = new Program();
            p.MainMenu(host);
        }
        private void MainMenu(IHost host)
        {

            int i = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1--Student Menu\n2--Teacher Menu\n3--Transaction Menu\n4--New Person\n5--Exit");
                string s = Console.ReadLine();
                switch (s)
                {
                    case "1":
                        StudentMenu(host);
                        break;
                    case "2":
                        TeacherMenu(host);
                        break;
                    case "3":
                        TransactionMenu(host);
                        break;
                    case "4":
                        NewPersonMenu(host);
                        break;
                    case "5":
                        i++;
                        break;
                    default:
                        break;
                }

            }
        }
        private void StudentMenu(IHost host)
        {
            int i = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Student Menu");
                Console.WriteLine("1--Payment\n2--Order\nQ--Back by 10\nE--forvard by 10\n3--Exit");
                string s = Console.ReadLine();
                switch (s)
                {
                    case "1":
                        PaymentMenu(host);
                        break;
                    case "2":

                        break;
                    case "Q":
                        break;
                    case "E":
                        break;
                    case "3":
                        i++;
                        break;
                    default:
                        break;
                }

            }
        }
        private void TeacherMenu(IHost host)
        {
            int i = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Teacher Menu");
                Console.WriteLine("1--Order\nQ--Back by 10\nE--forvard by 10\n2--Exit");
                string s = Console.ReadLine();
                switch (s)
                {
                    case "1":

                        break;
                    case "Q":
                        break;
                    case "E":
                        break;
                    case "2":
                        i++;
                        break;
                    default:
                        break;
                }

            }
        }
        private void TransactionMenu(IHost host)
        {
            int i = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Transaction Menu");
                Console.WriteLine("Q--Back by 10\nE--forvard by 10\n1--Exit");
                string s = Console.ReadLine();
                switch (s)
                {
                    case "Q":
                        break;
                    case "E":
                        break;
                    case "1":
                        i++;
                        break;
                    default:
                        break;
                }

            }
        }
        private void NewPersonMenu(IHost host)
        {
            Console.Clear();
            //-----------------------------------------------------
            //név
            string name = Name();

            //-----------------------------------------------------
            //életkor
            int age = Age();

            //-----------------------------------------------------
            //speciális kérés
            string specialrequest = SpecialRequest();

            //-----------------------------------------------------
            //tanuló vagy tanár
            string studentorteacher = StudentrTeacher();

            //-----------------------------------------------------
            //rendelés állapota
            string orderstate = OrderState();

            //-----------------------------------------------------
            //adatellenőrzés
            DataCheck(name, age, specialrequest, studentorteacher, orderstate, host);

        }
        private void PaymentMenu(IHost host)
        {
            Console.Clear();
            Console.Write("Neptun ID: ");
            Console.WriteLine();
            Console.Write("Amount: ");
            Console.WriteLine();

            Console.WriteLine("Is the data correct?\n1--yes | 2--no");

        }


        private bool IsTheDataCorrect()
        {
           
            string isok = "";
            do
            {
                Console.WriteLine("Is the data correct?\ny--yes | n--no");
                isok = Console.ReadLine();
            } while (isok != "y" && isok != "n");
            if (isok == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private int SaveOrNot(string name, int age, string specialrequest, string studentorteacher, string orderstate, IHost host)
        {
            string isok = "";
            do
            {
                Console.WriteLine("y--save and exit | n--don't save and exit");
                isok = Console.ReadLine();
            } while (isok != "y" && isok != "n");
            if (isok == "y")
            {
                using (var serviceScope = host.Services.CreateScope())
                {
                    var personCreate = serviceScope.ServiceProvider.GetRequiredService<IPersonCreate>();
                    var personAdd = serviceScope.ServiceProvider.GetRequiredService<IPersonAdd>();
                    Console.Clear();

                    // Use services...
                    Person person = personCreate.CreatePerson(name,age,specialrequest,studentorteacher,orderstate);
                    personAdd.Add(person);
                }
            }
            return 1;
           
            
        }
        private string Name()
        {
            string name = "";
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                Console.WriteLine();
            } while (string.IsNullOrWhiteSpace(name));
            return name;
        }
        private int Age()
        {
            int age = 0;
            do
            {
                Console.Write("Age:");
                try
                {
                    age = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong input!");
                }
            } while (age == 0);
            return age;
        }
        private string SpecialRequest()
        {
            string specialrequest = "";
            
            Console.Write("Special Request:");
            specialrequest = Console.ReadLine();
            Console.WriteLine();
            return specialrequest;
        }
        private string StudentrTeacher()
        {
            string studentorteacher = "";
            do
            {
                Console.Write("1--Student | 2--Teacher:");
                studentorteacher = Console.ReadLine();
                Console.WriteLine();
            } while (studentorteacher !="1"&&studentorteacher!="2");
            return studentorteacher;
        }
        private string OrderState()
        {
            string orderstate = "";
            do
            {
                Console.Write("1--Not Paid | 2--Paid | 3--Ordered: ");
                orderstate = Console.ReadLine();
                Console.WriteLine();
            } while (orderstate!="1"&&orderstate!="2"&&orderstate!="3");
            return orderstate;
        }
        private void DataCheck(string name, int age, string specialrequest, string studentorteacher, string orderstate, IHost host)
        {
            bool isok = IsTheDataCorrect();
            int x = 0;
            while (x == 0)
            {
                if (isok == true)
                {
                    x=SaveOrNot(name, age, specialrequest, studentorteacher, orderstate, host);
                }
                else
                {
                    int c = 0;
                    Console.WriteLine("What is that you want to change?");
                    Console.Write("1--Name | 2--Age | 3--Special request | 4--Student Or Teacher | 5--Order State");
                    do
                    {
                        string change = Console.ReadLine();
                        switch (change)
                        {
                            case "1":
                                Console.WriteLine("New Name:");
                                name = Name();
                                c = 1;
                                break;

                            case "2":
                                Console.WriteLine("New Age:");
                                age = Age();
                                c = 1;
                                break;

                            case "3":
                                Console.WriteLine("New Special Request:");
                                specialrequest = SpecialRequest();
                                c = 1;
                                break;

                            case "4":
                                Console.WriteLine("Change Studet or Teacher:");
                                studentorteacher = StudentrTeacher();
                                c = 1;
                                break;

                            case "5":
                                Console.WriteLine("Change Order State:");
                                orderstate = OrderState();
                                c = 1;
                                break;

                            default:
                                Console.WriteLine("Incorrect format!");
                                c = 1;
                                break;
                        }

                    } while (c == 0);
                    string studorteach = "";
                    if (studentorteacher=="1")
                    {
                        studorteach = "Student";
                    }
                    else
                    {
                        studorteach = "Teacher";
                    }
                    string ordstat = "";
                    if (orderstate == "1")
                    {
                        ordstat = "Not Paid";
                    }
                    else if (orderstate == "2")
                    {
                        ordstat = "Paid";
                    }
                    else { ordstat = "Ordered"; }
                    Console.Clear();
                    Console.WriteLine($"Name: {name}");
                    Console.WriteLine($"Age: {age.ToString()}");
                    Console.WriteLine($"Special Request: {specialrequest}");
                    Console.WriteLine($"{studorteach}");
                    Console.WriteLine($"{ordstat}");
                    Console.WriteLine();
                    DataCheck(name, age, specialrequest, studentorteacher, orderstate, host);
                }
            }
        }
        

    }
}
