using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quiz.DB;
using Quiz.Entity;
using Quiz.interfaces.Repositories;

namespace Quiz.Repository
{
    public class CardRepository : ICardRepository
    {
        AppDbContext _context= new AppDbContext();

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

        public bool Login(string Cardnumber, string password)
        {
            bool res1 = CardIsActive(Cardnumber);
            bool res2= _context.Cards.Any( c=> c.CardNumber==Cardnumber && c.Password==password);
            if (res1 && res2)
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
    }
}
