using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace Catalogo
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }


        void CargarFormulario(Form form)
        {

            if (this.panelPpal.Controls.Count > 0)
                this.panelPpal.Controls.Clear();
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false;
            this.panelPpal.Controls.Add(form);
            form.Show();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();

            CargarFormulario(agregar);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmEditar editar = new frmEditar();

            CargarFormulario(editar);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            frmBorrar borrar = new frmBorrar();
            CargarFormulario(borrar);
        }
    }
}
