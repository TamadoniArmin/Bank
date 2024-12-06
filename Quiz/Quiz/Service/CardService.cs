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

        public double GetCardBalance(string Cardnumber)
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

        public List<Card> GetListOfUserCards(string username)
        {
            return cardRepository.GetListOfUserCards(username);
        }

        public void IncreasAmount(double money, string cartnumber)
        {
            var CheckExist=cardRepository.GetCardByNumber(cartnumber);
            if (CheckExist==null)
            {
                Console.WriteLine("We can not Find Your Destonation Card!");
            }
            cardRepository.IncreasAmount(money, cartnumber);
        }

        public bool IncreasDailyTransaction(double money, string cartnumber)
        {
            return cardRepository.IncreasDailyTransaction(money, cartnumber);
        }

        public bool CheckPassword(string Cardnumber, string password)
        {
            return cardRepository.ChangePassword(Cardnumber, password);
        }

        public bool ReduceAmount(double money, string cartnumber, string DistansCardnumber)
        {
            var DistanceCard= cardRepository.GetCardByNumber(DistansCardnumber);
            if (DistanceCard is null)
            {
                Console.WriteLine("We can not Find Your Destonation Card!");
                return false;
            }
            else
            {
                if (money <= 0)
                {
                    Console.WriteLine("The enterd amount must be more than ZERO");
                    return false;
                }
                else
                {
                    double MoneyWithTax;
                    if(money > 1000)
                    {
                        MoneyWithTax = money * 0.015;

                        var action = cardRepository.ReduceAmount(MoneyWithTax+money, cartnumber);
                        if (!action)
                        {
                            Console.WriteLine("You don't have enough nomey!");
                            return false;
                        }
                    }
                    else if (10<=money && money < 1000)
                    {
                        MoneyWithTax = money * 0.005;
                        var action = cardRepository.ReduceAmount(MoneyWithTax+money, cartnumber);
                        if (!action)
                        {
                            Console.WriteLine("You don't have enough nomey!");
                            return false;
                        }
                    }
                    return true;
                }
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

        public void ChangePassword(string Cardnumber, string newpassword)
        {
            var ResultOfChangePass= cardRepository.ChangePassword(Cardnumber, newpassword);
            if (ResultOfChangePass)
            {
                Console.WriteLine("Done.");
            }
            else
            {
                Console.WriteLine("Ooops....Something Goes wrong! Please try again.");
            }
        }

        public Card GetDistancCard(string Cardnumber)
        {
            return cardRepository.GetCardByNumber(Cardnumber);
        }
    }
}
