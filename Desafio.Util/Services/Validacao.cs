using Desafio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Util.Services
{
    public static class Validacao
    {
        public static bool IsEmpty(Profile entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Nome)|| string.IsNullOrEmpty(entity.Senha )|| string.IsNullOrEmpty(entity.Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEmpty(LoginModel entity)
        {
            if (entity == null || string.IsNullOrEmpty(entity.Senha)|| string.IsNullOrEmpty(entity.Email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VerificaSessao(DateTime dataUltimoLogin)
        {
            if ((DateTime.Now - dataUltimoLogin).Minutes > 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
