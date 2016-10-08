using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/

        public ActionResult PrincipalAdministrador()
        {
            return View();
        }

        public ActionResult ListActividades(String mensaje, Int16? identificador)
        {
            ViewBag.mensaje = mensaje;
            List<entActividad> Lista = negActividad.Instancia.ListaActividades();
            return View(Lista);
        }
        public ActionResult RegistroActividad(String mensaje) { 
            ViewBag.mensaje = mensaje; 
            return View(); 
        }

        [HttpPost]
        public ActionResult RegistroActividad(entActividad a, HttpPostedFileBase archivo)
        {

            try
            {
                int i = negActividad.Instancia.InsUpdActividad(a,1);
                if(i>0){
                    if (archivo != null && archivo.ContentLength > 0) {
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/images/ImgActividades"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
                    return RedirectToAction("ListActividades", new { mensaje = "SE INSERTO CORRECTAMENTE !", identificador = 3 });
                }else{
                    return RedirectToAction("ListActividades", new { mensaje = "NO SE PUEDO REGISTRAR", identificador = 2 });
                }
            }
            catch (ApplicationException ex)
            {
                ViewBag.mensaje = ex.Message;
                return RedirectToAction("RegistroActividad", "Administrador", new { mensaje = ex.Message, identificador = 1 });
            }
            catch (Exception e)
            {

                return RedirectToAction("RegistroActividad", "Administrador", new { mensaje = e.Message, identificador = 2 });
            }
        
        }

    }
}
