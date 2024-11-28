using QQWRFO_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQWRFO_HSZF_2024251.Presistance.MsSql
{
    public interface IPersonDataProvider
    {
        Person GetPersonByNeptun(string neptun);
        List<Person> GetAllPeople();
    }
    public interface IPersonAdd
    {
        void Add(Person person);
    }
    public class PersonDataProvider : IPersonDataProvider, IPersonAdd
    {
        //Connection
        private readonly AppDbContext context;
        public PersonDataProvider(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        //implement IPersonDataProvider interface
        public List<Person> GetAllPeople()
        {
            List<Person> list = new List<Person>();
            foreach (var item in context.People)
            {
                list.Add(item);
            }
            return list;
        }

        public Person GetPersonByNeptun(string neptun)
        {
            throw new NotImplementedException();
        }

        public void Add(Person person)
        {
            context.People.Add(person);
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
