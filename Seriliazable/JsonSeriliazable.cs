using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Seriliazable
{
    class JsonSeriliazable : ISerializer
    {
        public T Deserialize<T>(string str)
        {
            return new JavaScriptSerializer().Deserialize<T>(str);
        }

        public string Serialize<T>(T output)
        {
            return new JavaScriptSerializer().Serialize(output);
        }
    }
}
