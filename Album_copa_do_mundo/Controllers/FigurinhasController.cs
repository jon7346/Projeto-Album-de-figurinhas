using Album_copa_do_mundo.Models;
using Album_copa_do_mundo.Services;
using SQLite;
namespace Album_copa_do_mundo.Controllers
{
    public class FigurinhaController
    {
        DatabaseService _database;
        SQLiteConnection _connection;

        public FigurinhaController()
        {
            _database = new DatabaseService();

            _connection = _database.GetConnection();
            _connection.CreateTable<Figurinha>();
        }

        public bool Insert(Figurinha value)
        {
            return _connection.Insert(value) > 0;
        }

        public bool Update(Figurinha value)
        {
            return _connection.Update(value) > 0;
        }

        public bool Delete(Figurinha value)
        {
            return _connection.Delete(value) > 0;
        }

        public Figurinha GetById(int value)
        {
            return _connection.Find<Figurinha>(value);
        }

        public List<Figurinha> GetByPlayerName(string value)
        {
            return _connection.Table<Figurinha>().
                    Where(x => x.NomeJogador.Contains(value)).ToList();
        }

        public List<Figurinha> GetAll()
        {
            return _connection.Table<Figurinha>().ToList();
        }
    }
}
