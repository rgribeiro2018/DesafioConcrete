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

namespace Desafio.Api.Tests
{
    [TestClass]
    public class Profile1Teste
    {
        private IProfileRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = new ProfileRepository();
        }

        [TestMethod]
        public void ValidaProfile()
        {
            try
            {
                HttpActionContext context = new HttpActionContext();
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Add("Authorization", "Bearer 71927D70BE8483D6A0F478E6C1D973EACE0F5CC3E29656353EDAD95CCBEA1C1A1744FFC33386B4170784548B06685FA89458E11B7F4602D9C4AE1B52E8CCBFFA");

                var controller = new Profile1Controller(_repository);
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();
                controller.ActionContext = context;

                Assert.IsNotNull(controller.Profile("FFB8DDC4-6B11-4AC1-9484-E1FE5E39A6D6"));
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
