using System.Collections.Generic;
using DotNetLab2.Models;

namespace DotNetLab2.QueryModels
{
    internal class ClientWithCreditMoney
    {
        public Client Client { get; set; }
        public IEnumerable<decimal> Money { get; set; }
    }
}
