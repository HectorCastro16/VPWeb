using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entActividad
    {
        public int idActividad { get; set; }
        public String tituloActividad { get; set; }
        public String  descripcionActividad{ get; set; }
        public DateTime fechaActividad { get; set; }
        public String imagenActividad { get; set; }
        public String estadoActividad { get; set; }
        public String direccionActividad { get; set; }
        public String lugarInicio { get; set; }
        public String lugarFin { get; set; }
        public String horaInicio{ get; set; }
        public String horaFin { get; set; }
        public Boolean Anulado{ get; set; }
    }
}
