using System.IO;
using System.Xml;
using System.Xml.Linq;
using DotNetLab2.DataSeeding;
using DotNetLab2.Models;
using DotNetLab2.XmlServices;

namespace DotNetLab2
{
    internal class Context
    {
        private readonly string _dirPath;
        private readonly XmlDataSeeder _xmlDataSeeder;
        private readonly WriterXml _xmlWriter;

        public Context(string dirPath)
        {
            _dirPath = dirPath;
            _xmlWriter = new WriterXml(_dirPath);
            _xmlDataSeeder = new XmlDataSeeder(_dirPath, _xmlWriter);
        }

        public XDocument Clients
        {
            get => GetXDocument(Path.Combine(_dirPath, Config.EntitiesFileNames[nameof(Client)]));
        }

        public XDocument Currencies
        {
            get => GetXDocument(Path.Combine(_dirPath, Config.EntitiesFileNames[nameof(Currency)]));
        }

        public XDocument Credits
        {
            get => GetXDocument(Path.Combine(_dirPath, Config.EntitiesFileNames[nameof(Credit)]));
        }

        public XDocument Deposits
        {
            get => GetXDocument(Path.Combine(_dirPath, Config.EntitiesFileNames[nameof(Deposit)]));
        }

        public XDocument ClientsToCredits
        {
            get => GetXDocument(Path.Combine(_dirPath, Config.EntitiesFileNames[nameof(ClientToCredit)]));
        }

        public XDocument ClientsToDeposits
        {
            get => GetXDocument(Path.Combine(_dirPath, Config.EntitiesFileNames[nameof(ClientToDeposit)]));
        }

        public static XDocument GetXDocument(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            using (var nodeReader = new XmlNodeReader(doc))
            {
                return XDocument.Load(nodeReader);
            }
        }
        public void EnsureDataSeeded()
        {
            _xmlDataSeeder.EnsureDataSeeded();
        }
    }
}
