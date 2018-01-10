using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Desafio.Util.Attributes
{
    public class BasicAuthentication : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Request.CreateResponse(new { statusCode = HttpStatusCode.Unauthorized, mensagem = "Não autorizado" });
            actionContext.Response = new HttpResponseMessage();
            actionContext.Response.StatusCode = HttpStatusCode.Unauthorized;
        }

        private bool AuthorizeRequest(HttpActionContext actionContext)
        {
            AuthenticationHeaderValue AuthValue = actionContext.Request.Headers.Authorization;
           
            if (AuthValue.Scheme != "Bearer" || AuthValue.Parameter == null || AuthValue.Parameter == "")
            {
                return false;
            }
            return true;
        }
    }
}
