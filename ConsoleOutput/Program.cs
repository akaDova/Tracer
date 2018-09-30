using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;
using TracerLib.Serialization;
using ConsoleOutput.Writer;


namespace ConsoleOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            new Example(tracer).Method();
            TraceResult result = tracer.GetTraceResult();
            var serialJSON = new SerializeJSON();
            var serialXML = new SerializeXML();
            new ConsoleWriter(result).WriteData(serialJSON.SerializeResult);
            new ConsoleWriter(result).WriteData(serialXML.SerializeResult);
            new FileWriter(result, "./result.json").WriteData(serialJSON.SerializeResult);
            new FileWriter(result, "./result.xml").WriteData(serialXML.SerializeResult);
        }
    }
}
