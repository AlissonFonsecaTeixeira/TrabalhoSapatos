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

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowCadastroPessoaJuridica.xaml
    /// </summary>
    public partial class WindowCadastroPessoaJuridica : Window
    {
        ModelPessoaJuridica modelPessoaJuridica = new ModelPessoaJuridica();

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
            modelPessoaJuridica.pessoasJuridicas.Add(pessoaJuridica);
            modelPessoaJuridica.SaveChanges();
            this.Hide();
            MessageBox.Show("Cliente cadastrado com sucesso");
            WindowVenda proximaJanela = new WindowVenda(cnpj.Text);
            proximaJanela.ShowDialog();
        }
    }
}
