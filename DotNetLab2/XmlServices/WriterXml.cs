using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DotNetLab2.XmlServices
{
    public class WriterXml
    {
        private readonly string _dirPath;

        public WriterXml(string dirPath)
        {
            _dirPath = dirPath;
        }
        
        public void WriteRange<T>(IEnumerable<T> items, string document, string rootElName)
        {
            var settings = new XmlWriterSettings() { Indent = true };
            using (var xmlWriter = XmlWriter.Create(Path.Combine(_dirPath, document), settings))
            {
                xmlWriter.WriteStartElement(rootElName);
                foreach (var item in items)
                {
                    var xElement = item.ToXElement();
                    xmlWriter.WriteStartElement(xElement.Name.LocalName);
                    foreach (var element in xElement.Elements())
                    {
                        xmlWriter.WriteElementString(element.Name.LocalName, element.Value);
                    }
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }
        }
    }
}