using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Text.RegularExpressions;
using Entidades.Control;
using Entidades.Class;
using TrabalhoSapatos.Control;

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowVendaProduto.xaml
    /// </summary>
    public partial class WindowVendaProduto : Window
    {
        SapatoController sapatoController = new SapatoController();
        PessoaController pessoaController = new PessoaController();
        VendaController vendaController = new VendaController();
        SapatoContext ctx = new SapatoContext();
        Venda venda = new Venda()
        {
            Sapatos = new List<Sapato>(),
        };
        int id = 0;
        int QuantidadeMaxima;

        public WindowVendaProduto(string nomeParameter = null)
        {
            InitializeComponent();
            nome_cliente.Text = nomeParameter;
            carregarGridSapato();
        }

        private void carregarGridSapato()
        {
            listagem_sapatos.ItemsSource = sapatoController.ObterSapatos();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            produto_carrinho.Text = (listagem_sapatos.SelectedItem as Sapato).nome;
            this.id = (listagem_sapatos.SelectedItem as Sapato).idSapato;
            this.QuantidadeMaxima = (listagem_sapatos.SelectedItem as Sapato).quantidadeDisponivel;
        }

        private void quantidade_carrinho_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        /**
         * Função para adicionar um sapato ao carrinho 
         **/
        private void adicionar_carrinho(object sender, RoutedEventArgs e)
        {
            quantidade_itens.Text = Convert.ToString(Convert.ToInt32(quantidade_itens.Text) + Convert.ToInt32(quantidade_carrinho.Text));
            Sapato sapato = sapatoController.buscarSapato(this.id);
            total.Text = Convert.ToString(Convert.ToDouble(sapato.preco * Convert.ToInt32(quantidade_carrinho.Text)) + Convert.ToDouble(total.Text));
            sapato.quantidadeDisponivel = sapato.quantidadeEstoque - Convert.ToInt32(quantidade_carrinho.Text);
            venda.Sapatos.Add(sapato);
            carregarGridSapato();
            produto_carrinho.Text = "";
            quantidade_carrinho.Text = "";
        }

        private void quantidade_carrinho_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox a = (TextBox) sender;
            int number;
            if (!int.TryParse(a.Text, out number))
            {
                quantidade_carrinho.Text = "";
                return;
            }
            if (Convert.ToInt32(a.Text) > QuantidadeMaxima)
            {
                quantidade_carrinho.Text = "";
                MessageBox.Show("Não há essa quantidade pra compra no estoque, máximo permitido: " + QuantidadeMaxima);
            }
        }

        /**
         * Função que finaliza a compra do cliente 
         **/
        private void finalizar_carrinho(object sender, RoutedEventArgs e)
        {

            var pessoa = pessoaController.buscarPessoaFisicaPeloNome(nome_cliente.Text);
            if (pessoa == null)
            {
                var pessoa1 = pessoaController.buscarPessoaJuridicaPeloNome(nome_cliente.Text);
                venda.IdPessoa = pessoa1.idPessoa;
            }
            else
            {
                venda.IdPessoa = pessoa.idPessoa;
            }
            venda.Total = Convert.ToDecimal(total.Text);
            //adiciona a lista de sapatos para uma variavel e depois zera a variavel, para não precisar fazer um foreach com EntityState.Unchanged
            IList<Sapato> listaSapatos = venda.Sapatos;
            venda.Sapatos = null;
            ctx.Vendas.Add(venda);
            ctx.SaveChanges();
            cadastrarItensDaVenda(listaSapatos, venda.IdVenda);
            //Chamada na função para atualizar o estoque, removendo da quantidade disponivel os que foram comprados
            sapatoController.removeDoEstoque();
            MessageBox.Show("Compra realizada com sucesso !");
            System.Threading.Thread.Sleep(2000);
            this.Close();
        }

        /**
         * Função que recebe uma lista de sapatos e a qual venda eles pertencem, aonde realiza os seus registros na tabela ItensVenda
         * */
        private void cadastrarItensDaVenda(IList<Sapato> sapatos, int idVendaParameter)
        {
            foreach (Sapato s in sapatos)
            {
                ctx.Entry(s).State = EntityState.Unchanged;
                ItensVenda itensVenda = new ItensVenda();
                itensVenda.IdVenda = idVendaParameter;
                itensVenda.IdSapato = s.idSapato;
                itensVenda.ValorUnidade = s.preco;
                itensVenda.Quantidade = s.quantidadeEstoque - s.quantidadeDisponivel;
                ctx.itensVenda.Add(itensVenda);
            }
            ctx.SaveChanges();
        }

    }
}
