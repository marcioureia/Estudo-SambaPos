using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient.Base;
using ProdutosSambaPOS.Modelo;
using TableDependency.SqlClient;

namespace ProdutosSambaPOS.Banco
{
    /* public class SqlTableDependency
    {
        private static string _conn = @"Data Source=SERVIDOR-PC\CREATIVE;Initial Catalog=SambaPOS3;Persist Security Info=True;User ID=sa;Password=16861211";

        public static void Monitoramento()
        {
           var mapper = new ModelToTableMapper<Payments>();
            mapper.AddMapping(c => c.Id, "Id");
            mapper.AddMapping(c => c.TicketId, "TicketId");

            using (var dep = new SqlTableDependency<Payments>(_conn, "Payoments", mapper: mapper)){

                dep.OnChanged += dep_Changed;
                dep.Start();

            }
    }*/
}
