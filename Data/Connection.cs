using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /*clase que servira para tener las instancias de la cadena de conexion
     y poder mapear con objetos cada tabla de la base Student a traves de objetos
    del tipo ITable donde estos seran del tipo de la clase donde se mapearan*/
    public class Connection: DataConnection
    {
        /*al constructor de la clase padre se envia la cadena de conexion llamada sqlcon que
         se encuentra en el archivo App.config del proyecto Student*/
        public Connection(): base("sqlcon")
        {

        }

        public ITable<Student> studentTable => GetTable<Student>();
    }
}
