using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion.Controllers
{
    public class InicioController : Controller
    {
        //
        // GET: /Inicio/

        public ActionResult Index(String mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }

        public ActionResult Login(String mensaje)
        {
            ViewBag.mensaje = mensaje;
            return View();
        }

        public ActionResult VerificaAcceso(FormCollection form)
        {
            try
            {
                String Usuario = form["txtUsuario"];
                String Password = form["txtPassword"];
                entUsuario u = negUsuario.Instancia.VerificarAccesoIntranet(Usuario, Password);
                Session["usuario"] = u; //agregando el objeto c en el atributo cliente de la sesion

                return RedirectToAction("PrincipalAdministrador", "Administrador");

            }
            catch (ApplicationException x)
            {
                ViewBag.mensaje = x.Message;
                return RedirectToAction("Login", "Inicio", new { mensaje = x.Message });
            }
            catch (Exception e)
            {

                return RedirectToAction("Login", "Inicio", new { mensaje = e.Message });
            }
        }

    }
}
