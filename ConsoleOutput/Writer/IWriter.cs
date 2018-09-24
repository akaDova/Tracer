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
    interface IWriter
    {
        void WriteData(Serializer serialize);
    }

    public delegate Stream Serializer(TraceResult traceResult, Stream stream);
}
