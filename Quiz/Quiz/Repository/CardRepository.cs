using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Quiz.DB;
using Quiz.Entity;
using Quiz.InferaStructure.Configurations;
using Quiz.interfaces.Repositories;

namespace Quiz.Repository
{
    public class CardRepository : ICardRepository
    {
        AppDbContext _context= new AppDbContext();

        public bool ActionsPermition(string Cardnumber)
        {
            return _context.Cards.Any(c=>c.CardNumber == Cardnumber && c.Daylitransaction<250);
        }

        public bool CardIsActive(string Cardnumber)
        {
            var card= _context.Cards.AsNoTracking().FirstOrDefault(c => c.CardNumber == Cardnumber && c.IsActice == true);
            if (card is not null)
            {
                return true;
            }
            return false;
        }

        public void ChangeStatus(string Cardnumber)
        {
            var card= GetCardByNumber(Cardnumber);
            card.IsActice = false;
            _context.SaveChanges();
        }

        public int CheckTimesOfInsertingPasswordIncorrectly(string Cardnumber)
        {
            var Card=GetCardByNumber(Cardnumber);
            return Card.InsertingPasswordWrongly;
        }

        public void CountInstertPasswordWrong(string Cardnumber)
        {
            var Card= GetCardByNumber(Cardnumber);
            Card.InsertingPasswordWrongly++;
            _context.SaveChanges();
        }

        public float GetCardBalance(string Cardnumber)
        {
            var card = _context.Cards.AsNoTracking().FirstOrDefault(c => c.CardNumber == Cardnumber);
            return card.Balance;
        }

        public Card? GetCardByNumber(string Cardnumber)
        {
            return _context.Cards.FirstOrDefault(c => c.CardNumber == Cardnumber);
        }
        public void IncreasAmount(int money, string cartnumber)
        {
            var card = GetCardByNumber(cartnumber);
            if (card is not null)
            {
                card.Balance += money;
                _context.SaveChanges();
            }

        }

        public bool IncreasDailyTransaction(int money, string cartnumber)
        {
            var card= GetCardByNumber(cartnumber);
            if (card.Daylitransaction+money>=250)
            {
                card.Daylitransaction += money;
                _context.SaveChanges();
                return true;
            }
            return false; 
        }

        public bool Login(string Cardnumber, string password)
        {
            bool res1 = CardIsActive(Cardnumber);
            bool res2= _context.Cards.Any( c=> c.CardNumber==Cardnumber && c.Password==password);
            if (res1==true && res2==true)
            {
                return true;
            }
            return false;
        }

        public bool ReduceAmount(int money, string cartnumber)
        {
            var card=GetCardByNumber(cartnumber);
            var balance=GetCardBalance(cartnumber);
            if (balance<money)
            {
                return false;
            }
            else
            {
                card.Balance-= money;
                _context.SaveChanges();
                return true;
            }
        }

        public void ResetLastTransactionDate(string Cardnumber)
        {
            var card = GetCardByNumber(Cardnumber);
            if ((DateTime.Now-card.SetedLimitationDate).TotalHours>24)
            {
                card.Daylitransaction = 0;
                card.SetedLimitationDate = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void SetLastTransactionDate(string Cardnumber)
        {
            var card = GetCardByNumber(Cardnumber);
            if (card.Daylitransaction>=250)
            {
                card.SetedLimitationDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
