﻿using System;
using System.Linq;
using Nancy.Hosting.Self;

namespace Publicus
{
    public static class MainClass
    {
        public static void Main(string[] args)
        {
            using (var db = Global.CreateDatabase())
            {
                Model.Install(db);
                var seeder = new Seeder(db);
                seeder.MinimalSeed();
            }

            var uri = "http://localhost:8889";
            Global.Log.Notice("Starting Nancy on " + uri);

            // initialize an instance of NancyHost
            var host = new NancyHost(new Uri(uri));
            host.Start();  // start hosting

            Global.Log.Notice("Application started");
            var runner = new TaskRunner();

            while (true)
            {
                runner.Run();
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
