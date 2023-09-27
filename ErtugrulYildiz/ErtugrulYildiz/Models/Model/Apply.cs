using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Basvur")]
	public class Apply
	{
        [Key]
        public int BasvurId { get; set; }
        [Required]
        public string BasvurAciklama { get; set; }
        [Required]
        public string BasvurFotograf { get; set; }
    }
}