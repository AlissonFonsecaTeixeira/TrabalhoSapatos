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
    /// Lógica interna para WindowRelatorioVendas.xaml
    /// </summary>
    public partial class WindowRelatorioVendas : Window
    {
        VendaController vendaController = new VendaController();

        public WindowRelatorioVendas()
        {
            InitializeComponent();
            DataContext = this;
            carregarGridVendas();
        }

        private void carregarGridVendas()
        {
            listagem_vendas.ItemsSource = vendaController.relatorioVenda();
        }
    }
}
