using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Entity;

namespace Quiz.interfaces.Services
{
    public interface ITransactionService
    {
        bool AddTransaction(string SourceCardNumber, string DestinationCardNumber, int AmountOfMoney);
        List<TransAction> GetListOfSourceAction(string Cardnumber);
        List<TransAction> GetListOfDestanceAction(string Cardnumber);
    }
}
