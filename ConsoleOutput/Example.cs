using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerLib;


namespace ConsoleOutput
{
    class Example
    {
        private ITracer Tracer;

        public Example(ITracer tracer)
        {
            Tracer = tracer;
        }

        public void Method()
        {
            Tracer.StartTrace();

            new Example(Tracer).Kek();

            Thread.Sleep(10);
            new Thread(this.Kek).Start();

            Tracer.StopTrace();
        }
        
        public void Kek()
        {
            Tracer.StartTrace();

            

            Thread.Sleep(10);

            Tracer.StopTrace();
        }
    }

    
}
