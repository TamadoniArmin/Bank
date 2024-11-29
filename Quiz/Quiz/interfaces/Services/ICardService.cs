using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Quiz.interfaces.Services
{
    public interface ICardService
    {
        bool Login(string Cardnumber, string password);
        float GetCardBalance(string Cardnumber);
        bool ReduceAmount(int money, string cartnumber, string DistansCardnumber);
        void IncreasAmount(int money, string cartnumber);
        void ChangeStatus(string Cardnumber);
        bool GetCardByNumber(string Cardnumber);
        bool ActionsPermition(string Cardnumber);
        void SetLastTransactionDate(string Cardnumber);
        void ResetLastTransactionDate(string Cardnumber);
        bool IncreasDailyTransaction(int money, string cartnumber);
        void CheckTimesOfInsertingPasswordIncorrectly(string Cardnumber);
        void CountInstertPasswordWrong(string Cardnumber);
    }
}
