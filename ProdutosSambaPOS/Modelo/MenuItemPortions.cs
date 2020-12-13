using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdutosSambaPOS.Modelo
{
    public class MenuItemPortions
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        public int MenuItemId { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1)]
        public int Multiplier { get; set; }

        public virtual MenuItems MenuItems { get; set; }

        
        public virtual ICollection<MenuItemPrices> MenuItemPrices { get; set; }
    }
}
