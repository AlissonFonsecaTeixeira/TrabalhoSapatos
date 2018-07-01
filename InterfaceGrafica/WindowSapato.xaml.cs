using System;
using System.Collections.Generic;
using System.IO;
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
using Entidades.Class;
using Entidades.Control;

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


        private void exportar_Relatorio(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Relatorio"; // Nome padrão
            dlg.DefaultExt = ".xlsx"; // Extensão do arquivo
            dlg.Filter = "Excel (.xlsx)|*.xlsx"; // Filtros
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true){
                sapatoController.relatorioEstoque(dlg.FileName);
            }
        }

        private void trocar_foto (object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Imagem"; // Nome padrão
            dlg.Filter = "Imagens (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            Nullable<bool> result = dlg.ShowDialog();

            // Somente irá salvar se o usuário selecionar um arquivo.
            if (result == true)
            {
                var uri = new Uri(dlg.FileName);
                var imagemFile = File.Open(dlg.FileName, FileMode.Open);
                var sapato = sapatoController.buscarSapato((listagem_sapatos.SelectedItem as Sapato).idSapato);
                sapato.Foto = new byte[imagemFile.Length];
                imagemFile.Read(sapato.Foto,
                    0, (int)imagemFile.Length);
                sapatoController.SalvarSapato(sapato);
                MessageBox.Show("Foto alterada com sucesso !");
            }
        }

        private void cadastrar_sapato(object sender, RoutedEventArgs e)
        {
            if (this.id != 0)
            {
                Sapato model = sapatoController.buscarSapato(this.id);
                model.nome = Convert.ToString(nome.Text);
                model.cadarco = Convert.ToBoolean(cadarco.IsChecked);
                model.cor = Convert.ToString(cor.Text);
                model.preco = Convert.ToDouble(preco.Text);
                model.material = Convert.ToString(material.Text);
                model.numero = Convert.ToInt32(numero.Text);
                model.quantidadeDisponivel = Convert.ToInt32(quantidade.Text);
                model.quantidadeEstoque = Convert.ToInt32(quantidade.Text);
                sapatoController.SalvarSapato(model);
            }
            else
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
            }
            carregarGridSapato();
        }
    }
}
