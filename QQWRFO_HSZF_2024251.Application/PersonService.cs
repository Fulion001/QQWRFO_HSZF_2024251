using Newtonsoft.Json;
using QQWRFO_HSZF_2024251.Model;
using QQWRFO_HSZF_2024251.Presistance.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWRFO_HSZF_2024251.Application
{
    //IPersonService interface
    public interface IPersonService
    {
        Person GetPersonByNeptun(string neptun);
        List<Person> GetAGroupOfPerson(int x);
        string Get10People(int x, int begin);
        void ExecuteOrder66();
    }
    public interface IPersonCreate
    {
        void CreatePerson(string name, int age, string specialRequest, string StudentorTeacher);
    }
    public interface IPersonUpdate
    {
        bool CheckNeptun(string neptun);
        void ModifyRequest(string neptun, string name, int age, string specialrequest, string studentorteacher, string orderstatus);
        
    }
    public interface IPersonSerialize
    {
        bool JsonSerialize(string path);
    }


    //Main Person Service class
    public class PersonService : IPersonService, IPersonCreate, IPersonUpdate, IPersonSerialize
    {
        //connection to data provider
        private readonly IPersonDataProvider personDataProvider;
        public PersonService(IPersonDataProvider pdp)
        {
            personDataProvider = pdp;
        }

        //implement IPersoonCreate interface
        public void CreatePerson(string name, int age, string specialRequest, string studentorteacher)
        {
            List<Person> p = new List<Person>(personDataProvider.GetAllPeople());
            int ok;
            string neptun = "";
            do
            {
                neptun = NeptunGenerator();
                ok = 0;
                foreach (var item in p)
                {

                    if (item.NeptunID==neptun)
                    {
                        ok++;
                    }
                }
            } while (ok!=0);
            Person person = new Person();
            person.NeptunID = neptun;
            person.Name = name;
            person.Age = age;
            person.SpecialRequest = specialRequest;
            if (studentorteacher == "1")
            {
                person.Student = Job.Student;
            }
            else
            {
                person.Student = Job.Teacher;
            }
            personDataProvider.Add(person);
            

        }

        //implement IPersonService interface
        public List<Person> GetAGroupOfPerson(int x)
        {
            List<Person> p = personDataProvider.GetAllPeople();
            if (x==0)
            {
                return p.Where(x=>x.Student==Job.Student).ToList();
            }
            else if (x==2)
            {
                return p.Where(x => x.OrderStatus == Status.Paid).ToList();
            }
            else
            {
                return p.Where(x => x.Student == Job.Teacher).ToList();
            }
        }
        public string Get10People(int x, int begin)
        {
            List<Person> p;
            string peoples = "";
            int end=begin+10;
            

                p = new List<Person>(GetAGroupOfPerson(x));
                if (begin >p.Count())
                {
                    begin = p.Count();
                }
                if (end > p.Count())
                {
                    end = p.Count();
                }
                
                for (int i = begin; i < end; i++)
                {
                    peoples += ConvertPersonToString(p[i]);
                }
                return peoples;
            

        }
        public string ConvertPersonToString(Person p)
        {
            string write="";
            for (int i = 0; i < 100; i++)
            {
                write += "-";
            }
            write += "\n";
            write += $"Neptun ID: {p.NeptunID}\tName: {p.Name,-16}\tAge: {p.Age,-
                3}|\t{p.Student}|\t{p.OrderStatus}\n\tSpecial Request: {p.SpecialRequest}\n";
            
            return write;

        }
        public void ExecuteOrder66()
        {
            List<Person> p = GetAGroupOfPerson(2);
            foreach (var item in p)
            {
                ModifyRequest(
                    item.NeptunID,
                    item.Name,
                    item.Age,
                    item.SpecialRequest,
                    item.Student.ToString(),
                    "Ordered"
                    );
            }
        }
        public Person GetPersonByNeptun(string neptun)
        {
            return personDataProvider.GetPersonByNeptun(neptun);
        }



        public string NeptunGenerator()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] result = new char[6];

            for (int i = 0; i < 6; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }

        //IPersonUpdate interface
        public bool CheckNeptun(string neptun)
        {
            List<Person> all = personDataProvider.GetAllPeople();
            foreach (var item in all)
            {
                if (item.NeptunID==neptun)
                {
                    return true;
                }
            }
            return false;
        }
        public void ModifyRequest(string neptun, string name, int age, string specialrequest, string studentorteacher, string orderstatus)
        {
            Person p = GetPersonByNeptun(neptun);
            p.Name = name;
            p.Age = age;
            p.SpecialRequest = specialrequest;
            if (studentorteacher == "1"|| studentorteacher== "Student")
            {
                p.Student = Job.Student;
            }
            else
            {
                p.Student = Job.Teacher;
            }
            if (orderstatus == "Not_Paid")
            {
                p.OrderStatus = Status.Not_Paid;
            }
            else if (orderstatus == "Paid")
            {
                p.OrderStatus = Status.Paid;
            }
            else
            {
                p.OrderStatus = Status.Ordered;
            }
            personDataProvider.Modify(p);
        }

        //IPersonSerialize interface
        public bool JsonSerialize(string path)
        {
            
            try
                {
                    string json = File.ReadAllText(path);

                    // JSON deszerializálása List<Person>-né
                    List<Person> people = JsonConvert.DeserializeObject<List<Person>>(json);

                    // Az adatok hozzáadása a personDataProvider-hez
                    foreach (var person in people)
                    {
                        personDataProvider.Add(person);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }



        }
    }
}
