using Newtonsoft.Json;
using QQWRFO_HSZF_2024251.Model;
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
        List<PitcherTransaction> GetTransactionsByNeptun(string neptun);
        List<PitcherTransaction> GetTransactions();
        string Get10Transaction(int x, int begin);
        int AmountNeeded(string neptun);
    }
    public interface ITransactionCreate
    {
        bool AddTrasaction(string neptun, int amount);
    }
    public interface ITransactionSerialize
    {
        bool JsonSerialize(string path);
    }


    //Main Transaction Service class
    public class TransactionService : ITransactionService, ITransactionCreate, ITransactionSerialize
    {
        //Data provider
        private readonly ITransactinDataProvider transactiondataProvider;
        public TransactionService(ITransactinDataProvider transactinDataProvider)
        {
            this.transactiondataProvider = transactinDataProvider;
        }



        
        


        //ITransactionService implements
        public List<PitcherTransaction> GetTransactions()
        {
            return transactiondataProvider.GetTransactions();
        }
        public List<PitcherTransaction> GetTransactionsByNeptun(string neptun)
        {
            List<PitcherTransaction> transactionByNeptun = GetTransactions();
            List<PitcherTransaction> returnvalue = new List<PitcherTransaction>();

            foreach (var item in transactionByNeptun)
            {
                if (item.NeptunID==neptun)
                {
                    returnvalue.Add(item);
                }
            }
            return returnvalue;
        }
        public string Get10Transaction(int x, int begin)
        {
            List<PitcherTransaction> p;
            string peoples = "";
            int end = begin + 10;


            p = new List<PitcherTransaction>(GetTransactions());
            if (begin > p.Count())
            {
                begin = p.Count();
            }
            if (end > p.Count())
            {
                end = p.Count();
            }

            for (int i = begin; i < end; i++)
            {
                peoples += ConvertTransactionToString(p[i]);
            }
            return peoples;
        }
        public int AmountNeeded(string neptun)
        {
            List<PitcherTransaction> transactions = GetTransactionsByNeptun(neptun);
            int amount = 0;
            foreach (var item in transactions)
            {
                amount += item.Amount;
            }
            if (5000 - amount > 0)
            {
                return 5000 - amount;
            }
            else
            {
                return -1;
            }

        }


        //ITransactionCreate implements
        public bool AddTrasaction(string neptun, int amount)
        {
            string ID = IDGenerator();
            DateTime current = DateTime.Now.Date;
            bool returnBool = transactiondataProvider.CreateTrasaction(ID, neptun, amount, current);
            return returnBool;
        }

        //ITransactionSerialize interface
        public bool JsonSerialize(string path)
        {
            try
            {
                string json = File.ReadAllText(path);

                // JSON deszerializálása List<Person>-né
                List<PitcherTransaction> trans = JsonConvert.DeserializeObject<List<PitcherTransaction>>(json);

                // Az adatok hozzáadása a personDataProvider-hez
                foreach (var tr in trans)
                {
                    transactiondataProvider.CreateTrasaction(tr.TransactionId, tr.NeptunID, tr.Amount, tr.PaymentTime);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string IDGenerator()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] result = new char[16];

            for (int i = 0; i < 16; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
        public string ConvertTransactionToString(PitcherTransaction p)
        {
            string write = "";
            for (int i = 0; i < 100; i++)
            {
                write += "-";
            }
            write += "\n";
            write += $"Transaction ID: {p.TransactionId,-16}\tNeptun ID: {p.NeptunID,-
                3}|\t{p.PaymentTime:yyyy.MM.dd}|\t{p.Amount.ToString()}\n";

            return write;

        }

        
    }
}
