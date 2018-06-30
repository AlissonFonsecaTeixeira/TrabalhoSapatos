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

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowVenda.xaml
    /// </summary>
    public partial class WindowVenda : Window
    {
        ModelPessoaFisica modelPessoaFisica = new ModelPessoaFisica();
        ModelPessoaJuridica modelPessoaJuridica = new ModelPessoaJuridica();
        ModelPessoa modelPessoa = new ModelPessoa();

        public WindowVenda(string cnpjParameter = null, string cpfParameter = null)
        {
            InitializeComponent();
            if (cnpjParameter != null)
            {
                cnpj.Text = cnpjParameter;
            }
            if (cpfParameter != null)
            {
                cpf.Text = cpfParameter;
            }
        }

        private void cadastro_pessoa_cpf (object sender, RoutedEventArgs e)
        {
            if (cpf.Text != null)
            {
                var pessoa = modelPessoa.pessoas.OfType<PessoaFisica>().Where(f => f.cpf == cpf.Text).FirstOrDefault();
                if (pessoa != null)
                {
                    WindowVendaProduto windowProduto = new WindowVendaProduto(pessoa.nome);
                    this.Hide();
                    windowProduto.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Este cliente não existe no sistema, favor cadastra-lo !");
                    WindowCadastroPessoaFisica windowFisica = new WindowCadastroPessoaFisica(cpf.Text);
                    this.Hide();
                    windowFisica.ShowDialog();
                }
                    // var itemExcluido = modelSapato.sapatos.Where(s => s.id == id).Single();
            }
            else
            {
                MessageBox.Show("Por favor, insira um CPF");
            }
        }

        private void cadastro_pessoa_cnpj(object sender, RoutedEventArgs e)
        {
            if (cnpj.Text != null)
            {
                var pessoa = modelPessoa.pessoas.OfType<PessoaJuridica>().Where(j => j.cnpj == cnpj.Text).FirstOrDefault();
                if (pessoa != null)
                {
                    WindowVendaProduto windowProduto = new WindowVendaProduto(pessoa.nome);
                    this.Hide();
                    windowProduto.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Este cliente não existe no sistema, favor cadastra-lo !");
                    WindowCadastroPessoaJuridica windowJuridica = new WindowCadastroPessoaJuridica(cnpj.Text);
                    this.Hide();
                    windowJuridica.ShowDialog();
                }
                // var itemExcluido = modelSapato.sapatos.Where(s => s.id == id).Single();
            }
            else
            {
                MessageBox.Show("Por favor, insira um CNPJ");
            }
        }
    }
}
