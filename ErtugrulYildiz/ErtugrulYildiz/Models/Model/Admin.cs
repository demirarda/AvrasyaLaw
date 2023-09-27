using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Admin")]
	public class Admin
	{
        [Key]
        public int AdminId { get; set; }
        [Required]
        public string Eposta { get; set; }
        [Required]
        public string Sifre { get; set; }
    }
}