using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pruebaTabla
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void librosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.librosBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.bibliotecaDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'bibliotecaDataSet.Libros' Puede moverla o quitarla según sea necesario.
            this.librosTableAdapter.Fill(this.bibliotecaDataSet.Libros);

            groupBox1.Enabled = false;
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            librosBindingSource.MoveFirst();         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            librosBindingSource.MovePrevious();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            librosBindingSource.MoveNext();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            librosBindingSource.MoveLast();
        }

        private void fechaDateTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            librosBindingSource.AddNew();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Borrar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                librosBindingSource.RemoveCurrent();
                
                statusStrip1.Text = "Libro borrado";
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Sirven los dos
            //librosTableAdapter.Update(bibliotecaDataSet);
            tableAdapterManager.UpdateAll(bibliotecaDataSet);
            
            statusStrip1.Text = "Datos guardados";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if(tituloTextBox.Text.Equals("") | iSBNTextBox.Text.Equals(""))
            {
                MessageBox.Show("Debe introducir todos los datos", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tituloTextBox.Text.Equals(""))
                {
                    errorProvider1.SetError(tituloTextBox, "Debe introducir el titulo");
                }
                else
                {
                    errorProvider1.SetError(iSBNTextBox, "Debe introducir el isbn");
                }
            }
            else
            {
                librosBindingSource.EndEdit();
                statusStrip1.Text = "Terminado";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            librosBindingSource.CancelEdit();
            errorProvider1.Clear();            
        }

        private void portadaPictureBox_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivos gráficos|*.bmp;*.gif;*.jpg;*.png";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                portadaPictureBox.Image = Image.FromFile(openFileDialog1.FileName);
            }
            else
            {
                portadaPictureBox.Image = null;
            }
        }

        private void stockTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void tituloTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (tituloTextBox.Text.Equals(""))
            {
                errorProvider1.SetError(tituloTextBox, "Debe introducir el titulo");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tituloTextBox, "");
            }
        }

        private void iSBNTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (iSBNTextBox.Text.Equals(""))
            {
                errorProvider1.SetError(iSBNTextBox, "Debe introducir el isbn");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(iSBNTextBox, "");
            }
        }

        private void librosBindingSource_PositionChanged(object sender, EventArgs e)
        {
            label1.Text="Libro "+(librosBindingSource.Position+1)+" de "+ librosBindingSource.Count;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!(bibliotecaDataSet.GetChanges() is null))
            {
                if(MessageBox.Show("¿Guardar las modificaciones pendientes antes de salir?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    this.librosBindingSource.CancelEdit();
                    this.tableAdapterManager.UpdateAll(this.bibliotecaDataSet);
                }
            }
        }
    }
}
