using QQWRFO_HSZF_2024251.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace QQWRFO_HSZF_2024251.Presistance.MsSql
{
    public interface ITransactinDataProvider
    {
        List<PitcherTransaction> GetTransactions();
        bool CreateTrasaction(string ID, string neptun, int amount, DateTime paymentTime);
    }
    

    //main Transactin Data Provider class
    public class TransactinDataProvider : ITransactinDataProvider
    {
        //Connection
        private readonly AppDbContext context;
        public TransactinDataProvider(AppDbContext dbContext)
        {
            context = dbContext;
        }


        //implement ITrasactionCreate interface
        public bool CreateTrasaction(string ID, string neptun, int amount, DateTime paymentTime)
        {

            var newTransaction = new PitcherTransaction
            {
                TransactionId = ID,
                Amount = amount,
                PaymentTime = paymentTime,
                NeptunID = neptun
            };

            try
            {
                context.Transactions.Add(newTransaction);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //implement ITransactinDataProvider interface
        public List<PitcherTransaction> GetTransactions()
        {
            return context.Transactions.ToList();
        }


    }
}
