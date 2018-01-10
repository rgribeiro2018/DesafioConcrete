using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        T Get(int Id);
        T Get(Guid Id);
        T Get(string Email);
        void Create(T entity);
        void Update(T entity);
       
        
    }
}
