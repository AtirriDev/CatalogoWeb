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
    public partial class Favoritos : System.Web.UI.Page
    {
        public Articulos articulo;
        public Usuario UsuarioLogin;

        public UsuarioNegocio UsuarioNeg = new UsuarioNegocio();
        public List<Articulos> ListaFavoritosUsuario = new List<Articulos>();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
               
                
                if (!IsPostBack)
                {
                    //Primera carga de la web
                    //Verificar que el usuario este logeado y que no sea admin
                    if (Session["Usuario"]!= null )
                    {
                        UsuarioLogin = (Usuario)Session["Usuario"];
                        if (UsuarioLogin.IsAdmin == false)
                        {
                            int idUser = UsuarioLogin.Id;
                            ListaFavoritosUsuario = UsuarioNeg.FavoritosUsuario(idUser);
                           
                                repTabla.DataSource = ListaFavoritosUsuario;
                                repTabla.DataBind();
                            
                           
                        }
                        
                        
                      
                            
                    }
                }
                
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
            
        }

      

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturar el argumento del boton

                int idProducto = int.Parse(((Button)sender).CommandArgument);
                Usuario user = (Usuario)Session["Usuario"];
                int idUser = user.Id;
                //Eliminar articulo seleccionado
                UsuarioNeg.EliminarFavoritos(idProducto, idUser);



                //Volver a cargar la pagina
                Response.Redirect("Favoritos.aspx", false);
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
           
            
        }

       


    }
}