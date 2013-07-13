using FileWatcher.Models;

namespace FileWatcher.Interface
{

    /// <summary>
    /// Interface for MainFileWatcher Class
    /// </summary>
    public interface IMainFileWatcher 
    {
        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="setting">The setting.</param>
        void SetConfiguration(ConfigurationSetting setting);
    }
}
