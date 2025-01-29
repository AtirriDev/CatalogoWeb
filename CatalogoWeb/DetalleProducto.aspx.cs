using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;


namespace CatalogoWeb
{
    public partial class LoginRegistro : System.Web.UI.Page
    {
        public List<Articulos> ListaArt = new List<Articulos>();
        public List<Articulos> ListaSeleccionado = new List<Articulos>();
        public  Articulos Seleccionado = new Articulos();
        public List<Articulos> listaAuxiliar = new List<Articulos>();
        public UsuarioNegocio UserNeg = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            
             


            try
            {
                //Lista con todos los productos que vamos a utilizar para otras listas
                ListaArt = (List<Articulos>)Session["ListaArticulos"];
               
                if (!IsPostBack)
                {
                   
                    MostrarProducto();




                }
                else
                {
                    MostrarProducto();
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx",false);
            }

            




            



           

            
            
        }

        protected void btnAgregarFavoritos_Click(object sender, EventArgs e)
        {
            // Aca vamos a tener que hacer un Insert en la tabla favoritos 
            // este insert debe tener idUser , IdArticulo .

            Seleccionado = (Articulos)Session["seleccionado"];
            try
            {
                if ((Usuario)Session["Usuario"] != null)
                {
                    // Si hay un usuario procedemos a la logica para ver si agrego o ya esta agregado 
                    // Pero si el boludito es admin no tenemos que dejar que agregue nada 

                    Usuario UserLogin = (Usuario)Session["Usuario"];

                    if (UserLogin.IsAdmin == true)
                    {
                        // Pasamos el valor al hidden
                        hfAdmin.Value = "true";
                        return; // retornamos para que frene la ejecucion
                    }
                    int IdUser = UserLogin.Id;
                    int idProducto = Seleccionado.Id;
                    // Agregar a favoritos 
                    //Traerme la lista de favoritos de ese usuario para validar que el producto no este ya agregado
                    List<Articulos> listaFavoritosUsuario = UserNeg.FavoritosUsuario(IdUser);


                    if (listaFavoritosUsuario.Any(item => item.Id == idProducto))
                    {
                        hfShowModal.Value = "true"; // Producto ya existe

                    }
                    else
                    {
                        // Producto no está en la lista, agregarlo
                        UserNeg.AgregarFavoritos(IdUser, idProducto);
                        hfShowModal.Value = "false"; // Producto agregado

                    }

                }
                else
                {
                    // Estrablecemos el hidden de user a false ;
                    hfUser.Value = "false";
                    hfShowModal.Value = "true"; // Mostrar el modal

                    //// Lo mandamos al login por salame 
                    //Response.Redirect("LoginRegistro.aspx", false);
                }









            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }



        }

        public void MostrarProducto() 
        {
            if (Request.QueryString["id"] != null)
            {
               
                string idArticulo = Request.QueryString["id"];
                // Lista para buscar el producto seleccionado
                ListaSeleccionado = ListaArt;
                Seleccionado = ListaSeleccionado.Find(x => x.Id == int.Parse(idArticulo));

                foreach (Articulos aux in ListaArt)
                {
                    if (aux.Id == int.Parse(idArticulo))
                    {
                        Seleccionado.Id = aux.Id;
                        Seleccionado.Nombre = aux.Nombre;
                        Seleccionado.CodigoArt = aux.CodigoArt;
                        Seleccionado.Precio = aux.Precio;
                        Seleccionado.Descripcion = aux.Descripcion;
                        Seleccionado.Imagen = aux.Imagen;
                        Seleccionado.Marca.Descripcion = aux.Marca.Descripcion;
                        Seleccionado.Categoria.Descripcion = aux.Categoria.Descripcion;

                    }
                }

                Session.Add("Seleccionado", Seleccionado);
                String Categoria = Seleccionado.Categoria.Descripcion;


                //Lista que voy a utilizar para mostrar los producrtos de la misma categoria como sugerencia
                listaAuxiliar = ListaArt.Where(x => x.Categoria.Descripcion == Categoria && x.Id != Seleccionado.Id).Take(2).ToList();

            }
            else
            {
                Response.Redirect("Home.aspx", false);
                return;
            }
            


        }
    }
}