using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LsiApp
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<LSI_APP> LSI_APP { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LSI_APP>()
                .Property(e => e.NazwaEksportu)
                .IsUnicode(false);

            modelBuilder.Entity<LSI_APP>()
                .Property(e => e.Uzytkownik)
                .IsUnicode(false);

            modelBuilder.Entity<LSI_APP>()
                .Property(e => e.Lokal)
                .IsUnicode(false);
        }
    }
}
