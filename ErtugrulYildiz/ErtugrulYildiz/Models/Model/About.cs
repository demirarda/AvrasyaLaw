using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Hakkimizda")]
	public class About
	{
        [Key]
        public int HakkimizdaId { get; set; }
        [Required]
        public string HakkimizdaBaslik { get; set; }
        [Required]
        public string HakkimizdaAciklama { get; set; }
        [Required]
        public string HakkimizdaFotograf { get; set; }
    }
}