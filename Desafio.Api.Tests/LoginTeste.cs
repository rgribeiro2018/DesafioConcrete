using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using Desafio.Domain;
using Desafio.Domain.Interfaces;
using Desafio.Data.Repositories;
using Desafio.Api.Controllers;

namespace Desafio.Api.Tests
{
    [TestClass]
    public class LoginTeste
    {
        private IProfileRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new ProfileRepository();
        }

        [TestMethod]
        public void ValidaLogin()
        {
            try
            {
                var loginModel = new LoginModel();
                loginModel.Email = "romulo.g.ribeiro@gmail.com";
                loginModel.Senha = "1234";

                var controller = new Profile1Controller(repository);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();

                Assert.IsNotNull(controller.Login(loginModel));
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
