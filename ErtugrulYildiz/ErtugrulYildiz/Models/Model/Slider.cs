using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Slider")]
	public class Slider
	{
        [Key]
        public int SliderId { get; set; }
        [Required]
        public string SliderBaslik { get; set; }
        [Required]
        public string SliderAciklama { get; set; }
        [Required]
        public string SliderFotograf { get; set; }
    }
}