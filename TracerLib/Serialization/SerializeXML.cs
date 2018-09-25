using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace TracerLib.Serialization
{
    public class SerializeXML : ISerialize
    {
        DataContractSerializer serializer;

        public Stream SerializeResult(TraceResult traceResult, Stream stream)
        {
            using (XmlWriter XMLWriter = XmlWriter.Create(stream, new XmlWriterSettings { Indent = true } ))
            {
                serializer.WriteObject(XMLWriter, traceResult);
            }
                
            return stream;
        }

        public SerializeXML()
        {

            serializer = new DataContractSerializer(typeof(TraceResult));
        }
    }
}
