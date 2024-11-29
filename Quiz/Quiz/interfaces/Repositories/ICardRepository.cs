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
        bool ActionsPermition(string Cardnumber);
        bool Login(string Cardnumber, string password);
        bool CardIsActive(string Cardnumber);
        float GetCardBalance(string Cardnumber);
        bool ReduceAmount(int money, string cartnumber);
        void CountInstertPasswordWrong(string Cardnumber);
        void IncreasAmount(int money, string cartnumber);
        Card? GetCardByNumber(string Cardnumber);
        void ChangeStatus(string Cardnumber);
        void SetLastTransactionDate(string Cardnumber);
        void ResetLastTransactionDate(string Cardnumber);
        bool IncreasDailyTransaction(int money, string cartnumber);
        int CheckTimesOfInsertingPasswordIncorrectly(string Cardnumber);
    }
}
