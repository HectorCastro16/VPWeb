using CapaAccesoDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class negActividad
    {
        #region Singleton
        private static readonly negActividad _Instancia = new negActividad();
        public static negActividad Instancia
        {
            get
            {
                return negActividad._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public List<entActividad> ListaActividades() {
            try
            {
                return datActividad.Instancia.ListaActividades();
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public int InsUpdActividad(entActividad a, Int16 TipoEdicion) {

            try
            {
                String cadXml = "";

                cadXml += "<Actividad ";
                cadXml += "idActividad='" + a.idActividad+ "' ";
                cadXml += "tituloActividad='" + a.tituloActividad + "' ";
                cadXml += "descripcionActividad='" + a.descripcionActividad + "' ";
                cadXml += "fechaActividad='" + a.fechaActividad.ToString("yyyy/MM/dd") + "' ";
                cadXml += "imagenActividad='" + a.imagenActividad + "' ";
                cadXml += "estadoActividad='" + a.estadoActividad + "' ";
                cadXml += "direccionActividad='" + a.direccionActividad + "' ";
                cadXml += "lugarInicio='" + a.lugarInicio + "' ";
                cadXml += "lugarFin='" + a.lugarFin + "' ";
                cadXml += "horaInicio='" + a.horaInicio + "' ";
                cadXml += "horaFin='" + a.horaFin + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml = "<root>" + cadXml + "</root>";

                int i = datActividad.Instancia.InsUpdDelActividad(cadXml);

                if (i == -2) { throw new ApplicationException("ACTIVIDAD YA REGISTRADA"); }
                return i;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public int DelActividad(entActividad a, Int16 TipoEdicion) {

            try
            {
                String cadXml = "";

                cadXml += "<Actividad ";
                cadXml += "idActividad='" + a.idActividad + "' ";
                cadXml += "TipoEdicion='" + TipoEdicion + "'/>";

                cadXml = "<root>" + cadXml + "</root>";

                int i = datActividad.Instancia.InsUpdDelActividad(cadXml);
                return i;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public entActividad DevuelveActividad(Int16 idActividad ) {
            try
            {
                return datActividad.Instancia.DevuelveActividad(idActividad);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion metodos

    }
}
