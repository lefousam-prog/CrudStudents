using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /*Esta clase sirve para mapear la tabla student de la base de datos Students,
     de tal forma que los campos de esa tabla se igualen o se hagan referencia a esta clase
    y que esta clase sea la tabla pero en codigo para poder trabajarla dinamicamente*/
    [Table(Name = "student")]
    public class Student
    {
        [PrimaryKey,Identity]
        public int id { get; set; }

        [Column(Name = "name"), NotNull]
        public string name { get; set; }

        [Column(Name = "surname"), NotNull]
        public string surname { get; set; }

        [Column(Name = "age"), NotNull]
        public int age { get; set; }

        [Column(Name = "email"), NotNull]
        public string email { get; set; }

        [Column(Name = "sex"), NotNull]
        public char sex { get; set; }

        [Column(Name = "image"), NotNull]
        public byte[] image { get; set; }
    }
}
