using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DotNetLab2.ConsoleServices;
using DotNetLab2.Models;
using DotNetLab2.XmlServices;

namespace DotNetLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context(Config.XmlFilesDirPath);
            context.EnsureDataSeeded();

            var queries = new Queries(context);
            var printer = new QueryPrinter();

            while (true)
            {
                MenuPrinter.Print();
                Console.Write("Enter an option: ");
                if (Int32.TryParse(Console.ReadLine(), out var option))
                {
                    Console.Clear();
                    switch (option)
                    {
                        case 0:
                            var client = ConsoleReader.ReadClient();
                            SaveEntity(client, context.Clients);
                            Console.WriteLine($"Client {client.FullName} was successfully added!");
                            break;

                        case 1:
                            var clientToCredit = ConsoleReader.ReadClientToCredit();
                            SaveEntity(clientToCredit, context.ClientsToCredits);
                            Console.WriteLine($"ClientToCredit was successfully saved");
                            break;

                        case 2:
                            var clientToDeposit = ConsoleReader.ReadClientToCredit();
                            SaveEntity(clientToDeposit, context.ClientsToDeposits);
                            Console.WriteLine($"ClientToDeposit was successfully saved");
                            break;

                        case 3:
                            var credit = ConsoleReader.ReadCredit();
                            SaveEntity(credit, context.Credits);
                            Console.WriteLine($"Credit was successfully saved");
                            break;

                        case 4:
                            var currency = ConsoleReader.ReadCurrency();
                            SaveEntity(currency, context.Currencies);
                            Console.WriteLine($"Currency {currency.Name} was successfully saved");
                            break;

                        case 5:
                            var deposit = ConsoleReader.ReadDeposit();
                            SaveEntity(deposit, context.Deposits);
                            Console.WriteLine($"Deposit was successfully saved");
                            break;

                        case 6:
                            printer.PrintClientsFullNames(
                                queries.GetClientsFullNames()
                            );
                            printer.PrintClientsWithAccountNumbers(
                                queries.GetClientsWithAccountNumbers().Distinct()
                            );
                            printer.PrintCreditsAfter2022(
                                queries.GetCreditsAfter2022()
                            );
                            printer.PrintDepositsGroupedByCurrency(
                                queries.GetDepositsGroupedByCurrency()
                            );
                            printer.PrintNotRepayedCredits(
                                queries.GetNotRepayedCredits()
                            );
                            printer.PrintDepositsAndTheirUsageQuantity(
                                queries.GetDepositsAndTheirUsageQuantity()
                            );
                            printer.PrintClientWithCreditsWithoutDeposits(
                                queries.GetClientWithCreditsWithoutDeposits()
                            );
                            printer.PrintClientAndNumberOfCredits(
                                queries.GetClientAndNumberOfCredits()
                            );
                            printer.PrintAverageDurationDepositsAndActualAverageDuration(
                                queries.GetAverageDurationDepositsAndActualAverageDuration()
                            );
                            printer.PrintQuantityOfClientsWithCreditNoLessThan50000UAH(
                                queries.GetQuantityOfClientsWithCreditNoLessThan50000UAH()
                            );
                            printer.PrintClientWithMostCreditsWithHisMoneyAndSortedMoney(
                                queries.GetClientWithMostCreditsWithHisMoneyAndSortedMoney()
                            );
                            printer.PrintCreditsWithRepaymentNoLess6Month(
                                queries.GetCreditsWithRepaymentNoLess6Month()
                            );
                            printer.PrintClientsWithoutDepositsAndCredits(
                                queries.GetClientsWithoutDepositsAndCredits()
                            );
                            printer.PrintClientsSuccessfullyRepayedTheirCredits(
                                queries.GetClientsSuccessfullyRepayedTheirCredits()
                            );
                            printer.PrintUnusedDeposits(
                                queries.GetUnusedDeposits()
                            );
                            printer.PrintDepositsAndCredits(
                                queries.GetDepositsAndCredits()
                            );
                            break;

                    }
                }
            }
        }

        private static void SaveEntity<T>(T entity, XDocument xDocument)
        {
            xDocument.Root?.Add(entity.ToXElement());
            var path = Path.Combine(Config.XmlFilesDirPath, Config.EntitiesFileNames[typeof(T).Name]);
            xDocument.Save(path);
        }
    }
}