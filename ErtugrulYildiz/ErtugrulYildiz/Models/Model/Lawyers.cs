using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Avukatlar")]
	public class Lawyers
	{
        [Key]
        public int AvukatId { get; set; }
        [Required]
        public string AvukatAdSoyad { get; set; }
        public string AvukatUnvan { get; set; }
        [Required]
        public string AvukatFotograf { get; set; }
    }
}