using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Hizmetlerimiz")]
	public class Services
	{
        [Key]
        public int HizmetId { get; set; }
        [Required]
        public string HizmetBaslik { get; set; }

        public string HizmetAciklama { get; set; }
        [Required]
        public string HizmetFotograf { get; set; }
        
        public string CalismaBaslik { get; set; }
        
        public string CalismaAciklama { get; set; }
    }
}