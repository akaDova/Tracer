﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
using System.Runtime.Serialization;

namespace TracerLib
{
    [DataContract]
    public class TraceResult
    {
        // fields

        public ConcurrentDictionary<int, ThreadResult> Threads;
        //ConcurrentBag<ThreadResult> Threads;
        [DataMember(Name = "threads")]
        public List<ThreadResult> ThreadResults
        {
            get => Threads.Values.ToList();


            private set { }
        }


        public TraceResult()
        {
            Threads = new ConcurrentDictionary<int, ThreadResult>();
        }

        internal ThreadResult SetCurrThread(int threadId)
        {
            var newThread = new ThreadResult(threadId);

            return Threads.GetOrAdd(threadId, newThread);
        }

        internal ThreadResult GetCurrThread(int threadId)
        {
            return Threads[threadId];
        }
    }
}
