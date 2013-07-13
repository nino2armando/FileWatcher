using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace FileWatcher.Context
{
    public class DropCreateDatabase : IDatabaseInitializer<FileWatcherContext>
    {
        private readonly IDatabaseInitializer<FileWatcherContext> _initializer;

        public DropCreateDatabase()
        {
            InitializeDatabase(new FileWatcherContext());
        }

        public void InitializeDatabase(FileWatcherContext context)
        {
            bool dbExist;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExist = context.Database.Exists();
            }
            if (dbExist)
            {
                // drop Database
                context.Database.ExecuteSqlCommand("ALTER DATABASE FileWatcher SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                context.Database.Delete();

                // create all database
                ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();

                context.SaveChanges();

            }
            else
            {
                // create all database
                ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
            }
        }
    }
}
