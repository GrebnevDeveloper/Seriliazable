using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml;

namespace Seriliazable
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string typeSeriliazable = Console.ReadLine();
            string strForSeriliazable = Console.ReadLine();
            if (typeSeriliazable.ToLower().Equals("xml"))
            {
                Console.WriteLine(handlerXml(strForSeriliazable));
            }
            else if (typeSeriliazable.ToLower().Equals("json"))
            {
                Console.WriteLine(handlerJson(strForSeriliazable));
            }
        }

        //todo: выделите интерфейс 
        //и наследуйте от него JsonSeriazlier, XmlSerializer (check)

        public static string handlerXml(string strForSeriliazable)
        {
            XmlSeriliazable xml = new XmlSeriliazable();
            Input input = xml.Deserialize<Input>(strForSeriliazable);
            Output output = new Output(input);
            return xml.Serialize<Output>(output);
        }
        public static string handlerJson(string strForSeriliazable)
        {
            JsonSeriliazable json = new JsonSeriliazable();
            Input input = json.Deserialize<Input>(strForSeriliazable);
            Output output = new Output(input);
            return json.Serialize<Output>(output);
        }
    }
}