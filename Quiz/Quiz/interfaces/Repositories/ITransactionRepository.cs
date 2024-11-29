using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.interfaces.Repositories
{
    public interface ITransactionRepository
    {
        bool AddTransaction(string SourceCardNumber, string DestinationCardNumber, int AmountOfMoney);
    }
}
