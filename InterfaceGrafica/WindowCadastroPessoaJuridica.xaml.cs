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
    /// Lógica interna para WindowCadastroPessoaJuridica.xaml
    /// </summary>
    public partial class WindowCadastroPessoaJuridica : Window
    {
        PessoaController pessoaController = new PessoaController();

        public WindowCadastroPessoaJuridica(string textBoxCnpj)
        {
            InitializeComponent();
            cnpj.Text = textBoxCnpj;
        }

        private void cadastrar_pessoa_juridica(object sender, RoutedEventArgs e)
        {
            PessoaJuridica pessoaJuridica = new PessoaJuridica();
            pessoaJuridica.cnpj = cnpj.Text;
            pessoaJuridica.nome = nome.Text;
            pessoaJuridica.razao_social = razao_social.Text;
            pessoaJuridica.endereco = endereco.Text;
            pessoaJuridica.nascimento = Convert.ToDateTime(nascimento.Text);
            pessoaController.cadastrar_pessoa_juridica(pessoaJuridica);
            this.Close();
            MessageBox.Show("Cliente cadastrado com sucesso");
            WindowVenda proximaJanela = new WindowVenda(cnpj.Text);
            proximaJanela.ShowDialog();
        }
    }
}
