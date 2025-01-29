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
    public partial class Home : System.Web.UI.Page
    {

           public List<Articulos> listaArticulos = new List<Articulos>();
           public List<Categorias> listaCat = new List<Categorias>();
           public List<Marcas> listaMarcas = new List<Marcas>();


        //Clases que vamos a usar
        public ArticulosNegocio artNegocio = new ArticulosNegocio();
        public CategoriaNegocio catNegocio = new CategoriaNegocio();
        public MarcaNegocio marcaNegocio = new MarcaNegocio();
        public UsuarioNegocio UserNeg = new UsuarioNegocio();

        //Objeto del usario que se va a logear
       public Usuario usuarioLogin;

        protected void Page_Load(object sender, EventArgs e)
        {

           


            if (!IsPostBack)
            {
               

                // Frente a la primer carga llenamos las listas 
                try
                {
                   
                    // Llenamos las listas 
                    listaMarcas = marcaNegocio.ListarHome();
                    listaArticulos = artNegocio.listar().OrderBy(x => x.Precio).ToList(); 
                    listaCat = catNegocio.ListarHome();

                    RepArticulos.DataSource = listaArticulos;
                    RepArticulos.DataBind();

                }
                catch ( Exception ex )
                {
                   
                    Session.Add("Error", ex.Message); 
                    Response.Redirect("PantallaError.aspx", false);
                }
            }
            else
            {

               
                // Frente al postback tmb carga llenamos las listas 
                try
                {
                   
                   
                    // Llenamos las listas 
                    listaMarcas = marcaNegocio.ListarHome();
                    listaArticulos = artNegocio.listar().OrderBy(x => x.Precio).ToList(); ;
                    listaCat = catNegocio.ListarHome();

                }
                catch (Exception ex)
                {

                    Session.Add("Error", ex.Message);
                    Response.Redirect("PantallaError.aspx", false);
                }
            }

        
        }

       

        protected void btnfiltrar_Click(object sender, EventArgs e)
        {
            //Volvemos a llenar la lista por que sino buscaba en una lista incompleta  por que quedaba con anteriores busquedas
            listaArticulos = artNegocio.listar();
            lblSinResultados.Text = "";


            try
            {
                
                // Captura los valores seleccionados , nos fijamos si son nulls o vacias . le asignamos null en ese caso y sino el valor
                int? idMarca = string.IsNullOrEmpty(RdbMarcaValor.Value) ? (int?)null : int.Parse(RdbMarcaValor.Value);
                int? idCategoria = string.IsNullOrEmpty(rdbCatValor.Value) ? (int?)null : int.Parse(rdbCatValor.Value);
                decimal? precioDesde = string.IsNullOrEmpty(TxtDesde.Text) ? (decimal?)null : decimal.Parse(TxtDesde.Text);
                decimal? precioHasta = string.IsNullOrEmpty(txtHasta.Text) ? (decimal?)null : decimal.Parse(txtHasta.Text);

                if (!idMarca.HasValue && !idCategoria.HasValue && !precioDesde.HasValue && !precioHasta.HasValue)
                {
                    // Si no se aplican filtros, se muestra toda la lista
                    listaArticulos = artNegocio.listar().OrderBy(x=> x.Precio).ToList();
                   
                    //La guardamos en session para el boton de ordenamiento
                    Session.Add("ListaParaOrdenar",listaArticulos);
                   
                    RepArticulos.DataSource = listaArticulos;
                    RepArticulos.DataBind();
                }
                else
                {
                    // Filtra los artículos con los valores seleccionados
                    listaArticulos = artNegocio.FiltroNuevo(idMarca, idCategoria, precioDesde, precioHasta);
                  
                    //La guardamos en session para el boton de ordenamiento
                    Session.Add("ListaParaOrdenar", listaArticulos);
                    
                    RepArticulos.DataSource = listaArticulos.OrderBy(x=> x.Precio).ToList();
                    RepArticulos.DataBind();
                   
                    if (listaArticulos.Count == 0)
                    {
                        lblSinResultados.Text = "No hay articulos disponibles con los filtros seleccionados...";
                    }
                }

                //Ponemos un marcador de filtros 
                if (!string.IsNullOrEmpty(RdbMarcaValor.Value))
                {
                    lblFiltro.Text = "Etiquetas:";
                    txtSeleccionMarca.Text = listaMarcas.Find(m => m.IdMarca.ToString() == RdbMarcaValor.Value).ToString();
                }
                else
                {
                   
                    txtSeleccionMarca.Text = "";
                }

                if (!string.IsNullOrEmpty(rdbCatValor.Value))
                {
                    string cat;
                    cat = listaCat.Find(c => c.IdCategoria.ToString() == rdbCatValor.Value).ToString();

                    if (!string.IsNullOrEmpty(RdbMarcaValor.Value))
                    {
                       
                        txtSeleccionCategoria.Text = $"> {cat}";
                       
                    }
                    else
                    {
                        lblFiltro.Text = "Etiquetas:";
                        txtSeleccionCategoria.Text = cat;



                    }
                    
                   
                }
                else
                {
                    
                    txtSeleccionCategoria.Text = "";
                }



                // Limpar los rdb para que no interfiera con busquedas posteriores 
                RdbMarcaValor.Value = string.Empty;
                rdbCatValor.Value = string.Empty;

                //poner el ddl en ascendente para no genera confuciones

                ddlFiltroPrecios.SelectedValue = "Precio Ascendente";
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }

          
        }

        protected void btnResetCards_Click(object sender, EventArgs e)
        {
            try
            {
                //Para el reset vamos a cargar nuevamente todas las cards a la lista
                Session.Remove("ListaParaOrdenar");

                //vamos a llenar el repeater
                RepArticulos.DataSource = listaArticulos;
                RepArticulos.DataBind();

                //poner el ddl en ascendente para no genera confuciones

                ddlFiltroPrecios.SelectedValue = "Precio Ascendente";

                lblFiltro.Text = "";
                txtSeleccionCategoria.Text = "";
                txtSeleccionMarca.Text = "";
                lblSinResultados.Text = "";
                txtHasta.Text = "";
                TxtDesde.Text = "";
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
           
        }

        protected void ddlFiltroPrecios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Aca vamos a tener que tomar una lista de session , dado que sino no me hace este ordenamiento si habia una seleccion de filtro previa 
                if (ddlFiltroPrecios.Text == "Precio Ascendente")
                {
                    // VALIDAMOS SI HAY UNA LISTA PARA ORDENAR QUE SE DA SI SE APLICO FILTRO ESPECIFICO
                    if (Session["ListaParaOrdenar"] != null)
                    {
                        List<Articulos> ListaParaOrdenar = (List<Articulos>)Session["ListaParaOrdenar"];
                        RepArticulos.DataSource = ListaParaOrdenar.OrderBy(x => x.Precio).ToList();
                        RepArticulos.DataBind();
                        return;
                    }

                    //SI NO SE MUESTRA LA LISTA ORIGINAL ORDENADA 

                    List<Articulos> listaArtDesc = artNegocio.listar().OrderBy(x => x.Precio).ToList();
                    listaArticulos = listaArtDesc;
                    RepArticulos.DataSource = listaArticulos;
                    RepArticulos.DataBind();


                }
                else if (ddlFiltroPrecios.Text == "Precio Descendente")
                {
                    // VALIDAMOS SI HAY UNA LISTA PARA ORDENAR QUE SE DA SI SE APLICO FILTRO ESPECIFICO
                    if (Session["ListaParaOrdenar"] != null)
                    {
                        List<Articulos> ListaParaOrdenar = (List<Articulos>)Session["ListaParaOrdenar"];

                        RepArticulos.DataSource = ListaParaOrdenar.OrderByDescending(x => x.Precio).ToList();
                        RepArticulos.DataBind();
                        return;
                    }

                    //SI NO SE MUESTRA LA LISTA ORIGINAL ORDENADA 
                    List<Articulos> listaArtDesc = artNegocio.listar().OrderByDescending(x => x.Precio).ToList();
                    listaArticulos = listaArtDesc;
                    RepArticulos.DataSource = listaArticulos;
                    RepArticulos.DataBind();


                }
            }
            catch (Exception ex )
            {

                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);

            }
          
        }

        protected void btnAgregarFavoritos_Click(object sender, EventArgs e)
        {
            // Aca vamos a tener que hacer un Insert en la tabla favoritos 
            // este insert debe tener idUser , IdArticulo .
            
            
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
                    int idProducto = int.Parse(((Button)sender).CommandArgument);
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
            catch ( Exception ex )
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("PantallaError.aspx", false);
            }
        }
    }
}