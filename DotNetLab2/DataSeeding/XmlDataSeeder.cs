using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using DotNetLab2.Models;
using DotNetLab2.XmlServices;

namespace DotNetLab2.DataSeeding
{
    public class XmlDataSeeder
    {
        private readonly string _dirPath;
        private readonly WriterXml _xmlWriter;

        public XmlDataSeeder(string dirPath, WriterXml xmlWriter)
        {
            _dirPath = dirPath;
            _xmlWriter = xmlWriter;
        }
        
        public void EnsureDataSeeded()
        {
            EnsureEntitySeeded(Seeds.Clients, Config.EntitiesFileNames[nameof(Client)], "Clients");
            EnsureEntitySeeded(Seeds.Credits, Config.EntitiesFileNames[nameof(Credit)], "Credits");
            EnsureEntitySeeded(Seeds.Currencies, Config.EntitiesFileNames[nameof(Currency)], "Currencies");
            EnsureEntitySeeded(Seeds.Deposits, Config.EntitiesFileNames[nameof(Deposit)], "Deposits");
            EnsureEntitySeeded(Seeds.ClientsToCredits, Config.EntitiesFileNames[nameof(ClientToCredit)], "ClientsToCredits");
            EnsureEntitySeeded(Seeds.ClientsToDeposits, Config.EntitiesFileNames[nameof(ClientToDeposit)], "ClientsToDeposits");
        }

        private void EnsureEntitySeeded<T>(IEnumerable<T> data, string fileName, string rootElName)
        {
            var path = Path.Combine(_dirPath, fileName);
            if (!File.Exists(path))
                _xmlWriter.WriteRange(data, fileName, rootElName);
        }
    }
}