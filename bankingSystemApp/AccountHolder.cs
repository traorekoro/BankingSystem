namespace bankingSystemApp;
public class AccountHolder
{
    public string Name {get;set;}
    public float Balance {get;set;}

    public AccountHolder(string name, float balance)
    {
        Name = name;
        Balance = balance;
    }

    public void ShowBalance()
    {
        Console.WriteLine($"{Name} has {Balance} Dollars");
    }

    public async Task<float> GetBalance()
    {
        //DB Communication Simulation
        await Task.Delay(5000);
        return Balance;
    }
}