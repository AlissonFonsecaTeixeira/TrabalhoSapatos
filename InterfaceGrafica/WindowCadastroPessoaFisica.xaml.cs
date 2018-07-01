using Entidades.Class;
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
using TrabalhoSapatos.Control;

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowCadastroPessoaFisica.xaml
    /// </summary>
    public partial class WindowCadastroPessoaFisica : Window
    {
        PessoaController pessoaController = new PessoaController();

        public WindowCadastroPessoaFisica(string cpfParameter)
        {
            InitializeComponent();
            cpf.Text = cpfParameter;
        }

        private void cadastrar_pessoa_fisica(object sender, RoutedEventArgs e)
        {
            PessoaFisica pessoaFisica = new PessoaFisica();
            pessoaFisica.cpf = cpf.Text;
            pessoaFisica.nome = nome.Text;
            pessoaFisica.nascimento = Convert.ToDateTime(nascimento.Text);
            pessoaController.cadastrar_pessoa_fisica(pessoaFisica);
            this.Close();
            MessageBox.Show("Cliente cadastrado com sucesso");
            WindowVenda proximaJanela = new WindowVenda(null, cpf.Text);
            proximaJanela.ShowDialog();
        }
    }
}
