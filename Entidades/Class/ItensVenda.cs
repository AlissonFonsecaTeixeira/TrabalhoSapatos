using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class ItensVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdItensVenda { get; set; }

        public int IdVenda { get; set; }
        [ForeignKey("IdVenda")]
        public Venda Venda { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnidade { get; set; }
        public int IdSapato { get; set; }
        [ForeignKey("IdSapato")]
        public Sapato Sapato { get; set; }
    }
}
