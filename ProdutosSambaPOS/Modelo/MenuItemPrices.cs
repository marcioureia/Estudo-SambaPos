using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosSambaPOS.Modelo
{
    public class MenuItemPrices
    {
        public int Id { get; set; }

        
        public int MenuItemPortionId { get; set; }

        public string PriceTag { get; set; }
        [Required]
        
        public decimal Price { get; set; }

        public virtual MenuItemPortions MenuItemPortions { get; set; }
    }
}
