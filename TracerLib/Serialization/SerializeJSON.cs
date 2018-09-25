using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace TracerLib.Serialization
{
    public class SerializeJSON : ISerialize
    {
        DataContractJsonSerializer serializer;

        public Stream SerializeResult(TraceResult traceResult, Stream stream)
        {
            using (XmlDictionaryWriter JSONWriter = JsonReaderWriterFactory.CreateJsonWriter(stream, Encoding.UTF8, ownsStream: false, indent: true))
            {
                serializer.WriteObject(JSONWriter, traceResult);
            }
                
            return stream;
        }

        public SerializeJSON()
        {
            serializer = new DataContractJsonSerializer(typeof(TraceResult));
        }
    }
}
