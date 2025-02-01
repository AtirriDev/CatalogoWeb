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
    
    public partial class PantallaUser1 : System.Web.UI.Page
    {
        
        
        Usuario UsuarioLogin;

        UsuarioNegocio UserNeg = new UsuarioNegocio();
        List<Usuario> listaUsers = new List<Usuario>();


        //Bandera para ocultar el input que es un fucking html
        public bool MostrarInputFile = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                UsuarioLogin = (Usuario)Session["Usuario"];

                if (UsuarioLogin == null)
                {
                    Response.Redirect("Home.aspx", false);
                    return;
                }

                if (UsuarioLogin.IsAdmin)
                {
                    Response.Redirect("Home.aspx", false);
                }


                txtNombre.Enabled = false;
                txtApellido.Enabled = false;
                txtEmail.Enabled = false;
                txtPass.Enabled = false;
               
                btnGuardarCambios.Visible = false;




                CargarDatosUsuario(UsuarioLogin, txtNombre, txtApellido, txtEmail, txtPass, imgUsuario);









            }
            
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            // Ponemos en true la bandera 
            MostrarInputFile = true;
            // Habilitamos los botones para Editar
            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            
            
            //txtImg.Enabled = true;
           
            btnEditar.Enabled = false;
            btnGuardarCambios.Visible = true;

            // el mail no se activa por que no se puede cambiar
            // Pensar otro mecanismo para cambiar la contraseña desde otro boton no aca
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            try
            {
                //Aca vamos a hacer un update en el usuario
                UsuarioLogin = (Usuario)Session["Usuario"];

                // Buscar la ruta para buscar la carpeta donde voy a guardar la imagen de perfil si la cargan desde la pc
                // para guardar el archivo vamos a modificar ese ruta agregando el nombre de la imagen 
              
                string ruta = Server.MapPath("./Documentos/"); // devolveria la ruta desde donde estoy parado eso seria CATALOGO WEB


                
               

                // voy a usar un objeto usuario como aux para probar 
                Usuario aux = new Usuario();
                aux.Id = UsuarioLogin.Id; // le doy el id del usuario logeado para que sobre este haga el update
                aux.Email = UsuarioLogin.Email;
                aux.Contraseña = UsuarioLogin.Contraseña;
                // dsp ya le paso todos los campos de los txt para que sobreeescriba
                aux.Nombre = string.IsNullOrEmpty(txtNombre.Text) ? null : txtNombre.Text;
                aux.Apellido = string.IsNullOrEmpty(txtApellido.Text) ? null : txtApellido.Text;


               

                // Si se selecciona algo en el input
                if (txtFileImage.PostedFile != null && txtFileImage.PostedFile.ContentLength > 0)
                {
                    

                    txtFileImage.PostedFile.SaveAs(ruta + "Usuario-" + $"id__{UsuarioLogin.Id.ToString()}" + ".jpg"); // hacemos un nombre dinamico con el id para que no se repitan 

                    aux.UrlImagen = "Usuario-" + $"id__{UsuarioLogin.Id.ToString()}" + ".jpg";

                    

                }
                else
                {
                    aux.UrlImagen = UsuarioLogin.UrlImagen;
                }

                


                //PENSAR COMO HACER PARA QUE CAMBIE LA CONTRASEÑA . EL MAIL NO SE PUEDE CAMBIAR !!!
                UserNeg.ActualizarPerfil(aux);


                // Limpiar caché y forzar recarga de la imagen . por que no me la actualizaba aunque se cambiaba en la carpeta documentos
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();


                // pasamos la bandera del input a false para q se oculte
                MostrarInputFile = false;
                Response.Redirect("PantallaUser.aspx",false);



            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                throw;
            }


        }

        // metodo para cargar los datos del Usuario
        public void CargarDatosUsuario(Usuario user, TextBox nombre, TextBox ape, TextBox email, TextBox pass , Image urlimage)
        {
            try
            {
                Usuario UsuarioLogin = new Usuario();
                listaUsers = UserNeg.ObtenerUsuarios();
                UsuarioLogin = listaUsers.Find(x => x.Id == user.Id);

                //Contraseña y pass nunca van a ser null en teoria 
                email.Text = UsuarioLogin.Email.ToString();
                pass.Text = UsuarioLogin.Contraseña.ToString();

                //Verificamos null en los campos que pueden ser null
                nombre.Text = string.IsNullOrEmpty(UsuarioLogin.Nombre) ? "" : UsuarioLogin.Nombre.ToString();
                ape.Text = string.IsNullOrEmpty(UsuarioLogin.Apellido) ? "" : UsuarioLogin.Apellido.ToString();






                // verificamos la img si es null mostramos el template de sin usuario 
                if (UsuarioLogin.UrlImagen == null)
                {
                    // si es null mostramos la imagen predeterminada 
                    imgUsuario.ImageUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png";





                }
                else
                {


                    //Cargar el control img 
                    // Para acceder a una carpeta siempre se utiliza virgulilla  ~
                    imgUsuario.ImageUrl = $"~/Documentos/{UsuarioLogin.UrlImagen}?v={DateTime.Now.Ticks}";


                }
            }
            catch (Exception ex )
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);

            }
           

            



        }

        
    }

}