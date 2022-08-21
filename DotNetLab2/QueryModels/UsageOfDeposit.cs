using DotNetLab2.Models;

namespace DotNetLab2.QueryModels
{
    internal class UsageOfDeposit
    {
        public Deposit Deposit { get; set; }
        public int Quantity { get; set; }
    }
}
