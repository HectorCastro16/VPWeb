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
            ViewBag.identificador = identificador;
            List<entActividad> Lista = negActividad.Instancia.ListaActividades();
            return View(Lista);
        }
        [ValidateInput(false)]
        public ActionResult RegistroActividad(String mensaje, Int16? identificador)
        {
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RegistroActividad(entActividad a, HttpPostedFileBase archivo, String sh1, String sh2)
        {

            try
            {
                if (archivo != null && archivo.ContentLength > 0)
                {
                    a.imagenActividad = Path.GetFileName(archivo.FileName);
                }
                else
                {
                    a.imagenActividad = "imgactividad.jpg";
                }

                if (sh1 == "" || sh1 == null)
                {
                    return RedirectToAction("RegistroActividad", "Administrador", new { mensaje = "Porfavor Elegir Sistema Horario de Hora Inicio (A.M. - P.M.)", identificador = 2 });
                }
                else if (sh2 == "" || sh2 == null)
                {
                    return RedirectToAction("RegistroActividad", "Administrador", new { mensaje = "Porfavor Elegir Sistema Horario de Hora Fin (A.M. - P.M.)", identificador = 2 });
                }
                else
                {
                    a.horaInicio = a.horaInicio + " " + sh1;
                    a.horaFin = a.horaFin + " " + sh2;
                }

                int i = negActividad.Instancia.InsUpdActividad(a, 1);
                if (i > 0)
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/images/ImgActividades"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
                    return RedirectToAction("ListActividades", new { mensaje = "SE INSERTO CORRECTAMENTE !", identificador = 3 });
                }
                else
                {
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

        [ValidateInput(false)]
        public ActionResult EditarActividad(Int16 idActividad, String mensaje, Int16? identificador)
        {
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            try
            {
                entActividad a = negActividad.Instancia.DevuelveActividad(idActividad);
                a.fechaActividad.Date.ToShortDateString();
                a.horaInicio = a.horaInicio.Remove(5, 5);
                a.horaFin = a.horaFin.Remove(5, 5);
                return View(a);
            }
            catch (Exception e)
            {

                return RedirectToAction("ListActividades", new { mensaje = e, identificador = 2 });
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditarActividad(entActividad a, HttpPostedFileBase archivo, String sh1, String sh2)
        {
            try
            {
                if (archivo != null && archivo.ContentLength > 0)
                {
                    a.imagenActividad = Path.GetFileName(archivo.FileName);
                }
                else
                {
                    entActividad act = negActividad.Instancia.DevuelveActividad(Convert.ToInt16(a.idActividad));
                    a.imagenActividad = act.imagenActividad;
                }                

                if (sh1=="" || sh1==null) {
                    return RedirectToAction("EditarActividad", "Administrador", new { idActividad = a.idActividad, mensaje = "Porfavor Confirme Sistema Horario de Hora Inicio (A.M. - P.M.)", identificador = 2 });
                }
                else if (sh2=="" || sh2==null) {
                    return RedirectToAction("EditarActividad", "Administrador", new { idActividad = a.idActividad, mensaje = "Porfavor Confirme Sistema Horario de Hora Fin (A.M. - P.M.)", identificador = 2 });
                }
                else {
                    a.horaInicio = a.horaInicio + " " + sh1;
                    a.horaFin = a.horaFin + " " + sh2;
                }

                int i = negActividad.Instancia.InsUpdActividad(a, 2);
                if (i > 0)
                {
                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/images/ImgActividades"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
                    return RedirectToAction("ListActividades", new { mensaje = "SE EDITO CORRECTAMENTE !", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("ListActividades", new { mensaje = "NO SE PUEDO REALIZAR EDICION", identificador = 2 });
                }
            }
            catch (ApplicationException ex)
            {
                ViewBag.mensaje = ex.Message;
                return RedirectToAction("EditarActividad", "Administrador", new { idActividad = a.idActividad, mensaje = ex.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("EditarActividad", "Administrador", new { idActividad = a.idActividad, mensaje = e.Message, identificador = 2 });
            }
            //return View();        
        }

        public ActionResult DeleteActividad(Int16 idActividad)
        {

            try
            {

                int i = negActividad.Instancia.DelActividad(idActividad, 3);
                if (i > 0)
                {
                    return RedirectToAction("ListActividades", new { mensaje = "SE ELIMINO CORRECTAMENTE !", identificador = 3 });
                }
                else
                {
                    return RedirectToAction("ListActividades", new { mensaje = "NO SE PUEDO ELIMINAR", identificador = 2 });
                }
            }
            catch (ApplicationException ex)
            {
                ViewBag.mensaje = ex.Message;
                return RedirectToAction("ListActividades", "Administrador", new { mensaje = ex.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListActividades", "Administrador", new { mensaje = e.Message, identificador = 2 });
            }
        }

        public ActionResult Pruebas()
        {
            try
            {
                List<entActividad> Lista = negActividad.Instancia.ListaActividades();
                ViewBag.Lista = Lista;
                return View();
            }
            catch (Exception e)
            {
                
                throw e;
            }
            
        }




    }
}
