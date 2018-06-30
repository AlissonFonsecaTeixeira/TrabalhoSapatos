using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class Venda
    {
        [Key]
        public int idVenda { get; set; }
        public IList<Sapato> sapatos { get; set; }
        public decimal total { get; set; }
        public int idPessoa { get; set; }
        [ForeignKey("idPessoa")]
        public Pessoa pessoa { get; set; }
    }
}
