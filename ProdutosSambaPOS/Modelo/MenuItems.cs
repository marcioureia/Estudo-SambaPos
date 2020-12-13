using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProdutosSambaPOS.Modelo
{
   public class MenuItems
    {
        public int Id { get; set; }
        [Required]
        public string GroupCode { get; set; }

        public string Barcode { get; set; }

        public string Tag { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string CustomTags { get; set; }
               
        public virtual ICollection<Fiscal> Fiscal { get; set; }
                
        public virtual ICollection<MenuItemPortions> MenuItemPortions { get; set; }
    }
}
