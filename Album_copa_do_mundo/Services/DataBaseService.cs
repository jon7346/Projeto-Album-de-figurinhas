using PCLExt.FileStorage.Folders;
using SQLite;

namespace Album_copa_do_mundo.Services
{
    public class DatabaseService
    {
        public SQLiteConnection GetConnection()
        {
            var pastaLocal = new LocalRootFolder();

            // Cria o arquivo do banco de dados se não existir
            // e obtém o caminho do arquivo
            var arquivoBanco =
                pastaLocal.CreateFile("appcopa",
                    PCLExt.FileStorage.CreationCollisionOption.
                        OpenIfExists);

            return new SQLiteConnection(arquivoBanco.Path);
        }
    }
}
