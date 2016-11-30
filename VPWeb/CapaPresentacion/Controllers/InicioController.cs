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
            List<entActividad> lista = negActividad.Instancia.ListaTopActividades();
            return View(lista);
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

        public ActionResult Conocenos() {

            return View();

        }

        public ActionResult TodasActividades() {

            List<entActividad> Lista = negActividad.Instancia.ListaActividades();
            ViewBag.ListActividades = Lista;
            return View();

        }

        public ActionResult NuestrasActividades() {

            return View();
        
        }

        [ValidateInput(false)]
        public ActionResult DetalleActividadExtranet(Int16 idActividad, String mensaje, Int16? identificador)
        {
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            try
            {
                entActividad a = negActividad.Instancia.DevuelveActividad(idActividad);
                return View(a);
            }
            catch (Exception e)
            {

                return RedirectToAction("TodasActividades", new { mensaje = e, identificador = 2 });
            }
        }

        public ActionResult IncrementaAsistencias(int idActividad) {

            int i = negActividad.Instancia.IncrementaAsistencias(idActividad);

            return RedirectToAction("Index", "Inicio", new { mensaje = "Gracias Por Confirmar Asistencia" });
        }

    }
}
