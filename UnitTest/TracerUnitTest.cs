using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TracerLib;

namespace UnitTest
{
    [TestClass]
    public class TracerUnitTest
    {
        private static ITracer tracer;
        private static readonly int waitTime = 100;
        private static readonly int threadsCount = 4;

        private void TimeTest(long actual, long expected)
        {
            Assert.IsTrue(actual >= expected);
        }

        private void SingleThreadedMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            tracer.StopTrace();
        }

        private void MultiThreadedMethod()
        {
            var threads = new List<Thread>();
            Thread newThread;
            for (int i = 0; i < threadsCount; i++)
            {
                newThread = new Thread(SingleThreadedMethod);
                threads.Add(newThread);
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            tracer.StopTrace();
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        [TestMethod]
        public void SingleThreadTest()
        {
            // only checks time
            tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            tracer.StopTrace();
            long actual = tracer.GetTraceResult().ThreadResults[0].Time;
            TimeTest(actual, waitTime);
        }

        [TestMethod]
        public void MultiThreadTest()
        {
            // only checks time
            tracer = new Tracer();
            var threads = new List<Thread>();
            long expected = 0;
            Thread newThread;
            for (int i = 0; i < threadsCount; i++)
            {
                newThread = new Thread(SingleThreadedMethod);
                threads.Add(newThread);
                newThread.Start();
                expected += waitTime;
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            long actual = 0;
            foreach (ThreadResult threadResult in tracer.GetTraceResult().ThreadResults)
            {
                actual += threadResult.Time;
            }
            TimeTest(actual, expected);
        }

        [TestMethod]
        public void TwoLevelMultiThreadTest()
        {
            // checks time, amount, classnames and methodnames
            tracer = new Tracer();
            var threads = new List<Thread>();
            long expected = 0;
            Thread newThread;
            for (int i = 0; i < threadsCount; i++)
            {
                newThread = new Thread(MultiThreadedMethod);
                threads.Add(newThread);
                newThread.Start();
                expected += waitTime * (threadsCount + 1);
            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            long actual = 0;
            TraceResult result = tracer.GetTraceResult();
            foreach (ThreadResult threadResult in result.ThreadResults)
            {
                actual += threadResult.Time;
            }
            TimeTest(actual, expected);
            Assert.AreEqual(threadsCount * threadsCount + threadsCount, result.ThreadResults.Count);
            int multiThreadedMethodCounter = 0, singleThreadedMethodCounter = 0;
            MethodResult methodResult;
            foreach (ThreadResult threadResult in result.ThreadResults)
            {
                Assert.AreEqual(threadResult.MethodResults.Count, 1);
                methodResult = threadResult.MethodResults[0];
                Assert.AreEqual(0, methodResult.MethodsResults.Count);
                Assert.AreEqual(nameof(TracerUnitTest), methodResult.Class);
                TimeTest(methodResult.Time, waitTime);
                if (methodResult.Name == nameof(MultiThreadedMethod))
                    multiThreadedMethodCounter++;
                if (methodResult.Name == nameof(SingleThreadedMethod))
                    singleThreadedMethodCounter++;
            }
            Assert.AreEqual(threadsCount, multiThreadedMethodCounter);
            Assert.AreEqual(threadsCount * threadsCount, singleThreadedMethodCounter);
        }

        [TestMethod]
        public void InnerMethodTest()
        {
            // checks time, amount, classnames and methodnames 
            tracer = new Tracer();
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            SingleThreadedMethod();
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual(1, traceResult.ThreadResults.Count);
            TimeTest(tracer.GetTraceResult().ThreadResults[0].Time, waitTime * 2);
            Assert.AreEqual(1, traceResult.ThreadResults[0].MethodResults.Count);
            MethodResult methodResult = traceResult.ThreadResults[0].MethodResults[0];
            Assert.AreEqual(nameof(TracerUnitTest), methodResult.Class);
            Assert.AreEqual(nameof(InnerMethodTest), methodResult.Name);
            TimeTest(methodResult.Time, waitTime * 2);
            Assert.AreEqual(1, methodResult.MethodsResults.Count);
            MethodResult innerMethodResult = methodResult.MethodsResults[0];
            Assert.AreEqual(nameof(TracerUnitTest), innerMethodResult.Class);
            Assert.AreEqual(nameof(SingleThreadedMethod), innerMethodResult.Name);
            TimeTest(innerMethodResult.Time, waitTime);
        }
    }
}
