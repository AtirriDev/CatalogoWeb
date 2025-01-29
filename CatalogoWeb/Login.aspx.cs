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
    public partial class Login : System.Web.UI.Page
    {
        public List<Usuario> ListaUsuarios = new List<Usuario>();
        private UsuarioNegocio UserNeg = new UsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // si es la primer carga 
                // primera carga de la pagina

                //Ponemos todos los hf que usamos para mostrar los modales en false
                hfLogin.Value = "false";
                hfLoginFallido.Value = "false";
                hfRegistroExitoso.Value = "false";
                hfCamposVacios.Value = "false";
                try
                {
                    //Llenamos la lista de usuario y la guardamos en session
                    ListaUsuarios = UserNeg.ObtenerUsuarios();
                    Session.Add("ListaUsuarios", ListaUsuarios);
                   
                }
                catch (Exception ex)
                {

                    Session.Add("Error", ex.Message);
                    Response.Redirect("PantallaError.aspx", false);
                }
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            //aca voy a verificar el login
            List<Usuario> lista = (List<Usuario>)Session["ListaUsuarios"];
            try
            {
                if (!Validaciones.ValidarVacio(txtEmail) || !Validaciones.ValidarVacio(txtContraseña))
                {
                    hfCamposVacios.Value = "true";
                    return;
                }
                if (!string.IsNullOrEmpty(txtContraseña.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    string email = txtEmail.Text;
                    string pass = txtContraseña.Text;

                    //Creo un objeto usuario por que lo voy a obtener y guarda en session
                    Usuario UsuarioLogin = new Usuario();
                    //Ahora vamos a verificar 
                    foreach (var item in lista)
                    {
                        if (item.Email.ToLower() == email.ToLower() && item.Contraseña.ToLower() == pass.ToLower())
                        {
                            hfLogin.Value = "true";

                            UsuarioLogin = item;

                            Session.Add("Usuario", UsuarioLogin);


                        }
                        else
                        {
                            hfLoginFallido.Value = "true"; // Login fallido
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

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            ////ACA VOY A HACER EL ALTA DEL NUEVO USUARIO
            Usuario nuevo = new Usuario(); // Usuario que vamos a agregar

            nuevo.Email = txtEmailRegistro.Text;
            nuevo.Contraseña = txtPassRegistro.Text;
            nuevo.Nombre = txtNombre.Text;
            nuevo.Apellido = txtApellido.Text;
            nuevo.UrlImagen = null; // mandamos un null para que cargue DbNull en este campo

            try
            {
                //metodo agregar 
                UserNeg.NuevoUsuario(nuevo);
                hfRegistroExitoso.Value = "true";
                txtEmailRegistro.Text = "";
                txtPassRegistro.Text = "";
                
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }



        }
    }
    
}