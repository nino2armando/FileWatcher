using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FileWatcher.Interface;
using FileWatcher.Models;
using FileWatcher.Models.Mapping;

namespace FileWatcher.Context
{
    public partial class FileWatcherContext : DbContext, IDbContext
    {
        static FileWatcherContext()
        {
            Database.SetInitializer(new DropCreateDatabase());
            Database.SetInitializer<FileWatcherContext>(null);          
        }

        public FileWatcherContext()
            : base("Name=FileWatcherContext")
        {
        }

        public DbSet<XmlData> XmlDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new XmlDataMap());
        }
    }
}
