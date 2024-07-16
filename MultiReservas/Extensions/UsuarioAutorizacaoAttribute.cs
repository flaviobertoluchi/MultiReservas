using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MultiReservas.Controllers;

namespace MultiReservas.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UsuarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        void IAuthorizationFilter.OnAuthorization(AuthorizationFilterContext context)
        {
            var sessao = (Sessao)context.HttpContext.RequestServices.GetRequiredService(typeof(Sessao));

            var usuario = sessao.ObterUsuario();
            if (usuario is null)
            {
                context.Result = new RedirectToActionResult(nameof(UsuarioController.Entrar), "Usuario", new { returnUrl = context.HttpContext.Request.Path });
                return;
            }
        }
    }
}
