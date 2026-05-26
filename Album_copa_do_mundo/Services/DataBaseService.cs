using PCLExt.FileStorage.Folders;
using SQLite;

namespace Album_copa_do_mundo.Services
{
    public class DatabaseService
    {
        public SQLiteConnection GetConnection()
        {
            var pasta = new LocalRootFolder();

            var arquivo =
                pasta.CreateFile("appcopa",
                    PCLExt.FileStorage.CreationCollisionOption.
                        OpenIfExists);

            return new SQLiteConnection(arquivo.Path);
        }
    }
}
