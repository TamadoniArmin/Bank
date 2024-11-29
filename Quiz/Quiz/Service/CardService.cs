using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.interfaces.Services;
using Quiz.Repository;

namespace Quiz.Service
{
    public class CardService :ICardService
    {
        CardRepository cardRepository=new CardRepository();

        public void ChangeStatus(string Cardnumber)
        {
            cardRepository.ChangeStatus(Cardnumber);
        }

        public float GetCardBalance(string Cardnumber)
        {
            return cardRepository.GetCardBalance(Cardnumber);
        }

        public bool GetCardByNumber(string Cardnumber)
        {
            var Card= cardRepository.GetCardByNumber(Cardnumber);
            if (Card is null)
            {
                return false;
            }
            return true;
        }

        public void IncreasAmount(int money, string cartnumber)
        {
            cardRepository.IncreasAmount(money, cartnumber);
        }

        public bool Login(string Cardnumber, string password)
        {
            return cardRepository.Login(Cardnumber, password);
        }

        public bool ReduceAmount(int money, string cartnumber)
        {
            if (money<=0)
            {
                return false;
            }
             var action=cardRepository.ReduceAmount(money, cartnumber);
            if (!action)
            {
                Console.WriteLine("You don't have enough nomey!");
                return false;
            }
            return true;

        }
    }
}
