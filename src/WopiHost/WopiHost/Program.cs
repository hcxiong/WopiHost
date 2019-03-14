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

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(cm.config));
            Console.WriteLine("");
            Console.WriteLine($"WOPISrc={cm.config.url}wopi/files/root子目录路径");
            Console.WriteLine("");
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();

            svr.Stop();
        }
    }
}