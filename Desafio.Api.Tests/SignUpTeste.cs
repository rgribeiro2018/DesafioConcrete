using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using Desafio.Domain.Interfaces;
using Desafio.Data.Repositories;
using Desafio.Api.Controllers;
using System.Web.Http.Controllers;
using Desafio.Domain;

namespace Desafio.Api.Tests
{
    [TestClass]
    public class SignUpTeste
    {
        private IProfileRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new ProfileRepository();
        }

        [TestMethod]
        public void ValidaSignUp()
        {
            try
            {
                var profile = new Profile();
                profile.Nome = "Romulo4";
                profile.Email = "teste1@gmail.com";
                profile.Senha = "1234";
                profile.Telefones = new List<Telefone>() { new Telefone() { DDD = "021", Numero = "33942492" } };

                var controller = new Profile1Controller(_repository);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                Assert.IsNotNull(controller.Create(profile));
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
