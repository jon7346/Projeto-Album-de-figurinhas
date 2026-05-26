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

        public List<Figurinha> GetByFilters(
            string nomeJogador,
            bool somenteObtidos,
            bool somenteDesejadas)
        {
            // Primeiro "criamos" a consulta sem filtros
            var queryFigurinhas = _connection.Table<Figurinha>();

            // Depois vamos aplicando os filtros conforme os parâmetros
            if (!string.IsNullOrWhiteSpace(nomeJogador))
            {
                // Usamos ToLower para que ele não considere diferenças
                // entre maiúsculas e minúsculas
                queryFigurinhas = queryFigurinhas.
                    Where(x => x.NomeJogador.ToLower().Contains(nomeJogador.ToLower()));
            }

            // Sempre filtra pelo valor true ou false de "obtido" ou "desejado"
            queryFigurinhas = queryFigurinhas.Where(x => x.Obtido == somenteObtidos);
            queryFigurinhas = queryFigurinhas.Where(x => x.Desejado == somenteDesejadas);

            // No final, retornamos uma lista com os resultados
            return queryFigurinhas.ToList();
        }
    }
}
