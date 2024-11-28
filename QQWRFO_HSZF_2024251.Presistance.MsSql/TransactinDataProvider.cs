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
        List<Transaction> GetTransactionsByNeptunId(string neptunid);
        List<Transaction> GetTransactions();
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

        //implement ITransactinDataProvider interface
        public List<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactionsByNeptunId(string neptunid)
        {
            throw new NotImplementedException();
        }
    }
}
