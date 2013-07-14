using System;
using System.Reflection;
using FileWatcher.Interface;
using FileWatcher.Models;
using FileWatcher.Service;
using Ninject;

namespace FileWatcher
{
    class Program
    { 

        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            IFileReader fileReader = kernel.Get<IFileReader>();

            // set up your basic configurations here
            var settings = new ConfigurationSetting
                {
                    Path = @"C:\Users\Nino\Desktop\test", // set this to the folder that you would like to monitor
                    RaiseEvent = true,
                    Extention = "*.xml" // extention of a file the needs to be monitored
                };

            // call the service
            IMainFileWatcher watcher = new MainFileWatcher(fileReader);

            // pass the config
            watcher.SetConfiguration(settings);

            Console.Read();
        }
    }
}
