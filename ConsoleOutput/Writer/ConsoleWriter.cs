using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;
using TracerLib.Serialization;

namespace ConsoleOutput.Writer
{
    class ConsoleWriter : IWriter
    {
        TraceResult result;
        Stream stream;

        public void WriteData(Serializer serialize)
        {
            using (stream = Console.OpenStandardOutput())
            using (var writer = new StreamWriter(serialize(result, stream)))
            {
                writer.AutoFlush = true;
                Console.SetOut(writer);
            }
            
        }

        public ConsoleWriter(TraceResult traceResult)
        {
            result = traceResult;
        }
    }
}
