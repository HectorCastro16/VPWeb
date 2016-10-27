﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CapaAccesoDatos
{
    public class Conexion
    {
        #region Singleton
        private static readonly Conexion _Instancia = new Conexion();
        public static Conexion Instancia
        {
            get
            {
                return Conexion._Instancia;
            }
        }
        #endregion Singleton

        #region metodos
        public SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection();
            //cn.ConnectionString = "Data Source=hcserver.database.windows.net; Initial Catalog=bdvpweb;User ID=administrador; Password=BdVPWeb123456"; 
            cn.ConnectionString = "Data Source=.; Initial Catalog=BD_VP;User ID=sa; Password=123456";
            return cn;
        }
        #endregion metodos
    }
}
