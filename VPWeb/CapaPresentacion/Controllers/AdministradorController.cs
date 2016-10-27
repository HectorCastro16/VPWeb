﻿using CapaEntidades;
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
        public ActionResult RegistroActividad(String mensaje, Int16? identificador)
        {
            ViewBag.mensaje = mensaje;
            ViewBag.identificador = identificador;
            return View();
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult RegistroActividad(entActividad a, HttpPostedFileBase archivo)
        {

            try
            {
                if (archivo != null && archivo.ContentLength > 0){
                    a.imagenActividad = Path.GetFileName(archivo.FileName);
                }else{
                    a.imagenActividad = "imgactividad.jpg";
                }
                int i = negActividad.Instancia.InsUpdActividad(a, 1);
                if (i > 0){
                    if (archivo != null && archivo.ContentLength > 0){
                        var namearchivo = Path.GetFileName(archivo.FileName);
                        var ruta = Path.Combine(Server.MapPath("~/images/ImgActividades"), namearchivo);
                        archivo.SaveAs(ruta);
                    }
                    return RedirectToAction("ListActividades", new { mensaje = "SE INSERTO CORRECTAMENTE !", identificador = 3 });
                }else{
                    return RedirectToAction("ListActividades", new { mensaje = "NO SE PUEDO REGISTRAR", identificador = 2 });
                }
            }catch (ApplicationException ex){
                ViewBag.mensaje = ex.Message;
                return RedirectToAction("RegistroActividad", "Administrador", new { mensaje = ex.Message, identificador = 1 });
            }catch (Exception e){
                return RedirectToAction("RegistroActividad", "Administrador", new { mensaje = e.Message, identificador = 2 });
            }
        }


        public ActionResult EditarActividad(Int16 idActividad, String mensaje, Int16? identificador)
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

                return RedirectToAction("ListActividades", new { mensaje = e, identificador = 2 });
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditarActividad(entActividad a, HttpPostedFileBase archivo)
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
                return RedirectToAction("EditarActividad", "Administrador", new { mensaje = ex.Message, identificador = 1 });
            }
            catch (Exception e)
            {
                return RedirectToAction("EditarActividad", "Administrador", new { mensaje = e.Message, identificador = 2 });
            }     
            //return View();        
        }

        public ActionResult DeleteActividad(Int16 idActividad) {

            try
            {
                
                int i = negActividad.Instancia.DelActividad(idActividad, 3);
                if (i > 0){
                    return RedirectToAction("ListActividades", new { mensaje = "SE ELIMINO CORRECTAMENTE !", identificador = 3 });
                }
                else {
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

        public ActionResult Pruebas() {
            return View();
        }

        


    }
}
