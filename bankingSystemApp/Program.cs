// See https://aka.ms/new-console-template for more information
using bankingSystemApp;

AccountManagement management = new AccountManagement();
AccountHolder maria = new AccountHolder("Maria",0);
AccountHolder anna = new AccountHolder("anna", 300);
await management.AddFunds(maria,100);
maria.ShowBalance();
/*anna.ShowBalance();
await management.MakeTransfer(maria, anna, 50);
anna.ShowBalance();
maria.ShowBalance();*/
management.TakeLoan(maria,1500);
maria.ShowBalance();
