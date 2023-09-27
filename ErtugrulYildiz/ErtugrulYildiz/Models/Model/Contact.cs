using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.Model
{
    [Table("Contact")]
	public class Contact
	{
        [Key]
        public int IletisimId { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
    }
}