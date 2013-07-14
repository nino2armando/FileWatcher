using System;
using FileWatcher.Models;

namespace FileWatcher.Interface
{
    /// <summary>
    /// Converts xml content into business model
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Tries the get file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        bool TryGetFile(string path);

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="innerXml">The inner XML.</param>
        /// <returns></returns>
        XmlData GetContent(string path);


        /// <summary>
        /// Pres the check input.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <returns></returns>
        ArgumentNullException PreCheckInput(string param, string paramName);

    }
}
