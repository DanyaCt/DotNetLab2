using System.Collections.Generic;
using DotNetLab2.Models;

namespace DotNetLab2
{
    public static class Config
    {
        public static string XmlFilesDirPath = "../../../DataStore";

        public static Dictionary<string, string> EntitiesFileNames = new Dictionary<string, string>()
        {
            { nameof(Client), "Clients.xml" },
            { nameof(ClientToCredit), "ClientToCredits.xml" },
            { nameof(ClientToDeposit), "ClientToDeposits.xml" },
            { nameof(Credit), "Credits.xml" },
            { nameof(Currency), "Currencies.xml" },
            { nameof(Deposit), "Deposits.xml" }
        };
    }
}