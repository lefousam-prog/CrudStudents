using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.SubClasses
{
    /*esta clase se utilizara para hacer instancias de las demas subclases
     de la capa Logic, de tal forma que al hacer que person tendra la instancia
    a esta clase y esta tendra instancia a todas las demas subclases p*/
    public class LogicSubClases
    {
        //clase para metodos relacionados a el picturebox
        public Items uploadimg = new Items();
        //clase relacionada a eventos de textbox
        public TextEvents txtevents = new TextEvents();
    }
}
