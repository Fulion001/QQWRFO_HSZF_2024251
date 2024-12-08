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
        void Add(Person person);
        void Modify(Person person);
    }
    

    public class PersonDataProvider : IPersonDataProvider
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
            return context.People.ToList();
        }

        public Person GetPersonByNeptun(string neptun)
        {
            return context.People.Where(x => x.NeptunID == neptun).First();
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

        public void Modify(Person person)
        {
            var modifyperson=context.People.Where(x=>x.NeptunID==person.NeptunID).First();
            if (modifyperson!=null)
            {
                modifyperson.Name = person.Name;
                modifyperson.Student = person.Student;
                modifyperson.Age = person.Age;
                modifyperson.OrderStatus = person.OrderStatus;
                modifyperson.SpecialRequest = person.SpecialRequest;
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {

                
            }
            
        }
    }
}
