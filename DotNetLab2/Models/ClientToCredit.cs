using System;

namespace DotNetLab2.Models
{
    public class ClientToCredit
    {
        public int ClientId { get; set; }
        public int CreditId { get; set; }
        public DateTimeOffset DateOfIssue { get; set; }
        public DateTimeOffset? DateOfRepayment { get; set; }
        public decimal AmountOfMoneyTaken { get; set; }
    }
}
