using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace AgendaMedicamentos
{
   public class ConexionBD
    {
        //3. Declarar el objeto conexion
        public OdbcConnection con { get; set; }

        //4. Programar el constructor
        public ConexionBD()
        {
            //Constructor
            //5. Declarar objeto para acceder al web.config
            System.Configuration.Configuration webConfig;
            //6. Decirle al objeto de configuracion cual web.config
            //debe abrir --> Debe ser el nombre del proyecto
            webConfig = System.Web.Configuration
                .WebConfigurationManager
                .OpenWebConfiguration("/AgendaMedicamentos");
            //7. Declarar objeto para el string de conexion
            System.Configuration.ConnectionStringSettings stringDeConexion;
            //8. Extraer el string de conexion con ayuda del objeto
            //para acceder al web.config
            stringDeConexion = webConfig.ConnectionStrings
                .ConnectionStrings["BDAgendaMedicamentos"];
            //9. Instanciar la conexion usando el string de conexion
            con = new OdbcConnection(stringDeConexion.ToString());
            //10. Abrir la conexion
            con.Open();
        }
    }
}