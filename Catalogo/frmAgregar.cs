using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Catalogo
{
    public partial class frmAgregar : Form
    {
        string foto; //ruta foto
        GestionDB GestionDB = new GestionDB(); // Gestionar la bd

        public frmAgregar()
        {
            InitializeComponent();
        }

        private void bntSeleccionar_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog(); // Abre una ventana de dialogo para seleccionar el archivo
                fileDialog.Filter = "Images|*.png;*.bmp;*.jpg"; // se indica que el tipo de archivo deb ser de imagen png o jpg
                string dir = System.Environment.CurrentDirectory; // se seleciona  el archivo de recursos
                dir = dir.Substring(0, dir.LastIndexOf("bin")) + "Recursos\\";
                //Ar dar click en la ventana de diálogo donde se selecciona el archivo y se guardar la imagen en la ruta especificada anteriormente
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    dir = dir + fileDialog.SafeFileName; // se concatena la ruta del archivo con el nombre del archivo cargado
                    Bitmap b = new Bitmap(fileDialog.FileName); // se crea un mapa de bits con la imagen cargada
                    b.Save(dir); // Se guarda la imagen en la rutaespecificada.
                    foto = fileDialog.SafeFileName; // nombre de la foto 
                    MessageBox.Show("Imagen cargada con éxito", "Confirmación",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No es posible guardar la imagen", "error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Limpiar los campos
        void Limpiar()
        {
            txtColor.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtReferencia.Text = string.Empty;
            txtPlaca.Text = string.Empty;

        }

        //cuando se cargue el form se agregan las marcas
        private void frmAgregar_Load(object sender, EventArgs e)
        {
            LlenarComboBoxMarcas(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        // metodo para llenar el combobox con cada una de las marcas
        public void LlenarComboBoxMarcas(ComboBox comboBox1)
        {
            List<string> marcas = GestionDB.MostrarMarcas();

            foreach (string marca in marcas)
            {
                comboBox1.Items.Add(marca);
            }
        }

        // metodo para guardar vehiculo
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // si los campos no estan vacios o nulos procede, sino advertencia
                if (string.IsNullOrEmpty(txtPlaca.Text) || string.IsNullOrEmpty(comboBox1.Text)
                  || string.IsNullOrEmpty(txtReferencia.Text) || string.IsNullOrEmpty(txtPrecio.Text)
                  || string.IsNullOrEmpty(txtColor.Text) || string.IsNullOrEmpty(txtModelo.Text))
                {
                    MessageBox.Show("No deje campos sin diligenciar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                { // instancia a vehiculo
                    Vehiculo vehiculo = new Vehiculo();
                    // se guarda cada uno de los datos segun el campo ya sea textbox o combobox
                    vehiculo.Placa = txtPlaca.Text;
                    vehiculo.Fabricante = comboBox1.SelectedIndex + 1; // Asume que el ComboBox está configurado para devolver el ID del fabricante
                    vehiculo.Referencia = txtReferencia.Text;
                    vehiculo.Precio = double.Parse(txtPrecio.Text);
                    vehiculo.Color = txtColor.Text;
                    vehiculo.Modelo = int.Parse(txtModelo.Text);
                    vehiculo.Foto = System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")) + "Recursos\\" + foto;
                    //guarda la ruta de la foto

                    if (GestionDB.AgregarVehiculo(vehiculo) > 0) //mensaje de confirmacion
                    {
                        MessageBox.Show("Vehiculo guardado con éxito", "Confirmación",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                }

            }
            catch (FormatException ex) // si se produce un error muestra cual fue
            {
                MessageBox.Show("Se produjo un error por: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();

            }

        }
    }
}

