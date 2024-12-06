using Quiz.Repository;
using Quiz.Service;
using System.Threading;
using System.Timers;

//int NegetivePoint = 0;
//string username = "armin";
FileRepository fileRepository = new FileRepository();
fileRepository.Password();
while (true)
{
    //CardService cardService = new CardService();
    //Console.WriteLine("***** Wellcome *****");
    //Console.Write("Please enter your card number: ");
    //var Cardnumber = Console.ReadLine()!;
    //var Card = cardService.GetCardByNumber(Cardnumber);
    //if (!Card)
    //{
    //    Console.WriteLine("There is no card with this information in database!");
    //}
    //else
    //{
    //    cardService.ResetLastTransactionDate(Cardnumber);
    //    var Limit = cardService.ActionsPermition(Cardnumber);
    //    if (!Limit)
    //    {
    //        Console.WriteLine("You have reached your limet on transactios! you must wait at least one day.");
    //    }
    //    else
    //    {
    //        Console.Write("Please enter your password: ");
    //        var CardPassword = Console.ReadLine()!;
    //        var ResultOfLogin = cardService.Login(Cardnumber, CardPassword);
    //        if (ResultOfLogin)
    //        {
    //            Transaction(Cardnumber);
    //            NegetivePoint = 0;
    //        }
    //        else
    //        {
    //            cardService.CountInstertPasswordWrong(Cardnumber);
    //            Console.WriteLine("You have entered your password Wrongly!");
    //            cardService.CheckTimesOfInsertingPasswordIncorrectly(Cardnumber);
    //        }

    //    }
    //}
    Console.Clear();
    Console.WriteLine("***** Wellcome *****");
    Console.Write("Please enter your username: ");
    var Username = Console.ReadLine()!;
    Console.Write("Please enter yourpassword: ");
    var Password = Console.ReadLine()!;
    UserService userService = new UserService();
    bool ResultOfLogin = userService.Login(Username, Password);
    if (ResultOfLogin)
    {
        CardServices(Username);
    }
}

void CardServices(string Username)
{
    bool Logout = false;
    CardService cardService = new CardService();
    do
    {
        Console.WriteLine("***** Card Services *****");
        Console.WriteLine("Please Chooese your ation: ");
        Console.WriteLine("1. See your Cards");
        Console.WriteLine("2. Get your cards' balance");
        Console.WriteLine("3. Transaction");
        Console.WriteLine("4. Change Your card's password");
        Console.WriteLine("5.Logout");
        int Action = int.Parse(Console.ReadLine()!);
        switch (Action)
        {
            case 1:
                var Cards = cardService.GetListOfUserCards(Username);
                foreach (var card in Cards)
                {
                    if (card.IsActice == false)
                    {
                        Console.WriteLine($"Card Number : {card.CardNumber} | Is Not Active");
                    }
                    else
                    {
                        Console.WriteLine($"Card Number : {card.CardNumber} | Is Active");
                    }
                }
                break;
            case 2:
                var cards = cardService.GetListOfUserCards(Username);
                Console.WriteLine("Please enter the number of your wanted card: ");
                int Count = 1;
                foreach (var card in cards)
                {
                    Console.WriteLine($"{Count}. Card Number : {card.CardNumber}");
                    Count++;
                }
                var WantedCard = Console.ReadLine()!;
                var Balance = cardService.GetCardBalance(WantedCard);
                Console.WriteLine($"Your balamce in ({WantedCard}) is : {Balance} $");
                Console.WriteLine("************************************************");
                break;
            case 3:
                var usercards = cardService.GetListOfUserCards(Username);
                foreach (var card in usercards)
                {
                    if (card.IsActice == false)
                    {
                        Console.WriteLine($"Card Number : {card.CardNumber} | Is Not Active");
                    }
                    else
                    {
                        Console.WriteLine($"Card Number : {card.CardNumber} | Is Active");
                    }
                }
                Console.Write("Please enter the card number that you want to transaction with: ");
                string Cardnumber = Console.ReadLine()!;
                var ResultOfAtion = cardService.GetCardByNumber(Cardnumber);
                if (ResultOfAtion)
                {
                    Transaction(Cardnumber);
                }
                break;
            case 4:
                var cards1 = cardService.GetListOfUserCards(Username);
                foreach (var card in cards1)
                {
                    Console.WriteLine($"* Card Number : {card.CardNumber}");
                }
                Console.Write("Please enter the card's number that you want to chage it's password: ");
                var WantToChangePassword = Console.ReadLine()!;
                bool Res2 = cardService.GetCardByNumber(WantToChangePassword);
                if (Res2)
                {
                    Console.Write("Please enter the prior password: ");
                    string PriorPass = Console.ReadLine()!;
                    bool ResultOfCheckPassword = cardService.CheckPassword(WantToChangePassword, PriorPass);
                    if (ResultOfCheckPassword)
                    {
                        Console.Write("Please enter your New password: ");
                        string NewPassword = Console.ReadLine()!;
                        cardService.ChangePassword(WantToChangePassword, NewPassword);
                    };
                };
                break;
            case 5:
                Logout = true;
                break;
            default:
                break;
        }
    } while (!Logout);

}

void Transaction(string cardnumber)
{
    System.Timers.Timer timer = new();
    CardService cardService = new CardService();
    TransactionService transactionService = new TransactionService();
    bool logout = false;
    do
    {
        Console.WriteLine("***** Transaction *****");
        Console.WriteLine("Please select your action:");
        Console.WriteLine("1.Transaction");
        Console.WriteLine("2.See list of your Transactions");
        Console.WriteLine("3.Card Service Page");
        int action = int.Parse(Console.ReadLine()!);
        switch (action)
        {
            case 1:
                Console.Write("Please entre your DestinationCard:");
                var DestinationCard = Console.ReadLine()!;
                var Card = cardService.GetCardByNumber(DestinationCard);
                if (!Card)
                {
                    transactionService.AddTransaction(cardnumber, DestinationCard, 1);
                    Console.WriteLine("There is no card with this information in database!");
                }
                else
                {
                    Console.Write("Please enter your amoun of nomey: ");
                    var amount = int.Parse(Console.ReadLine()!);
                    var DistanceCard = cardService.GetDistancCard(DestinationCard);
                    Console.WriteLine($"from: {cardnumber}");
                    Console.WriteLine($"To: {DistanceCard.CardNumber} | For: {DistanceCard.Holder.Name}");
                    Console.WriteLine($"Amount: {amount} $");
                    Console.WriteLine("1. Continue");
                    Console.WriteLine("2. Try again");
                    int Action = int.Parse(Console.ReadLine()!);
                    if (Action == 1)
                    {
                        timer = new(1 * 20 * 1000);
                        timer.Elapsed += TimeUp;
                        timer.AutoReset = false;
                        timer.Start();
                        Console.Write("Please enter the validation code (You have 5 minutes) :");
                        var ValidationCode = Console.ReadLine()!;
                        timer.Stop();
                        var FilePassword = fileRepository.GetPasswordFromFile();
                        if (ValidationCode == FilePassword)
                        {
                            //var CheckLimit = cardService.IncreasDailyTransaction(amount, cardnumber);
                            //var Redusingmoney = cardService.ReduceAmount(amount, cardnumber, DestinationCard);
                            //if (!Redusingmoney)
                            //{
                            //    Console.WriteLine("Try again.");
                            //}

                            var result = transactionService.AddTransaction(cardnumber, DestinationCard, amount);
                            if (!result)
                            {
                                Console.WriteLine("Transaction Failed!");
                            }
                            cardService.SetLastTransactionDate(cardnumber);

                        }
                        else
                        {
                            Console.WriteLine("The validation code is incorrect! Try again.");
                        }
                    }
                };
                break;
            case 2:
                var sourceTrans = transactionService.GetListOfSourceAction(cardnumber);
                Console.WriteLine("Your Source Transaction activity: ");
                foreach (var trans in sourceTrans)
                {
                    if (trans.isSuccessful == true)
                    {
                        Console.WriteLine($"id={trans.TransactionId} | amount={trans.Amount} USD | To: {trans.DestinationCardNumber} | Date:{trans.TransactionDate} | Status: Succesful");
                        Console.WriteLine("***********************************************************************");
                    }
                    else if (trans.isSuccessful == false)
                    {
                        Console.WriteLine($"id={trans.TransactionId} | amount={trans.Amount} USD | To: {trans.DestinationCardNumber} | Date:{trans.TransactionDate} | Status: Not Succesful");
                        Console.WriteLine($"Details : {trans.TransactionDetails}");
                        Console.WriteLine("***********************************************************************");
                    }
                }
                var destanceTeans = transactionService.GetListOfDestanceAction(cardnumber);
                Console.WriteLine("Your Distance transaction activity: ");
                foreach (var trans in destanceTeans)
                {
                    if (trans.isSuccessful)
                    {
                        Console.WriteLine($"id={trans.TransactionId} | amount={trans.Amount} USD | From: {trans.SourceCardNumber} | Date:{trans.TransactionDate} | Status: Succesful ");
                        Console.WriteLine("***********************************************************************");
                    }
                    else if (trans.isSuccessful == false)
                    {
                        Console.WriteLine($"id={trans.TransactionId} | amount={trans.Amount} USD | From: {trans.SourceCardNumber} | Date:{trans.TransactionDate} | Status: Not Succesful");
                        Console.WriteLine($"Details : {trans.TransactionDetails}");
                        Console.WriteLine("***********************************************************************");
                    }
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                break;
            case 3:
                logout = true;
                break;
            default:
                break;
        }
    }
    while (!logout);
}
void TimeUp(object sender, ElapsedEventArgs e)
{
    Console.WriteLine("The time of entering validation code is expaired!");
    //Environment.Exit(0);
}

