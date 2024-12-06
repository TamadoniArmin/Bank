using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Entity;
using Quiz.interfaces.Services;
using Quiz.Repository;

namespace Quiz.Service
{
    public class TransactionService : ITransactionService
    {
        TransactionRepository repository=new TransactionRepository();
        public bool AddTransaction(string SourceCardNumber, string DestinationCardNumber, double AmountOfMoney)
        {
            var ResultOfAction= repository.AddTransaction(SourceCardNumber, DestinationCardNumber, AmountOfMoney);
            if (!ResultOfAction)
            {
                Console.WriteLine("Action is invalid!!!");
                return false;
            }
            else { return true; }
        }

        public List<TransAction> GetListOfDestanceAction(string Cardnumber)
        {
            return repository.GetListOfDestanceAction(Cardnumber);
        }

        public List<TransAction> GetListOfSourceAction(string Cardnumber)
        {
            return repository.GetListOfSourceAction(Cardnumber);
        }
    }
}
