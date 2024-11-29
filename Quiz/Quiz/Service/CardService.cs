using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Entity;
using Quiz.interfaces.Services;
using Quiz.Repository;

namespace Quiz.Service
{
    public class CardService : ICardService
    {
        CardRepository cardRepository = new CardRepository();

        public bool ActionsPermition(string Cardnumber)
        {
            return cardRepository.ActionsPermition(Cardnumber);
        }

        public void ChangeStatus(string Cardnumber)
        {
            cardRepository.ChangeStatus(Cardnumber);
        }

        public void CheckTimesOfInsertingPasswordIncorrectly(string Cardnumber)
        {
            var Cout=cardRepository.CheckTimesOfInsertingPasswordIncorrectly(Cardnumber);
            if (Cout>=3)
            {
                cardRepository.ChangeStatus(Cardnumber);
                Console.WriteLine("Account locked due to too many incorrect attempts.");
            }
        }

        public void CountInstertPasswordWrong(string Cardnumber)
        {
            cardRepository.CountInstertPasswordWrong(Cardnumber);
        }

        public float GetCardBalance(string Cardnumber)
        {
            return cardRepository.GetCardBalance(Cardnumber);
        }

        public bool GetCardByNumber(string Cardnumber)
        {
            var Card = cardRepository.GetCardByNumber(Cardnumber);
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

        public bool IncreasDailyTransaction(int money, string cartnumber)
        {
            return cardRepository.IncreasDailyTransaction(money, cartnumber);
        }

        public bool Login(string Cardnumber, string password)
        {
            return cardRepository.Login(Cardnumber, password);
        }

        public bool ReduceAmount(int money, string cartnumber, string DistansCardnumber)
        {
            var DistanceCard= cardRepository.GetCardByNumber(DistansCardnumber);
            if (DistanceCard is null)
            {
                return false;
            }
            else
            {
                if (money <= 0)
                {
                    return false;
                }
                var action = cardRepository.ReduceAmount(money, cartnumber);
                if (!action)
                {
                    Console.WriteLine("You don't have enough nomey!");
                    return false;
                }
                return true;
            }
        }

        public void ResetLastTransactionDate(string Cardnumber)
        {
            cardRepository.ResetLastTransactionDate(Cardnumber);
        }

        public void SetLastTransactionDate(string Cardnumber)
        {
            cardRepository.SetLastTransactionDate(Cardnumber);
        }
    }
}
