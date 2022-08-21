using System;
using DotNetLab2.Models;

namespace DotNetLab2.ConsoleServices
{
    public static class ConsoleReader
    {
        public static Client ReadClient()
        {
            return TryReadEntity(() =>
            {
                var client = new Client();
                
                Console.Write("\tId: ");
                client.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tFull name: ");
                client.FullName = Console.ReadLine();

                Console.Write("\tPhone number: ");
                client.PhoneNumber = Console.ReadLine();

                Console.Write("\tRegistration code: ");
                client.RegistrationCode = Console.ReadLine();

                return client;
            });
        }
        
        public static ClientToCredit ReadClientToCredit()
        {
            return TryReadEntity(() =>
            {
                var clientToCredit = new ClientToCredit();
                
                Console.Write("\tClientId: ");
                clientToCredit.ClientId = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tCreditId: ");
                clientToCredit.CreditId = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("\tDate of issue: ");
                clientToCredit.DateOfIssue = Convert.ToDateTime(Console.ReadLine());
                
                Console.Write("\tDate of repayment: ");
                clientToCredit.DateOfRepayment = Convert.ToDateTime(Console.ReadLine());
                
                Console.Write("\tAmount of money taken: ");
                clientToCredit.AmountOfMoneyTaken = Convert.ToDecimal(Console.ReadLine());

                return clientToCredit;
            });
        }
        public static ClientToDeposit ReadClientToDeposit()
        {
            return TryReadEntity(() =>
            {
                var clientToDeposit = new ClientToDeposit();
                
                Console.Write("\tClientId: ");
                clientToDeposit.ClientId = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tDepositId: ");
                clientToDeposit.DepositId = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tAccount Number: ");
                clientToDeposit.AccountNumber = Console.ReadLine();

                Console.Write("\tDate Of Beginning: ");
                clientToDeposit.DateOfBeginning = Convert.ToDateTime(Console.ReadLine());

                Console.Write("\tDate Of Ending: ");
                clientToDeposit.DateOfEnding = Convert.ToDateTime(Console.ReadLine());
                
                Console.Write("\tAmount of money to deposit: ");
                clientToDeposit.AmountOfMoneyToDeposit = Convert.ToDecimal(Console.ReadLine());
                
                return clientToDeposit;
            });
        }
        public static Credit ReadCredit()
        {
            return TryReadEntity(() =>
            {
                var credit = new Credit();
                
                Console.Write("\tId: ");
                credit.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tCurrencyId: ");
                credit.CurrencyId = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tPayment duration in months: ");
                credit.RepaymentDurationInMonths = Convert.ToSingle(Console.ReadLine());

                Console.Write("\tPercent rate: ");
                credit.PercentRate = Convert.ToSingle(Console.ReadLine());

                return credit;
            });
        }
        public static Currency ReadCurrency()
        {
            return TryReadEntity(() =>
            {
                var currency = new Currency();
                
                Console.Write("\tId: ");
                currency.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tName: ");
                currency.Name = Console.ReadLine();

                return currency;
            });
        }
        public static Deposit ReadDeposit()
        {
            return TryReadEntity(() =>
            {
                var deposit = new Deposit();
                
                Console.Write("\tId: ");
                deposit.Id = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tCurrencyId: ");
                deposit.CurrencyId = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tDuration in months: ");
                deposit.DurationInMonths = Convert.ToInt32(Console.ReadLine());

                Console.Write("\tMinimal investment: ");
                deposit.MinimalInvestment = Convert.ToDecimal(Console.ReadLine());

                Console.Write("\tMinimal investment: ");
                deposit.MinimalInvestment = Convert.ToDecimal(Console.ReadLine());
                
                Console.Write("\tPercent rate: ");
                deposit.PercentRate = Convert.ToSingle(Console.ReadLine());
                
                return deposit;
            });
        }

        private static T? TryReadEntity<T>(Func<T> inputFunc) where T : class
        {
            Console.WriteLine($"Enter info about {typeof(T).Name}:");
            T result;
            try
            {
                result = inputFunc();
                return result;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid format error. Try Again");
                return TryReadEntity<T>(inputFunc);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Value out of available range. Try Again");
                return TryReadEntity<T>(inputFunc);
            }
        } 
    }
}