using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosSambaPOS.Modelo
{
    public class Payments
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int PaymentTypeId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int AccountTransactionId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public int AccountTransaction_Id { get; set; }
        public int AccountTransaction_AccountTransactionDocumentId { get; set; }
        public decimal TenderedAmount { get; set; }
        public int DepartmentId { get; set; }
        public int TerminalId { get; set; }
        
    }
}
