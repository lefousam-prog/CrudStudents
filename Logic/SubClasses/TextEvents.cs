using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic.SubClasses
{
    public class TextEvents
    {
        //validaciones para los textbox
        /*estas validaciones utilizan el evento de keypress, se captura el evento actual
         y se valida si cumple la condicion, dependiendo de esta en el codigo a efectuar,
        utilizo la propiedad handled de dicho evento para permitir o no la escritura en el textbox
        si su valor boolean es false se permite, si es true se deniega*/
        public void textKeyPress(KeyPressEventArgs e)
        {
            /*evalua si lo que se captura del evento por teclado es una letra*/
            if (char.IsLetter(e.KeyChar)) { e.Handled = false; }

            /*evalia si se presiona enter, entonces no permite que se efectue el salto de linea*/
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }

            /*evalua si se presiona la tecla de backspace o la de borrar*/
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }

            /*evalua si se presiona el espacio*/
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }

            /*sino es ninguna de las anteriores se bloquea el ingreso*/
            else { e.Handled = true; }
        }

        public void numberKeyPress(KeyPressEventArgs e)
        {
            /*evalua si lo que se captura del evento por teclado es un numero digito*/
            if (char.IsNumber(e.KeyChar)) {  e.Handled = false; }
           
            /*evalia si se presiona enter, entonces no permite que se efectue el salto de linea*/
            else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }

            /*evalua si se presiona la tecla de backspace o la de borrar*/
            else if (char.IsControl(e.KeyChar)) { e.Handled = false; }

            /*evalua si se presiona el espacio*/
            else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }

            /*sino es ninguna de las anteriores se bloquea el ingreso*/
            else { e.Handled = true; }
        }
    }
}
