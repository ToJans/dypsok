using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dypsok
{
    static class Guard
    {
        public static void That<TException>(bool Assertion) where TException:Exception,new()
        {
            if (!Assertion) throw new TException();
        }

        public static void Against<TException>(bool Assertion) where TException:Exception,new()
        {
            if (Assertion) throw new TException();
        }

    }
}
