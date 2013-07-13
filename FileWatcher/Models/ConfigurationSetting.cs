namespace FileWatcher.Models
{
    public class ConfigurationSetting
    {
        public string Path { get; set; }
        public bool RaiseEvent { get; set; }
        public string Extention { get; set; }
    }
}
