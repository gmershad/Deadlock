using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadCodes
{

    public class DeadLock
    {
        private static object lockA = new object();
        private static object lockB = new object();

        #region Deadlock Case

        private static void CompleteWork1()
        {
            lock (lockA)
            {
                Console.WriteLine("Trying to Acquire lock on lockB");

                lock (lockB)
                {
                    Console.WriteLine("Critical Section of CompleteWork1");
                    //Access some shared critical section.
                }
            }
        }

        private static void CompleteWork2()
        {
            lock (lockB)
            {
                Console.WriteLine("Trying to Acquire lock on lockA");

                lock (lockA)
                {
                    Console.WriteLine("Critical Section of CompleteWork2");
                    //Access some shared critical section.
                }
            }
        }

        public static void ExecuteDeadLockCode()
        {
            Thread thread1 = new Thread(CompleteWork1);
            Thread thread2 = new Thread(CompleteWork2);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            //Below code section will never execute due to deadlock.
            Console.WriteLine("Processing Completed....");

        }

        #endregion


        #region Deallock Avoidance
        private static void MyWork1()
        {
            lock (lockA)
            {
                Console.WriteLine("Trying to acquire lock on lockB");

                // This will try to acquire lock for 5 seconds.
                if (Monitor.TryEnter(lockB, 5000))
                {
                    try
                    {
                        // This block will never be executed.
                        Console.WriteLine("In DoWork1 Critical Section.");
                        // Access some shared resource here.
                    }
                    finally
                    {
                        Monitor.Exit(lockB);
                    }
                }
                else
                {
                    // Print lock not able to acquire message.
                    Console.WriteLine("Unable to acquire lock, exiting MyWork1.");
                }
            }
        }

        private static void MyWork2()
        {
            lock (lockB)
            {
                Console.WriteLine("Trying to acquire lock on lockA");
                lock (lockA)
                {
                    Console.WriteLine("In MyWork2 Critical Section.");
                    // Access some shared resource here.
                }
            }
        }

        public static void ExecuteDeadlockAvoidance()
        {
            // Initialize thread with address of DoWork1
            Thread thread1 = new Thread(MyWork1);

            // Initilaize thread with address of DoWork2
            Thread thread2 = new Thread(MyWork2);

            // Start the Threads.
            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("Done Processing...");
        }

        #endregion

    }
}
