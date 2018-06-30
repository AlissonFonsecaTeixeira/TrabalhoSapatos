using Entidades.Class;
using Entidades.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoSapatos.Control
{
    public class VendaController
    {
        SapatoContext ctx = new SapatoContext();

        public void cadastrarVenda(Venda vendaParameter)
        {
            foreach (Sapato s in vendaParameter.sapatos)
            {
                ctx.Entry(s).State = System.Data.Entity.EntityState.Unchanged;
                ItensVenda itensVenda = new ItensVenda();
                itensVenda.idItensVenda = vendaParameter.idVenda;
                itensVenda.idSapato = s.idSapato;
                itensVenda.quantidade = s.quantidadeEstoque - s.quantidadeDisponivel;
                ctx.itensVenda.Add(itensVenda);
            }
            ctx.Vendas.Add(vendaParameter);
            ctx.SaveChanges();
        }

        public IList<Venda> listarVendas()
        {
            return ctx.Vendas.ToList();
        }

        public IList<ItensVenda> relatorioVenda()
        {
            IList<ItensVenda> itensVendas = ctx.itensVenda.Include("venda.pessoa").Include("venda").Include("sapato").ToList();

            return itensVendas;
        }
    }
}
