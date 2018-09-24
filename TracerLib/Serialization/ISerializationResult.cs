using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib.Serialization
{
    interface ISerializationResult
    {
        void SerializeResult(TraceResult traceResult, Stream stream);
    }
}
