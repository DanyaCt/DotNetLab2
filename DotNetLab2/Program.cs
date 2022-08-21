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

            MenuPrinter.Print();

            while (true)
            {
                Console.Write("Enter an option: ");
                if (Int32.TryParse(Console.ReadLine(), out var option))
                {
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
                            break;

                        case 7:
                            printer.PrintClientsWithAccountNumbers(
                                queries.GetClientsWithAccountNumbers().Distinct()
                            );
                            break;

                        case 8:
                            printer.PrintCreditsAfter2022(
                                queries.GetCreditsAfter2022()
                            );
                            break;

                        case 9:
                            printer.PrintDepositsGroupedByCurrency(
                                queries.GetDepositsGroupedByCurrency()
                            );
                            break;

                        case 10:
                            printer.PrintNotRepayedCredits(
                                queries.GetNotRepayedCredits()
                            );
                            break;

                        case 11:
                            printer.PrintDepositsAndTheirUsageQuantity(
                                queries.GetDepositsAndTheirUsageQuantity()
                            );
                            break;

                        case 12:
                            printer.PrintClientWithCreditsWithoutDeposits(
                                queries.GetClientWithCreditsWithoutDeposits()
                            );
                            break;

                        case 13:
                            printer.PrintClientAndNumberOfCredits(
                                queries.GetClientAndNumberOfCredits()
                            );
                            break;

                        case 14:
                            printer.PrintAverageDurationDepositsAndActualAverageDuration(
                                queries.GetAverageDurationDepositsAndActualAverageDuration()
                            );
                            break;

                        case 15:
                            printer.PrintQuantityOfClientsWithCreditNoLessThan50000UAH(
                                queries.GetQuantityOfClientsWithCreditNoLessThan50000UAH()
                            );
                            break;

                        case 16:
                            printer.PrintClientWithMostCreditsWithHisMoneyAndSortedMoney(
                                queries.GetClientWithMostCreditsWithHisMoneyAndSortedMoney()
                            );
                            break;

                        case 17:
                            printer.PrintCreditsWithRepaymentNoLess6Month(
                                queries.GetCreditsWithRepaymentNoLess6Month()
                            );
                            break;

                        case 18:
                            printer.PrintClientsWithoutDepositsAndCredits(
                                queries.GetClientsWithoutDepositsAndCredits()
                            );
                            break;

                        case 19:
                            printer.PrintClientsSuccessfullyRepayedTheirCredits(
                                queries.GetClientsSuccessfullyRepayedTheirCredits()
                            );
                            break;

                        case 20:
                            printer.PrintUnusedDeposits(
                                queries.GetUnusedDeposits()
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