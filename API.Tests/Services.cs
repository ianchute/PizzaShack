using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzWare.NBuilder;

namespace API.Tests
{
    public static class Services
    {
        static Services()
        {
            Refresh();
        }

        public static void Refresh()
        {
            Generator = new RandomGenerator(new Random(DateTime.Now.Millisecond));
        }

        public static RandomGenerator Generator { get; set; }
    }
}
