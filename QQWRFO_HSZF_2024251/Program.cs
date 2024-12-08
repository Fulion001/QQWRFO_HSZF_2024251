using Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using QQWRFO_HSZF_2024251.Application;
using QQWRFO_HSZF_2024251.Model;
using QQWRFO_HSZF_2024251.Presistance.MsSql;
using System.Collections.Generic;

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
                    services.AddSingleton<IPersonUpdate, PersonService>();
                    services.AddSingleton<IPersonSerialize, PersonService>();
                    services.AddSingleton<IPersonDataProvider, PersonDataProvider>();
                    services.AddSingleton<ITransactionService, TransactionService>();
                    services.AddSingleton<ITransactionCreate, TransactionService>();
                    services.AddSingleton<ITransactionSerialize, TransactionService>();
                    services.AddSingleton<ITransactinDataProvider, TransactinDataProvider>();
                })
                .Build();

            host.Start();

            //Call services
            using (var serviceScope = host.Services.CreateScope())
            {
                var transactionService = serviceScope.ServiceProvider.GetRequiredService<ITransactionService>();
                var transactionCreate = serviceScope.ServiceProvider.GetRequiredService<ITransactionCreate>();
                var personService = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();
                var personcreate = serviceScope.ServiceProvider.GetRequiredService<IPersonCreate>();

                // Use services...
            }


            //Display
            Program p = new Program();
            p.MainMenu(host);
        }
        
        
        //Core menus
        private void MainMenu(IHost host)
        {

            int i = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1--Student Menu\n2--Teacher Menu\n3--Transaction Menu\n4--New Person\n5--Excecute Order 66\n6--person Serialize\n7--Exit");
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
                        using (var serviceScope = host.Services.CreateScope())
                        {
                            var personUpdate = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();

                            Console.Clear();

                            // Use services...
                            personUpdate.ExecuteOrder66();

                        }
                        break;
                    case "7":
                        i++;
                        break;
                    case "6":
                        JsonSerializer(host);
                        break;
                    default:
                        break;
                }

            }
        }
        private void StudentMenu(IHost host)
        {
            int i = 0;
            int start = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Student Menu");
                Console.WriteLine("1--Payment | 2--Order | Q--Back by 10 | E--forvard by 10 | 3--Modify | 4--Exit");
                Console.WriteLine();
                Console.WriteLine();
                using (var serviceScope = host.Services.CreateScope())
                {
                    var tenPerson = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();

                    Console.WriteLine(tenPerson.Get10People(0, start));
                }
                string s = Console.ReadLine();

                switch (s)
                {
                    case "1":
                        PaymentMenu(host);
                        break;
                    case "2":
                        
                            break;
                    case "Q":
                        start -= 10;
                        if (start<0)
                        {
                            start = 0;
                        }
                        break;
                    case "E":
                        start+= 10;
                        break;
                    case "3":
                        ModifyPersonMenu(host);
                        break;
                    case "4":
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
            int start = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Teacher Menu");
                Console.WriteLine("1--Payment | 2--Order | Q--Back by 10 | E--forvard by 10 | 3--Modify | 4--Exit");
                Console.WriteLine();
                Console.WriteLine();
                using (var serviceScope = host.Services.CreateScope())
                {
                    var tenPerson = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();

                    Console.WriteLine(tenPerson.Get10People(1, start));
                }
                string s = Console.ReadLine();

                switch (s)
                {
                    case "1":
                        PaymentMenu(host);
                        break;
                    case "2":

                        break;
                    case "Q":
                        start -= 10;
                        if (start < 0)
                        {
                            start = 0;
                        }
                        break;
                    case "E":
                        start += 10;
                        break;
                    case "3":
                        ModifyPersonMenu(host);
                        break;
                    case "4":
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
            int start = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Transaction Menu");
                Console.WriteLine("Q--Back by 10 | E--forvard by 10 | 1--Exit");
                using (var serviceScope = host.Services.CreateScope())
                {
                    var tenTransaction = serviceScope.ServiceProvider.GetRequiredService<ITransactionService>();

                    Console.WriteLine(tenTransaction.Get10Transaction(i,start));
                }
                string s = Console.ReadLine();
                switch (s)
                {
                    case "Q":
                        start -= 10;
                        if (start < 0)
                        {
                            start = 0;
                        }
                        break;
                    case "E":
                        start += 10;
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
            int age = AgeOrAmount("Age");

            //-----------------------------------------------------
            //speciális kérés
            string specialrequest = SpecialRequest();

            //-----------------------------------------------------
            //tanuló vagy tanár
            string studentorteacher = StudentrTeacher();

            //-----------------------------------------------------
            //adatellenőrzés
            DataCheck(name, age, specialrequest, studentorteacher, host);

        }
        private void JsonSerializer(IHost host)
        {
            int i = 0;
            while (i == 0)
            {
                Console.Clear();
                Console.WriteLine("Student Menu");
                Console.WriteLine("1--People | 2--Trasactions | 3--Exit");
                Console.WriteLine();
                Console.WriteLine();
                string s = Console.ReadLine();

                switch (s)
                {
                    case "1":
                        PersonJson(host);
                        break;
                    case "2":
                        TransactionJson(host);
                        break;
                    case "3":
                        i++;
                        break;
                    default:
                        break;
                }

            }
        }
        
        //person specific menus
        private void PaymentMenu(IHost host)
        {
            
            using (var serviceScope = host.Services.CreateScope())
            {
                var transactionCreate = serviceScope.ServiceProvider.GetRequiredService<ITransactionCreate>();
                var transactionService = serviceScope.ServiceProvider.GetRequiredService<ITransactionService>();
                var checkNeptun = serviceScope.ServiceProvider.GetRequiredService<IPersonUpdate>();
                var getPersonToModify = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();



                // Use services...
                string neptun = "";
                do
                {
                    Console.WriteLine("Neptun ID:");
                    neptun = Console.ReadLine();
                } while (neptun.Length != 6);
                Console.Clear();
                if (checkNeptun.CheckNeptun(neptun) == true)
                {
                    Console.WriteLine($"Neptun ID: {neptun}");
                    int amount = AgeOrAmount("Amount");

                    string inp = "";
                    do
                    {
                        Console.WriteLine("Is the data correct?\n1--yes | 2--no");
                        inp = Console.ReadLine();
                    } while (inp != "y" && inp != "n");
                    bool i = false;
                    
                        i = transactionCreate.AddTrasaction(neptun, amount);
                    

                    if (i == true)
                    {
                        Console.WriteLine("Success");
                        if (transactionService.AmountNeeded(neptun)>0)
                        {
                            Console.WriteLine($"This person need another: {transactionService.AmountNeeded(neptun).ToString()} HUF");
                        }
                        else
                        {
                            checkNeptun.ModifyRequest(
                                neptun, 
                                getPersonToModify.GetPersonByNeptun(neptun).Name, 
                                getPersonToModify.GetPersonByNeptun(neptun).Age,
                                getPersonToModify.GetPersonByNeptun(neptun).SpecialRequest,
                                getPersonToModify.GetPersonByNeptun(neptun).Student.ToString(), 
                                "Paid");

                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Neptun not found!");
                    Console.ReadLine();
                }
            }
        }
        private void ModifyPersonMenu(IHost host)
        {
            
            string neptun = "";
            do
            {
                
                Console.WriteLine("Neptun ID:");
                neptun = Console.ReadLine();
            } while (neptun.Length!=6);
            bool help;
            using (var serviceScope = host.Services.CreateScope())
            {
                var personCreate = serviceScope.ServiceProvider.GetRequiredService<IPersonCreate>();
                var personModify = serviceScope.ServiceProvider.GetRequiredService<IPersonService>();
                var personCheck = serviceScope.ServiceProvider.GetRequiredService<IPersonUpdate>();
                Console.Clear();

                // Use services...

                 help = personCheck.CheckNeptun(neptun);

                if (help == true)
                {
                    //Person p = personModify.GetPersonByNeptun(neptun);
                    Console.WriteLine($"Neptun ID: {personModify.GetPersonByNeptun(neptun).NeptunID}\nName: {personModify.GetPersonByNeptun(neptun).Name}\nAge: {personModify.GetPersonByNeptun(neptun).Age}\n{personModify.GetPersonByNeptun(neptun).Student.ToString()}\n{personModify.GetPersonByNeptun(neptun).OrderStatus.ToString()}");
                    Console.WriteLine();
                    Console.WriteLine("What is that you want to change?\n1--name | 2--age | 3--Student Or Teacher | 4--Special Request");
                    string change = "";
                    int c = 0;
                    string studorteacher = personModify.GetPersonByNeptun(neptun).Student.ToString();
                    string name = personModify.GetPersonByNeptun(neptun).Name;
                    int age = personModify.GetPersonByNeptun(neptun).Age;
                    string special = personModify.GetPersonByNeptun(neptun).SpecialRequest;
                    string orderstatus = personModify.GetPersonByNeptun(neptun).OrderStatus.ToString();
                    do
                    {

                        change = Console.ReadLine();
                        Console.Clear();
                        switch (change)
                        {
                            case "1":
                                Console.WriteLine("New Name:");
                                name = Name();
                                c = 1;
                                break;

                            case "2":
                                Console.WriteLine("New Age:");
                                age = AgeOrAmount("Age");
                                c = 1;
                                break;

                            case "4":
                                Console.WriteLine("New Special Request:");
                                special = SpecialRequest();
                                c = 1;
                                break;

                            case "3":
                                Console.WriteLine("Change Studet or Teacher:");
                                studorteacher = StudentrTeacher();
                                c = 1;
                                break;

                            default:
                                Console.WriteLine("Incorrect format!");
                                c = 1;
                                break;
                        }

                    } while (c == 0);
                    if (studorteacher=="Student")
                    {
                        studorteacher = "1";
                    }
                    
                    personCheck.ModifyRequest(neptun, name, age, special, studorteacher, orderstatus);
                }
            }
            
            
        }

        //Json Serializer menus
        private void PersonJson(IHost host)
        {
            Console.Clear();
            Console.WriteLine("Filepath: ");
            string path = "";
            do
            {
                path = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(path));
            using (var serviceScope = host.Services.CreateScope())
            {
                var personSerialize = serviceScope.ServiceProvider.GetRequiredService<IPersonSerialize>();

                Console.Clear();

                // Use services...
                bool cw =personSerialize.JsonSerialize(path);
                if (cw == true)
                {
                    Console.WriteLine("Success");
                }
                else { Console.WriteLine("Failed"); }
                Console.ReadLine();

            }
        }
        private void TransactionJson(IHost host)
        {
            Console.Clear();
            Console.WriteLine("Filepath: ");
            string path = "";
            do
            {
                path = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(path));
            using (var serviceScope = host.Services.CreateScope())
            {
                var transactionSerialize = serviceScope.ServiceProvider.GetRequiredService<ITransactionSerialize>();

                Console.Clear();

                // Use services...
                bool cw = transactionSerialize.JsonSerialize(path);
                if (cw == true)
                {
                    Console.WriteLine("Success");
                }
                else { Console.WriteLine("Failed"); }
                Console.ReadLine();

            }
        }

        //Sub methods
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
        private int SaveOrNot(string name, int age, string specialrequest, string studentorteacher, IHost host)
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
                    
                    Console.Clear();

                    // Use services...
                    personCreate.CreatePerson(name,age,specialrequest,studentorteacher);
                    
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
        private int AgeOrAmount(string name)
        {
            int age = 0;
            do
            {
                Console.Write($"{name}:");
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
        private void DataCheck(string name, int age, string specialrequest, string studentorteacher, IHost host)
        {
            int x = 0;
            bool isok = IsTheDataCorrect();
            
                if (isok == true)
                {
                    x = SaveOrNot(name, age, specialrequest, studentorteacher, host);
                    
                }
                else
                {
                    int c = 0;
                    Console.WriteLine("What is that you want to change?");
                    Console.WriteLine("1--Name | 2--Age | 3--Special request | 4--Student Or Teacher");
                    do
                    {

                        string change = Console.ReadLine();
                        Console.Clear();
                        switch (change)
                        {
                            case "1":
                                Console.WriteLine("New Name:");
                                name = Name();
                                c = 1;
                                break;

                            case "2":
                                Console.WriteLine("New Age:");
                                age = AgeOrAmount("Age");
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

                            default:
                                Console.WriteLine("Incorrect format!");
                                c = 1;
                                break;
                        }

                    } while (c == 0);
                    string studorteach = "";
                    string ordstat = "";
                    if (studentorteacher == "1")
                    {
                        studorteach = "Student";
                        ordstat = "Not Paid";
                    }
                    else
                    {
                        studorteach = "Teacher";
                        ordstat = "Paid";
                    }
                    Console.Clear();
                    Console.WriteLine($"Name: {name}");
                    Console.WriteLine($"Age: {age.ToString()}");
                    Console.WriteLine($"Special Request: {specialrequest}");
                    Console.WriteLine($"{studorteach}");
                    Console.WriteLine($"{ordstat}");
                    Console.WriteLine();
                    DataCheck(name, age, specialrequest, studentorteacher, host);
                }
            
        }


        

    }
}
