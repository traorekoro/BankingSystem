using System.Security.Cryptography;

namespace bankingSystemApp;

public class CreditCompany
{
    public async Task<int> GetCreditScore(AccountHolder accountHolder)
    {
        Console.WriteLine($"Getting credit score for {accountHolder.Name}");
        await Task.Delay(5000);
        var randomNumberGenerator = new Random();
        return randomNumberGenerator.Next(300,850);
    }
}