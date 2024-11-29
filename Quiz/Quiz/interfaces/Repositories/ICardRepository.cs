using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Entity;

namespace Quiz.interfaces.Repositories
{
    public interface ICardRepository
    {
        bool Login(string Cardnumber, string password);
        bool CardIsActive(string Cardnumber);
        float GetCardBalance(string Cardnumber);
        bool ReduceAmount(int money, string cartnumber);
        void IncreasAmount(int money, string cartnumber);
        Card? GetCardByNumber(string Cardnumber);
        void ChangeStatus(string Cardnumber);
    }
}
