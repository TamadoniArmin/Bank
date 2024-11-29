using Quiz.Service;

int NegetivePoint = 0;
while (true)
{
    CardService cardService = new CardService();
    Console.WriteLine("***** Wellcome *****");
    Console.Write("Please enter your card number: ");
    var Cardnumber = Console.ReadLine()!;
    var Card = cardService.GetCardByNumber(Cardnumber);
    if (!Card)
    {
        Console.WriteLine("There is no card with this information in database!");
    }
    else
    {
        cardService.ResetLastTransactionDate(Cardnumber);
        var Limit = cardService.ActionsPermition(Cardnumber);
        if (!Limit)
        {
            Console.WriteLine("You have reached your limet on transactios! you must wait at least one day.");
        }
        else
        {
            Console.Write("Please enter your password: ");
            var CardPassword = Console.ReadLine()!;
            var ResultOfLogin = cardService.Login(Cardnumber, CardPassword);
            if (ResultOfLogin)
            {
                Transaction(Cardnumber);
                NegetivePoint = 0;
            }
            else
            {
                cardService.CountInstertPasswordWrong(Cardnumber);
                Console.WriteLine("You have entered your password Wrongly!");
                cardService.CheckTimesOfInsertingPasswordIncorrectly(Cardnumber);
            }

        }
    }
}

void Transaction(string cardnumber)
{
    CardService cardService = new CardService();
    TransactionService transactionService = new TransactionService();
    bool logout = false;
    do
    {
        Console.WriteLine("***** Transaction *****");
        Console.WriteLine("Please select your action:");
        Console.WriteLine("1.Transaction");
        Console.WriteLine("2.See list of your Transactions");
        Console.WriteLine("3.Logout");
        int action = int.Parse(Console.ReadLine()!);
        switch (action)
        {
            case 1:
                Console.Write("Please entre your DestinationCard:");
                var DestinationCard = Console.ReadLine()!;
                var Card = cardService.GetCardByNumber(DestinationCard);
                if (!Card)
                {
                    Console.WriteLine("There is no card with this information in database!");
                }
                else
                {
                    Console.WriteLine("Please enter your amoun of nomey");
                    var amount = int.Parse(Console.ReadLine()!);
                    var CheckLimit = cardService.IncreasDailyTransaction(amount, cardnumber);
                    var Redusingmoney = cardService.ReduceAmount(amount, cardnumber, DestinationCard);
                    if (!Redusingmoney)
                    {
                        Console.WriteLine("Try again.");
                    }
                    else
                    {
                        cardService.IncreasAmount(amount, DestinationCard);
                        var result = transactionService.AddTransaction(cardnumber, DestinationCard, amount);
                        if (!result)
                        {
                            Console.WriteLine("The action is invalid! Try again.");
                        }
                        cardService.SetLastTransactionDate(cardnumber);
                    }
                };
                break;
            case 2:
                var sourceTrans = transactionService.GetListOfSourceAction(cardnumber);
                Console.WriteLine("Your Source Transaction activity: ");
                foreach (var trans in sourceTrans)
                {
                    Console.WriteLine($"id={trans.TransactionId} | amount={trans.Amount} USD | To: {trans.DestinationCardNumber} | Date:{trans.TransactionDate} ");
                    Console.WriteLine("***********************************************************************");
                }
                var destanceTeans = transactionService.GetListOfDestanceAction(cardnumber);
                Console.WriteLine("Your Distance transaction activity: ");
                foreach (var trans in destanceTeans)
                {
                    Console.WriteLine($"id={trans.TransactionId} | amount={trans.Amount} USD | To: {trans.DestinationCardNumber} | Date:{trans.TransactionDate} ");
                    Console.WriteLine("***********************************************************************");
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

