using PCLExt.FileStorage.Folders;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_copa_do_mundo.Services
{
    public class DataBaseService
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
