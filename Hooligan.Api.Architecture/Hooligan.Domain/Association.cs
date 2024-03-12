using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooligan.Domain
{
    public class Association
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string First { get; set; }
        [Required]
        public string Second { get; set; }
        [Required]
        public string Result { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
