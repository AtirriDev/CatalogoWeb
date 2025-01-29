using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using Dominio;
using System.Data.SqlClient;







namespace Negocio
{
    public class ArticulosNegocio
    {

        public List<Articulos> listar()
        {
            //Creamos la lista 
            List<Articulos> lista = new List<Articulos>();

            AccesoDatos datos = new AccesoDatos();

            //Configurar la conexion dentro de un Try 
            try
            {

                datos.SetConsulta("Select A.Id ,Codigo , Nombre, A.Descripcion , ImagenUrl ,Precio,C.Descripcion Categoria , M.Descripcion Marca , A.IdMarca , A.IdCategoria From ARTICULOS A , CATEGORIAS C , MARCAS M  Where A.IdMarca = M.Id And A.IdCategoria = C.Id");
                datos.EjecutarLectura();
                

                // Mediante una repetitiva while leemos y agregamos a la lista 
                while (datos.Lector.Read())
                {

                    
                    Articulos aux = new Articulos();
                    aux.Marca = new Marcas();
                    aux.Categoria = new Categorias();

                    // Pasamos los datos a cada campo correspondiente
                    aux.Id= (int)datos.Lector["Id"];
                    aux.CodigoArt = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // validamos si la imagen es null


                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagen = datos.Lector["ImagenUrl"].ToString();

                    }


                    aux.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Precio = (Decimal)datos.Lector["Precio"];






                    // Agregamos el pokemon a la lista
                    lista.Add(aux);
                }



                // Retornamos la lista 
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // cerramos la conexion
                datos.CerrarConexion(); 
            }

        }


        public void Agregar(Articulos nuevo) 
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("insert into ARTICULOS(Codigo , Nombre , Descripcion  ,ImagenUrl, IdCategoria , IdMarca , Precio) values (@cod , @nom , @desc , @imagen ,@idcat,@idmarca,@precio)");
                
                datos.setearParametros("@cod",nuevo.CodigoArt);
                datos.setearParametros("@nom", nuevo.Nombre);
                datos.setearParametros("@desc", nuevo.Descripcion);
                datos.setearParametros("@imagen", nuevo.Imagen);
                datos.setearParametros("@idcat", nuevo.Categoria.IdCategoria);
                datos.setearParametros("@idmarca", nuevo.Marca.IdMarca);
                datos.setearParametros("precio", nuevo.Precio);




                datos.ejecutarAccion();


            }
            catch ( Exception ex)
            {

                throw ex ;
            }
            finally { datos.CerrarConexion(); }
        
        
        
        }


        public void Modificar(Articulos modificar) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("update ARTICULOS set Codigo = @cod, Nombre = @nombre, Descripcion = @desc, ImagenUrl = @imagen, IdMarca = @idmarca, IdCategoria = @idcat , Precio = @precio where Id = @idarticulo");
               
                datos.setearParametros("@cod",modificar.CodigoArt);
                datos.setearParametros("@nombre",modificar.Nombre);
                datos.setearParametros("@desc",modificar.Descripcion);
                datos.setearParametros("@imagen",modificar.Imagen);
                datos.setearParametros("@idmarca",modificar.Marca.IdMarca);
                datos.setearParametros("@idcat",modificar.Categoria.IdCategoria);
                datos.setearParametros("@precio",modificar.Precio);
                datos.setearParametros("@idarticulo",modificar.Id);

                datos.ejecutarAccion();

            }
            catch ( Exception ex)
            {

                throw ex ;
            }
            finally { datos.CerrarConexion(); }
        
        
        
        }

        public void Eliminar(int id) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("delete from ARTICULOS where id = @idArt");
                datos.setearParametros("@idArt",id);
                datos.ejecutarAccion();
            }
            catch ( Exception ex)
            {

                throw ex ;
            }
            finally { datos.CerrarConexion(); }

        }

        public List<Articulos> Filtar (string campo , string criterio , string filtro , decimal rango) 
        {
            List<Articulos> listafiltrada = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion , ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca, A.IdMarca, A.IdCategoria From ARTICULOS A, CATEGORIAS C, MARCAS M  Where A.IdMarca = M.Id And A.IdCategoria = C.Id And ";

                switch (campo)
                {
                    case "Descripcion":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.Descripcion like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "A.Descripcion like '%" + filtro + "'";
                                break;

                            default:
                                consulta += "A.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Categoria":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "C.Descripcion like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "C.Descripcion like '%" + filtro + "'";
                                break;

                            default:
                                consulta += "C.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    default:
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "Precio > " + rango;
                                break;
                            case "Menor a":
                                consulta += "Precio < " + rango;
                                break;
                            default:
                                consulta += "Precio = " + filtro;
                                break;
                        }
                        break;
                }

                datos.SetConsulta(consulta);
                datos.EjecutarLectura();

                while (datos.Lector.Read())
                {


                    Articulos aux = new Articulos();
                    aux.Marca = new Marcas();
                    aux.Categoria = new Categorias();

                    // Pasamos los datos a cada campo correspondiente
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArt = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // validamos si la imagen es null


                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagen = datos.Lector["ImagenUrl"].ToString();

                    }


                    aux.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Precio = (Decimal)datos.Lector["Precio"];






                    // Agregamos el pokemon a la lista
                    listafiltrada.Add(aux);
                }


                return listafiltrada;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }


        }

    

        public List<Articulos> FiltroNuevo(int? idMarca, int? idCat, decimal? precioDesde, decimal? precioHasta)
        {
            List<Articulos> listaFiltrada = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion , ImagenUrl, Precio, C.Descripcion Categoria, M.Descripcion Marca, A.IdMarca, A.IdCategoria FROM ARTICULOS A INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id";



                // Lista para construir las condiciones dinámicamente
                List<string> condiciones = new List<string>();

                // Agregar condiciones según los parámetros proporcionados
                if (idMarca.HasValue) 
                {
                    condiciones.Add("A.IdMarca = @marca");
                   
                }
                if (idCat.HasValue)
                {
                    condiciones.Add("A.IdCategoria = @cat");
                   
                }

                if (precioDesde.HasValue)
                {
                    condiciones.Add("A.Precio >= @Desde");
                   
                }
                if (precioHasta.HasValue)
                {
                    condiciones.Add("A.Precio <= @Hasta");
                   
                }

            





                // Combinar todas las condiciones si existen
                if (condiciones.Count > 0)
                {
                    consulta += " WHERE " + string.Join(" AND ", condiciones);
                }

              
                // Configurar la consulta y los parámetros
                datos.SetConsulta(consulta);
                if (idMarca.HasValue)
                    datos.setearParametros("@marca", idMarca.Value);
                if (idCat.HasValue)
                    datos.setearParametros("@cat", idCat.Value);
                if (precioDesde.HasValue)
                    datos.setearParametros("@Desde", precioDesde.Value);
                if (precioHasta.HasValue)
                    datos.setearParametros("@Hasta", precioHasta.Value);


                datos.EjecutarLectura();

                // Construir la lista de artículos
                while (datos.Lector.Read())
                {
                    Articulos aux = new Articulos();
                    aux.Marca = new Marcas();
                    aux.Categoria = new Categorias();

                    // Pasamos los datos a cada campo correspondiente
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArt = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    // validamos si la imagen es null


                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagen = datos.Lector["ImagenUrl"].ToString();

                    }


                    aux.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Precio = (Decimal)datos.Lector["Precio"];






                   //Agregamos a la nueva lista

                    listaFiltrada.Add(aux);
                }

                return listaFiltrada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

       
    }
}
