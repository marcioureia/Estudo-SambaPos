using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProdutosSambaPOS.Modelo
{
    public class Fiscal
    {

        public int id { get; set; }

        public int menuItemId { get; set; }

        [Required]
        public string ean { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        public string ncm { get; set; }

        
        [StringLength(7, MinimumLength = 0)]
        public string ncm_CEST { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string cst { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string cfop { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string ipi { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string pis { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string cofins { get; set; }

       [Required]
       [StringLength(1, MinimumLength = 1)]
        public string tipo { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string unidade { get; set; }

        public virtual MenuItems MenuItems { get; set; }
    }
}
