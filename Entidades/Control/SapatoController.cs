using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Entidades.Class;

namespace Entidades.Control
{
    public class SapatoController
    {
        SapatoContext ctx = new SapatoContext();

        public SapatoController()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SapatoContext>());
            if (ObterSapatos().Count == 0)
            {
                this.SalvarSapato(new Sapato()
                {
                    nome = "Havaianas",
                    cadarco = false,
                    cor = "Amarela",
                    numero = 43,
                    material = "Leve",
                    preco = 22.90,
                    quantidadeDisponivel = 5,
                    quantidadeEstoque = 5,
                });
                this.SalvarSapato(new Sapato()
                {
                    nome = "AirMax",
                    cadarco = true,
                    cor = "Preto",
                    numero = 42,
                    material = "Pesado",
                    preco = 322.90,
                    quantidadeDisponivel = 9,
                    quantidadeEstoque = 9,
                });
                this.SalvarSapato(new Sapato()
                {
                    nome = "KiChute",
                    cadarco = true,
                    cor = "Preto",
                    numero = 41,
                    material = "Pesado",
                    preco = 131.70,
                    quantidadeDisponivel = 4,
                    quantidadeEstoque = 4,
                });
            }
        }

        public Sapato buscarSapato(int id)
        {
            return ctx.Sapatos.Where(s => s.idSapato == id).FirstOrDefault();
        }

        public IList<Sapato> ObterSapatos()
        {
            return ctx.Sapatos.ToList();
        }

        public void SalvarSapato(Sapato sapato)
        {
            try
            {
                if (sapato.idSapato == 0)
                {
                    ctx.Sapatos.Add(sapato);
                }
                else
                {
                    var temp = ctx.Sapatos.SingleOrDefault(c => c.idSapato == sapato.idSapato);
                    temp.idSapato = sapato.idSapato;
                    temp.nome = sapato.nome;
                    temp.material = sapato.material;
                    temp.cor = sapato.cor;
                    temp.cadarco = sapato.cadarco;
                    temp.numero = sapato.numero;
                    temp.preco = sapato.preco;
                    temp.quantidadeDisponivel = sapato.quantidadeDisponivel;
                    temp.quantidadeEstoque = sapato.quantidadeEstoque;
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Util.HandleSQLDBException(ex);
            }
        }

        public void RemoverSapato(Sapato current)
        {
            if (current.idSapato != 0)
            {
                var resultado = from s in ctx.Sapatos
                                where s.idSapato == current.idSapato
                                select s;
                ctx.Sapatos.Remove(resultado.First());
                ctx.SaveChanges();
            }
        }

        public void removeDoEstoque()
        {
            foreach (Sapato spt in ctx.Sapatos)
            {
                spt.quantidadeEstoque = spt.quantidadeDisponivel;
            }
            ctx.SaveChanges();
        }

        public void ajustaEstoque()
        {
            foreach (Sapato spt in ctx.Sapatos)
            {
                spt.quantidadeDisponivel = spt.quantidadeEstoque;
            }
            ctx.SaveChanges();
        }

        public void relatorioEstoque(String caminho)
        {
            //Criar um Workbook. Um arquvio excel.
            var workbook = new XLWorkbook();
            int ListaSapatosLinhaInicio = 2;
            //Um arquivo excel pode conter várias planilhas. 
            var worksheet = workbook.Worksheets.Add("Sapatos");
            foreach (Sapato s in ctx.Sapatos.ToList())
            {
                worksheet.Cell("A1").Value = "Nome do Sapato";
                worksheet.Cell("B1").Value = "Numero";
                worksheet.Cell("C1").Value = "Material";
                worksheet.Cell("F1").Value = "Cor";
                worksheet.Cell("E1").Value = "Cadarco";
                worksheet.Cell("D1").Value = "Preco";
                var columnNome = worksheet.Column("A");
                var columnNumero = worksheet.Column("B");
                var columMaterial = worksheet.Column("C");
                var columPreco = worksheet.Column("D");
                var columCadarco = worksheet.Column("E");
                var columCor = worksheet.Column("F");
                worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.Gray;
                worksheet.Row(1).Style.Font.Bold = true;
                columnNome.Cell(ListaSapatosLinhaInicio).Value = s.nome;
                columnNumero.Cell(ListaSapatosLinhaInicio).Value = s.numero;
                columMaterial.Cell(ListaSapatosLinhaInicio).Value = s.material;
                columCor.Cell(ListaSapatosLinhaInicio).Value = s.cor;
                columCadarco.Cell(ListaSapatosLinhaInicio).Value = s.cadarco;
                columPreco.Cell(ListaSapatosLinhaInicio).Value = s.preco;
                ListaSapatosLinhaInicio++;
            }
            //Excel pode utilizar a referência A1 [A1,B2...] ou R1C1
            //Se quiser ler mais sobre acesse o link: https://www.reddit.com/r/excel/comments/6tpgk3/reference_style_r1c1_vs_a1/
            workbook.ReferenceStyle = XLReferenceStyle.A1;
            //Calcular automaticamente os valores das fórmulas.
            workbook.CalculateMode = ClosedXML.Excel.XLCalculateMode.Auto;
            //Persistir o arquivo.
            workbook.SaveAs(caminho, true, evaluateFormulae: true);
        }
    }
}
