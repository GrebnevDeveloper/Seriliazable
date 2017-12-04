using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seriliazable
{
    public interface ISerializer
    {
        string Serialize<T>(T str);
        T Deserialize<T>(string str);
    }
}
