<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="PantallaAdmin.aspx.cs" Inherits="CatalogoWeb.PantallaUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container mt-5  ">
        <div class="row d-flex">
            <div class="col-5 " ">
                <h2 style="text-decoration:underline; " class="text-primary"><b>Gestion de Productos</b></h2>
            </div>
            <div class="col-4">

            </div>
           
            
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <div class="row mt-3 d-flex justify-content-around" style="width:50%;margin-inline-start:4px">
                    <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" placeholder="Busqueda rapida" AutoPostBack="true" OnTextChanged="txtBusqueda_TextChanged"></asp:TextBox>
                 </div>

                <div class="row mt-3 mb-3 table table-responsive" style="width:100% ">
                    <asp:GridView ID="dgvArticulos" runat="server" CssClass="table" DataKeyNames="Id" AutoGenerateColumns="false" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" 
                         AllowPaging="true" PageSize="5" OnPageIndexChanging="dgvArticulos_PageIndexChanging"
                            >
                        <Columns>
                               
                               <asp:BoundField  HeaderText="Codigo" DataField="CodigoArt"/> 
                               <asp:BoundField  HeaderText="Producto" DataField="Nombre"/> 
                               <asp:BoundField  HeaderText="Descripcion" DataField="Descripcion"/> 
                               <asp:BoundField  HeaderText="Categoria" DataField="Categoria.Descripcion"/>  
                               <asp:BoundField  HeaderText="Marca" DataField="Marca.Descripcion"/> 
                               <asp:BoundField  HeaderText="Precio" DataField="Precio" DataFormatString="{0:N0}" HtmlEncode="false"/> 
                               <asp:CommandField SelectText="Editar" ShowSelectButton="true" ControlStyle-CssClass="btn btn-warning"/>
                        </Columns>
                    </asp:GridView>






                </div>
     
       
        <div class="row mb-3 d-flex justify-content-end " <%--style="border:1px solid black"--%>>
            <div class="col-5 d-flex justify-content-end gap-1" <%--style =" border: solid red"--%>>
                 
                <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" CssClass="btn btn-primary" OnClick="btnAgregarProducto_Click" />
                <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btn btn-outline-warning" OnClick="btnCerrar_Click" />
            </div>
           
                  
            
        
        </div>
       
        <%if (EditActivo == true || AgregarActivo == true)
            {%>
        <div class="row mb-3">
            <div class="text-center">

                <%if (EditActivo == true)
                    {%>
                <h2>Editar Producto</h2>

                <% }
                    else
                    {%>
                <h2>Agregar Producto</h2>
                <%} %>
            </div>
            <div class="row">
                <div class="mb-3 col-6 mx-auto">
                        
                     <%--  Codigo de producto--%>
                    <div class="mb-1">   
                        <label for="txtCodigo" class="form-label"><b>Codigo:</b></label>
                        <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                        <asp:Label  runat="server" ID="lblCodigo" style="color:red" />
                    </div>
                       
                        
                    <%--Nombre de producto--%>
                    <div class="mb-1">   
                         <label for="txtProducto" class="form-label"><b>Producto:</b></label>
                        <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control" MaxLength="30" ></asp:TextBox>
                        <asp:Label  runat="server" ID="lblNombre" style="color:red" />
                    </div>
                       
                       
                      
                    <%-- Descripcion--%>
                    <div class="mb-1">
                        <label for="txtDescripcion" class="form-label"><b>Descripcion:</b></label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="150" TextMode="MultiLine"></asp:TextBox>
                        <asp:Label  runat="server" ID="lblDescripcion" style="color:red"/>
                    </div>
                        
                       
                    <%--   Imagen--%>
                    <div class="mb-1">
                          <label for="txtimg" class="form-label"><b>Url Imagen:</b></label>
                           <asp:TextBox ID="txtImg" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtImg_TextChanged" MaxLength="1000"></asp:TextBox>
                    </div>
                      
                  
                    <%--Marcas--%>
                    <div class="mb-1">
                        <label for="ddlMarca" class="form-label"><b>Marca:</b></label>
                        <asp:DropDownList ID="ddlMarcas" runat="server" CssClass="btn btn-outline-info dropdown " Style="width: 100%;"></asp:DropDownList>
                          <div class="mt-1 mt-1 d-flex flex-column align-items-end">
                              <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="form-control mb-1 " style="width:150px"></asp:TextBox> 
                            <asp:Button Text="Agregar Marca" runat="server" CssClass="btn btn-info mb-1" style="height:34px;" ID="btnAgregarMarca" OnClick="btnAgregarMarca_Click"/>
                        </div>
                    </div>
                 
                    <%--Categorias--%>
                    <div class="mb-1" >
                         <label for="ddlCategorias" class="form-label"><b>Categoria:</b></label>
                        <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="btn btn-outline-info dropdown " Style="width: 100%"></asp:DropDownList>
                        <div class="mt-1 mt-1 d-flex flex-column align-items-end">
                             <asp:TextBox ID="txtNuevaCategoria" runat="server" CssClass="form-control mb-1 " style="width:150px"></asp:TextBox>
                            <asp:Button Text="Agregar categoria" runat="server" CssClass="btn btn-info mb-1" style="height:34px;" ID="btnAgregarCat" OnClick="btnAgregarCat_Click"/>
                        </div>
                    </div>
                  
                       
             
                       
                    <%--  Precio--%>
                    <div class="mb-1">
                          <label for="txtPrecio" class="form-label"><b>Precio:</b></label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                         <asp:Label  runat="server" ID="lblPrecio" style="color:red" />
                        <asp:RegularExpressionValidator ErrorMessage="*Ingrese solo numeros por favor" ControlToValidate="txtPrecio" ValidationExpression="^\$?(\d+(\.\d+)*)$"   runat="server" />
                    </div>
                      
                </div>
                  
                <div class="mb-3 col-6 mx-auto text-center d-flex justify-content-center align-items-center " >
                   
                   <asp:Image ID="ImgDetalle" runat="server"  ImageUrl="https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg" style="height:500px;min-width:7%" />
                   
                   
                </div>

                <div class="row mb-3 mx-auto ">
                    <div class="col-5 d-flex justify-content-start gap-2 mb-1 "  >
                        <asp:Button ID="btnConfirmacion" runat="server" Text="Confirmar" OnClick="btnConfirmacion_Click" CssClass="btn btn-outline-primary" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CssClass="btn btn-warning" />
                        
                       
                    </div>
                    
                     
                </div>
                <div class="row">   
                    
                    
                    <div class="col-5 d-flex justify-content-start gap-2 mb-1">
                        <asp:TextBox runat="server" ID="txtEliminar" PlaceHolder="Ingrese el codigo del producto y presione Tab"  CssClass="form-control" style="width: 350px" AutoPostBack="true" OnTextChanged="txtEliminar_TextChanged"/>  
                         <asp:Button ID="btnConfirmarEliminacion" runat="server" Text="Eliminar" OnClick="btnConfirmarEliminacion_Click" CssClass="btn btn-danger m-1"  />
                        
                       
                    </div>
                    
                 </div>






            </div>










        </div>


        <% }%>

  
           </ContentTemplate>
        </asp:UpdatePanel> 
        <asp:Literal ID="litModalScript" runat="server" />
        <%--Script para darle funcionalidad de onerror al img --%>
         <script type="text/javascript">
             <%--Funcion que asigna el onerror --%>
             function ImagenOnError() {
                 var img = document.getElementById('<%= ImgDetalle.ClientID %>');
                 if (img) {
                     img.onerror = function () {
                         this.src = 'https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg';
                     };
                 }
             } 

             // Esto se ejecutará después de cada actualización parcial.
             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                 ImagenOnError();
             });

            

         </script>
        
    </div>
</asp:Content>
