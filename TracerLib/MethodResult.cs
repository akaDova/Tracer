using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Reflection;
using System.Diagnostics;

namespace TracerLib
{
    [DataContract]
    public class MethodResult
    {
        
        Stack<MethodResult> Methods;
        [DataMember(Name = "methods")]
        public List<MethodResult> MethodsResults
        {
            get => new List<MethodResult>(Methods);
            private set { }
        }

        [DataMember(Name = "time")]
        string TimeMs
        {
            get => Time.ToString() + "ms";
            set { }
        }
        [DataMember(Name = "class")]
        public string Class
        {
            get;
            private set;
        }
        [DataMember(Name = "name")]
        public string Name
        {
            get;
            private set;
        }

        public long Time
        {
            get => stopwatch.ElapsedMilliseconds;

        }
        Stopwatch stopwatch;

        public MethodResult(MethodBase methodBase)
        {
            stopwatch = new Stopwatch();
            Name = methodBase.Name;
            Class = methodBase.ReflectedType.Name;
            Methods = new Stack<MethodResult>();
        }

        public void AddMethod(MethodResult method)
        {
            Methods.Push(method);

        }

        

        public MethodResult GetCurrMethod()
        {

            return Methods.Peek();
        }

        public MethodResult PopCurrMethod()
        {

            return Methods.Pop();
        }

        public void StartTiming()
        {
            stopwatch.Start();
        }

        public void StopTiming()
        {
            stopwatch.Stop();
        }
    }
}
