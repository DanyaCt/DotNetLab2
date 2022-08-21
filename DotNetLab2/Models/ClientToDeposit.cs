using System;

namespace DotNetLab2.Models
{
    public class ClientToDeposit
    {
        public int ClientId { get; set; }
        public int DepositId { get; set; }
        public string AccountNumber { get; set; }
        public DateTimeOffset DateOfBeginning { get; set; }
        public DateTimeOffset? DateOfEnding { get; set; }
        public decimal AmountOfMoneyToDeposit { get; set; }
    }
}
