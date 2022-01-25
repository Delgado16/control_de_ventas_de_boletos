using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control_De_Venta_de_boletos
{
    public partial class frmCine : Form
    {
        double precio = 0;
        String categorita = "";
        String Valor = "";
        double aNiño = 0, aJovenI = 0, aJovenII = 0, aAdultoI = 0, aAdultoII = 0;
        public frmCine()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            //Capturando los datos necesarios 
            categorita = lblCategoria.Text;
            int cantidad = int.Parse(txtCantidad.Text);

            //Realizando los calculos 
            double subtotal = precio * cantidad;
            double descuento = 0;
            switch (categorita)
            {
                case "Niño" : descuento = 20.0 / 100 * subtotal; break;
                case "Joven I" : descuento = 10.0 / 100 * subtotal; break;
                case "Joven II" : descuento = 5.0 / 100 * subtotal; break;
                case "Adulto I" : descuento = 10.0 / 100 * subtotal; break;
                case "Adulto II" : descuento = 20.0 / 100 * subtotal; break;
            }
            double importe = subtotal - descuento; 

            //imprimir en la lista 
            ListViewItem fila = new ListViewItem(categorita);
            fila.SubItems.Add(precio.ToString("0.00"));
            fila.SubItems.Add(cantidad.ToString("0.00"));
            fila.SubItems.Add(subtotal.ToString("0.00"));
            fila.SubItems.Add(descuento.ToString("0.00"));
            fila.SubItems.Add(importe.ToString("0.00"));
            lvRegistro.Items.Add(fila);

            lvEstadisticas.Items.Clear(); 
            

        }

        private void frmCine_Load(object sender, EventArgs e)
        {

        }

        private void cboEdad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Capturando la edad seleccionada
            int edad = cboEdad.SelectedIndex;

            //Asignando el precio y categoria segun la edad seleccionada 
            switch (edad)
            {
                case 0: precio = 10; categorita = "Niño" ; break;
                case 1: precio = 15; categorita = "Joven I" ; break;
                case 2: precio = 25; categorita = "Joven II" ;break;
                case 3: precio = 15; categorita = "Adulto I" ; break;
                case 4: precio = 10; categorita = "Adulto II" ; break;
            }

            //Mostrando el precio y la categoria 
            lblPrecio.Text = precio.ToString("C");
            lblCategoria.Text = categorita;
        }

        private void lvRegistro_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvRegistro.GetItemAt(e.X, e.Y);

            if (item != null)
            {
                //txtId.Text = item.Text;
                Valor = item.SubItems[0].Text;
                //txtRazon.Text = item.SubItems[0].Text;
            }




            //pictureBox1.Visible = true;
            switch (Valor)
            {
                case "Niño":
                    pbNiños.Visible = true;
                    pbJovenI.Visible = false;
                    pbjovenII.Visible = false;
                    pbAdulto.Visible = false;
                    pbadultoII.Visible = false;
                    break;
                case "Joven I":
                    pbJovenI.Visible = true;
                    pbjovenII.Visible = false;
                    pbAdulto.Visible = false;
                    pbadultoII.Visible = false;
                    pbNiños.Visible = false;
                    break;
                case "Joven II":
                    pbjovenII.Visible = true;
                    pbAdulto.Visible = false;
                    pbadultoII.Visible = false;
                    pbNiños.Visible = false;
                    pbJovenI.Visible = false;
                    break;
                case "Adulto I":
                    pbAdulto.Visible = true;
                    pbadultoII.Visible = false;
                    pbNiños.Visible = false;
                    pbJovenI.Visible = false;
                    pbjovenII.Visible = false;
                    break;
                case "Adulto II":
                    pbadultoII.Visible = true;
                    pbNiños.Visible = false;
                    pbJovenI.Visible=false;
                    pbjovenII.Visible=false;
                    pbAdulto.Visible=false;
                    break;
            }
        }

        private void btnEstadistica_Click(object sender, EventArgs e)



        {
           
            
            
            
            
            lvEstadisticas.Items.Clear();


           





            //Hallar el monto total sin descuento 
            double tsubototal = 0;
            int i; 
            for(i = 0; i < lvRegistro.Items.Count; i++)
            {
                tsubototal += double.Parse(lvRegistro.Items[i].SubItems[3].Text);
                i++;
            }

            //Hallar el monto total que la empresa no percibe 
            //Los descuentos realizados 
            double tdescuento = 0;
            i = 0;
            while (i < lvRegistro.Items.Count)
            {
                tdescuento += double.Parse(lvRegistro.Items[i].SubItems[4].Text);
                i++;
            }

            //Hallar el monto total acumulado por categoria
           // double aNiño = 0, aJovenI = 0, aJovenII = 0, aAdultoI = 0, aAdultoII = 0;
            i = 0;
            do
            {
                string categoria = lvRegistro.Items[i].SubItems[0].Text;
                switch (categoria)
                {
                    case "Niño":
                        aNiño += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                    case "Joven I":
                        aJovenI += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                    case "Joven II":
                        aJovenII += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                    case "Adulto I":
                        aAdultoI += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                    case "Adulto II":
                        aAdultoII += double.Parse(lvRegistro.Items[i].SubItems[5].Text);
                        break;
                }
                i++;


            }while(i < lvRegistro.Items.Count);

            //Imprimiendo las estadisticas 
            lvEstadisticas.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Monto total sin descuento";
            elementosFila[1] = tsubototal.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total que la empresa no percibe";
            elementosFila[1] = tdescuento.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total acumulado por categoria Niño";
            elementosFila[1] = aNiño.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total acumulado por categoria Joven I";
            elementosFila[1] = aJovenI.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total acumulado por categoria Joven II";
            elementosFila[1] = aJovenII.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total acumulado por categoria Adulto I";
            elementosFila[1] = aAdultoI.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto total acumulado por categoria Adulto II";
            elementosFila[1] = aAdultoII.ToString("C");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

        }

        private void lvRegistro_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Estas seguro de salir? ",
                "Control de ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (r == DialogResult.Yes) this.Close();
        }

        
       
    }
}
