using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Class
{
    public class PessoaJuridica : Pessoa
    {
        public string razao_social { get; set; }
        public string endereco { get; set; }
        public string cnpj { get; set; }
    }
}
