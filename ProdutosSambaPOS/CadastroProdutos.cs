using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProdutosSambaPOS.Modelo;
using System.ComponentModel.DataAnnotations;
using ProdutosSambaPOS.Banco;

namespace ProdutosSambaPOS
{
    public partial class CadastroProdutos : Form
    {
        private TelaPrincipal produtoTelaPricipal;
        private Produtos prod;
        //CONSTRUTORES

        public CadastroProdutos(TelaPrincipal produtoTela )
        {
            //armazena a tela pruincipal 
            produtoTelaPricipal = produtoTela;
            InitializeComponent();
        }

        public CadastroProdutos(TelaPrincipal produtoTela, int Id)
        {
            produtoTelaPricipal = produtoTela;
            
            InitializeComponent();
            prod = ProdutodoDataAccess.PegarProduto(Id);
            ProdutoParaTela(prod);




        }

        // FIM - CONSTRUTORES 
        //METODOS
        private  void ProdutoParaTela(Produtos produtos)
        {
            

            txtNome.Text = produtos.MenuItems.Name;
           txtGrupo.Text = produtos.MenuItems.GroupCode;
            txtTag.Text = produtos.MenuItems.Tag;


            txtPorcao.Text= produtos.MenuItemPortions.Name;
            txtMultiplos.Text = produtos.MenuItemPortions.Multiplier.ToString();


            txtPreco.Text = produtos.MenuItemPrices.Price.ToString();



            txtUnd.Text = produtos.Fiscal.unidade;
            txtEAN.Text = produtos.Fiscal.ean;

            txtNCM.Text = produtos.Fiscal.ncm;
            txtCEST.Text = produtos.Fiscal.ncm_CEST;
            txtCST.Text= produtos.Fiscal.cst;
            txtOrigem.Text = produtos.Fiscal.tipo;
            txtIPI.Text = produtos.Fiscal.ipi;
            txtPIS.Text = produtos.Fiscal.pis;
            txtCOFINS.Text = produtos.Fiscal.cofins;
            txtCFOP.Text = produtos.Fiscal.cfop;
        }


        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void SalvarAction(object sender, EventArgs e)
        {
            //mover os dados para a classes
            //instanciando as Classes

            MenuItems menuItems = new MenuItems();
            menuItems.Name = txtNome.Text;
            menuItems.GroupCode= txtGrupo.Text;
            menuItems.Tag = txtTag.Text;

            MenuItemPortions menuItemPortions = new MenuItemPortions();
            menuItemPortions.Name = txtPorcao.Text;
            menuItemPortions.Multiplier = int.Parse(txtMultiplos.Text);

            MenuItemPrices menuItemPrices = new MenuItemPrices();
            menuItemPrices.Price = decimal.Parse(txtPreco.Text);
            
            Fiscal fiscal = new Fiscal();
                      
            fiscal.unidade = txtUnd.Text;
            fiscal.ean =  txtEAN.Text;

            fiscal.ncm = txtNCM.Text;
            fiscal.ncm_CEST = txtCEST.Text;
            fiscal.cst =txtCST.Text;
            fiscal.tipo =txtOrigem.Text;
            fiscal.ipi =txtIPI.Text;
            fiscal.pis = txtPIS.Text;
            fiscal.cofins = txtCOFINS.Text;
            fiscal.cfop = txtCFOP.Text;


            //validar
            //criando e instanciando uma Lista de erros vazia;
            List<ValidationResult> listErros = new List<ValidationResult>();
            //criando um contexto 
            ValidationContext contextoMenuItems = new ValidationContext(menuItems);
            ValidationContext contextoFiscal = new ValidationContext(fiscal);
            bool validado= Validator.TryValidateObject(menuItems, contextoMenuItems, listErros, true);
            validado = Validator.TryValidateObject(fiscal, contextoFiscal, listErros, true);
            

            if(validado)
            {
                //validação OK;
                //Salvar os dados 
                if(ProdutodoDataAccess.SalvarProduto(menuItems, menuItemPortions, menuItemPrices, fiscal))
                {
                    //sucesso
                    produtoTelaPricipal.AtualizarTabela();
                    this.Close();

                }
                else
                {
                    //erro
                    lblErros.Text = "Erro na Inseção - Banco!";
                }
                                          
                
               
            }
            else
            {
                // validação erro;
                StringBuilder sb = new StringBuilder();

                foreach(ValidationResult erro in listErros)
                {
                    sb.Append(erro.ErrorMessage + "\n");
                }
                lblErros.Text = sb.ToString();
            }
            
           
        }
    }
}
