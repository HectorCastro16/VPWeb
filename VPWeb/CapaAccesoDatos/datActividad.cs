using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class datActividad
    {
        #region Singleton

        private static readonly datActividad _Instancia = new datActividad();
        public static datActividad Instancia
        {
            get
            {
                return datActividad._Instancia;
            }
        }

        #endregion Singleton

        #region metodos

        public List<entActividad> ListaActividades() {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entActividad> Lista = null;

            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListaActividades", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entActividad>();
                while(dr.Read()){
                    entActividad a = new entActividad();
                    a.idActividad = Convert.ToInt32(dr["idActividad"]);
                    a.tituloActividad = dr["tituloActividad"].ToString();
                    a.descripcionActividad = dr["descripcionActividad"].ToString();
                    a.fechaActividad = Convert.ToDateTime(dr["fechaActividad"]);
                    a.imagenActividad = dr["imagenActividad"].ToString();
                    a.estadoActividad = dr["estadoActividad"].ToString();
                    a.direccionActividad = dr["direccionActividad"].ToString();
                    a.lugarInicio = dr["lugarInicio"].ToString();
                    a.lugarFin = dr["lugarFin"].ToString();
                    a.horaInicio = dr["horaInicio"].ToString();
                    a.horaFin = dr["horaFin"].ToString();
                    Lista.Add(a);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally {
                cmd.Connection.Close();
            }
            return Lista;
        
        }

        public int InsUpdDelActividad(String cadXML)
        {
            SqlCommand cmd = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsUpdDelActividad", cn);
                cmd.Parameters.AddWithValue("@prmstrCadXML", cadXML);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter m = new SqlParameter("@retorno", DbType.Int32);
                m.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(m);
                cn.Open();
                cmd.ExecuteNonQuery();

                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally {
                cmd.Connection.Close();
            }

        }

        public entActividad DevuelveActividad(Int16 idActividad) {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entActividad a = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDevolverActividad", cn);
                cmd.Parameters.AddWithValue("@prmintIdActividad", idActividad);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    a = new entActividad();
                    a.idActividad = Convert.ToInt32(dr["idActividad"]);
                    a.tituloActividad = dr["tituloActividad"].ToString();
                    a.descripcionActividad = dr["descripcionActividad"].ToString();
                    a.fechaActividad = Convert.ToDateTime(dr["fechaActividad"]);
                    a.imagenActividad = dr["imagenActividad"].ToString();
                    a.estadoActividad = dr["estadoActividad"].ToString();
                    a.direccionActividad = dr["direccionActividad"].ToString();
                    a.lugarInicio = dr["lugarInicio"].ToString();
                    a.lugarFin = dr["lugarFin"].ToString();
                    a.horaInicio = dr["horaInicio"].ToString();
                    a.horaFin = dr["horaFin"].ToString();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally {
                cmd.Connection.Close();
            }
            return a;
        

        }

        #endregion metodos


    }
}
