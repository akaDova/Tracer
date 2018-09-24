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
    internal class MethodResult
    {
        [DataMember(Name = "methods")]
        Stack<MethodResult> Methods;
        [DataMember(Name = "time")]
        string TimeMs
        {
            get => Time.ToString() + "ms";
            set { }
        }
        [DataMember(Name = "class")]
        string Class;
        [DataMember(Name = "name")]
        string Name;

        internal long Time
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
