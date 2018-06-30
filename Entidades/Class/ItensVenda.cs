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
        public int idItensVenda { get; set; }
        public int idVenda { get; set; }
        [ForeignKey("idVenda")]
        public Venda venda { get; set; }
        public int quantidade { get; set; }
        public int idSapato { get; set; }
        [ForeignKey("idSapato")]
        public Sapato sapato { get; set; }
    }
}
