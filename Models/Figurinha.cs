using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace App_Copa.Models
{
    public class Figurinha
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
