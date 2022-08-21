using System.Collections.Generic;
using DotNetLab2.Models;

namespace DotNetLab2.QueryModels
{
    internal class DepositsByCurrency
    {
        public string CurrencyName { get; set; }
        public IEnumerable<Deposit> Deposits { get; set; }
    }
}
