<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="CatalogoWeb.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            overflow-x: hidden;
        }

        .table {
            width: 100%;
            table-layout: fixed;
        }

        img {
            max-width: 100%;
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5"  >
        <%if (ListaFavoritosUsuario.Count > 0)
            { %>
        <table class="table table-responsive caption-top text-center table-responsive-sm ">
            <caption><b>Productos Favoritos</b></caption>
            <thead>
                <tr>

                    <th scope="col"></th>
                    <th scope="col">Producto</th>
                    <th scope="col">Precio</th>
                    <th scope="col"></th>
                </tr>
            </thead>
            <tbody>


                <asp:Repeater ID="repTabla" runat="server">
                    <ItemTemplate>
                        <tr>

                            <td>
                                <a href="DetalleProducto.aspx?id=<%#Eval("Id") %>">
                                    <img src="<%#Eval("Imagen") %>" alt="Alternate Text" style="height: 200px" onerror="this.src='https://st4.depositphotos.com/14953852/24787/v/450/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg';" /></a>
                            </td>


                            <td><%#Eval("Nombre") %></td>
                            <td>$<%#Eval("Precio", "{0:N0}") %></td>
                            <td>
                                <asp:Button Text="Eliminar" CssClass="btn btn-danger" runat="server" ID="btnEliminar" OnClick="btnEliminar_Click" CommandName="ArticuloId" CommandArgument='<%#Eval("Id") %>' /></td>






                        </tr>
                    </ItemTemplate>
                </asp:Repeater>


            </tbody>
        </table>

        <% }
            else
            { %>
                        <div class="d-flex justify-content-center align-items-center mt-5 mb-3">
                             <div class="card" style="width: 30rem;height: 150px ; box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);">
                                                    <div class="card-body text-center">
                                                        <h3 class="card-title">No tienes articulos en favoritos</h3>
                                                     </div>   
                                                     <div class="d-flex justify-content-end mb-1 " style ="margin-inline-end:10px">  
                                                          <a href="Home.aspx" class="btn btn-primary card-link">Home</a>
                                                     </div>
                                 
                             </div>    
                                       
                        </div>
                               
                                  
        <% } %>
    </div>
</asp:Content>
