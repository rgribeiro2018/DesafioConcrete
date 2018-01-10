using Desafio.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Data.Mapping
{
    public class TelefoneMap : EntityTypeConfiguration<Telefone>
    {

        public TelefoneMap()
        {
            ToTable("Telefone");
            HasKey(x => x.TelefoneId);
            Property(x => x.Numero).IsRequired();
            Property(x => x.DDD).IsRequired();
        
        }
       
    }
}
