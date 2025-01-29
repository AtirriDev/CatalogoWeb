using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Negocio
{
    public class EmailService
    {


      
            private readonly SmtpClient cliente;
            private readonly string fromEmail = "juansebastianmaradona@gmail.com";
            private readonly string fromPassword = "z t j m y f l s z x w p q j z n";

            public EmailService()
            {
                cliente = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, fromPassword)
                };
            }

           

            private MailMessage CrearMensaje(string to, string subject )
            {
                MailAddress AddressFrom = new MailAddress(fromEmail, "Atirri Tech");
                MailAddress AddressTo = new MailAddress(to);


                //MANIPULAR LA URL DE GOOGLE DRIVE SINO NO FUNCIONA 
                // LO QUE SE HIZO FUE GENERAR UN ENLACE DIRECNTO 
                // ESTO SE HACE https://drive.google.com/file/d/1hYnY706NDfi3-rz2uSqVRAkQU9zJXKhW/view?usp=drive_link SACANDO DE LA URL DE COMPARTIR DE DRIVE 
                // HAY QUE TOMAR DESDE EL D/ HASTA EL /VIEW 
                // AGARRAR https://drive.google.com/uc?id= Y PONERLE ESA PARTE QUE TOMAMOS Y NOS QUEDA UN ENLACE DIRECTO A LA IMAGEN QUE TENGAMOS EN DRIVE Y QUEREMOS MOSTRAR

                string urlImagenPublica = "https://drive.google.com/uc?id=1hYnY706NDfi3-rz2uSqVRAkQU9zJXKhW";
                            string cuerpo = $"<img src=\"{urlImagenPublica}\" alt=\"Imagen de bienvenida\" />" +
                                            "<p>Visitar <a href='http://www.AtirriTech.com'>www.AtirriTech.com</a></p>";



                    MailMessage mensaje = new MailMessage(AddressFrom, AddressTo)
                            {
                                Subject = subject,
                                IsBodyHtml = true,
                                Body = cuerpo
                            };



                    return mensaje;
            }


            public void EnviarCorreo(string to, string subject)
            {
                MailMessage mensaje = CrearMensaje(to, subject);
               
                    try
                    {
                        cliente.Send(mensaje);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            }






    }
}
