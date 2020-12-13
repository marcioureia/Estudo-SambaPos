using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ProdutosSambaPOS.Modelo;

namespace ProdutosSambaPOS.Banco
{
    public class ProdutodoDataAccess
    {
        private static SqlConnection conn = new SqlConnection(@"Data Source=SERVIDOR-PC\CREATIVE;Initial Catalog=SambaPOS3;Persist Security Info=True;User ID=sa;Password=16861211");

        public static DataTable ObterProdutos()
        {
            

            SqlDataAdapter da = new SqlDataAdapter("Select MenuItems.Id, MenuItems.Name, " +

                "MenuItems.GroupCode, " +
                "MenuItemPrices.Price, Fiscal.ean, Fiscal.ncm, Fiscal.ncm_CEST " +
                "FROM MenuItems " +
                "INNER JOIN MenuItemPortions" +
                " ON MenuItemPortions.MenuItemId = MenuItems.Id " +
                "LEFT JOIN MenuItemPrices " +
                "ON MenuItemPrices.MenuItemPortionId = MenuItemPortions.Id " +
                "LEFT JOIN Fiscal ON " +
                "Fiscal.menuItemId = MenuItems.Id  ", conn);
            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds.Tables[0];
                                    
        }

        public static bool SalvarProduto(MenuItems menuItems, MenuItemPortions menuItemPortions, MenuItemPrices menuItemPrices, Fiscal fiscal)
        {
            // TABELA MENUITENS
            string sql = "INSERT INTO [MenuItems] (GroupCode, Tag, Name) VALUES (@GroupCode, @Tag, @Name)"+
            "SELECT MAX (Id) FROM MenuItems";


            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.Add("@GroupCode", menuItems.GroupCode);
            comando.Parameters.Add("@Tag", menuItems.Tag);
            comando.Parameters.Add("@Name", menuItems.Name);

            SqlParameter param = new SqlParameter("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            comando.Parameters.Add(param);


            conn.Open();
            int newId = (int)comando.ExecuteScalar();

            if (newId > 0 )
            {
                
                //TABELA FISCAL 

                string sqlFiscal = "INSERT INTO [Fiscal] (menuItemId, ean, ncm, ncm_CEST, cst, " +
                    "cfop, ipi, pis, cofins, tipo, unidade) VALUES (@menuItemId, @ean, @ncm, @ncm_CEST, @cst, " +
                    "@cfop, @ipi, @pis, @cofins, @tipo, @unidade)";

                SqlCommand comandoFiscal = new SqlCommand(sqlFiscal, conn);

                comandoFiscal.Parameters.Add("@menuItemId", newId);
                comandoFiscal.Parameters.Add("@ean", fiscal.ean);
                comandoFiscal.Parameters.Add("@ncm", fiscal.ncm);
                comandoFiscal.Parameters.Add("@ncm_CEST", fiscal.ncm_CEST);
                comandoFiscal.Parameters.Add("@cst", fiscal.cst);
                comandoFiscal.Parameters.Add("@cfop", fiscal.cfop);
                comandoFiscal.Parameters.Add("@ipi", fiscal.ipi);
                comandoFiscal.Parameters.Add("@pis", fiscal.pis);
                comandoFiscal.Parameters.Add("@cofins", fiscal.cofins);
                comandoFiscal.Parameters.Add("@tipo", fiscal.tipo);
                comandoFiscal.Parameters.Add("@unidade", fiscal.unidade);

                comandoFiscal.ExecuteNonQuery();
                
                //TABELA MENUITEMPORTIONS

                string sqlPortions = "INSERT INTO [MenuItemPortions] (Name, MenuItemId, Multiplier) VALUES (@Name, @MenuItemId, @Multiplier)" +
                "SELECT MAX (Id) FROM MenuItemPortions";

                SqlCommand comandoPortions = new SqlCommand(sqlPortions, conn);

                comandoPortions.Parameters.Add("@Name", menuItemPortions.Name);
                comandoPortions.Parameters.Add("@MenuItemId", newId);
                comandoPortions.Parameters.Add("@Multiplier", menuItemPortions.Multiplier);

                SqlParameter param2 = new SqlParameter("@ID", SqlDbType.Int);
                param2.Direction = ParameterDirection.Output;
                comandoPortions.Parameters.Add(param2);
                int newId2 = (int) comandoPortions.ExecuteScalar();

                if (newId2 > 0)
                {
                    

                    // TABELA MENUITEMPRICES

                    string sqlPrices = "INSERT INTO [MenuItemPrices](MenuItemPortionId, Price) VALUES (@MenuItemPortionId, @Price)";

                    SqlCommand comandoPrices = new SqlCommand(sqlPrices, conn);
                    comandoPrices.Parameters.Add("@MenuItemPortionId", newId2);
                    comandoPrices.Parameters.Add("@Price", menuItemPrices.Price);
                    comandoPrices.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            else
            {
                conn.Close();
                return false;
            }

           
        }

        public static Produtos PegarProduto(int id )
        {
          
            // TABELA MENUITENS
            string sql = "SELECT * FROM [MenuItems] WHERE Id = @id";



            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.Add("@id", id);



            conn.Open();
            SqlDataReader resposta = comando.ExecuteReader();
            Produtos produtos = new Produtos();
           
            while (resposta.Read())
            {
               produtos.MenuItems.Id = resposta.GetInt32(0);
                produtos.MenuItems.GroupCode = resposta.GetString(1);
                produtos.MenuItems.Tag = resposta.GetString(3);
                produtos.MenuItems.Name = resposta.GetString(4);
            }


            var menuItemId = produtos.MenuItems.Id;

            //TABELA FISCAL 

            string sqlFiscal = "SELECT * [Fiscal] WHERE menuItemId = " + menuItemId;

            SqlCommand comandoFiscal = new SqlCommand(sqlFiscal, conn);

            comandoFiscal.Parameters.Add("@menuItemId", menuItemId);

            SqlDataReader respostaFiscal = comando.ExecuteReader();
            
            while (respostaFiscal.Read())
            {
                produtos.Fiscal.menuItemId = respostaFiscal.GetInt32(2);
                produtos.Fiscal.ean = respostaFiscal.GetString(3);
                produtos.Fiscal.ncm = respostaFiscal.GetString(4);
                produtos.Fiscal.ncm_CEST = respostaFiscal.GetString(5);
                produtos.Fiscal.cst = respostaFiscal.GetString(6);
                produtos.Fiscal.cfop = respostaFiscal.GetString(7);
                produtos.Fiscal.ipi = respostaFiscal.GetString(8);
                produtos.Fiscal.pis = respostaFiscal.GetString(9);
                produtos.Fiscal.cofins = respostaFiscal.GetString(10);
                produtos.Fiscal.tipo = respostaFiscal.GetString(11);
                produtos.Fiscal.unidade = respostaFiscal.GetString(12);
            }

            //TABELA MENUITEMPORTIONS

            string sqlPortions = "SELECT * [MenuItemPortions] WHERE MenuItemId = " + menuItemId;

            SqlCommand comandoPortions = new SqlCommand(sqlPortions, conn);

            comandoPortions.Parameters.Add("@MenuItemId", menuItemId);

            SqlDataReader respostaPortions = comando.ExecuteReader();
           
            while (respostaPortions.Read())
            {
                produtos.MenuItemPortions.MenuItemId = respostaPortions.GetInt32(2);
                produtos.MenuItemPortions.Name = respostaPortions.GetString(1);
                produtos.MenuItemPortions.Multiplier = respostaPortions.GetInt32(3);

            }


            // TABELA MENUITEMPRICES
            var portionsId = respostaPortions.GetInt32(0);

            string sqlPrices = "SELECT * FROM [MenuItemPrices] WHERE MenuItemPortionId =" + portionsId;

            SqlCommand comandoPrices = new SqlCommand(sqlPrices, conn);

            comandoPrices.Parameters.Add("@MenuItemPortionId", portionsId);

            SqlDataReader respostaPrices = comandoPrices.ExecuteReader();
           

            while (respostaPrices.Read())
            {
                produtos.MenuItemPrices.MenuItemPortionId = respostaPrices.GetInt32(1);
                produtos.MenuItemPrices.Price = respostaPrices.GetInt32(3);

            }
            
            conn.Close();

            return produtos;
        }
           


    }
}
