using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class Sapato
    {
        [Key]
        public int idSapato { get; set; }

        public string nome { get; set; }
        public string cor { get; set; }
        public double preco { get; set; }
        public string material { get; set; }
        public Boolean cadarco { get; set; }
        public int quantidadeDisponivel { get; set; }
        public int quantidadeEstoque { get; set; }
        public int numero { get; set; }
    }
}
