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
using BibliotecaModel;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowVendaProduto.xaml
    /// </summary>
    public partial class WindowVendaProduto : Window
    {
        ModelSapato modelSapato = new ModelSapato();
        ModelVenda ModelVenda = new ModelVenda();
        Venda venda = new Venda()
        {
            sapatos = new List<Sapato>(),
        };
        int id = 0;
        int QuantidadeMaxima;
        int Idsapato;

        public WindowVendaProduto(string nomeParameter = null, Sapato sapatoParameter = null, string quantidadeDesejadaProduto = null)
        {
            InitializeComponent();
            nome_cliente.Text = nomeParameter;
            quantidade_itens.Text = "0";
            //Database.SetInitializer<ModelVenda>(new DropCreateDatabaseIfModelChanges<ModelVenda>());
            if (sapatoParameter != null && quantidadeDesejadaProduto != null)
            {
                total.Text = Convert.ToString(sapatoParameter.preco * Convert.ToInt32(quantidadeDesejadaProduto));
                if (quantidade_itens.Text == "Quantidade de Itens")
                {
                    quantidade_itens.Text = "0";
                }
                quantidade_itens.Text = Convert.ToString(Convert.ToInt32(quantidadeDesejadaProduto));
            }
            //venda.sapatos = new IList<Sapato>();
            carregarGridSapato();
        }

        private void carregarGridSapato()
        {
            listagem_sapatos.ItemsSource = modelSapato.sapatos.ToList();
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            //WindowVendaQuantidade windowVenda = new WindowVendaQuantidade((listagem_sapatos.SelectedItem as Sapato), nome_cliente.Text);
            produto_carrinho.Text = (listagem_sapatos.SelectedItem as Sapato).nome;
            this.id = (listagem_sapatos.SelectedItem as Sapato).idSapato;
            this.QuantidadeMaxima = (listagem_sapatos.SelectedItem as Sapato).quantidadeDisponivel;
            this.Idsapato = (listagem_sapatos.SelectedItem as Sapato).idSapato;
            //this.Hide();
            //windowVenda.ShowDialog();
        }

        private void quantidade_carrinho_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void adicionar_carrinho(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Convert.ToString(Convert.ToInt32(quantidade_itens.Text) + Convert.ToInt32(quantidade_carrinho.Text)));
            quantidade_itens.Text = Convert.ToString(Convert.ToInt32(quantidade_itens.Text) + Convert.ToInt32(quantidade_carrinho.Text));
            Sapato sapato = modelSapato.sapatos.Where(s => s.idSapato == this.Idsapato).FirstOrDefault();
            total.Text = Convert.ToString(Convert.ToDouble(sapato.preco * Convert.ToInt32(quantidade_itens.Text)));
            sapato.quantidadeDisponivel = sapato.quantidadeEstoque - Convert.ToInt32(quantidade_carrinho.Text);
            //MessageBox.Show("Quantidade disponivel: " + sapato.quantidadeDisponivel);
            venda.sapatos.Add(sapato);
            modelSapato.SaveChanges();
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

        private void finalizar_carrinho(object sender, RoutedEventArgs e)
        {
            ModelPessoa modelPessoa = new ModelPessoa();
            var pessoa = modelPessoa.pessoas.OfType<PessoaFisica>().Where(p => p.nome == nome_cliente.Text).FirstOrDefault();
            if (pessoa == null)
            {
                var pessoa1 = modelPessoa.pessoas.OfType<PessoaJuridica>().Where(j => j.nome == nome_cliente.Text).FirstOrDefault();
                venda.idPessoa = pessoa1.idPessoa;
            }
            else
            {
                venda.pessoa.idPessoa = pessoa.idPessoa;
            }
            //venda.total = Convert.ToDecimal(total.Text);
            ModelVenda.vendas.Add(venda);
            ModelVenda.SaveChanges();
            atualizarEstoque();
            MessageBox.Show("Compra realizada com sucesso !");
            System.Threading.Thread.Sleep(2000);
            this.Hide();
        }

        private void atualizarEstoque()
        {
            ModelSapato modelSapato = new ModelSapato();
            foreach (Sapato spt in modelSapato.sapatos)
            {
                spt.quantidadeEstoque = spt.quantidadeDisponivel;
            }
            modelSapato.SaveChanges();
        }
    }
}
