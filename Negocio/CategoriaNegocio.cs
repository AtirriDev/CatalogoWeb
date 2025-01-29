using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        // Listar para llenar el combo box de Marca
        public List<Categorias> Listar()
        {

            // Creamos la listar

            List<Categorias> lista = new List<Categorias>();


            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("SELECT Id , Descripcion FROM CATEGORIAS");


                datos.EjecutarLectura();


                // Cargar los datos 

                while (datos.Lector.Read())
                {

                    Categorias aux = new Categorias();
                    aux.IdCategoria = (int)datos.Lector["Id"];
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

        public void AgregarCat(Categorias nuevo) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("insert into CATEGORIAS(Descripcion) values(@Desc)");
                datos.setearParametros("@Desc",nuevo.Descripcion);
                datos.ejecutarAccion();
            }
            catch ( Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }
        
        
        
        }

        public List<Categorias> ListarHome()
        {

            // Creamos la listar para el home Solo vamos a listar las categorias en las que tenemos existencias de productos

            List<Categorias> lista = new List<Categorias>();


            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("SELECT DISTINCT cat.Id , cat.Descripcion FROM CATEGORIAS cat inner join ARTICULOS art on cat.id = art.IdCategoria");


                datos.EjecutarLectura();


                // Cargar los datos 

                while (datos.Lector.Read())
                {

                    Categorias aux = new Categorias();
                    aux.IdCategoria = (int)datos.Lector["Id"];
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
