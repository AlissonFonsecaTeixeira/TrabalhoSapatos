using BibliotecaModel;
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

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowVendaQuantidade.xaml
    /// </summary>
    public partial class WindowVendaQuantidade : Window
    {
        int quantidadeMaxima = 0;
        string nomeClienteParameter;
        Sapato sapatoParameter;

        public WindowVendaQuantidade(Sapato sapatoSelecionado, string nomeCliente)
        {
            InitializeComponent();
            produto_nome.Text = sapatoSelecionado.nome;
            this.quantidadeMaxima = sapatoSelecionado.quantidadeDisponivel;
            this.nomeClienteParameter = nomeCliente;
            this.sapatoParameter = sapatoSelecionado;
        }

        private void adicionar_carrinho (object sender, RoutedEventArgs e)
        {
            WindowVendaProduto windowProduto = new WindowVendaProduto(this.nomeClienteParameter, sapatoParameter, quantidade_produto.Text);
            sapatoParameter.quantidadeDisponivel = sapatoParameter.quantidadeDisponivel - Convert.ToInt32(quantidade_produto.Text);
            ModelSapato modelSapato = new ModelSapato();
            modelSapato.SaveChanges();
            this.Hide();
            windowProduto.ShowDialog();

        }

        private void quantidade_produto_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number;

            if (int.TryParse(quantidade_produto.Text, out number))
            {
                if (number > this.quantidadeMaxima)
                {
                    quantidade_produto.Text = Convert.ToString(this.quantidadeMaxima);
                    MessageBox.Show("Não há essa quantidade pra compra no estoque, máximo permitido: " + this.quantidadeMaxima);
                }
            }
            else
            {
                quantidade_produto.Text = "0";
                MessageBox.Show("Favor digitar um número válido !");
            }
        }
    }
}
