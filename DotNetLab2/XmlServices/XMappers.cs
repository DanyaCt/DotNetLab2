using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Xml.Serialization;


namespace DotNetLab2.XmlServices
{
    public static class XMappers
    {
        public static T ToEntity<T>(this XElement xElement)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(xElement.CreateReader());
        }

        public static XElement ToXElement<T>(this T entity)
        {
            var type = typeof(T);
            var props = type.GetProperties();

            var propertiesNodes = props
                .Select(p => new XElement(p.Name, p.GetValue(entity)));

            var element = new XElement(type.Name, propertiesNodes);
            return element;
        }
    }
}