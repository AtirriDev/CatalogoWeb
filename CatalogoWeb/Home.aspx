<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CatalogoWeb.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Estilos%20Css/EstilosMasterPage.css" rel="stylesheet" />
    <style>
        /* animacion de las cards */
            .card {
                animation: Appear linear;
                animation-timeline: view();
                animation-range: entry 0% cover 40%;
            }

            @keyframes Appear {
                from {
                    opacity: 0;
                    scale:0.5;
                  clip-path: inset(%100 100% 0 0);
                }

                to {
                    opacity: 1;
                    scale:1;
                    clip-path: inset(0 0 0 0);
                }
            }
    </style>
   
   <script>
       function ValorRdb(radio, hiddenFieldId) {
           // Asigna el valor del radio seleccionado al HiddenField correspondiente
           document.getElementById(hiddenFieldId).value = radio.value;
       }

   </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- ACA ARRANCAMOS CON EL BANER QUE SE MUEVE --%>

    <%-- COMIENZO DEL CARROUSEL--%>
    <div id="carouselExampleIndicators" class="carousel slide">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="https://images.fravega.com/f300/320886a93d13d65f972055dd805d162b.jpg.webp" class="d-block w-100" alt="...">
            </div>
            <div class="carousel-item">
                <img src="https://images.fravega.com/f300/76fe5d9c67de23f0d1eb5844d7209e9c.jpg.webp" class="d-block w-100" alt="...">
            </div>
            <div class="carousel-item">
                <img src="https://images.fravega.com/f300/a6b1033759915dfd53d69d0e0f44e891.jpg.webp" class="d-block w-100" alt="...">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>

        <%--  FIN DEL CARROUSEL--%>
    </div>


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <%-- Seguimos con la muestra de los productos mediante cards --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container mb-5 ">
                <div class="row mt-2">
                    <!-- Sidebar -->
                    <div class=" col-lg-3 col-md-4 mb-4">
                        <div class="sidebar p-3 ">
                            <h2>Filtros</h2>
                            <div class="filter-group mb-3">
                                <h4>Marcas</h4>
                                <% 
                                    foreach (Dominio.Marcas marca in listaMarcas)
                                    {
                                %>
                                <label class="d-block">
                                    <input type="radio" name="opcionMarca" value="<%= marca.IdMarca %>" onchange="ValorRdb(this,'<%= RdbMarcaValor.ClientID %>')">
                                    <%: marca.Descripcion %>
                                </label>
                                <% 
                                    }
                                %>
                                <asp:HiddenField ID="RdbMarcaValor" runat="server" />
                            </div>

                            


                            <div class="filter-group mb-3">
                                <h4>Categorias</h4>
                                <% 
                                    foreach (Dominio.Categorias cat in listaCat)
                                    {
                                %>
                                <label class="d-block">
                                    <input type="radio" name="opcionCat" value="<%= cat.IdCategoria %>" onchange="ValorRdb(this,'<%= rdbCatValor.ClientID %>')">
                                    <%: cat.Descripcion %>
                                </label>
                                <% 
                                    }
                                %>
                                <asp:HiddenField ID="rdbCatValor" runat="server" />
                            </div>


                            <div class="filter-group">
                                <h4>Precio</h4>
                                <label class="d-block">
                                    Desde: 
                                    <asp:TextBox ID="TxtDesde" CssClass="form-control " runat="server"></asp:TextBox>

                                </label>
                                <label class="d-block">
                                    Hasta: 
                                    <asp:TextBox ID="txtHasta" CssClass="form-control " runat="server"></asp:TextBox>

                                </label>

                            </div>
                            <asp:Button ID="btnfiltrar" runat="server" Text="Buscar" Cssclass="btn btn-light btn-outline-primary mt-3" OnClick="btnfiltrar_Click" />
                             <asp:Button ID="btnResetCards" runat="server" Text="Reset" Cssclass="btn  btn-warning mt-3" OnClick="btnResetCards_Click" />
                            
                        </div>
                    </div>

                   

                    <div class="col-lg-8 col-md-7">

                        <div class="row d-flex">
                            <div class="col-4">
                                <asp:DropDownList ID="ddlFiltroPrecios" runat="server" CssClass=" btn btn-secondary dropdown-toggle " Style="box-shadow: none; outline: none" OnSelectedIndexChanged="ddlFiltroPrecios_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Precio Ascendente"  />
                                    <asp:ListItem Text="Precio Descendente" />
                                </asp:DropDownList>
                                     
                            </div>
                        </div>
                          <div class="col-6 d-flex align-items-center gap-1 m-2">
                                       
                                            <asp:Label Text="" runat="server" ID="lblFiltro" />
                                            <asp:Label Text="" runat="server" ID="txtSeleccionMarca" CssClass="text-primary" />
                                            <asp:Label Text="" CssClass="text-primary" runat="server" ID="txtSeleccionCategoria" />
                                             
                                    
                                            
                                        
                         </div>
                               <asp:Label Text="" CssClass="text-danger" runat="server" ID="lblSinResultados" />
                               
                               
                           
                             <asp:Label Text="" runat="server" CssClass="text-danger" ID="lblEtiqueta" />    
                         
                        <div class="row mt-1"  ">
                            <%-- -- Desde aca el repetar o foreach--%>
                           
                                        
                            <asp:Repeater ID="RepArticulos" runat="server">
                                <ItemTemplate>
                                    <div class="col-xl-4 col-lg-6 col-md-6 col-sm-12 mb-4 text-center " >
                                       
                                        <!-- Cards  -->
                                        <div class="card h-100 border-0 " style="min-width:250px;" >
                                            <a href="DetalleProducto.aspx?id=<%#Eval("Id") %>">
                                                <img src="<%#Eval("Imagen") %>" class="card-img-top" alt="<%#Eval("Nombre") %>"  onerror="this.src='https://www.shutterstock.com/image-vector/no-image-available-vector-illustration-260nw-744886198.jpg'" style="height: 450px">
                                            </a>


                                            <div class="card-body">
                                                
                                                <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                                 
                                                                
                                               <div class="card-footer  text-center " style="background-color: lavender">

                                                    <p class="price text-black">$<%#Eval("Precio", "{0:N0}") %>  </p>
                                                    <asp:Button Text="Añadir a favoritos" runat="server" CssClass="btn btn-primary" ID="btnAgregarFavoritos" OnClick="btnAgregarFavoritos_Click" CommandName="IdArticulo" CommandArgument='<%#Eval("Id") %>' /> 
                                                   
                                                    
                                                   
                                                </div>
                                                   
                                                

                                                
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                               
                            
                        </div>

                           









                         

                    </div>





                </div>
            </div>
             <asp:HiddenField ID="hfShowModal" runat="server" />
            <asp:HiddenField ID="hfUser" runat="server" />
            <asp:HiddenField ID="hfAdmin" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
   
 <%--  aca finaliza el bloque de cards y filtro--%>
        <div class="modal" tabindex="-1" id="MyModal">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Favoritos</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p class="modal-text text-black"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

   
    
  <script type="text/javascript">
      // Escuchar eventos de actualización parcial
      Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
          // Ejecutar la lógica al finalizar la actualización parcial
          checkAndShowModal();
      });

      // Mostrar el modal basado en el HiddenField
      function checkAndShowModal() {
          var hfShowModal = document.getElementById('<%= hfShowModal.ClientID %>');
          var hfUser = document.getElementById('<%= hfUser.ClientID %>');
          var hfAdmin = document.getElementById('<%= hfAdmin.ClientID %>')
          
          const modalTitle = document.querySelector('#MyModal .modal-title');// seleccionar el boton por selector css
          const ButtonModal = document.querySelector('#MyModal .modal-footer .btn'); // seleccionar el boton por selector css
          const TextModal = document.querySelector('#MyModal .modal-body .modal-text');

          if (hfUser.value === "false") {
              if (hfShowModal.value === "true") {
                  modalTitle.textContent = "Funcion restringida";
                  TextModal.textContent = "Debes iniciar sesion para poder agregar articulos a tu lista de favoritos.";
                  ButtonModal.textContent = "Loguearse"; // le cambiamos para que diga logearse 

                  // le damos funcion de redireccion para logearse 
                  ButtonModal.addEventListener('click', function () {
                      window.location.href = "Login.aspx"; // Cambia la URL según sea necesario
                  });
              }
          }
          else {
              // Aca debemos validar si el usuario es admin o no 

              if (hfAdmin.value === "true") {
                  modalTitle.textContent = "Funcion restringida";
                  TextModal.textContent = "Como admin no tienes lista de favoritos . Puedes ir a la pantalla de administrador para ver y gestionar los articulos";
                  ButtonModal.textContent = "Pantalla de administracion"; // le cambiamos para que diga logearse 

                  // le damos funcion de redireccion para logearse 
                  ButtonModal.addEventListener('click', function () {
                      window.location.href = "PantallaAdmin.aspx"; // Cambia la URL según sea necesario
                  });

              }
              else {
                  // si es usuario normal

                  if (hfShowModal.value === "true") {
                      //SI EL PRODUCTO YA ESTA EN FAVORITOS
                      modalTitle.textContent = "Advertencia";
                      TextModal.textContent = "El producto seleccionado ya se encuentra en tu lista de favoritos.";
                      ButtonModal.textContent = "Mis Favoritos";
                      // le damos funcion de redireccion para logearse 
                      ButtonModal.addEventListener('click', function () {
                          window.location.href = "Favoritos.aspx"; // Cambia la URL según sea necesario
                      });


                  } else if (hfShowModal.value === "false") {
                      //Agregar producto a favoritos

                      modalTitle.textContent = "Favoritos";


                      TextModal.textContent = "Producto agregado exitosamente a tus favoritos."
                      ButtonModal.textContent = "Ir a favoritos"; // le cambiamos para que diga logearse 

                      // le damos funcion de redireccion para logearse 
                      ButtonModal.addEventListener('click', function () {
                          window.location.href = "Favoritos.aspx"; // Cambia la URL según sea necesario
                      });
                  } else {
                      return; // No hacer nada si no hay valores relevantes
                  }

              }


          }


          // Mostrar el modal
          const myModal = new bootstrap.Modal(document.getElementById('MyModal'));
          myModal.show();

          // Restablecer el valor del HiddenField
          hfShowModal.value = "";
          hfUser.value = "";
          hfAdmin.value = "";


      }
  </script>



    
</asp:Content>
