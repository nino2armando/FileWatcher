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
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

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

            XmlData result = null;
            try
            {
                var doc = XDocument.Load(path);

                // you need to make sure to validate your schema againts xsd
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
                throw new XmlException(e.Message);
            }

            if (result == null)
            {
                throw new NullReferenceException("result");
            }

            return result;
        }
    }
}
