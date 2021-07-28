using Data;
using LinqToDB;
using Logic.SubClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic
{
    
    public class Person
    {
        private List<Label> listlabel;
        public LogicSubClases subclases;
        private Connection db;
        private DataGridView dataTable;
        private PictureBox image;
        private Bitmap imgBitman;

        private string name;
        private string surname;
        private int age;
        private char sex;
        private string email;
        private int idupdate;

        private bool result;
        private string typeInsert;
        

        public Person(List<Label> lista, Object[] imgobject)
        {
            listlabel = lista;
            image = (PictureBox)imgobject[0];
            imgBitman = (Bitmap)imgobject[1];
            subclases = new LogicSubClases();
            db = new Connection();
            result = false;
            dataTable = (DataGridView)imgobject[2];
            idupdate = 0;
            
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public char Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int IdUpdate
        {
            get { return idupdate; }
            set { idupdate = value; }
        }

       
        /*metodo que se llama al darle al boton de ingresar, donde se evaluan si los campos estan vacios
         si el email tiene formato correcto y si no esta repetido en la base, si es asi
        se llama al metodo createStuden que ingresa al estudiante por primera vez, en el caso que
        el correo exista en la base con esa fila se compara el id de ese registro en la base con el ide
        de la tabla actual en el caso se le haya dado click para actualizar, esto se hace para verificar
        que se esta actualizando y no registrando pro primera vez pues la variable idupdate por defecto es cero
        si el correo esta repetido y la variable idupdate es igual a cero entonces solo cambia el color del label
        y le notifica que no puede registrarlo porque ya existe, pero si la variable idupdate tiene un valor distinto de cero
        es porque se ha seleccionado una row en el datagridviw y se quiere actualizar, por lo que el correo
        coincidira con uno en la base y el id de esta row en el datagridviw tambien y ahi es donde se llama
        al metodo createStudent con la diferente que la variable bandera typeInsert decidira en una si se crea
        o se actualiza, si todo funciona bien este metodo retorna true*/
        public bool insertStudent()
        {
            if (name.Equals("") || surname.Equals("") || age==0 || email.Equals(""))
            {
                if (name.Equals(""))
                {
                    listlabel[0].Text = "Nombre requerido";
                    listlabel[0].ForeColor = Color.Red;
                    listlabel[0].Focus();
                }
                else if (surname.Equals(""))
                {
                    listlabel[1].Text = "Apellido requerido";
                    listlabel[1].ForeColor = Color.Red;
                    listlabel[1].Focus();
                }
                else if (age == 0)
                {
                    listlabel[2].Text = "Edad requerida";
                    listlabel[2].ForeColor = Color.Red;
                    listlabel[2].Focus();
                }
                else if (email.Equals(""))
                {
                    listlabel[3].Text = "Email requerida";
                    listlabel[3].ForeColor = Color.Red;
                    listlabel[3].Focus();
                }
            }
            else if (!new EmailAddressAttribute().IsValid(email))
            {
                listlabel[3].Text = "Email invalido";
                listlabel[3].ForeColor = Color.Red;
                listlabel[3].Focus();
            }
            else
            {
                var user = db.studentTable.Where(data => data.email.Equals(email)).ToList();

                if (user.Count.Equals(0))
                {
                    typeInsert = "create";
                    createStudent();
                    
                }
                else
                {
                    if (user[0].id.Equals(idupdate))
                    {
                        typeInsert = "update";
                        createStudent();
                        
                    }
                    else
                    {
                        listlabel[3].Text = "Email ya esta registrado";
                        listlabel[3].ForeColor = Color.Red;
                        listlabel[3].Focus();
                        result = false;
                    }
                    
                }
                
            }

            return result;
        }

        /*en este metodo se hace el llenado a la clase connection para crear una instancia
         de conexion, donde utilizaremos el metodo using para utilizar esta instancia
        y por transacciones iniciar la transaccion basado en si se va a crear o actualizar.
        Luego llamando al objeto que contiene la tabla studen mapeada se envian los valores
        de las propiedaes correspondientes y la imagen*/
        public void createStudent()
        {
            Connection dbcreate = new Connection();
            try
            {
                byte[] imgByte = subclases.uploadimg.ImgToByte(image.Image);

                
                using (dbcreate)
                {
                    dbcreate.BeginTransaction();

                    if (typeInsert=="create")
                    {
                        dbcreate.studentTable
                          .Value(p => p.name, this.name)
                          .Value(p => p.surname, this.surname)
                          .Value(p => p.age, this.age)
                          .Value(p => p.email, this.email)
                          .Value(p => p.sex, this.sex)
                          .Value(p => p.image, imgByte)
                          .Insert();
                        MessageBox.Show("Registro exitoso", "Registro");
                        
                    }
                    else if (typeInsert=="update")
                    {
                        dbcreate.studentTable
                          .Where(p => p.id.Equals(idupdate))
                          .Set(p => p.name, this.name)
                          .Set(p => p.surname, this.surname)
                          .Set(p => p.age, this.age)
                          .Set(p => p.email, this.email)
                          .Set(p => p.sex, this.sex)
                          .Set(p => p.image, imgByte)
                          .Update();
                        MessageBox.Show("Actualización exitosa", "Actualización");
                        
                    }


                    dbcreate.CommitTransaction();
                }
                
                readStudents("");
                result = true;
                
            }
            catch (Exception e)
            {
                dbcreate.RollbackTransaction();
                MessageBox.Show(e.Message, "Ha ocurrido un error");
            }
        }

       

        private int registroPagina = 2, numPagina = 1;

        /*Metodo que trae los registros de la tabla student de la base Students
         este recibe un valor, si es una cadena vacia, traera todos los registros que contiene
        el objeto mapeado en la clase connection y los enlistara en la list del mismo tipo de la clase
        mapeada, en el caso que el element contenga algo del campo buscar entonces buscara
        en una consulta personalizada buscando si coincide el nombre o el apellido con algun campo dentro de la base
        y lo enlistara para luego asignar si encuentra registros en el datatable*/
        public void readStudents(string element)
        {
            Connection dbRead = new Connection();
            List<Student> query = new List<Student>();

            int inicio = (numPagina - 1) * registroPagina;

            if (element=="")
            {
                query = dbRead.studentTable.ToList();
            }
            else
            {
                query = dbRead.studentTable.Where(data => data.name.StartsWith(element) || data.surname.StartsWith(element)
                ).ToList();
                
            }
            
            if (query.Count > 0)
            {
                dataTable.DataSource = query.Select(data => new
                {
                    data.id,
                    data.name,
                    data.surname,
                    data.age,
                    data.email,
                    data.sex,
                    //data.image,
                }).ToList();
                //dataTable.Columns[6].Visible = false;              
            }
            
        }

        /*metodo para eliminar un registro basado en el ipdate correspondiente al row
         seleccionado en el datagridview*/
        public void deleteStudent()
        {
            if (idupdate.Equals(0))
            {
                MessageBox.Show("Seleccione un estudiante a eliminar");
            }
            else
            {
                if (MessageBox.Show("¿Está seguro que desea eliminar al estudiante?","Eliminar estudiante",
                    MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    db.studentTable.Where(c => c.id.Equals(idupdate)).Delete();
                    readStudents("");
                }
            }
        }


    }
}
