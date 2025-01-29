using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
   public class MarcaNegocio
    {
        // Listar para llenar el combo box de Marca
        public List<Marcas> Listar()
        {
            
            // Creamos la listar

            List<Marcas> lista = new List<Marcas>();

            
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("SELECT Id , Descripcion FROM MARCAS");

                
                datos.EjecutarLectura();


                // Cargar los datos 

                while (datos.Lector.Read())
                {
                   
                    Marcas aux = new Marcas();
                    aux.IdMarca = (int)datos.Lector["Id"];
                    aux.Descripcion = datos.Lector["Descripcion"].ToString();
                   
                    lista.Add(aux);

                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                datos.CerrarConexion();
            }

           






        }

        public void Agregar(Marcas nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("insert into MARCAS(Descripcion) values(@Desc)");
                datos.setearParametros("@Desc", nuevo.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }



        }

        public List<Marcas> ListarHome()
        {

            // Creamos la listar

            List<Marcas> lista = new List<Marcas>();


            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("SELECT DISTINCT m.Id, m.Descripcion FROM MARCAS m inner join ARTICULOS art on m.id = art.IdMarca");


                datos.EjecutarLectura();


                // Cargar los datos 

                while (datos.Lector.Read())
                {

                    Marcas aux = new Marcas();
                    aux.IdMarca = (int)datos.Lector["Id"];
                    aux.Descripcion = datos.Lector["Descripcion"].ToString();

                    lista.Add(aux);

                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                datos.CerrarConexion();
            }








        }
    }
}
