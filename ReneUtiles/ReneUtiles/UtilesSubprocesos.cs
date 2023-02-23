using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ReneUtiles
{
    public abstract class UtilesSubprocesos
    {
    	public static Task subp(Action a){
    		return Task.Run(a);
    	}
    	public static Task subp<TResult>(Func<TResult> function){
    		return Task.Run<TResult>(function);
    	}
//        public static Thread subp(ThreadStart ts)
//        {
//            Thread th = new Thread(ts);
//            th.Start();
//            return th;
//        }
    }
}
