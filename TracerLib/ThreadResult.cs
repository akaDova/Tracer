using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.Serialization;


namespace TracerLib
{
    [DataContract]
    internal class ThreadResult
    {
        [DataMember(Name = "id")]
        public int Id
        {
            get;
            private set;
        }
        [DataMember(Name = "time")]
        string TimeMs
        {
            get => BreadthMethods.Sum(method => method.Time).ToString() + "ms";
            set { }
        }
        [DataMember(Name = "methods")]
        Stack<MethodResult> BreadthMethods;
        Stack<MethodResult> DepthMethods;
        
        public ThreadResult(int id)
        {
            Id = id;
            BreadthMethods = new Stack<MethodResult>();
            DepthMethods = new Stack<MethodResult>();
        }

        public void AddMethod(MethodResult method)
        {
            BreadthMethods.Push(method);

        }

        public MethodResult GetCurrMethod()
        {

            return DepthMethods.Peek();
        }

        public MethodResult PopCurrMethod()
        {

            return DepthMethods.Pop();
        }
        public void AddDepthMethod(MethodResult method)
        {
            DepthMethods.Push(method);
        }

        public bool IsFirstLevelMethod()
        {
            return DepthMethods.Count == 0;
        }
    }
    //public static class ConcurentStackEx
    //{
    //    public static bool IsHas<T>(this ICollection<T> list, T elem)
    //    {
    //        return list.Where(x => x == elem).Count > 0;
    //    }
    //}
}

