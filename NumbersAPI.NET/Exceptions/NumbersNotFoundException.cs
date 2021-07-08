using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumbersAPI.NET.Exceptions
{
    public class NumbersNotFoundException : Exception
    {
        public NumbersNotFoundException() : base("Not found any facts about given object") { }
    }
}
