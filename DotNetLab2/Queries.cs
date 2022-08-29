using System;
using System.Collections.Generic;
using System.Linq;
using DotNetLab2.Models;
using DotNetLab2.QueryModels;
using DotNetLab2.XmlServices;

namespace DotNetLab2
{
    internal class Queries
    {
        private readonly Context _context;

        public Queries(Context context)
        {
            _context = context;
        }

        public IEnumerable<string> GetClientsFullNames()
        {
            return from client in _context.Clients.Root?.Elements()
                select client.Element("FullName")?.Value;
        }

        public IEnumerable<ClientWithAccountNumber> GetClientsWithAccountNumbers()
        {
            return from client in _context.Clients.Root?.Elements()
                join clientToDeposit in _context.ClientsToDeposits.Root?.Elements()
                    on client.Element("Id")?.Value equals clientToDeposit.Element("ClientId")?.Value
                select new ClientWithAccountNumber()
                {
                    AccountCode = clientToDeposit.Element("AccountNumber")?.Value,
                    FullName = client.Element("FullName")?.Value
                };
        }

        public IEnumerable<Credit> GetCreditsAfter2022()
        {
            return from credit in _context.Credits.Root?.Elements()
                join creditToClient in _context.ClientsToCredits.Root?.Elements()
                    on credit.Element("Id")?.Value equals creditToClient.Element("CreditId")?.Value
                where Convert.ToDateTime(creditToClient.Element("DateOfIssue")?.Value).Year >= 2022
                select credit.ToEntity<Credit>();
        }

        public IEnumerable<DepositsByCurrency> GetDepositsGroupedByCurrency()
        {
            return from deposit in _context.Deposits.Root?.Elements()
                join currency in _context.Currencies.Root?.Elements()
                    on deposit.Element("CurrencyId")?.Value equals currency.Element("Id")?.Value
                group deposit by currency.Element("Name")?.Value
                into grouped
                select new DepositsByCurrency()
                {
                    CurrencyName = grouped.Key,
                    Deposits = grouped.Select(el => el.ToEntity<Deposit>())
                };
        }

        public IEnumerable<Credit> GetNotRepayedCredits()
        {
            return from credit in _context.Credits.Root?.Elements()
                join clientToCredit in _context.ClientsToCredits.Root?.Elements()
                    on credit.Element("Id")?.Value equals clientToCredit.Element("CreditId")?.Value
                where string.IsNullOrEmpty(clientToCredit.Element("DateOfRepayment")?.Value)
                select credit.ToEntity<Credit>();
        }

        public IEnumerable<UsageOfDeposit> GetDepositsAndTheirUsageQuantity()
        {
            return from deposit in _context.Deposits.Root?.Elements()
                join clientToDeposit in _context.ClientsToDeposits.Root?.Elements()
                    on deposit.Element("Id")?.Value equals clientToDeposit.Element("DepositId")?.Value
                group clientToDeposit by deposit
                into grouped
                select new UsageOfDeposit()
                {
                    Deposit = grouped.Key.ToEntity<Deposit>(),
                    Quantity = grouped.Count()
                };
        }

        public IEnumerable<Client> GetClientWithCreditsWithoutDeposits()
        {
            var clientsWithoutDeposits = _context.Clients.Root?.Elements()
                .Select(c => c.ToEntity<Client>())
                .Except(
                    _context.Clients.Root?.Elements()
                        .Join(_context.ClientsToDeposits.Root?.Elements()!,
                            client => client.Element("Id").Value,
                            deposit => deposit.Element("ClientId").Value,
                            (client, deposit) => (client, deposit))
                        .GroupBy(x => x.client)
                        .Select(x => x.Key.ToEntity<Client>())
                        .Distinct());

            var clientsWithCredits = _context.Clients.Root?.Elements()
                .Join(_context.ClientsToCredits.Root?.Elements(),
                    client => client.Element("Id").Value,
                    credit => credit.Element("ClientId").Value,
                    (client, credit) => (client, credit))
                .GroupBy(x => x.client)
                .Where(x => x.Any())
                .Select(x => x.Key.ToEntity<Client>());

            return clientsWithCredits.Intersect(clientsWithoutDeposits);
        }

        public IEnumerable<ClientWithQuantity> GetClientAndNumberOfCredits()
        {
            return from client in _context.Clients.Root?.Elements()
                join clientsToCredit in _context.ClientsToCredits.Root?.Elements()
                    on client.Element("Id")?.Value equals clientsToCredit.Element("ClientId")?.Value
                group clientsToCredit by client
                into clientGroup
                select new ClientWithQuantity()
                {
                    Client = clientGroup.Key.ToEntity<Client>(),
                    Quantity = clientGroup.Count()
                };
        }

        public (float, float) GetAverageDurationDepositsAndActualAverageDuration()
        {
            var average = (float)_context.Deposits.Root?.Elements()
                .Select(x => Convert.ToInt32(x.Element("DurationInMonths")?.Value) * 30)
                .Average();

            var actualAverage = (float)_context.Deposits.Root?.Elements()
                .Join(_context.ClientsToDeposits.Root.Elements(),
                    deposit => deposit.Element("Id")?.Value,
                    clientToDeposit => clientToDeposit.Element("DepositId")?.Value,
                    (deposit, clientToDeposit) => (deposit, clientToDeposit))
                .Where(x => !string.IsNullOrEmpty(x.clientToDeposit.Element("DateOfEnding")?.Value))
                .Select(x =>
                    (float)(Convert.ToDateTime(x.clientToDeposit.Element("DateOfEnding")?.Value)
                            - Convert.ToDateTime(x.clientToDeposit.Element("DateOfBeginning")?.Value)).Days)
                .Average();

            return (average, actualAverage);
        }

        public int GetQuantityOfClientsWithCreditNoLessThan50000UAH()
        {
            return _context.ClientsToCredits.Root?.Elements()
                .Join(_context.Credits.Root?.Elements()!,
                    ctc => ctc.Element("CreditId")?.Value,
                    credit => credit.Element("Id")?.Value,
                    (ctc, credit) => (ctc, credit))
                .Join(_context.Currencies.Root?.Elements()!,
                    credit => credit.credit.Element("CurrencyId")?.Value,
                    currency => currency.Element("Id")?.Value,
                    (credit, currency) => (credit.ctc, credit.credit, currency))
                .Count(x => x.currency.Element("Name").Value.Equals("UAH")
                            && Convert.ToDecimal(x.ctc.Element("AmountOfMoneyTaken").Value) >= 50000m) ?? 0;
        }

        public ClientWithCreditMoney GetClientWithMostCreditsWithHisMoneyAndSortedMoney()
        {
            var groups = _context.Clients.Root?.Elements()
                .Join(_context.ClientsToCredits.Root?.Elements()!,
                    client => client.Element("Id")?.Value,
                    credit => credit.Element("ClientId")?.Value,
                    (client, credit) =>
                        (Client: client.ToEntity<Client>(),
                            AmountOfMoneyTaken: Convert.ToDecimal(credit.Element("AmountOfMoneyTaken")?.Value)))
                .GroupBy(x => x.Client)
                .Select(x => new ClientWithCreditMoney()
                {
                    Client = x.Key,
                    Money = x.Select(x => x.AmountOfMoneyTaken)
                        .OrderByDescending(money => money),
                });

            var maxQuantity = groups
                .Select(x => x.Money.Count())
                .Max();

            return groups.FirstOrDefault(x => x.Money.Count() == maxQuantity);
        }

        public IEnumerable<Credit> GetCreditsWithRepaymentNoLess6Month()
        {
            return _context.Credits.Root?.Elements()
                .Where(x => Convert.ToInt32(x.Element("RepaymentDurationInMonths").Value) >= 6)
                .Select(x => x.ToEntity<Credit>());
        }

        public IEnumerable<Client> GetClientsWithoutDepositsAndCredits()
        {
            var clientsWithCreditHistory = _context.ClientsToCredits.Root?.Elements()
                .Join(_context.Clients.Root?.Elements(),
                    credit => credit.Element("ClientId").Value,
                    client => client.Element("Id").Value,
                    (credit, client) => (credit, client))
                .Select(x => x.client.ToEntity<Client>())
                .Distinct();

            var clientsWithDepositHistory = _context.ClientsToDeposits.Root?.Elements()
                .Join(_context.Clients.Root?.Elements(),
                    deposit => deposit.Element("ClientId").Value,
                    client => client.Element("Id").Value,
                    (deposit, client) => (deposit, client))
                .Select(x => x.client.ToEntity<Client>())
                .Distinct();

            return _context.Clients.Root?.Elements()
                .Select(x => x.ToEntity<Client>())
                .Except(clientsWithDepositHistory.Union(clientsWithCreditHistory));
        }

        public IEnumerable<Client> GetClientsSuccessfullyRepayedTheirCredits()
        {
            return _context.Clients.Root?.Elements()
                .Join(_context.ClientsToCredits.Root?.Elements(),
                    client => client.Element("Id")?.Value,
                    credit => credit.Element("ClientId")?.Value,
                    (client, credit) =>
                        (client: client.ToEntity<Client>(), credit: credit.ToEntity<ClientToCredit>()))
                .GroupBy(x => x.client)
                .Select(x => (x.Key, x.Select(y => y.credit)))
                .Where(x =>
                    !x.Item2.Any(ctc => ctc.DateOfRepayment is null))
                .Select(x => x.Key);
        }

        public IEnumerable<Deposit> GetUnusedDeposits()
        {
            return _context.Deposits.Root?.Elements().Select(d => d.ToEntity<Deposit>())
                .Except(
                    _context.ClientsToDeposits.Root?.Elements()
                        .Join(_context.Deposits.Root?.Elements(),
                            ctd => ctd.Element("DepositId")?.Value,
                            deposit => deposit.Element("Id")?.Value,
                            (ctd, deposit) => deposit.ToEntity<Deposit>())
                );
        }
        public IEnumerable<(string, int)> GetDepositsAndCredits()
        {
            return _context.Deposits.Root?.Elements().Select(d => d.ToEntity<Deposit>())
                .Select(x => (nameof(Deposit), x.Id))
                .Concat(
                    _context.Credits.Root?.Elements().Select(d => d.ToEntity<Credit>())
                        .Select(y => (nameof(Credit), y.Id))
                );
        }
    }
}