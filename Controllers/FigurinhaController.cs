using App_Copa.Models;
using App_Copa.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Copa.Controllers
{
    public class FigurinhaController
    {
        DataBaseService _database;
        SQLiteConnection _connection;

        public FigurinhaController()
        {
            _database = new DataBaseService();

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

        public List<Figurinha> GetByName(string value)
        {
            return _connection.Table<Figurinha>().
                    Where(x => x.NomeJogador.Contains(value)).ToList();
        }

        public List<Figurinha> GetAll()
        {
            //SELECT * FROM Pessoa
            return _connection.Table<Figurinha>().ToList();
        }
    }
}
