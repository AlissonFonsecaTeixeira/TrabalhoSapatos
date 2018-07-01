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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVenda { get; set; }

        public IList<Sapato> Sapatos { get; set; }
        public decimal Total { get; set; }
        public int IdPessoa { get; set; }
        [ForeignKey("IdPessoa")]
        public Pessoa Pessoa { get; set; }
    }
}
