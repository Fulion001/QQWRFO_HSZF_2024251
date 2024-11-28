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

        List<Transaction> GetPeople();
    }
    public interface IPersonCreate
    {
        Person CreatePerson(string name, int age, string specialRequest, string StudentorTeacher, string OrderState);
    }

    //Main Person Service class
    public class PersonService : IPersonService, IPersonCreate
    {
        //connection to data provider
        private readonly IPersonDataProvider personDataProvider;
        public PersonService(IPersonDataProvider pdp)
        {
            personDataProvider = pdp;
        }

        //implement IPersoonCreate interface
        public Person CreatePerson(string name, int age, string specialRequest, string studentorteacher, string orderstate)
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
            Person person = new Person(neptun, name, age,specialRequest,studentorteacher, orderstate);
            return person;
            

        }

        //implement IPersonService interface
        public List<Transaction> GetPeople()
        {
            throw new NotImplementedException();
        }

        public Person GetPersonByNeptun(string neptun)
        {
            throw new NotImplementedException();
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
    }
}
