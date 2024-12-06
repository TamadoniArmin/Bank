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
        AppDbContext _context = new AppDbContext();
        CardRepository cardRepository = new CardRepository();
        CardService cardService = new CardService();
        public bool AddTransaction(string SourceCardNumber, string DestinationCardNumber, double AmountOfMoney)
        {
            try
            {
                var FindCard = cardRepository.GetCardByNumber(DestinationCardNumber);
                if (FindCard is null)
                {
                    Console.WriteLine("Unsuccessful transaction due to not finding the destination card");
                    TransAction transaction = new TransAction()
                    {
                        SourceCardNumber = SourceCardNumber,
                        DestinationCardNumber = DestinationCardNumber,
                        Amount = 0,
                        TransactionDate = DateTime.Now,
                        TransactionDetails = "Unsuccessful transaction due to not finding the destination card",
                        isSuccessful = false
                    };
                    _context.Transactions.Add(transaction);
                    _context.SaveChanges();
                    return false;
                }
                var CheckLimit = cardService.IncreasDailyTransaction(AmountOfMoney, SourceCardNumber);
                if (!CheckLimit)
                {
                    Console.WriteLine("Transaction failed due to daily transaction limit");
                    TransAction transaction = new TransAction()
                    {
                        SourceCardNumber = SourceCardNumber,
                        DestinationCardNumber = DestinationCardNumber,
                        Amount = AmountOfMoney,
                        TransactionDate = DateTime.Now,
                        TransactionDetails = "Transaction failed due to daily transaction limit",
                        isSuccessful = false
                    };
                    _context.Transactions.Add(transaction);
                    _context.SaveChanges();
                    return false;
                }
                var ReducingMoney = cardService.ReduceAmount(AmountOfMoney, SourceCardNumber, DestinationCardNumber);
                if (!ReducingMoney)
                {
                    Console.WriteLine("Transaction failed due to insufficient balance");
                    TransAction transaction = new TransAction()
                    {
                        SourceCardNumber = SourceCardNumber,
                        DestinationCardNumber = DestinationCardNumber,
                        Amount = AmountOfMoney,
                        TransactionDate = DateTime.Now,
                        TransactionDetails = "Transaction failed due to insufficient balance",
                        isSuccessful = false
                    };
                    _context.Transactions.Add(transaction);
                    _context.SaveChanges();
                    return false;
                }
                else if (FindCard is not null && ReducingMoney is true && CheckLimit is true)
                {
                    cardRepository.IncreasAmount(AmountOfMoney, DestinationCardNumber);
                    TransAction transaction = new TransAction()
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
                return false;
            }
            catch (Exception ex)
            {
                cardService.IncreasAmount(AmountOfMoney, SourceCardNumber);
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<TransAction> GetListOfDestanceAction(string Cardnumber)
        {
            return _context.Transactions.Where(t => t.DestinationCard.CardNumber == Cardnumber).ToList();
        }

        public List<TransAction> GetListOfSourceAction(string Cardnumber)
        {
            return _context.Transactions.Where(t => t.SourceCard.CardNumber == Cardnumber).ToList();
        }
    }
}
