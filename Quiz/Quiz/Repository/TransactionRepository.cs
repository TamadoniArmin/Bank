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
            try
            {
                var ReducingMoney = cardService.ReduceAmount(AmountOfMoney, SourceCardNumber, DestinationCardNumber);
                if (!ReducingMoney)
                {
                    return false;
                }
                var FindCard = cardRepository.GetCardByNumber(DestinationCardNumber);
                if (FindCard is null)
                {
                    return false;
                }
                else
                {
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
            return _context.Transactions.Where(t=>t.SourceCard.CardNumber == Cardnumber).ToList();
        }

        public List<TransAction> GetListOfSourceAction(string Cardnumber)
        {
            return _context.Transactions.Where(t=>t.DestinationCard.CardNumber == Cardnumber).ToList();
        }
    }
}
