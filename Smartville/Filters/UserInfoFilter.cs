using Smartville.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smartville.Filters
{
    public class UserInfoFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            var session = filterContext.RequestContext.HttpContext.Session;
            
            //Verifica se existe algum cookie com a identificação AuthID
            if (request.Cookies.AllKeys.Contains("AuthToken"))
            {
                var cookieAuthId = request.Cookies["AuthToken"].Value;

                //Busca o usuário no banco com o token de autenticação existente no cookie
                DatabaseContext db = new DatabaseContext();
                var usuarioDb = db.Users
                                  .Where(m => m.AuthToken == cookieAuthId)
                                  .FirstOrDefault();

                //Se o usuário existir
                if (usuarioDb != null && usuarioDb.AuthToken == cookieAuthId)
                {
                    //Guarta o nome, se está autenticado, se é administrador no ViewBag
                    //para poder utilizar em qualquer View do sistema (Ex: menus) 
                    //e fazer as verificações para exibir ou ocultar elementos
                    filterContext.Controller.ViewBag.CurrentUser = usuarioDb;
                    filterContext.Controller.ViewBag.Authenticated = true;
                    
                }
            }
        }
    }
}