using QQWRFO_HSZF_2024251.Presistance.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace QQWRFO_HSZF_2024251.Application
{
    //ITransactionService interface
    public interface ITransactionService
    {
        List<Transaction> GetTransactionsByNeptun(string neptun);

        List<Transaction> GetTransactions();
    }


    //Main Transaction Service class
    public class TransactionService : ITransactionService
    {
        //Data provider
        private readonly ITransactinDataProvider transactiondataProvider;
        public TransactionService(ITransactinDataProvider transactinDataProvider)
        {
            this.transactiondataProvider = transactinDataProvider;
        }


        //ITransactionService implements
        public List<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactionsByNeptun(string neptun)
        {
            return transactiondataProvider.GetTransactionsByNeptunId(neptun);
        }
    }
}
