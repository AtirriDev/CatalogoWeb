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
    public partial class MiMaster : System.Web.UI.MasterPage
    {
        public bool SessionActiva = false;
        public bool IsUser = false;

        //Listas que vamos a usar
        public List<Articulos> listaArticulos = new List<Articulos>();
        public List<Marcas> listaMarcas = new List<Marcas>();
        public List<Categorias> listaCat = new List<Categorias>();


        //Clases que vamos a usar
        public ArticulosNegocio artNegocio = new ArticulosNegocio();
        public CategoriaNegocio catNegocio = new CategoriaNegocio();
        public MarcaNegocio marcaNegocio = new MarcaNegocio();


        // Usuario del login
        public Usuario UserLogin = null;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            try
            {
                UserLogin = (Usuario)Session["Usuario"];


                CerrarSesion.Visible = false;
                btnFavoritos.Visible = false;


                if (!IsPostBack)
                {
                    hfModalEmail.Value = "false";
                    if (Session["Usuario"] != null)
                    {
                        BtnLogin.ImageUrl = "Documentos/logouser.png";
                        if (UserLogin.IsAdmin == false)
                        {
                            btnFavoritos.Visible = true;
                            
                        }
                        CerrarSesion.Visible = true;
                    }
                   
                    

                    // Llenamos las listas 
                    listaMarcas = marcaNegocio.Listar();
                    listaArticulos = artNegocio.listar();
                    listaCat = catNegocio.Listar();

                    Session.Add("ListaArticulos", listaArticulos);
                }
                else
                {

                    if (Session["Usuario"] != null)
                    {
                        BtnLogin.ImageUrl = "Documentos/logouser.png";
                        if (UserLogin.IsAdmin == false)
                        {
                            btnFavoritos.Visible = true;

                        }
                        CerrarSesion.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }



        }

        protected void BtnLogin_Click(object sender, ImageClickEventArgs e)
        {
            // recuperamos el user de session para verificar
            UserLogin = (Usuario)Session["Usuario"];
            if (UserLogin != null)
            {
                if (UserLogin.IsAdmin != true)
                {
                    Response.Redirect("PantallaUser.aspx", false);
                }
                else
                {
                    Response.Redirect("PantallaAdmin.aspx", false);
                }
               
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
            
           
        }

        protected void btnFavoritos_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Favoritos.aspx", false);
        }

        protected void CerrarSesion_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("Usuario");
            Response.Redirect("Login.aspx", false);
        }

        protected void btnSuscripcion_Click(object sender, EventArgs e)
        {
            string emailDestino = txtSuscripcion.Text;

            // Aca vamos a enviar un mail a la persona que se registre para recibir novededades de la tienda
            try
            {
               

                EmailService servicio = new EmailService();
                servicio.EnviarCorreo(emailDestino, "Suscripción Atirri Tech");

                hfModalEmail.Value = "true";
                //Response.Redirect("Home.aspx", false);






            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);

                //Mandar a la pantalla de error 
                throw;
            }
            
           
        }
    }
}