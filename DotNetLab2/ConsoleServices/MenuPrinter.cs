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
            sb.Append("6 - Print all queries \n");
            Console.WriteLine(sb.ToString());
        }
    }
}