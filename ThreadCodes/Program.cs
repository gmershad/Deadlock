using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadCodes
{
    class Program
    {
        static void Main(string[] args)
        {
            //DeadLock.ExecuteDeadLockCode();
            //DeadLock.ExecuteDeadlockAvoidance();
            DeadLock1.SimplestDeadlocking();
            Console.ReadKey();
        }
    }
}
