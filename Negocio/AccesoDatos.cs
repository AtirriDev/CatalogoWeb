using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        


        // creamos un constructor Acceso a datos para cuando nazca la conexion

        public AccesoDatos()
        {
            // ACA ESTABLECEMOS QUE CUANDO INVOQUEMOS ESTE CONSTRUCTOR YA VA A TENER PREDEFINIDO ESTOS PARAMETROS 
            //conexion = new SqlConnection("server=.\\SQLEXPRESS; database=CATALOGO_WEB_DB; integrated security=true");
            conexion = new SqlConnection(ConfigurationManager.AppSettings["cadenaConexion"]);
            comando = new SqlCommand();
        }

        // METODO DE SETEAR LA CONSULTA

        public void SetConsulta(string consulta)
        {
            // Aca establecemos el metodo de la consulta en este caso Text para sql  
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        //Apertura y ejecucion de la Lectura 

        public void EjecutarLectura()
        {
            // le damos una conexion al comando 
            comando.Connection = conexion;
            try
            {
                // abrimos la conexion 
                conexion.Open();

                // ejecutamos el lector mediante comando

                lector = comando.ExecuteReader();
            }
            catch (Exception)
            {

                throw;
            }



        }

        // Verificamos y cerramos Conexion y lector

        public void CerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }

        // Metodo para ejecutar accion

        public void ejecutarAccion()
        {
            // le damos la conexion al comando 

            comando.Connection = conexion;
            try
            {
                // abrimos la conexion y ejecutamos la consulta 
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        // Metodo para agregar Parametros , este recibe un string y un object

        public void setearParametros(string nombre, object valor)
        {

                // utilizamos la funcion agregar parametros con valores 
                
                comando.Parameters.AddWithValue(nombre, valor);

        }








    }
}
