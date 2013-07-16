using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using FileWatcher.Interface;
using FileWatcher.Models;

namespace FileWatcher.Service
{
    /// <summary>
    /// Extract the xml and converts it to a model
    /// </summary>
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Tries the get file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">path</exception>
        public bool TryGetFile(string path)
        {
            // validate the input
            this.PreCheckInput(path, "path");

            if (File.Exists(path))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="innerXml">The inner XML.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public XmlData GetContent(string path)
        {
            this.PreCheckInput(path, "path");

            XmlData result = null;
            try
            {
                var doc = XDocument.Load(path);

                //todo: you need to make sure to validate your schema againts xsd
                // http://msdn.microsoft.com/en-us/library/thydszwy.aspx

                result = (from a in doc.Descendants("note")
                              select new XmlData()
                              {
                                  To = a.Element("to").Value,
                                  From = a.Element("from").Value,
                                  Heading = a.Element("heading").Value,
                                  Body = a.Element("body").Value
                              }).FirstOrDefault();
            }
            catch (XmlException e)
            {
                
                Console.WriteLine(e.Message);

                //todo: 
                // you might want to handle this exception more gracefully
                // for application flow
                throw new XmlException(e.Message);
            }

            if (result == null)
            {
                //todo: 
                // you might want to handle this exception more gracefully
                // for application flow
                throw new NullReferenceException("result");
            }

            return result;
        }

        /// <summary>
        /// Pres the check input.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ArgumentNullException PreCheckInput(string param, string paramName)
        {
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentNullException(paramName);
            }

            return null;
        }
    }
}
