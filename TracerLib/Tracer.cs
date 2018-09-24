using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace TracerLib
{
    public class Tracer : ITracer
    {

        TraceResult traceResult;

        public Tracer()
        {
            traceResult = new TraceResult();
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public void StartTrace()
        {
            ThreadResult currThread = traceResult.SetCurrThread(Thread.CurrentThread.ManagedThreadId);
            MethodBase methodBase = new StackTrace(1).GetFrame(0).GetMethod();
            var method = new MethodResult(methodBase);
            if (currThread.IsFirstLevelMethod())
                currThread.AddMethod(method);
            else
                currThread.GetCurrMethod().AddMethod(method);
            currThread.AddDepthMethod(method);
            method.StartTiming();
        }

        public void StopTrace()
        {
            ThreadResult currThread = traceResult.GetCurrThread(Thread.CurrentThread.ManagedThreadId);
            MethodResult method = currThread.PopCurrMethod();
            method.StopTiming();
        }
    }
}
