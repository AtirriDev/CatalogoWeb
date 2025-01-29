using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> ObtenerUsuarios() 
        {
           List<Usuario> lista = new List<Usuario>();

            AccesoDatos datos = new AccesoDatos();
            try
            {
                //Seteamos consulta
                datos.SetConsulta("select Id,email,pass,nombre,apellido,urlImagenPerfil,admin from USERS");
                //Ejecutamos lectura
                datos.EjecutarLectura();

                while (datos.Lector.Read())// mientras el lector este leyendo 
                {
                    Usuario user = new Usuario(); // nuevo objeto usuario para llenar

                    //pasamos los datos 
                    user.Id = (int)datos.Lector["Id"];
                    user.Email = (string)datos.Lector["email"];
                    user.Contraseña = (string)datos.Lector["pass"];
                    user.IsAdmin = (bool)datos.Lector["admin"];
                    //Los proximos campos pueden llegar a venir null de la base de datos entonces comprobamos 
                    user.Nombre = datos.Lector["nombre"] == DBNull.Value ? null : (string)datos.Lector["nombre"];
                    user.Apellido = datos.Lector["apellido"] == DBNull.Value ?  null : (string)datos.Lector["apellido"];
                    user.UrlImagen = datos.Lector["urlImagenPerfil"] == DBNull.Value ? null : (string)datos.Lector["urlImagenPerfil"];

                    lista.Add(user);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { datos.CerrarConexion(); }


            return lista;
        }

        public void ActualizarPerfil(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Usuario usuario = user;

                string consulta = "Update USERS set nombre = @nom , apellido = @ape ,urlImagenPerfil = @img where Id = @id ";
                datos.SetConsulta(consulta);

                //Usamos la funcion Verificar valor para que se fije si es null para poner DbNull
                //Tenemos que hacerle un casteo explicito que es un object para que no se ponga en forra 
                datos.setearParametros("@nom",(Object)VerificarValor(user.Nombre));
                datos.setearParametros("@ape", (Object)VerificarValor(user.Apellido));
                datos.setearParametros("@img", (Object)VerificarValor(user.UrlImagen));

                datos.setearParametros("@id", Convert.ToInt32(user.Id));

                // Ejecutamos 
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }
        }
        public void NuevoUsuario(Usuario user) 
        {
            //ACA VAMOS A REGISTRAR EL NUEVO USUARIO EL CUAL SOLO PUEDE SER USUARIO COMUN 
            AccesoDatos datos = new AccesoDatos();
            try
            {
                Usuario nuevo = user;
                string consulta = "INSERT INTO USERS (email, pass, nombre, apellido, urlImagenPerfil, admin) VALUES (@email, @pass, @nom, @ape, @img, 0)";
                datos.SetConsulta(consulta);
                //CAMPOS QUE NO PUEDEN TENER DB NULL
                datos.setearParametros("@email",nuevo.Email);
                datos.setearParametros("@pass",nuevo.Contraseña);
              
                // Los proximos cambios verificamos para ver su valor y asi insertar o no DbNull
                datos.setearParametros("@nom",(Object)VerificarValor(nuevo.Nombre));
                datos.setearParametros("@ape", (Object)VerificarValor(nuevo.Apellido));
                datos.setearParametros("@img", (Object)VerificarValor(nuevo.UrlImagen));

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }
          
            
        
        
        }
        //METODO PARA VERIFICAR VALORES E INSERTAR DBNULL SI ES QUE CORRESPONDE
        public object VerificarValor(string valor) 
        {
            if (string.IsNullOrEmpty(valor))
            {
                return DBNull.Value;
            }
            else
            {
                return valor;
            }
        }
        public List<Articulos> FavoritosUsuario(int IdUsuario)
        {
            List<Articulos> lista = new List<Articulos>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                String consulta = "select A.id , A.Codigo,A.Nombre,A.Descripcion,A.IdMarca,A.IdCategoria,A.ImagenUrl , A.Precio from Favoritos F inner join Articulos A on F.IdArticulo = A.Id where IdUser = @IdUser";

                datos.SetConsulta(consulta);

                datos.setearParametros("@IdUser", IdUsuario);

                datos.EjecutarLectura();

                while (datos.Lector.Read()) // Mientras el lector lea
                {
                    Articulos aux = new Articulos(); // creamos el objeto y lo cargamos
                    aux.Marca = new Marcas();
                    aux.Categoria = new Categorias();

                    aux.Id = (int)datos.Lector["id"];
                    aux.CodigoArt = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (Decimal)datos.Lector["Precio"];

                    // validamos si la imagen es null


                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagen = datos.Lector["ImagenUrl"].ToString();

                    }


                    aux.Marca.IdMarca = (int)datos.Lector["IdMarca"];


                    aux.Categoria.IdCategoria = (int)datos.Lector["IdCategoria"];


                    //Agregamos el articulo a la lista
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }






        }

        public void EliminarFavoritos(int id, int iduser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "delete from Favoritos where IdArticulo = @idArt and IdUser= @idUser";
                datos.SetConsulta(consulta);
                datos.setearParametros("@idArt", id);
                datos.setearParametros("@idUser", iduser);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }

        }
        public void AgregarFavoritos(int iduser, int idart)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Insert into Favoritos values (@idUser , @idArt) ";
                datos.SetConsulta(consulta);
                datos.setearParametros("@idUser", iduser);
                datos.setearParametros("@idArt", idart);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.CerrarConexion(); }

        }
    }
}
