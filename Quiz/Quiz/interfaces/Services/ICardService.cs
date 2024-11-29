using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.interfaces.Services
{
    public interface ICardService
    {
        bool Login(string Cardnumber, string password);
        float GetCardBalance(string Cardnumber);
        bool ReduceAmount(int money, string cartnumber);
        void IncreasAmount(int money, string cartnumber);
        void ChangeStatus(string Cardnumber);
        bool GetCardByNumber(string Cardnumber);
    }
}
