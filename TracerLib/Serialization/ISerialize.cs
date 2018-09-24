using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib.Serialization
{
    public interface ISerialize
    {
        Stream SerializeResult(TraceResult traceResult, Stream stream);
    }
}
