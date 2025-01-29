<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="CatalogoWeb.LoginRegistro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Sugerencia:hover {
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>


    <asp:UpdatePanel runat="server">
        <ContentTemplate>

        

                    <div class="container ">

                        <%--  Comienzo Muestra Producto Seleccionado--%>
                        <div class="row mb-5" style="min-height: 70vh; margin-inline: 10px; /*border: 5px solid lightgray; border-radius: 12px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2)*/">

                            <%if (Seleccionado != null)
                                {%>
                            <div class="col-sm-12 col-md-12 col-lg-5 mx-auto mb-2  d-flex justify-content-center align-items-center" style="">
                                <img src="<%:Seleccionado.Imagen %>" alt="<%:Seleccionado.Nombre %>"
                                    style="max-width: 100%; max-height: 100%; object-fit: contain; min-height: 500px" onerror="this.src='https://www.shutterstock.com/image-vector/no-image-available-vector-illustration-260nw-744886198.jpg'" />
                            </div>

                            <div class="col-sm-12 col-md-12 col-lg-4 mx-auto mb-2 d-flex flex-column justify-content-center  ">

                                <h6><%:Seleccionado.Categoria %> </h6>

                                <h4>
                                    <asp:Label ID="lblProducto" runat="server" /><%:Seleccionado.Nombre %></h4>

                                <h2>
                                    <asp:Label ID="lblPrecio" runat="server" />$<%:Seleccionado.Precio.ToString("N0") %></h2>

                                <asp:Button Text="Agregar Favoritos" CssClass="btn btn-primary" runat="server" ID="btnAgregarFavoritos" OnClick="btnAgregarFavoritos_Click" />





                            </div>
                            <% } %>
                        </div>


                        <%--  Comienzo Muestra Sugerencias productos de la misma categoria--%>
                        <%if (listaAuxiliar.Count > 0)
                            {%>
                        <div class="row mt-3 mb-3 mx-auto">

                            <div class="row text-center">
                                <h2>Productos de la misma categoria</h2>
                            </div>
                            <%-- Aca tiene que empezar la repetitiva para mostrar maximo 2 productos de la misma categoria --%>

                            <%

                                foreach (var articulo in listaAuxiliar)
                                { %>
                            <div class="col-12 col-md-6 mx-auto">
                                <a href="DetalleProducto.aspx?id=<%: articulo.Id %>" style="text-decoration: none">
                                    <div class="card m-2 text-center Sugerencia" style="width: 100%; border-radius: 16px">
                                        <div>
                                            <img src="<%:articulo.Imagen %>"
                                                class="card-img-top" alt="..." onerror="this.src='https://www.shutterstock.com/image-vector/no-image-available-vector-illustration-260nw-744886198.jpg'"
                                                style="height: 300px; width: auto; border-radius: 16px;">
                                        </div>

                                        <div class="card-body" style="background-color: lavender; border-radius: 16px">
                                            <h2>
                                                <asp:Label runat="server" />
                                                $<%:articulo.Precio.ToString("N0") %>
                                            </h2>
                                            <h6>
                                                <asp:Label runat="server" />
                                                <%:articulo.Nombre %>
                                            </h6>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <% } %>
                        </div>


                        <% } %>
                    </div>
     
            <asp:HiddenField ID="hfShowModal" runat="server" />
            <asp:HiddenField ID="hfUser" runat="server" />
            <asp:HiddenField ID="hfAdmin" runat="server" />
              </ContentTemplate>
 </asp:UpdatePanel>


     <%--  aca finaliza el bloque de cards y filtro--%>
        <div class="modal" tabindex="-1" id="Modal">
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

           const modalTitle = document.querySelector('#Modal .modal-title');// seleccionar el boton por selector css
           const ButtonModal = document.querySelector('#Modal .modal-footer .btn'); // seleccionar el boton por selector css
           const TextModal = document.querySelector('#Modal .modal-body .modal-text');

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
           const Modal = new bootstrap.Modal(document.getElementById('Modal'));
           Modal.show();

           // Restablecer el valor del HiddenField
           hfShowModal.value = "";
           hfUser.value = "";
           hfAdmin.value = "";


       }
   </script>
    
   


</asp:Content>
