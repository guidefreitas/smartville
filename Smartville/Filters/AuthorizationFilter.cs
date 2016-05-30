using Smartville.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smartville.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private UserType userType;

        public AuthorizationFilter(UserType userType)
        {
            this.userType = userType;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var session = filterContext.RequestContext.HttpContext.Session;

            try
            {
                
                //Encontra um cookie com a identificação AuthID, se não encontrar
                //será disparada uma excessão, caindo no catch.
                var cookieAuthId = request.Cookies["AuthToken"].Value;

                //Verfica se existe algum usuário no banco com o mesmo token de identificação do cookie
                DatabaseContext db = new DatabaseContext();
                var usuarioDb = db.Users
                                  .Where(m => m.AuthToken == cookieAuthId)
                                  .FirstOrDefault();


                //Se não existir nenhum usuário com esse token, ou seja, o usuarioDB virá nulo, redireciona
                //para a página de login
                if (usuarioDb == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { area = "", controller = "Authentication", action = "Login", redirectTo = request.Url }));
                }

                if (usuarioDb.UserType != this.userType)
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { area = "", controller = "Authentication", action = "Login", redirectTo = request.Url }));
                }
                
            }
            catch(Exception ex)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { area = "", controller = "Authentication", action = "Login", redirectTo = request.Url }));
            }
        }
    }
}