using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Seriliazable
{
    class XmlSeriliazable : ISerializer
    {
        public T Deserialize<T>(string str)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            StringReader sr = new StringReader(str);
            T obj = (T)xs.Deserialize(sr);
            sr.Dispose();
            return obj;
        }

        public string Serialize<T>(T output)
        {
            StringWriter sw = new StringWriter();
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            formatter.Serialize(sw, output, xsn);
            string xml = sw.ToString().Substring(sw.ToString().IndexOf('?', 3) + 2, sw.ToString().Length - sw.ToString().IndexOf('?', 3) - 2);
            xml = RemoveSpaces(xml);
            xml = RemoveEnter(xml);
            sw.Dispose();
            return xml;
        }

        private string RemoveSpaces(string text)
        {
            text = text.Replace(" ", "");

            if (text.Contains(" "))
            {
                text = RemoveSpaces(text);
            }
            else
            {
                return text;
            }
            return text;
        }

        private string RemoveEnter(string text)
        {
            text = text.Replace("\r\n", "");

            if (text.Contains("\r\n"))
            {
                text = RemoveEnter(text);
            }
            else
            {
                return text;
            }
            return text;
        }
    }
}
