using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.DB;
using Quiz.Entity;
using Quiz.interfaces.Repositories;
using Quiz.Service;

namespace Quiz.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        AppDbContext _context=new AppDbContext();
        CardRepository cardRepository = new CardRepository();
        CardService cardService = new CardService();
        public bool AddTransaction(string SourceCardNumber, string DestinationCardNumber, int AmountOfMoney)
        {
            var ReducingMoney = cardService.ReduceAmount(AmountOfMoney, SourceCardNumber);
            if (!ReducingMoney)
            {
                return false;
            }
            var FindCard=cardRepository.GetCardByNumber(DestinationCardNumber);
            if (FindCard is null)
            {
                return false;
            }
            else
            {
                Transaction transaction = new Transaction()
                {
                    SourceCardNumber = SourceCardNumber,
                    DestinationCardNumber = DestinationCardNumber,
                    Amount = AmountOfMoney,
                    TransactionDate = DateTime.Now,
                    isSuccessful = true
                };
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return true;
            }

        }
    }
}
