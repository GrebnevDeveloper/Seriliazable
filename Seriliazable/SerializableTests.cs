using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seriliazable
{
    [TestFixture]
    class SerializableTests
    {
        [TestCase]
        public void handlerXml()
        {
            Program program = new Program();
            Assert.AreEqual("<Output><SumResult>30.30</SumResult><MulResult>4</MulResult><SortedInputs><decimal>1</decimal><decimal>1.01</decimal><decimal>2.02</decimal><decimal>4</decimal></SortedInputs></Output>",
                Program.handlerXml("<Input><K>10</K><Sums><decimal>1.01</decimal><decimal>2.02</decimal></Sums><Muls><int>1</int><int>4</int></Muls></Input>"));
        }

        [TestCase]
        public void handlerJson()
        {
            Assert.AreEqual("{\"SumResult\":30.30,\"MulResult\":4,\"SortedInputs\":[1,1.01,2.02,4]}",
                Program.handlerJson("{\"K\":10,\"Sums\":[1.01,2.02],\"Muls\":[1,4]}"));
        }
    }
}
