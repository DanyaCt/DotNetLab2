using System;
using System.Text;

namespace DotNetLab2.ConsoleServices
{
    public class MenuPrinter
    {
        public static void Print()
        {
            var sb = new StringBuilder();
            sb.Append("Options: \n");
            sb.Append("0 - Add New Сlient \n");
            sb.Append("1 - Add New ClientToCredit \n");
            sb.Append("2 - Add New ClientToDeposit \n");
            sb.Append("3 - Add New Credit \n");
            sb.Append("4 - Add New Currency \n");
            sb.Append("5 - Add New Deposit \n");
            sb.Append("6 - Get Clients FullNames \n");
            sb.Append("7 - Get All items In Storage \n");
            sb.Append("8 - Get Clients With Account Numbers\n");
            sb.Append("9 - Get Credits After 2022 \n");
            sb.Append("10 - Get Deposits Grouped By Currency \n");
            sb.Append("11 - Get Not Repayed Credits \n");
            sb.Append("12 - Get Client With Credits And Without Deposits \n");
            sb.Append("13 - Get Client And Number Of Credits\n");
            sb.Append("14 - Get Average Duration Deposits And Actual Average Duration \n");
            sb.Append("15 - Get Quantity Of Clients With Credit No Less Than 50000UAH \n");
            sb.Append("16 - Get Client With Most Credits With His Money And Sorted Money \n");
            sb.Append("17 - Get Credits With Repayment No Less 6 Month \n");
            sb.Append("18 - Get Clients Without Deposits And Credits \n");
            sb.Append("19 - Get Clients Successfully Repayed Their Credits \n");
            sb.Append("20 - Get Unused Deposits \n");
            Console.WriteLine(sb.ToString());
        }
    }
}