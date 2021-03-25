using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Windows.Forms;
namespace Viaro
{
    class Conexion
    {
        public OdbcConnection nuevaConexion() 
        {
            OdbcConnection conectar = new OdbcConnection("Dsn=dsnColegio");
            conectar.Open();
            return conectar;
        }
    }
}
