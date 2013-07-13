using System;

namespace FileWatcher.Models
{
    public class XmlData
    {
        public int Id { get; set; }
        public Nullable<Guid> AppId { get; set; }
        public string Heading { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
    }
}
