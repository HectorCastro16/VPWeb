using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using CapaEntidades;

namespace CapaAccesoDatos
{
    public class datUsuario
    {
        #region Singleton

        private static readonly datUsuario _Instancia = new datUsuario();
        public static datUsuario Instancia
        {
            get
            {
                return datUsuario._Instancia;
            }
        }

        #endregion Singleton

        #region metodos
        public entUsuario VerificarAccesoIntranet(String prmstrLogin, String prmstrPassw)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entUsuario u = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spVerificarAccesoIntranet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmstrLogin", prmstrLogin);
                cmd.Parameters.AddWithValue("@prmstrPassw", prmstrPassw);
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new entUsuario();
                    u.idUsuario = Convert.ToInt32(dr["idUsuario"]);

                    entTipoUsuario t = new entTipoUsuario();
                    t.NombreTipo = dr["NombreTipo"].ToString();
                    u.TipoUsuario = t;

                    u.Nombres = dr["Nombres"].ToString();
                    u.Apellidos = dr["Apellidos"].ToString();
                    u.Dni = dr["Dni"].ToString();
                    u.Email = dr["Email"].ToString();
                    u.FechaHasta = Convert.ToDateTime(dr["FechaHasta"]);
                    u.Login = dr["Login"].ToString();
                    u.Foto = dr["Foto"].ToString();
                    u.Activo = Convert.ToBoolean(dr["Activo"]);
                    u.UsuarioRegistro = dr["UsuarioRegistro"].ToString();

                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally { cmd.Connection.Close(); }
            return u;
        }
        #endregion metodos
    }
}
