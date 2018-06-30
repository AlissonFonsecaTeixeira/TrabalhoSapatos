using Entidades.Class;
using Entidades.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabalhoSapatos.Control
{
    public class PessoaController
    {
        SapatoContext ctx = new SapatoContext();

        public void cadastrar_pessoa_fisica(PessoaFisica context)
        {
            PessoaFisica pessoaFisica = new PessoaFisica();
            pessoaFisica.cpf = context.cpf;
            pessoaFisica.nome = context.nome;
            pessoaFisica.nascimento = context.nascimento;
            ctx.Pessoas.Add(pessoaFisica);
            ctx.SaveChanges();
        }

        public PessoaFisica buscarPessoaFisica (string cpfParameter)
        {
            return ctx.Pessoas.OfType<PessoaFisica>().Where(c => c.cpf == cpfParameter).FirstOrDefault();
        }

        public PessoaFisica buscarPessoaFisicaPeloNome (string nomeParameter)
        {
            return ctx.Pessoas.OfType<PessoaFisica>().Where(c => c.nome == nomeParameter).FirstOrDefault();
        }

        public PessoaJuridica buscarPessoaJuridica (string cnpjParameter)
        {
            return ctx.Pessoas.OfType<PessoaJuridica>().Where(c => c.cnpj == cnpjParameter).FirstOrDefault();
        }

        public PessoaJuridica buscarPessoaJuridicaPeloNome (string nomeParameter)
        {
            return ctx.Pessoas.OfType<PessoaJuridica>().Where(c => c.nome == nomeParameter).FirstOrDefault();
        }

        public void cadastrar_pessoa_juridica(PessoaJuridica context)
        {
            PessoaJuridica pessoaFisica = new PessoaJuridica();
            pessoaFisica.cnpj = context.cnpj;
            pessoaFisica.nome = context.nome;
            pessoaFisica.nascimento = context.nascimento;
            ctx.Pessoas.Add(pessoaFisica);
            ctx.SaveChanges();
        }
    }
}
