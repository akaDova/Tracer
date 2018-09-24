using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace TracerLib.Serialization
{
    public class SerializeJSON : ISerialize
    {
        DataContractJsonSerializer serializer;

        public Stream SerializeResult(TraceResult traceResult, Stream stream)
        {
            
            serializer.WriteObject(stream, traceResult);
            return stream;
        }

        public SerializeJSON()
        {
            serializer = new DataContractJsonSerializer(typeof(TraceResult));
        }
    }
}
