using SQLite;

namespace Album_copa_do_mundo.Models
{
    public class Figurinha
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DirImagem { get; set; }
        public string NomeJogador { get; set; }
        public string Selecao { get; set; }
        public string Tipo { get; set; }
        public bool Obtido { get; set; }
        public bool Desejado { get; set; }

    }
}
