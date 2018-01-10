using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain
{
    public class Profile
    {
        public Guid ProfileId { get; set; }
        public string Nome { get; set; }
        public string  Email { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataUltimoLogin { get; set; }
        public List<Telefone> Telefones { get; set; }
    }
}
