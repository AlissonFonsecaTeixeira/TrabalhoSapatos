using Entidades.Class;
using Entidades.Control;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabalhoSapatos.Control
{
    public class VendaController
    {
        SapatoContext ctx = new SapatoContext();

        public IList<Venda> listarVendas()
        {
            return ctx.Vendas.ToList();
        }

        public IList<ItensVenda> relatorioVenda()
        {
            return ctx.itensVenda.Include("venda.pessoa").Include("venda").Include("sapato").ToList();
        }
    }
}
