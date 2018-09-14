using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;

namespace ConsoleOutput
{
    class Example
    {
        private readonly ITracer Tracer;

        Example(ITracer tracer)
        {
            Tracer = tracer;
        }


    }
}
