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
            bool? obtidas,
            bool? desejadas)
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

            // obtidas == false => não busca figurinhas obtidas
            // obtidas == true => busca somente figurinhas obtidas
            // obtidas == null => não filtra por obtidas (obtidas e não obtidas)
            queryFigurinhas = queryFigurinhas.Where(x => x.Obtido == obtidas || obtidas == null);

            // desejadas == false => não busca figurinhas desejadas
            // desejadas == true => busca somente figurinhas desejadas
            // desejadas == null => não filtra por desejadas (desejadas e não desejadas)
            queryFigurinhas = queryFigurinhas.Where(x => x.Desejado == desejadas || desejadas == null);

            // No final, retornamos uma lista com os resultados
            return queryFigurinhas.ToList();
        }
    }
}
