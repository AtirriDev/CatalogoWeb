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
    public partial class PantallaUser : System.Web.UI.Page
    {
        public bool EditActivo;
        public bool AgregarActivo;
        int idArticulo;
       
        MarcaNegocio MarcaNegocio = new MarcaNegocio();
        CategoriaNegocio CatNeg = new CategoriaNegocio();

        Articulos seleccionado = null;
        public List<Articulos> listaArt = new List<Articulos>();
        ArticulosNegocio ArtNegocio = new ArticulosNegocio();
        public List<Categorias> listaCategorias = new List<Categorias>();
        public List<Marcas> listaMarcas = new List<Marcas>();
        public List<Articulos> listaBusqueda = new List<Articulos>();

        Usuario UsuarioLogin;

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                if (!IsPostBack)
                {
                    if (Session["Usuario"] != null )
                    {
                        UsuarioLogin = (Usuario)Session["Usuario"];

                        if (UsuarioLogin.IsAdmin == false)
                        {
                            Response.Redirect("Home.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("Home.aspx", false);
                        return;
                    }

                    // si es la primer carga de la pagina !!
                    seleccionado = null;
                    EditActivo = false;
                    AgregarActivo = false;
                    btnCerrar.Visible = false;
                    listaArt = ArtNegocio.listar().OrderBy(x => x.Precio).ToList();

                    Session.Add("ListaArticulos", listaArt); // Guardar la lista en session

                    dgvArticulos.DataSource = listaArt;
                    dgvArticulos.DataBind();

                    listaCategorias = CatNeg.Listar();
                    listaMarcas = MarcaNegocio.Listar();

                    ddlCategorias.DataSource = listaCategorias;
                    ddlMarcas.DataSource = listaMarcas;

                    ddlCategorias.DataTextField = "Descripcion";
                    ddlCategorias.DataValueField = "IdCategoria";
                    ddlMarcas.DataTextField = "Descripcion";
                    ddlMarcas.DataValueField = "IdMarca";

                    ddlCategorias.DataBind();
                    ddlMarcas.DataBind();

                    // boton de confirmar eliminacion y txt desactivados e invisibles

                    btnConfirmarEliminacion.Visible = false;
                    btnConfirmarEliminacion.Enabled = false;
                    txtEliminar.Visible = false;
                    txtNuevaMarca.Visible = false;
                    txtNuevaCategoria.Visible = false;

                }
                else
                {
                    listaArt = ArtNegocio.listar().OrderBy(x => x.Precio).ToList();

                    if (AgregarActivo == true)
                    {
                        AgregarActivo = true;
                    }
                    else
                    {
                        AgregarActivo = false;
                    }

                  
                }
                



                

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }

            
        }

       

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (AgregarActivo == false)
            {
                AgregarActivo = true;
                EditActivo = false;
                btnCerrar.Visible = true;
               
                
            }
            //por las dudas limpiamos articulo seleccionado por si antes de esta accion se selecciono uno y no se finalizo de editar
            Session.Remove("seleccionado");
            btnEliminar.Visible = false;
            LimpiarCajas();

          
        }

        protected void btnConfirmacion_Click(object sender, EventArgs e)
        {
            // Aca tengo que manejar si es un producto editado o si vamos a insertar uno nuevo 

            //validamos la pagina
            Page.Validate();
            if (!Page.IsValid)
                return;

            listaArt = (List<Articulos>)Session["ListaArticulos"];
            Articulos seleccionado = (Articulos)Session["seleccionado"];
            
            try
            {
                if (seleccionado != null)
                {
                    bool Validacion = false;

                    if (!Validaciones.ValidarVacioCargaArticulo(txtPrecio, lblPrecio))
                        Validacion = true;
                    if (!Validaciones.ValidarVacioCargaArticulo(txtProducto, lblNombre))
                        Validacion = true;
                    if (!Validaciones.ValidarVacioCargaArticulo(txtDescripcion, lblDescripcion))
                        Validacion = true;
                    if (!Validaciones.ValidarVacioCargaArticulo(txtCodigo, lblCodigo))
                        Validacion = true;


                    if (Validacion != false)
                    {
                        AgregarActivo = true; // Para que no se cierre el form de Agregar
                        return;
                    }


                    //Update por que estamos editando


                    seleccionado.CodigoArt = txtCodigo.Text;
                    seleccionado.Nombre = txtProducto.Text;
                    seleccionado.Descripcion = txtDescripcion.Text;
                    seleccionado.Imagen = txtImg.Text;


                    seleccionado.Precio = Convert.ToDecimal(txtPrecio.Text.Replace("$", "").Replace(",", "")); // Me tiraba error por el signo pesos aca esta la solucion 
                    
                    seleccionado.Marca.IdMarca = Convert.ToInt32(ddlMarcas.SelectedValue);
                    seleccionado.Categoria.IdCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);
                   
                    ArtNegocio.Modificar(seleccionado);

                    //Borramos el objeto de la session 
                    Session.Remove("seleccionado");
                    EditActivo = false;
                    Response.Redirect("PantallaAdmin.aspx");
                }
                else
                {
                    //Insert por que estamos agregando
                    // Pasamos los valores

                    // validaciones de cambios 
                    // usamos un bool para ver si hubo alguna validacion que funciono y retornamos para que no siga el codigo
                    bool Validacion = false;

                    if (!Validaciones.ValidarVacioCargaArticulo(txtPrecio, lblPrecio))
                        Validacion = true;
                    if (!Validaciones.ValidarVacioCargaArticulo(txtProducto, lblNombre))
                        Validacion = true;
                    if (!Validaciones.ValidarVacioCargaArticulo(txtDescripcion, lblDescripcion))
                        Validacion = true;
                    if (!Validaciones.ValidarVacioCargaArticulo(txtCodigo, lblCodigo))
                        Validacion = true;


                    if (Validacion != false)
                    {
                        AgregarActivo = true; // Para que no se cierre el form de Agregar
                        return;
                    }


                    Articulos nuevo = new Articulos();
                    //Validacion para que no se pueda cargar un producto con el mismo codigo ocea ya existente
                    bool ArticuloExistente = listaArt.Any(x => x.CodigoArt == txtCodigo.Text);

                    if (ArticuloExistente)
                    {
                        lblCodigo.Text = "Codigo de articulo ya existente , por favor verifique la existencia del producto.";
                        AgregarActivo = true; // Para que no se cierre el form de Agregar
                        return;

                    }
                  
                    nuevo.CodigoArt = txtCodigo.Text;
                    nuevo.Nombre = txtProducto.Text;
                    nuevo.Descripcion = txtDescripcion.Text;


                    // Solucionar la puta img de menos de mil caracteres
                    if (txtImg.Text.Length > 1000)
                    {
                        txtImg.Text = "La url no puede superar los 1000 caracteres , intente con otra vez ";
                        AgregarActivo = true; // lo pasamos a true para que no me cierre el formulario de agregar 
                        return;

                    }

                    nuevo.Imagen = txtImg.Text;

                    nuevo.Precio = Convert.ToDecimal(txtPrecio.Text.Replace("$", "").Replace(",", "")); // Me tiraba error por el signo pesos aca esta la solucion 

                    //Debemos crear los objetos marcas y categorias para poder cargar esa propiedad en articulos
                    nuevo.Marca = new Marcas();
                    nuevo.Categoria = new Categorias();


                    nuevo.Marca.IdMarca = int.Parse(ddlMarcas.SelectedValue);
                    nuevo.Categoria.IdCategoria = int.Parse(ddlCategorias.SelectedValue);

                  
                    ArtNegocio.Agregar(nuevo);


                    Response.Redirect("PantallaAdmin.aspx");
                }





























            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                //Response.Redirect("PantallaError.aspx", false);
                throw;
            }
           

            
        }



        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // ACA VAMOS A ACTIVAR EL VERDADERO ELIMINAR
            btnConfirmarEliminacion.Visible = true;
            txtEliminar.Visible = true;
            EditActivo = true;

        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // limpiamos las labels de validaciones 
                lblCodigo.Text = "";
                lblDescripcion.Text = "";
                lblNombre.Text = "";
                lblPrecio.Text = "";

                // obtenemos el articulo seleccionado medianto su dataKey
                idArticulo = Convert.ToInt32(dgvArticulos.SelectedDataKey.Value.ToString());
                btnEliminar.Visible = true;
                btnCerrar.Visible = true;
                EditActivo = true; // Solo activa edición si se selecciona un artículo
                AgregarActivo = false;

                seleccionado = listaArt.Find(x => x.Id == idArticulo);
                //Guardar seleccionado en session para usarlo posteriormente
                Session.Add("seleccionado", seleccionado);

                txtCodigo.Text = seleccionado.CodigoArt.ToString();
                txtProducto.Text = seleccionado.Nombre;
                txtDescripcion.Text = seleccionado.Descripcion;
                txtImg.Text = seleccionado.Imagen;
                if (string.IsNullOrEmpty(seleccionado.Imagen))
                {
                    ImgDetalle.ImageUrl = "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg";
                }
                else
                {
                    ImgDetalle.ImageUrl = txtImg.Text;
                }
               

                txtPrecio.Text = $"${seleccionado.Precio.ToString("N0")}";

                ddlMarcas.SelectedValue = seleccionado.Marca.IdMarca.ToString();
                ddlCategorias.SelectedValue = seleccionado.Categoria.IdCategoria.ToString();

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
            
            
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;

            CargarArticulos();
            dgvArticulos.DataBind();
        }

        public void LimpiarCajas() 
        {

            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtImg.Text = "";
            txtPrecio.Text = "";
            txtProducto.Text = "";
            ImgDetalle.ImageUrl = "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg";
            lblCodigo.Text = "";
            lblDescripcion.Text = "";
            lblNombre.Text = "";
            lblPrecio.Text = "";



        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Aca vamos a poner la logica de la busqueda rapida mediante el TextBox

                listaBusqueda = listaArt.FindAll(x => x.CodigoArt.ToLower().Contains(txtBusqueda.Text.ToLower()) || x.Nombre.ToLower().Contains(txtBusqueda.Text.ToLower()) || x.Marca.Descripcion.ToLower().Contains(txtBusqueda.Text.ToLower())
                            || x.Categoria.Descripcion.ToLower().Contains(txtBusqueda.Text.ToLower()));


                dgvArticulos.DataSource = listaBusqueda;
                dgvArticulos.DataBind();

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
           



        }

        protected void txtImg_TextChanged(object sender, EventArgs e)
        {
            AgregarActivo = true;
            if (txtImg.Text != "")
            {
                ImgDetalle.ImageUrl = txtImg.Text;
            }
            else
            {
                ImgDetalle.ImageUrl = "https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg";
            }
            
        }

        private void CargarArticulos()
        {
            dgvArticulos.DataSource = listaArt; // listaArt es tu lista de artículos
            dgvArticulos.DataBind();
        }

        protected void btnConfirmarEliminacion_Click(object sender, EventArgs e)
        {
            // Aca vamos a eliminar de verdad !!!
            Articulos seleccionado = (Articulos)Session["seleccionado"];
            try
            {
                // Doble validacion 
                if (txtEliminar.Text.ToUpper() == seleccionado.CodigoArt.ToUpper())
                {
                    ArtNegocio.Eliminar(seleccionado.Id);
                    Response.Redirect("PantallaAdmin.aspx", false);
                }
                else
                {
                    // ver como meto un modal aca !!
                    
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
            

            
        }

        protected void txtEliminar_TextChanged(object sender, EventArgs e)
        {
            EditActivo = true;
            if (!string.IsNullOrEmpty(txtEliminar.Text) && txtEliminar.Text.Count() == 3 )
            {
                btnConfirmarEliminacion.Enabled = true;
            }
            else
            {
                btnConfirmarEliminacion.Enabled = false ;
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            AgregarActivo = false;
            EditActivo = false;
            btnCerrar.Visible = false;
        }

        protected void btnAgregarCat_Click(object sender, EventArgs e)
        {
            // Vamos a agregar una nueva categoria 
            txtNuevaCategoria.Visible = true;
            // Para que se mantengan los paneles de edicion o agregar activos
            if (AgregarActivo != true)
            {
                EditActivo = true;
            }
            else
            {
                AgregarActivo = true;
            }
            try
            {
                Categorias nueva = new Categorias();
                if (!Validaciones.ValidarVacio(txtNuevaCategoria))
                {
                    return;
                }
                else
                {
                    if (!listaCategorias.Any(cat => cat.Descripcion == txtNuevaCategoria.Text))
                    {

                        nueva.Descripcion = txtNuevaCategoria.Text;

                        CatNeg.AgregarCat(nueva);

                        //Cargar nuevamente el ddl
                        listaCategorias = CatNeg.Listar();

                        ddlCategorias.DataTextField = "Descripcion";
                        ddlCategorias.DataValueField = "IdCategoria";

                        ddlCategorias.DataSource = listaCategorias;
                        ddlCategorias.DataBind();
                    }

                    txtNuevaCategoria.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            // Agregar una nueva Marca
            txtNuevaMarca.Visible = true;
            // Para que se mantengan los paneles de edicion o agregar activos
            if (AgregarActivo != true)
            {
                EditActivo = true;
            }
            else
            {
                AgregarActivo = true;
            }
            try
            {
                Marcas nueva = new Marcas();
                if (!Validaciones.ValidarVacio(txtNuevaMarca)) {
                   
                    return;
                }
                else
                {
                    if (!listaMarcas.Any(m => m.Descripcion == txtNuevaMarca.Text))
                    {

                        nueva.Descripcion = txtNuevaMarca.Text;

                        MarcaNegocio.Agregar(nueva);

                        //Cargar nuevamente el ddl
                        listaMarcas = MarcaNegocio.Listar();

                        ddlMarcas.DataTextField = "Descripcion";
                        ddlMarcas.DataValueField = "IdMarca";

                        ddlMarcas.DataSource = listaMarcas;
                        ddlMarcas.DataBind();
                    }   

                    txtNuevaMarca.Visible = false;
                    return;
                    
                    

                }


                


                
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
        }
    }
}