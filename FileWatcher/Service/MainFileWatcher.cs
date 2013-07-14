using System;
using System.IO;
using FileWatcher.Context;
using FileWatcher.Interface;
using FileWatcher.Models;
using FileWatcher.UnitOfWork;

namespace FileWatcher.Service
{
    /// <summary>
    /// All the main file monitoring occurs here
    /// </summary>
    public class MainFileWatcher : IMainFileWatcher
    {
        private readonly FileSystemWatcher _fileSystemWatcher;
        private static IFileReader _fileReader;

        public MainFileWatcher(IFileReader fileReader)
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileReader = fileReader;
        }

        /// <summary>
        /// Sets the configuration.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void SetConfiguration(ConfigurationSetting setting)
        {
            _fileSystemWatcher.Path = setting.Path;
            _fileSystemWatcher.EnableRaisingEvents = setting.RaiseEvent;
            _fileSystemWatcher.Filter = setting.Extention;
            _fileSystemWatcher.Created += new FileSystemEventHandler(OnCreated);
            _fileSystemWatcher.Changed += new FileSystemEventHandler(OnChanged);
            _fileSystemWatcher.Deleted += new FileSystemEventHandler(OnDeleted);
            _fileSystemWatcher.Renamed += new RenamedEventHandler(OnRenamed);
        }

        /// <summary>
        /// Called when [changed].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            DisplayMessage(e);
        }

        /// <summary>
        /// Called when [created].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public static void OnCreated(object source, FileSystemEventArgs e)
        {
            DisplayMessage(e);

            // call the file reader
            var data = _fileReader.GetContent(e.FullPath);
            data.AppId = Guid.NewGuid();

            var unitOfWork = new UnitOfWork<FileWatcherContext>();
            var repo = unitOfWork.GetRepository<XmlData>();
            repo.Add(data);
            unitOfWork.Save();
        }

        /// <summary>
        /// Called when [deleted].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public static void OnDeleted(object source, FileSystemEventArgs e)
        {
            DisplayMessage(e);
        }

        /// <summary>
        /// Called when [renamed].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public static void OnRenamed(object source, FileSystemEventArgs e)
        {
            DisplayMessage(e);
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public static void OnError(object source, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays the message.
        /// </summary>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public static void DisplayMessage(FileSystemEventArgs e)
        {
            Console.WriteLine("File: {0} {1} at {2}", e.Name, e.ChangeType, DateTime.Now);
        }
    }
}
