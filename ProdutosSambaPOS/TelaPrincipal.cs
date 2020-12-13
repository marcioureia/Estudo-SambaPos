using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProdutosSambaPOS
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
            AtualizarTabela();
        }

        public void AtualizarTabela()
        {
            dgvProdutos.DataSource = Banco.ProdutodoDataAccess.ObterProdutos();
        }

        private void Produtos_Load(object sender, EventArgs e)
        {

        }

        private void NovoAction(object sender, EventArgs e)
        {
            new CadastroProdutos(this).Show();
        }

        private void EditarActions(object sender, EventArgs e)
        {
            //pegando o registro que foi selecionado pelo Id; 
            int id = (int) dgvProdutos.SelectedRows[0].Cells[0].Value;
            //atribuindo a tela de cadastro para o botão editar chamando o formulario ;
            new CadastroProdutos(this, id).Show();
        }
    }
}
