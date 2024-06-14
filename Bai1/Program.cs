using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bai1
{
    internal class Program
    {
        static Random random = new Random();
        static object lockObj = new object();
        static int number;        
        static void Thread1()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lockObj)
                {
                    number = random.Next(1, 11);
                    Console.WriteLine("Thead1: Số ngẫu nhiên: " + number);
                    Monitor.Pulse(lockObj);
                    Monitor.Wait(lockObj);
                    Thread.Sleep(2000);
                }                
            }            
        }

        static void Thread2()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lockObj)
                {
                    Monitor.Wait(lockObj);
                    int squaredNumber = number * number;
                    Console.WriteLine("Thread2: Bình phương của số: " + squaredNumber);
                    Monitor.Pulse(lockObj);
                    Thread.Sleep(1000);
                }                
            }
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Thread thread1 = new Thread(Thread1);
            Thread thread2 = new Thread(Thread2);

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();


            Console.WriteLine("Kết thúc chương trình.");
            Console.ReadLine();
        }        
    }
}
