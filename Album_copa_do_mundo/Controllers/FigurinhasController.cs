using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Album_copa_do_mundo.Services;
using Album_copa_do_mundo.Models;
namespace Album_copa_do_mundo.Controllers
{
    public class FigurinhaController
    {
        DataBaseService _database;
        SQLiteConnection _connection;

        public FigurinhaController()
        {
            _database = new DataBaseService();

            _connection = _database.GetConnection();

            _connection.CreateTable<FigurinhasModel>();
        }

        public bool Insert(FigurinhasModel value)
        {
            return _connection.Insert(value) > 0;
        }

        public bool Update(FigurinhasModel value)
        {
            return _connection.Update(value) > 0;
        }

        public bool Delete(FigurinhasModel value)
        {
            return _connection.Delete(value) > 0;
        }

        public FigurinhasModel GetById(int value)
        {
            return _connection.Find<FigurinhasModel>(value);
        }

        public List<FigurinhasModel> GetByName(string value)
        {
            return _connection.Table<FigurinhasModel>().
                    Where(x => x.NomeJogador.Contains(value)).ToList();
        }

        public List<FigurinhasModel> GetAll()
        {
            //SELECT * FROM Pessoa
            return _connection.Table<FigurinhasModel>().ToList();
        }
    }
}
