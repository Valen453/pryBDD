using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryBDD
{
    public partial class frmAgregarCliente : Form
    {
        public frmAgregarCliente()
        {
            InitializeComponent();
        }

        clsClientes x = new clsClientes();

        clsAutomoviles y = new clsAutomoviles();
        private void frmAgregarCliente_Load(object sender, EventArgs e)
        {
            y.listar(cbAutos);
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            x.Nombre = txtNombre.Text;
            x.Limite = Convert.ToDecimal(txtLimite.Text);
            x.Auto = Convert.ToInt32(cbAutos.SelectedValue);
            x.agregar();
            MessageBox.Show("Datos grabados correctamente");
            txtNombre.Text = "";
            txtLimite.Text = "";
            cbAutos.SelectedIndex = -1;
            
        }
    }
}
