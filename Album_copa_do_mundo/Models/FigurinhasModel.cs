using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite; 
using System.ComponentModel;    

namespace Album_copa_do_mundo.Models
{
    public class FigurinhasModel 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FotoDir { get; set; }
        public string NomeJogador { get; set; }
        public string Selecao { get; set; }
        public string Tipo { get; set; }
        public bool Obtido { get; set; }
        public bool Desejado { get; set; }

    }
}
