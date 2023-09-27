using ErtugrulYildiz.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ErtugrulYildiz.Models.DataContext
{
	public class ErtugrulYildizDbContext:DbContext
	{
        public ErtugrulYildizDbContext():base("ErtugrulYildizDb")
        {
            
        }
        public DbSet<About> About { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Apply> Apply { get; set; }
        public DbSet<Choose> Choose { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Lawyers> Lawyers { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Slider> Slider { get; set; }

    }
}