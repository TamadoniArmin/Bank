using Quiz.Service;

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
        int NegetivePoint = 0;
        do
        {
            Console.Write("Please enter your password: ");
            var CardPassword = Console.ReadLine()!;

            var ResultOfLogin = cardService.Login(Cardnumber, CardPassword);
            if (!ResultOfLogin)
            {
                NegetivePoint++;
                Console.WriteLine("Password is incorrect!");
            }
            if (NegetivePoint==3)
            {
                cardService.ChangeStatus(Cardnumber);
                Console.WriteLine("Your card has been blocked.");
            }
            if (ResultOfLogin)
            {
                Transaction(Cardnumber);
            }
        } while (NegetivePoint > 3);
    }
}

void Transaction(string cardnumber)
{
    CardService cardService = new CardService();
    TransactionService transactionService = new TransactionService();
    bool logout=false;
    do
    {
        Console.WriteLine("***** Transaction *****");
        Console.WriteLine("Please select your action:");
        Console.WriteLine("1.Transaction");
        Console.WriteLine("2.Logout");
        int action=int.Parse(Console.ReadLine());
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
                    var amount=int.Parse(Console.ReadLine()!);
                    var Redusingmoney = cardService.ReduceAmount(amount, cardnumber);
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
                    }
                };
                break;
            case 2:
                logout = true;
                break;
            default:
                break;
        }
    }
    while (!logout);
}

