using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosSambaPOS.Modelo
{
     public class Produtos
    {
        public MenuItems MenuItems { get; set; }
        public MenuItemPortions MenuItemPortions { get; set; }
        public MenuItemPrices MenuItemPrices { get; set; }
        public Fiscal Fiscal { get; set; }
    }
}
