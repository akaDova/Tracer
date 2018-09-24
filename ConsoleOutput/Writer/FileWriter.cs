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
    class FileWriter : IWriter
    {
        TraceResult result;

        FileStream stream;

        string FileName
        {
            get;
            set;
        }

        public void WriteData(Serializer serialize)
        {
            using (stream = new FileStream(FileName, FileMode.OpenOrCreate))
            {
                serialize(result, stream);               
            }
                
        }

        public FileWriter(TraceResult traceResult, string fileName)
        {
            result = traceResult;
            FileName = fileName;
        }
    }
}
