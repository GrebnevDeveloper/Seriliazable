using System;
using System.IO;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using System.Xml;

namespace Seriliazable
{
    class Program
    {
        static void Main(string[] args)
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
        public static string handlerXml(string strForSeriliazable)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Input));
            StringReader sr = new StringReader(strForSeriliazable);
            Input input = (Input)xs.Deserialize(sr);
            StringWriter sw = new StringWriter();
            XmlSerializer formatter = new XmlSerializer(typeof(Output));
            Output output = new Output(input);
            XmlSerializerNamespaces xsn = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            formatter.Serialize(sw, output, xsn);
            string xml = sw.ToString().Substring(sw.ToString().IndexOf('?', 3) + 2, sw.ToString().Length - sw.ToString().IndexOf('?', 3) - 2);
            xml = RemoveSpaces(xml);
            xml = RemoveEnter(xml);
            return xml;
        }
        public static string handlerJson(string strForSeriliazable)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Input input = jss.Deserialize<Input>(strForSeriliazable);
            Output output = new Output(input);
            string json = "{\"SumResult\":" + jss.Serialize(output.SumResult)
                + ",\"MulResult\":" + jss.Serialize(output.MulResult)
                + ",\"SortedInputs\":[";
            for (int i = 0; i < output.SortedInputs.Length; i++)
            {
                json += output.SortedInputs[i].ToString("0.0###########", System.Globalization.CultureInfo.InvariantCulture);
                if (i < output.SortedInputs.Length - 1)
                {
                    json += ",";
                }
            }
            json += "]}";
            return json;
        }

        private static string RemoveSpaces(string text)
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

        private static string RemoveEnter(string text)
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

    public class Input
    {
        public int K { get; set; }
        public decimal[] Sums { get; set; }
        public int[] Muls { get; set; }
    }

    public class Output
    {
        public decimal SumResult { get; set; }
        public int MulResult { get; set; }
        public decimal[] SortedInputs { get; set; }
        public Output()
        { }
        public Output(Input input)
        {
            SumResult = calculationSumResult(input);
            MulResult = calculationMulResult(input);
            SortedInputs = calculationSortedResult(input);
        }
        private decimal calculationSumResult(Input input)
        {
            decimal sumResult = 0;
            for (int i = 0; i < input.Sums.Length; i++)
            {
                sumResult += input.Sums[i];
            }
            sumResult *= input.K;
            return sumResult;
        }
        private int calculationMulResult(Input input)
        {
            int mulResult = 0;
            mulResult = input.Muls[0];
            for (int i = 1; i < input.Muls.Length; i++)
            {
                mulResult *= input.Muls[i];
            }

            return mulResult;
        }
        private decimal[] calculationSortedResult(Input input)
        {
            decimal[] sortedInputs = new decimal[input.Sums.Length + input.Muls.Length];
            for (int i = 0; i < input.Sums.Length; i++)
            {
                sortedInputs[i] = input.Sums[i];
            }
            for (int i = input.Sums.Length; i < input.Sums.Length + input.Muls.Length; i++)
            {
                sortedInputs[i] = (decimal)input.Muls[i - input.Sums.Length];
            }
            Array.Sort(sortedInputs);
            return sortedInputs;
        }

    }
}