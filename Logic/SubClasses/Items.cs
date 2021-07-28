using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic.SubClasses
{
    //clase para metodos relacionados a la imagen en picturebox
    public class Items
    {
        private OpenFileDialog file = new OpenFileDialog();

        /*metodo que abre la ventana para cargar una imagen de pc en el picturebox*/
        public void LoadImage(PictureBox pic)
        {
            pic.WaitOnLoad = true;
            file.Filter = "Imagenes|*.jpg;*.gif;*.png;*.bmp";
            file.ShowDialog();
            if (file.FileName != String.Empty)
            {
                pic.ImageLocation = file.FileName;
            }
        }


        /*este metodo convierte la imagen del picturebox que venia en formato image
         a un arreglo de byte[], asi se puede guardar en la base de datos*/
        public byte[] ImgToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();

            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }


        /*hace el proceso inverso al metodo anterior*/
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            return Image.FromStream(new MemoryStream(byteArrayIn));
        }
    }
}
