using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Catalogo
{
    public partial class frmEditar : Form
    {

        string dir = System.Environment.CurrentDirectory; // se seleciona  el archivo de recursos
        GestionDB GestionDB = new GestionDB(); // Gestionar la bd

        public frmEditar()
        {
            InitializeComponent();
            dir = dir.Substring(0, dir.LastIndexOf("bin")) + "Recursos\\";//Aputa ala carpta recursos dodne deber estar la foto
        }

        // se inicia con el combobox de las marcas y las placas cargados
        private void frmEditar_Load(object sender, EventArgs e)
        {
            LlenarComboBoxPlacas(cbPlaca);
            LlenarComboBoxMarcas(comboBox1);
        }

        // metodo para llenar el combobox de las placas
        public void LlenarComboBoxPlacas(ComboBox cbPlaca)
        {
            List<string> placas = GestionDB.MostrarPlacas();

            foreach (string placa in placas)
            {
                cbPlaca.Items.Add(placa);
            }
        }

        // metodo para llenar el combobox de las marcas
        public void LlenarComboBoxMarcas(ComboBox comboBox1)
        {
            List<string> marcas = GestionDB.MostrarMarcas();

            foreach (string marca in marcas)
            {
                comboBox1.Items.Add(marca);
            }
        }

        // metodo que limpia los textbox
        void Limpiar()
        {
            txtColor.Text = string.Empty;
            txtModelo.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtReferencia.Text = string.Empty;
            txtPlaca.Text = string.Empty;
        }

        // boton para buscar y mostrar la info del vehiculo 
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string placa = cbPlaca.SelectedItem.ToString(); // se toma la placa seleccionada

            Vehiculo vehiculo = GestionDB.BuscarVehiculoPorPlaca(placa); // se carga los datos a la instancia vehiculo

            // si se encontro un vehiculo
            if (vehiculo != null)
            {
                groupBox1.Visible = true; // se muestra toda la info, es decir se habilita el groupBox1 
                txtPlaca.Enabled = false; // se inhabilita para que no se cambie de placa
                // Se toma asignan los datos a cada item
                txtPlaca.Text = vehiculo.Placa;
                comboBox1.SelectedIndex = vehiculo.Fabricante - 1; //Para seleccionarlo le resta -1 ya que empieza desde 0
                txtReferencia.Text = vehiculo.Referencia;
                txtColor.Text = vehiculo.Color;
                txtPrecio.Text = vehiculo.Precio.ToString();
                txtModelo.Text = vehiculo.Modelo.ToString();
                pbFoto.Image = Image.FromFile(vehiculo.Foto);
            }
            else
            {
                MessageBox.Show("Vehículo no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
        }

        // boton para guardar la informacion que a sido cambiada
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPlaca.Text) || string.IsNullOrEmpty(comboBox1.Text)
                  || string.IsNullOrEmpty(txtReferencia.Text) || string.IsNullOrEmpty(txtPrecio.Text)
                  || string.IsNullOrEmpty(txtColor.Text) || string.IsNullOrEmpty(txtModelo.Text))
                {
                    MessageBox.Show("No deje campos sin diligenciar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // como la placa no esta cambiada la toma y busca el vehiculo por placa
                    string placa = txtPlaca.Text;
                    Vehiculo vehiculo = GestionDB.BuscarVehiculoPorPlaca(placa);

                    //toma todo menos placa y foto ya que eso no cambia, solo cambia lo otro
                    vehiculo.Fabricante = comboBox1.SelectedIndex + 1; // le suma uno debido a que empieza desde 0
                    vehiculo.Referencia = txtReferencia.Text;
                    vehiculo.Precio = double.Parse(txtPrecio.Text);
                    vehiculo.Color = txtColor.Text;
                    vehiculo.Modelo = int.Parse(txtModelo.Text);

                    //mensaje de confirmacion
                    if (GestionDB.EditarVehiculo(vehiculo) > 0)
                    {
                        MessageBox.Show("Vehiculo editado con exito", "Confirmacion",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Se produjo un error por: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();

            }
        }
    }
}
