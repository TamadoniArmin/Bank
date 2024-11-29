using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.interfaces.Services;
using Quiz.Repository;

namespace Quiz.Service
{
    public class TransactionService : ITransactionService
    {
        TransactionRepository repository=new TransactionRepository();
        public bool AddTransaction(string SourceCardNumber, string DestinationCardNumber, int AmountOfMoney)
        {
            var ResultOfAction= repository.AddTransaction(SourceCardNumber, DestinationCardNumber, AmountOfMoney);
            if (!ResultOfAction)
            {
                Console.WriteLine("Action is invalid!!!");
                return false;
            }
            else { return true; }
        }
    }
}
