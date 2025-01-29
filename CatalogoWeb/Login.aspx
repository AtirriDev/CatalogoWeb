<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CatalogoWeb.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Atirri Tech</title>
    <link href="Estilos%20Css/EstilosNuevoLogin.css" rel="stylesheet" />
     <%--Link de boots --%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="banner">


                <svg width="743" height="123" viewBox="0 0 743 123" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M9.096 43.056C6.792 43.056 4.78667 42.2027 3.08 40.496C1.37333 38.7893 0.52 36.784 0.52 34.48C0.52 31.8347 1.288 29.744 2.824 28.208C4.36 26.672 6.45067 25.904 9.096 25.904C11.3147 25.904 13.2773 26.7573 14.984 28.464C16.776 30.1707 17.672 32.176 17.672 34.48C17.672 37.04 16.8613 39.1307 15.24 40.752C13.6187 42.288 11.5707 43.056 9.096 43.056ZM2.44 122.16V88.752L4.104 53.808H13.576L15.24 88.752V122.16H2.44ZM102.338 95.28L92.354 69.68H50.882L40.898 95.024L30.53 90.8L65.602 4.39999H78.914L113.474 90.8L102.338 95.28ZM54.722 59.44H88.386L71.618 16.432L54.722 59.44ZM135.925 94V15.152H108.405V4.39999H175.605V15.152H148.085V94H135.925ZM188.395 94V4.39999H200.555V94H188.395ZM275.869 95.536L264.733 71.216C261.917 65.1573 258.376 60.848 254.109 58.288C249.842 55.728 244.381 54.448 237.725 54.448H235.805V94H223.645V4.656C228.168 4.22933 232.477 3.93066 236.573 3.75999C240.754 3.504 245.192 3.376 249.885 3.376C260.808 3.376 269 5.59467 274.461 10.032C280.008 14.4693 282.781 20.1867 282.781 27.184C282.781 33.4133 280.904 38.448 277.149 42.288C273.48 46.0427 268.146 48.8587 261.149 50.736C263.453 51.76 265.501 52.9973 267.293 54.448C269.085 55.8133 270.792 57.6907 272.413 60.08C274.12 62.384 275.826 65.4133 277.533 69.168L287.261 90.16L275.869 95.536ZM235.805 44.208H249.885C256.029 44.208 261.021 42.8427 264.861 40.112C268.701 37.3813 270.621 33.456 270.621 28.336C270.621 24.0693 268.829 20.5707 265.245 17.84C261.661 15.024 256.541 13.616 249.885 13.616C246.984 13.616 244.381 13.6587 242.077 13.744C239.773 13.8293 237.682 13.9573 235.805 14.128V44.208ZM354.869 95.536L343.733 71.216C340.917 65.1573 337.376 60.848 333.109 58.288C328.842 55.728 323.381 54.448 316.725 54.448H314.805V94H302.645V4.656C307.168 4.22933 311.477 3.93066 315.573 3.75999C319.754 3.504 324.192 3.376 328.885 3.376C339.808 3.376 348 5.59467 353.461 10.032C359.008 14.4693 361.781 20.1867 361.781 27.184C361.781 33.4133 359.904 38.448 356.149 42.288C352.48 46.0427 347.146 48.8587 340.149 50.736C342.453 51.76 344.501 52.9973 346.293 54.448C348.085 55.8133 349.792 57.6907 351.413 60.08C353.12 62.384 354.826 65.4133 356.533 69.168L366.261 90.16L354.869 95.536ZM314.805 44.208H328.885C335.029 44.208 340.021 42.8427 343.861 40.112C347.701 37.3813 349.621 33.456 349.621 28.336C349.621 24.0693 347.829 20.5707 344.245 17.84C340.661 15.024 335.541 13.616 328.885 13.616C325.984 13.616 323.381 13.6587 321.077 13.744C318.773 13.8293 316.682 13.9573 314.805 14.128V44.208ZM381.645 94V4.39999H393.805V94H381.645ZM434.175 94V15.152H406.655V4.39999H473.855V15.152H446.335V94H434.175ZM486.645 94V4.39999H540.405V14.896H498.805V42.16H534.645V52.4H498.805V83.504H540.405V94H486.645ZM592.077 95.536C584.312 95.536 577.442 93.7013 571.469 90.032C565.496 86.2773 560.845 80.9867 557.517 74.16C554.189 67.248 552.525 59.056 552.525 49.584C552.525 40.4533 554.189 32.3893 557.517 25.392C560.93 18.3093 565.709 12.8053 571.853 8.88C578.082 4.86933 585.336 2.864 593.613 2.864C597.88 2.864 601.762 3.29066 605.261 4.144C608.76 4.912 612.045 6.02133 615.117 7.47199L611.405 17.712C608.76 16.432 606.029 15.408 603.213 14.64C600.482 13.872 597.24 13.488 593.485 13.488C588.024 13.488 583.117 14.896 578.765 17.712C574.498 20.528 571.128 24.5813 568.653 29.872C566.178 35.1627 564.941 41.5627 564.941 49.072C564.941 56.24 566.136 62.512 568.525 67.888C571 73.264 574.37 77.4453 578.637 80.432C582.989 83.4187 587.938 84.912 593.485 84.912C597.666 84.912 601.293 84.4 604.365 83.376C607.437 82.352 610.424 81.0293 613.325 79.408L617.037 89.52C613.88 91.2267 610.253 92.6773 606.157 93.872C602.146 94.9813 597.453 95.536 592.077 95.536ZM633.645 94V4.39999H645.805V42.672H689.965V4.39999H702.125V94H689.965V53.168H645.805V94H633.645ZM728.857 67.504L727.193 32.56V0.687996H739.993V32.56L738.329 67.504H728.857ZM733.721 95.536C731.417 95.536 729.412 94.6827 727.705 92.976C725.998 91.2693 725.145 89.264 725.145 86.96C725.145 84.3147 725.913 82.224 727.449 80.688C728.985 79.152 731.076 78.384 733.721 78.384C735.94 78.384 737.902 79.2373 739.609 80.944C741.401 82.6507 742.297 84.656 742.297 86.96C742.297 89.52 741.486 91.6107 739.865 93.232C738.244 94.768 736.196 95.536 733.721 95.536Z" fill="black" />
                </svg>
                <h4 class="texto">Somos tecnologia a tu alcance...</h4>

            </div>
            <!-- Formulario de inicio de sesión -->

                <div class="form-container">
                    <!-- Formulario de inicio de sesión -->
                    <div class="login-form">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-input" TextMode="Email" PlaceHolder="Email"></asp:TextBox>
                        <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-input" TextMode="Password"  PlaceHolder="Ingrese su password"></asp:TextBox>
                        <div class="form-buttons">
                            <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn-submit" OnClick="btnIngresar_Click"/>
                            <button type="button" class="switch-form">Registrarse</button>
                        </div>
                    </div>

                    <!-- Formulario de registro -->
                    <div class="register-form">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-input" PlaceHolder="Nombre"></asp:TextBox>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-input"  PlaceHolder="Apellido"></asp:TextBox>
                        <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-input"></asp:TextBox>
                        <asp:TextBox ID="txtEmailRegistro" runat="server" CssClass="form-input" TextMode="Email" PlaceHolder="Email"></asp:TextBox>
                        <asp:TextBox ID="txtPassRegistro" runat="server" CssClass="form-input" TextMode="Password" PlaceHolder="Ingrese su password"></asp:TextBox>
                        <div class="form-buttons">
                            <asp:Button ID="btnRegistro" runat="server" Text="Registrarse" CssClass="btn-submit" OnClick="btnRegistro_Click" />
                            <button type="button" class="switch-form">Volver</button>
                        </div>
                    </div>
                </div>
        </div>

        <asp:HiddenField ID="hfLogin" runat="server" />
        <asp:HiddenField ID="hfLoginFallido" runat="server" />
        <asp:HiddenField ID="hfRegistroExitoso" runat="server" />
        <asp:HiddenField ID="hfCamposVacios" runat="server" />
       
        
    </form>
    <%-- Modal de login --%>
            <div class="modal" tabindex="-1" id="modLogin">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title"></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <p class="modal-text"></p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal"></button>
                            
                        </div>
                    </div>
                </div>
            </div>
     <script type="text/javascript">

         document.addEventListener('DOMContentLoaded', function () {

             var hfLogin = document.getElementById('<%= hfLogin.ClientID %>');
           var hfLoginFallido = document.getElementById('<%= hfLoginFallido.ClientID %>');
          var hfRegistroExitoso = document.getElementById('<%= hfRegistroExitoso.ClientID %>');
          var hfCamposVacios = document.getElementById('<%= hfCamposVacios.ClientID %>');

           //Constantes del modal
           const Mensaje = new bootstrap.Modal(document.getElementById('modLogin'));
           const Titulo = document.querySelector('#modLogin .modal-title');
           const ButtonModal = document.querySelector('#modLogin .modal-footer .btn');
           const BtnClose = document.querySelector('#modLogin  .btn-close');
           const TextModal = document.querySelector('#modLogin .modal-body .modal-text');

           checkAndShowModal(hfLogin, hfLoginFallido, hfRegistroExitoso, hfCamposVacios);




           // Mostrar el modal basado en el HiddenField
           function checkAndShowModal(LoginExitoso, LoginIncorrecto, hfRegistroExitoso, hfCamposVacios) {

               // si el login es exitoso
               if (hfLogin.value === "true") {
                   Titulo.textContent = "Inicio de Session";
                   TextModal.textContent = "Session iniciada correctamente";
                   ButtonModal.textContent = "Ir al home";
                   BtnClose.style.display = "none";



                   // le damos funcion de redireccion para logearse 
                   ButtonModal.addEventListener('click', function () {
                       window.location.href = "Home.aspx"; // Cambia la URL según sea necesario
                   });

                   //Mostramos el modal
                   Mensaje.show();
                   hfLogin.value = "false";

               }
               else if (hfLoginFallido.value === "true") {

                   //Cambiamos lo que se muestra en el modal 
                   Titulo.textContent = "Inicio de Session";
                   TextModal.textContent = "Usuario o Constraseña incorrecto , verifique y vuelva a intentar";
                   ButtonModal.textContent = "Cerrar";

                   //Mostramos el modal
                   Mensaje.show();

                   hfLoginFallido.value = "false";
               }
               else if (hfRegistroExitoso.value === "true") {
                   Titulo.textContent = "Registro Usuario";
                   TextModal.textContent = "Tu cuenta se ha creado correctamente , por favor inicia session ";
                   ButtonModal.textContent = "Cerrar";
                   //Mostramos el modal
                   Mensaje.show();

                   hfRegistroExitoso.value = "false";
               }
               else if (hfCamposVacios.value === "true") {

                   Titulo.textContent = "Inicio de Session";
                   TextModal.textContent = "Por favor complete los campos para iniciar session ";
                   ButtonModal.textContent = "Cerrar";
                   //Mostramos el modal
                   Mensaje.show();

                   hfCamposVacios.value = "false";
               }








           }
         });

         document.querySelectorAll('.switch-form').forEach(button => {
             button.addEventListener('click', () => {
                 // Visibilidad del formulario registro
                 document.querySelector('.form-container').classList.toggle('show-register');

               
             });
         });

         
     </script>

    <%--link de js--%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js"></script>
   
</body>
</html>
