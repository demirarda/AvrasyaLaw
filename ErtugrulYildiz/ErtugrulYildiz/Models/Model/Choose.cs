using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Secim")]
	public class Choose
	{
        [Key]
        public int SecimId { get; set; }
        [Required]
        public string SecimBaslik { get; set; }
        public string SecimAciklama { get; set; }
        public string SecimFotograf { get; set; }


    }
}