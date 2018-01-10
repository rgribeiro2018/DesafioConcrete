using Desafio.Data.Repositories;
using Desafio.Domain;
using Desafio.Domain.Interfaces;
using Desafio.Util.Attributes;
using Desafio.Util.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Desafio.Api.Controllers
{
    [RoutePrefix("api/profile")]
    public class Profile1Controller : ApiController
    {

        private IProfileRepository _repository;


        public Profile1Controller(IProfileRepository repository)
        {
            this._repository = repository;
        }

        

        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(LoginModel login)
        {
            try
            {
                //Valida se dados foram preenchidos
                //if (login == null || login.Email == "" || login.Senha == "")

                if (Validacao.IsEmpty(login))
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "E-mail e senha obrigatórios" });

                //Busca Usuário
                var profile = _repository.Get(login.Email);

                //Valida existência de Usuário
                if (profile == null)
                {
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Usuário e/ou senha inválidos" });
                }



                var verificaAcesso = Hash.VerificarSenha(login.Senha, profile.Senha, SHA512.Create());
                if (!verificaAcesso)
                {
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Usuário e/ou senha inválidos" });
                }

                //Atualiza Último Login de Acesso
                profile.DataUltimoLogin = DateTime.Now;
                _repository.Update(profile);

                //Retorno Ok
                return Request.CreateResponse(new { statusCode = HttpStatusCode.OK, usuarioLogado = profile });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { statusCode = HttpStatusCode.InternalServerError, erro = ex.Message });
            }
        }


        [HttpPost]
        [Route("SignUp")]
        public HttpResponseMessage Create(Profile profile)
        {
            try
            {
                //Valida se dados foram preenchidos
                //if (profile == null || profile.Nome == "" || profile.Senha == "" || profile.Email == "")
                if (Validacao.IsEmpty(profile))
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Dados obrigatórios" });


                var usuarioCadastrado = _repository.Get(profile.Email);

                if (usuarioCadastrado != null)
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "E-mail já existente" });

                Guid guid = Guid.NewGuid();




                profile.ProfileId = guid;
                profile.Senha = Hash.CriptografarSenha(profile.Senha);
                profile.Token = Hash.CriptografarSenha(Guid.NewGuid().ToString());
                profile.DataCriacao = DateTime.Now;
                profile.DataAtualizacao = DateTime.Now;
                profile.DataUltimoLogin = DateTime.Now;

                _repository.Create(profile);

                return Request.CreateResponse
                                (new
                                {
                                    statusCode = HttpStatusCode.OK,
                                    nome = profile.Nome,
                                    email = profile.Email,
                                    senha = profile.Senha,
                                    telefones = profile.Telefones
                                });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new
                {
                    statusCode = HttpStatusCode.InternalServerError,
                    erro = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("Profile1/{profileId}")]
        public HttpResponseMessage Profile(string profileId)
        {
            try
            {
                HttpActionContext contexto = base.ActionContext;
                AuthenticationHeaderValue ValorAuthentication = contexto.Request.Headers.Authorization;

                //AuthValue.Parameter é o TOKEN recebido
                if (ValorAuthentication.Scheme != "Bearer" || ValorAuthentication.Parameter == null || ValorAuthentication.Parameter == "")
                {
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Não autorizado" });
                }


                var idGuidUsuario = new Guid(profileId);

                var profile = _repository.Get(idGuidUsuario);

                if (profile.Token != ValorAuthentication.Parameter)
                {
                    return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Não autorizado" });
                }
                else
                {

                    if (Validacao.VerificaSessao(profile.DataUltimoLogin))
                        return Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Sessão inválida" });


                    return Request.CreateResponse(new { statusCode = HttpStatusCode.OK, nome = profile.Nome, email = profile.Email, senha = profile.Senha, telefones = profile.Telefones });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { statusCode = HttpStatusCode.InternalServerError, erro = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
        }
    }
}
