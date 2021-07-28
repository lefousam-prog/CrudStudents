using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Students
{
    public partial class Form1 : Form
    {
        private Person estudiante;
        private char sex;
        public Form1()
        {
            InitializeComponent();

            //list de labels para modificar colores en validaciones
            List<Label> listlabels = new List<Label>();
            listlabels.Add(lbName);
            listlabels.Add(lbSurname);
            listlabels.Add(lbAge);
            listlabels.Add(lbEmail);
            

            txtAge.Text = "0";
            
            /*en un arreglo de tipo object se envia al constructor el contro del picturebox, la imagen actual
             que contiene por defecto y el control del datagridview*/
            Object[] imgObject = { pbStudent, Properties.Resources.user, dataGridView };
            estudiante = new Person(listlabels, imgObject);

            //instancia para cargar el datagridview con datos, si se envia una cadena vacia
            //es porque trae todos los registros, sino, el parametro que recibe
            //es del textbox de busqueda y trae una consulta personalizada segun con que coincide
            estudiante.readStudents("");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            estudiante.subclases.uploadimg.LoadImage(pbStudent);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        /*eventos que se ejecturan cuando los cajas de texto cambien al ingresarles 
         caracteres, si esta vacia la caja el color de los labels sera gris,
        si tiene algo la caja el color sera verde, el color inicial de la caja es azul*/
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                lbName.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lbName.ForeColor = Color.DarkGreen;
                lbName.Text = "Nombre";

            }
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            if (txtSurname.Text.Equals(""))
            {
                lbSurname.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lbSurname.ForeColor = Color.DarkGreen;
                lbSurname.Text = "Apellido";

            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.Equals(""))
            {
                lbEmail.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lbEmail.ForeColor = Color.DarkGreen;
                lbEmail.Text = "Email";
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            if (txtAge.Text.Equals(""))
            {
                lbAge.ForeColor = Color.LightSlateGray;
            }
            else
            {
                lbAge.ForeColor = Color.DarkGreen;
                lbAge.Text = "Edad";
            }
        }


        /*estos eventos se evalua cada caracter ingresado, para el textbox de nombre y apellido
         se evalua que sean string y para el de edad que sea solo numeros, se llama a la clase persona
        y esta tiene una instancia de la clase LogicSubClases que es una clase intermedia que contiene
        las instancias de las demas clases adentro de la carpeta SubClases en Logic, asi llamados
         a la instancia que tiene esta clase que es txtevents que hace referencia a la clase TextEvents que
        tiene un metodo para evaluar string y numeros*/
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.subclases.txtevents.textKeyPress(e);
        }

        private void txtSurname_KeyPress(object sender, KeyPressEventArgs e)
        {
            estudiante.subclases.txtevents.textKeyPress(e);
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
                         
            estudiante.subclases.txtevents.numberKeyPress(e); 
            
        }


        /*Metodo para insertar registros tanto por primera vez como para actualizzar
         aqui se asaginan los valores de los compoentes a las propiedades de la clase Person
        para ser utilizadas como atributos alla*/
        private void btnInsert_Click(object sender, EventArgs e)
        {
            estudiante.Name = txtName.Text;
            estudiante.Surname = txtSurname.Text;
            if (txtAge.Text=="")
            {       
                estudiante.Age = 0;
            }
            else
            {
                estudiante.Age = Convert.ToInt32(txtAge.Text);
            }
            if (rbM.Checked == true)
            {
                sex = 'M';
            }
            else if (rbF.Checked == true)
            {

                sex = 'F';
            }
            estudiante.Sex = sex;
            estudiante.Email = txtEmail.Text;

            //si todo funciono, retornara un true y se resetearan los elementos
            if (estudiante.insertStudent())
            {
                resetComponents();
            }
            
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }


        /*Evento del datagridview que permite darle clic a una celda, row o columns, al hacer esto
         primero vemos si la cantidad de filas es mayor que cero, si es asi, cada cadto del allar de las celdas
        de cada fila donde se encuentra la celda seleccionada se asigna a los elementos de textbox correspondientes
        con la diferencia que el id de ese registro se envia a una propiedad en el objeto de la clase*/
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView.Rows.Count!=0)
            {
                estudiante.IdUpdate = Convert.ToInt16(dataGridView.CurrentRow.Cells[0].Value);
                txtName.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                txtSurname.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                txtAge.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
                if (dataGridView.CurrentRow.Cells[5].Value.ToString() == "M")
                {
                    rbM.Checked = true;
                }
                else if (dataGridView.CurrentRow.Cells[5].Value.ToString() == "F")
                {
                    rbF.Checked = true;
                }
                txtEmail.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            }
        }

        /*Evento para el textbox de busqueda, donde se obtiene el valor y se envia al metodo de readStudents
         donde busca en las row de la tabla si se encuentra un dato que coincida, solo funciona para nombre y apellido
        nada mas*/
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            estudiante.readStudents(txtSearch.Text);
        }


        /*hace lo mismo que el evento cellclick con la diferencia que funciona con las teclas de arriba y abajo
         del teclado*/
        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView.Rows.Count != 0)
            {
                estudiante.IdUpdate = Convert.ToInt16(dataGridView.CurrentRow.Cells[0].Value);
                txtName.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
                txtSurname.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
                txtAge.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
                if (dataGridView.CurrentRow.Cells[5].Value.ToString() == "M")
                {
                    rbM.Checked = true;
                }
                else if (dataGridView.CurrentRow.Cells[5].Value.ToString() == "F")
                {
                    rbF.Checked = true;
                }
                txtEmail.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            }
        }

        /*evecto click que llama al metodo de eliminar estudiante de los registros de la tabla correspondiente
         luego resetea los componentes*/
        private void btnDelete_Click(object sender, EventArgs e)
        {
            estudiante.deleteStudent();
            resetComponents();
        }


        //metodo para resetear los componentes despues de insertar, actualizar y eliminar
        public void resetComponents()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtAge.Text = "0";
            txtEmail.Text = "";
            txtSearch.Text = "";
            pbStudent.Image = Properties.Resources.user;
            lbName.ForeColor = Color.Blue;
            lbSurname.ForeColor = Color.Blue;
            lbAge.ForeColor = Color.Blue;
            lbEmail.ForeColor = Color.Blue;
            rbM.Checked = true;
        }
    }
}
