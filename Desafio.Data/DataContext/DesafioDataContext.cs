using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Desafio.Domain;
using Desafio.Data.Mapping;

namespace Desafio.Data.DataContext
{
    public class DesafioDataContext : DbContext
    {
        public DesafioDataContext()
            : base("DesafioConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Telefone> Telefones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new TelefoneMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
