using System;
using System.Security.Cryptography;
using System.Text;

namespace Desafio.Util.Services
{
    public static class Hash
    {
        
        //Padrão de HASH é o SA512.
        public static string CriptografarSenha(string senha)
        {
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = SHA512.Create().ComputeHash(encodedValue);

            //Gerando um salt para o Hash.
            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string CriptografarSenha(string senha, HashAlgorithm algoritmo)
        {
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = algoritmo.ComputeHash(encodedValue);

            //Gerando um salt para o Hash.
            var sb = new StringBuilder();
            foreach (var caracter in encryptedPassword)
            {
                sb.Append(caracter.ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool VerificarSenha(string senhaDigitada, string senhaCadastrada, HashAlgorithm algoritmo)
        {
         
            if (string.IsNullOrEmpty(senhaCadastrada))
                throw new NullReferenceException("Cadastre uma senha.");

            var encryptedPassword = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
            {
                sb.Append(caractere.ToString("X2"));
            }

            return sb.ToString() == senhaCadastrada;
        }
    }
}