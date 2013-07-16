using FileWatcher.Interface;
using FileWatcher.Service;
using Ninject.Modules;
using Ninject;

namespace FileWatcher.App_Start
{
    public class Bindings : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IFileReader>().To<FileReader>();
            Bind<IMainFileWatcher>().To<IMainFileWatcher>();
        }
    }
}
