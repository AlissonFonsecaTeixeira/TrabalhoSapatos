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
using TrabalhoSapatos.Class;
using TrabalhoSapatos.Control;

namespace InterfaceGrafica
{
    /// <summary>
    /// Lógica interna para WindowSapato.xaml
    /// </summary>
    public partial class WindowSapato : Window
    {
        SapatoController sapatoController = new SapatoController();
        int id = 0;
        //MessageBox.Show("Freight must be convertible to decimal.");
        public WindowSapato()
        {
            InitializeComponent();
            carregarGridSapato();
        }

        private void carregarGridSapato()
        {
            listagem_sapatos.ItemsSource = sapatoController.ObterSapatos();
        }

        private void apagar_sapato(object sender, RoutedEventArgs e)
        {
            int id = (listagem_sapatos.SelectedItem as Sapato).idSapato;
            var itemExcluido = sapatoController.buscarSapato(id);
            sapatoController.RemoverSapato(itemExcluido);
            carregarGridSapato();
        }

        private void editar_sapato(object sender, RoutedEventArgs e)
        {
            int id = (listagem_sapatos.SelectedItem as Sapato).idSapato;
            var itemCarregado = sapatoController.buscarSapato(id);
            this.id = itemCarregado.idSapato;
            nome.Text = itemCarregado.nome;
            cor.Text = itemCarregado.cor;
            material.Text = itemCarregado.material;
            preco.Text = itemCarregado.preco.ToString();
            quantidade.Text = itemCarregado.quantidadeDisponivel.ToString();
            numero.Text = itemCarregado.numero.ToString();
        }

        private void cadastrar_sapato(object sender, RoutedEventArgs e)
        {
            Sapato model = new Sapato()
            {
                nome = Convert.ToString(nome.Text),
                cadarco = Convert.ToBoolean(cadarco.IsChecked),
                cor = Convert.ToString(cor.Text),
                preco = Convert.ToDouble(preco.Text),
                material = Convert.ToString(material.Text),
                numero = Convert.ToInt32(numero.Text),
                quantidadeDisponivel = Convert.ToInt32(quantidade.Text),
                quantidadeEstoque = Convert.ToInt32(quantidade.Text),
            };
            sapatoController.SalvarSapato(model);
            carregarGridSapato();
        }
    }
}
