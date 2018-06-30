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
    /// Lógica interna para WindowRelatorioVendas.xaml
    /// </summary>
    public partial class WindowRelatorioVendas : Window
    {
        ModelVenda ModelVenda = new ModelVenda();

        public WindowRelatorioVendas()
        {
            InitializeComponent();
            DataContext = this;
            carregarGridVendas();
        }

        private void carregarGridVendas()
        {
            //Database.SetInitializer<ModelSapato>(new DropCreateDatabaseIfModelChanges<ModelSapato>());
            //MessageBox.Show("ID: " + ModelVenda.vendas.Where(s => s.pessoaFisica != null).Single().id);
            listagem_vendas.ItemsSource = ModelVenda.vendas.ToList();
        }
    }
}
