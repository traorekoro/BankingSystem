namespace bankingSystemApp;

public class AccountManagement
{
   //Async Task is used to handle communication time with external systems like DBs, files System ... 
    public async Task AddFunds(AccountHolder accountHolder, float amount)
    {
        Console.WriteLine($"Adding funds to the account ...");
        // Database communication simulation
        await Task.Delay(2000);
        //With the await keyword the app will not be blocked in case there is any problem during the 
        // communication with the DB
        accountHolder.Balance+=amount;
    }
    public async Task MakeTransfer(AccountHolder sender, AccountHolder receiver,float amount)
    {
        Console.WriteLine($"Initializing a transaction of {amount} from {sender.Name} to {receiver.Name}");
        //Getting account holders balances
        var senderFunds = sender.GetBalance();
        var receiverFunds = receiver.GetBalance();
        //Execute the 2 tasks above concurrently
        await Task.WhenAll(senderFunds,receiverFunds);
        //Checking sender Balance
        if(senderFunds.Result < amount)
        {
            throw new Exception("The transaction is impossible, the sender does not have the required amount");
        }
        //Apply the transaction
        sender.Balance= senderFunds.Result - amount;
        receiver.Balance = receiverFunds.Result + amount;
    }
    public void TakeLoan(AccountHolder accountHolder, float amount)
    {
        var isLoanApproved = IsCreditEnoughToTakeLoan(accountHolder, amount);
        Console.WriteLine($"{accountHolder.Name} had his load request approved ? {(isLoanApproved ? "Yes":"No" )}");
        if(isLoanApproved)
        {
            accountHolder.Balance+= amount;
        }
    }
    public static bool IsCreditEnoughToTakeLoan(AccountHolder accountHolder, float amount)
    {
        if(amount <=0)
        {
            throw new Exception("The amount informed is not valid");
        }
        //Getting Credit Score from two different companies
        var creditCompanyOne = new CreditCompany();
        var creditCompanyTwo = new CreditCompany();
        var creditCompanyOneScore = creditCompanyOne.GetCreditScore(accountHolder);
        var creditCompanyTwoScore = creditCompanyTwo.GetCreditScore(accountHolder);
        // Run the tasks above concurrently and block the app to wait them to finish
        Task.WaitAll(creditCompanyOneScore, creditCompanyTwoScore);
        // Calculate the average of credits
        var averageCredit = (creditCompanyOneScore.Result+creditCompanyTwoScore.Result)/2;
        Console.WriteLine($"The average credit score for {accountHolder.Name} is {averageCredit}");

        switch(amount)
        {
            case < 1000:
                return averageCredit > 300;
            case <= 1000 and < 5000:
                return averageCredit > 450;
            case <= 5000 and < 10000:
                return averageCredit > 600;
            case > 10000:
                return averageCredit >= 800;
            default:
                return false;
        }

    }
}