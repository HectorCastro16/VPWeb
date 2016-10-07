using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class negUsuario
    {
        #region Singleton
        private static readonly negUsuario _Instancia = new negUsuario();
        public static negUsuario Instancia
        {
            get
            {
                return negUsuario._Instancia;
            }
        }
        #endregion Singleton

        #region metodos

        public entUsuario VerificarAccesoIntranet(String prmstrLogin, String prmstrPassw)
        {
            try
            {
                if (prmstrLogin == null)
                {
                    throw new ApplicationException("Debe Ingresar el Usuario");
                }
                if (prmstrPassw == null)
                {
                    throw new ApplicationException("Debe Ingresar el Password");
                }
                entUsuario u = datUsuario.Instancia.VerificarAccesoIntranet(prmstrLogin, prmstrPassw);
                if (u == null)
                {
                    throw new ApplicationException("Usuario y/o Password Incorrectos");
                }
                if (!u.Activo)
                {
                    throw new ApplicationException("Usuario Inactivo");
                }
                DateTime fechaHoy = DateTime.Now;
                if (DateTime.Compare(fechaHoy, u.FechaHasta) > 0)
                {
                    throw new ApplicationException("Ha expirado su tiempo");
                }

                return u;
            }
            catch (ApplicationException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        #endregion metodos
    }
}
