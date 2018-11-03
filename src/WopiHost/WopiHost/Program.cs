using System;

namespace WopiHost
{
    class Program
    {
        static void Main()
        {
            var cm = new ConfigManager();

            CobaltServer svr = new CobaltServer(cm.config);
            svr.Start();

            Console.WriteLine(cm.config.url);
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey();

            svr.Stop();
        }
    }
}
