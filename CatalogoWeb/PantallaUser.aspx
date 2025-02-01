<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="PantallaUser.aspx.cs" Inherits="CatalogoWeb.PantallaUser1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h3 class="m-1">Mis Datos</h3>
        <div class="row m-1" style="min-height: 450px; box-shadow: 0 4px 8px rgba(1, 1, 1, 1); border-radius: 16px">
            
            <div class="col-sm-12 col-md-5  m-2 mx-auto">
                <label for="txtNombre" class="form-label">Nombre(s)</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>

                <label for="txtApellido" class="form-label">Apellido(s)</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>

                <label for="txtEmail" class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>

                <label for="txtPass" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPass" runat="server" CssClass="form-control"></asp:TextBox>

                
              
                 <% if (MostrarInputFile != false)
                          {%>
                                <label for="txtFileImage" class="form-label">Imagen Perfil</label>
                                <input type="file"  id="txtFileImage" runat="server" class="form-control mt-2 mb-2" />
                         <% } %>
            </div>

            <div class="col-sm-12 col-md-5 m-2 text-center ">
                     
                     
               
                
                     <asp:Image ID="imgUsuario" runat="server"   alt="foto usuario" style="height: 300px; width: 70%; border-radius: 50px" />
                
               
            </div>

        </div>
        <div class="mt-4 mb-3 m-3">
            <asp:Button Text="Editar" runat="server" ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-outline-warning" />
            <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardarCambios" OnClick="btnGuardarCambios_Click" CssClass="btn btn-primary" />

        </div>

    </div>
    <asp:HiddenField ID="hfModal" runat="server" />
     
</asp:Content>











