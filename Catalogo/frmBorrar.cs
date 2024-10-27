using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Catalogo
{
    public partial class frmBorrar : Form
    {
        GestionDB GestionDB = new GestionDB(); // Gestionar la bd

        public frmBorrar()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, System.EventArgs e)
        {
            try
            { // toma la placa seleccionada
                string placa = cbPlaca.Text;

                // muestra un mensaje de confirmacion
                DialogResult result = MessageBox.Show($"¿Esta seguro que desea boorrar el vehiculo de placa '{placa}'?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //si el usuario confirma borra el vehiculo

                    if (GestionDB.BorrarVehiculo(placa) > 0)
                    {
                        MessageBox.Show("Vehiculo borrado exitosamente", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //se actualiza la lista de placas 
                        LlenarComboBoxPlacas(cbPlaca);
                    }
                    else
                    {
                        MessageBox.Show("No se encontro ningun vehiculo con la placa dada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Se produjo un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // metodo para llenar el combobox con las placas
        public void LlenarComboBoxPlacas(ComboBox cbPlaca)
        {
            List<string> placas = GestionDB.MostrarPlacas();

            foreach (string placa in placas)
            {
                cbPlaca.Items.Add(placa);
            }
        }

        // se inicia con las placas cargadas
        private void frmBorrar_Load(object sender, System.EventArgs e)
        {
            LlenarComboBoxPlacas(cbPlaca);
        }
    }
}
