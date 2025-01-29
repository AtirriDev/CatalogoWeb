using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;


namespace Negocio
{
    public static class Validaciones
    {

        public static bool ValidarVacio(object Control) 
        {

            if (Control is TextBox texto)
            {
                if (string.IsNullOrEmpty(texto.Text))
                {
                    return false;
                }
                else
                   return true;
            }

            return false;
        }

        public static bool ValidarVacioCargaArticulo(object Control , object label)
        {

            if (Control is TextBox texto)
            {
                if (string.IsNullOrEmpty(texto.Text))
                {
                    ((Label)label).Text = "*Este Campo es de caracter obligatorio";



                    return false;

                }
                else {
                    ((Label)label).Text = "";
                    return true;
                }
                    
            }

            return false;
        }
    }
}
