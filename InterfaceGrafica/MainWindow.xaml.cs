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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using TrabalhoSapatos.Control;

namespace InterfaceGrafica
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        SapatoController sapatoController = new SapatoController();

        public MainWindow()
        {
            InitializeComponent();
            sapatoController.ajustaEstoque();
        }

        private void tela_sapato (object sender, RoutedEventArgs e)
        {
            WindowSapato windowSapato = new WindowSapato();
            windowSapato.ShowDialog();
        }

        private void tela_venda(object sender, RoutedEventArgs e)
        {
            WindowVenda windowVenda = new WindowVenda();
            windowVenda.ShowDialog();
        }

        private void tela_relatorio_fisica(object sender, RoutedEventArgs e)
        {
            WindowRelatorioVendas windorRelatorioFIsica = new WindowRelatorioVendas();
            windorRelatorioFIsica.ShowDialog();
        }
    }
}
