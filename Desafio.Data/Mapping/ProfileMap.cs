using Desafio.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Data.Mapping
{
    public class ProfileMap : EntityTypeConfiguration<Profile>
    {
        public ProfileMap()
        {
            ToTable("Profile");
            HasKey(x => x.ProfileId);
            Property(x => x.Nome).IsRequired();
            Property(x => x.Senha).IsRequired();
            Property(x => x.Email).IsRequired();
            HasMany(x => x.Telefones);
        }
    }
}
