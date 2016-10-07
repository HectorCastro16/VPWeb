using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class entUsuario
    {
        public int idUsuario { get; set; }
        public entTipoUsuario TipoUsuario { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Dni { get; set; }
        public String Email { get; set; }
        public DateTime FechaHasta { get; set; }
        public String Login { get; set; }
        public String Foto { get; set; }
        public String Password { get; set; }
        public Boolean Activo { get; set; }
        public Boolean Anulado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public String UsuarioRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
        public String UsuarioModificacion { get; set; }
    }
}
