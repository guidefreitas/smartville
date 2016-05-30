using Smartville.Models;
using Smartville.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Smartville.Controllers
{
    public class AuthenticationController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult Login()
        {
            VMLogin vm = new VMLogin();
            vm.RedirectTo = Request.Params["redirectTo"];
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(VMLogin vm)
        {
            if (ModelState.IsValid)
            {
                //Busca o usuário no banco com o mesmo email que foi informado na tela
                User usuarioDb = db.Users.Where(u => u.Email == vm.Email).FirstOrDefault();


                if(usuarioDb == null)
                {
                    ModelState.AddModelError("", "Usuário ou senha incorretos");
                    return View(vm);
                }

                //Compara a senha que foi informada na tela com a senha criptografada armazenada
                //no banco
                bool senhaConfere = Crypto.VerifyHashedPassword(usuarioDb.Password, vm.Password);

                if (!senhaConfere)
                {
                    ModelState.AddModelError("", "Usuário ou senha incorretos");
                    return View(vm);
                }

                //Gera um token de autenticação único.
                //O método Guid.NewGuid() gera uma string aleatória que nunca se repete
                //Ex: f61dbbae2-2e29-4c6c-a445-aetdop12
                string authId = Guid.NewGuid().ToString();
                //Session["AuthToken"] = authId;

                //Cria um novo cookie com a identificacao AuthID
                var cookie = new HttpCookie("AuthToken");

                //Programa o cookie para expirar após uma semana, assim o usuário não precisa ficar 
                //logando toda hora, mesmo se fechar o browser e voltar.
                cookie.Expires = DateTime.Now.AddDays(7);

                //Seta o valor do cookie com o token de identificação 
                cookie.Value = authId;

                //Faz o cookie ser enviado para o browser do usuário junto com a resposta da página
                Response.Cookies.Add(cookie);

                //Atualizar o usuário do banco com o token de autenticação
                usuarioDb.AuthToken = authId;
                db.SaveChanges();

                if (!String.IsNullOrEmpty(vm.RedirectTo))
                {
                    return Redirect(vm.RedirectTo);
                }

                return RedirectToAction("Index", "Home");

            }
            return View(vm);
        }


        public ActionResult Logout()
        {
            if (Request.Cookies.AllKeys.Contains("AuthToken"))
            {
                String authId = Request.Cookies["AuthToken"].Value;
                User usuarioDb = db.Users.Where(c => c.AuthToken == authId).FirstOrDefault();
                if(usuarioDb != null)
                {
                    usuarioDb.AuthToken = "";
                    db.SaveChanges();
                }
                Request.Cookies.Remove("AuthToken");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}